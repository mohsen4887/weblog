namespace Weblog.Requests
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public UpdateUserRequest()
        {
        }
        public UpdateUserRequest( string name, string email )
        {
            Name = name;
            Email = email;
        }
    }
}
