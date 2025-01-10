namespace AppointDoc.Domain.Dtos.Request
{
    public class LoginRegisterRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
