namespace EShop.Application.Dtos.Login
{
    public class ValidarCredenciales2FADto
    {
        public string Jti { get; set; } = default!;
        public string Otp { get; set; } = default!;
    }
}
