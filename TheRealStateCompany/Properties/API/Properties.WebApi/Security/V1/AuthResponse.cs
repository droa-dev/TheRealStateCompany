using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.Security.V1
{
    /// <summary>
    ///     Create AuthResponse Response.
    /// </summary>
    public sealed class AuthResponse
    {
        /// <summary>
        ///     CreateOwnerResponse Constructor.
        /// </summary>
        public AuthResponse(ViewModels.TokenModel tokenModel) => this.Token = tokenModel;

        /// <summary>
        ///     Gets Owner.
        /// </summary>
        [Required]
        public ViewModels.TokenModel Token { get; }
    }
}
