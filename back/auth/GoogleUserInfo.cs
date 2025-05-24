namespace backapi.auth
{
    public class GoogleUserInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Picture { get; set; }
        public bool EmailVerified { get; set; }
    }
}
