namespace AppointDoc.Domain.Dtos.Response
{
    public class AuthenticationResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
