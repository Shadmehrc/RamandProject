using Core.Models;

namespace Core.ViewModels
{
    public class LoginUserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public static implicit operator LoginUserModel(LoginUserViewModel vModel)
        {
            return new LoginUserModel()
            {
                UserName = vModel.UserName
            };
        }

    }
}
