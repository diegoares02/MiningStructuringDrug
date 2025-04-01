namespace MiningStructuringDrug.Core.Application.Users.Dtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserDto? User { get; set; }
    }
}
