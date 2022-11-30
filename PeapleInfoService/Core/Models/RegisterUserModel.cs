namespace Core.Models
{
    public class RegisterUserModel
    {
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string FullName { get; set; }
        public string ProvinceTitle { get; set; }
        public int Age { get; set; }

    }
}
