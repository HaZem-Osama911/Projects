using MyApp.ViewModels.Roles;

namespace MyApp.ViewModels.User
{
    public class UserRolesVieweModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
