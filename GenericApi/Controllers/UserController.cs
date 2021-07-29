using GenericApi.Bl.Dto;
using GenericApi.Model.Entities;
using GenericApi.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GenericApi.Controllers
{
    public class UserController : BaseController<User, UserDto>
    {
        private readonly IUserService _userService;

        public UserController(IUserService service) : base(service)
        {
            _userService = service;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequestDto model)
        {
            var response = await _userService.GetToken(model);

            if (response is null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public override async Task<IActionResult> Post([FromBody] UserDto dto)
        {
            return await base.Post(dto);
        }

    }
}
