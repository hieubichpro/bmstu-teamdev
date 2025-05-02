using Store.Models;

namespace Dto
{
    public class UserDto
    {
        private string login;
        private string password;
        private string role;
        public string Password { get => password; set => password = value; }
        public string Login { get => login; set => login = value; }
        public string Role { get => role; set => role = value; }
        public UserDto(string login, string password, string role)
        {
            this.password = password;
            this.login = login;
            this.role = role;
        }
    }
}
