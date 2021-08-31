namespace Weblog.Requests
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UpdateUserRequest()
        {
        }
        public UpdateUserRequest( string name, string email, string password )
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
