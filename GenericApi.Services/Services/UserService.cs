using AutoMapper;
using FluentValidation;
using GenericApi.Bl.Dto;
using GenericApi.Bl.Extensions;
using GenericApi.Core.Abstract;
using GenericApi.Core.Settings;
using GenericApi.Model.Entities;
using GenericApi.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GenericApi.Services.Services
{ 
    public interface IUserService : IBaseService<User, UserDto>  {
        Task<AuthenticateResponseDto> GetToken(AuthenticateRequestDto model);
        Task<IEntityOperationResult<UserDto>> AddUser(UserDto dto);
        string EncriptPassword(string password);
    }
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        private readonly JwtSettings _jwtSettings;
        public UserService(
            IUserRepository repository, 
            IMapper mapper, 
            IValidator<UserDto> validator, IOptions<JwtSettings> jwtSettings) : base(repository, mapper, validator)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<IEntityOperationResult<UserDto>> AddUser(UserDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (validationResult.IsValid is false)
                return validationResult.ToOperationResult<UserDto>();

            User entity = _mapper.Map<User>(dto);
            entity.Password = EncriptPassword(entity.Password);
            var entityResult = await _repository.Add(entity);

            _mapper.Map(entityResult, dto);

            var result = dto.ToOperationResult();
            return result;
        }

        public async Task<AuthenticateResponseDto> GetToken(AuthenticateRequestDto model)
        {
            var user = await _repository.Query()
                .Where(x => x.UserName == model.UserName)
                .Select(x=> new { 
                    x.Id,
                    x.UserName,
                    x.Name,
                    x.LastName,
                    x.Password
                })
                .FirstOrDefaultAsync();

            if (user is null)
                return null;

            //TODO: Validate password

            if (model.Password == DesEncriptarPassWord(user.Password))
            {
                var response = new AuthenticateResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    LastName = user.LastName
                };

                response.Token = GenerateJwtToken(response);

                return response;
            }
            else
            {
                return null;
            }


        }
        private string GenerateJwtToken(AuthenticateResponseDto user)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var symmetricSecurityKey = new SymmetricSecurityKey(key);

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimsIdentity = new ClaimsIdentity(new[] { 
                new Claim("id", user.Id.ToString()),
                new Claim("username",user.UserName)
            });

            var claims = new Dictionary<string, object>
            {
                { "name", user.Name },
                { "lastName", user.LastName },
            };

            var description  = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Claims = claims,
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                SigningCredentials = signingCredentials
            };

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(description);

            var token = handler.WriteToken(securityToken);

            return token;
        }

        public string EncriptPassword(string password)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.Unicode.GetBytes(password);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public string DesEncriptarPassWord(string password)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(password);
            result = Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
