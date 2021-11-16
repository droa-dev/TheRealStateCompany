using System.Collections.Generic;

namespace Properties.WebApi.ViewModels
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public bool Result { get; set; }
        public List<string>? Errors { get; set; }
    }
}
