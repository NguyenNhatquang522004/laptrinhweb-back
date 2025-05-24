namespace backapi.auth
{
    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public string Provider { get; set; }
    }
}
