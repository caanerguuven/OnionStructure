namespace OnionStructure.API.ViewModels.Response
{
    public class AccountLoginResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int CasinoId { get; set; }
        public string[] Role { get; set; }
        public string Token { get; set; }
    }
}
