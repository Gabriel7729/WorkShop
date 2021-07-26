using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Bl.Dto
{
    public class AuthenticateRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
