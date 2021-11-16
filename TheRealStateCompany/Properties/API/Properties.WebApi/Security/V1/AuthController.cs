using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Properties.WebApi.Modules.Common.Features;
using Properties.WebApi.Modules.Common.JWT;
using Properties.WebApi.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Properties.WebApi.Security.V1
{
    [ApiVersion("1.0")]
    [FeatureGate(CustomFeature.Authentication)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthTokenHandler _authTokenHandler;
        private readonly string _badRequest;
        public AuthController(IAuthTokenHandler authTokenHandler)
        {
            _authTokenHandler = authTokenHandler;
            _badRequest = "invalid credentials";
        }

        /// <summary>
        ///     Create a new auth token.
        /// </summary>        
        /// <response code="200">Owner already exist.</response>        
        /// <response code="400">Bad request.</response>        
        /// <param name="username"></param>
        /// <param name="password"></param>        
        /// <returns>JWT token</returns>        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AuthResponse))]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post(
            [FromForm][Required] string username,
            [FromForm][Required] string password)
        {
            TokenModel tokenModel = new();
            List<string> errorList = new();
            UserModel user = new() { Username = username, Password = password };

            bool isValidUser = await this._authTokenHandler
                .ValidCredentials(user)
                .ConfigureAwait(false);

            if (!isValidUser)
            {
                errorList.Add(_badRequest);
                tokenModel = new() { Token = string.Empty, Result = false, Errors = errorList };

                return BadRequest(tokenModel);
            };

            string token = await this._authTokenHandler
                .GenerateJwtToken(user)
                .ConfigureAwait(false);

            tokenModel = new() { Token = token, Result = true, Errors = errorList };

            return Ok(tokenModel);
        }
    }
}
