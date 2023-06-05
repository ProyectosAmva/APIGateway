using AMVA.JwtAuthenticationManager;
using AMVA.JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMVA.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            this.jwtTokenHandler=jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = jwtTokenHandler.GenerateJwtToken(authenticationRequest);
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
        }
    }
}
