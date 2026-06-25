
using Newtonsoft.Json;

namespace api.Application.DTOs.Usuario
{
    public class UpdatePasswordRequest
    {
        [JsonProperty("password")]
        public required string Password {get;set;}
    }
}