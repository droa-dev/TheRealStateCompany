using Properties.WebApi.ViewModels;
using System.Threading.Tasks;

namespace Properties.WebApi.Modules.Common.JWT
{
    public interface IAuthTokenHandler
    {
        /// <summary>
        ///     Generate Jwt Token.
        /// </summary>
        Task<string> GenerateJwtToken(UserModel user);
        /// <summary>
        ///     Validate Credentials.
        /// </summary>
        Task<bool> ValidCredentials(UserModel user);
    }
}
