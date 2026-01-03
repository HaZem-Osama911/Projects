namespace MyApp.ViewModels.User
{
    public class UserViewModel
    {
        public string id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string UserName { get; set; } 
        public string Email { get; set; } 
        public IEnumerable<string> Roles { get; set; }
    }
}
