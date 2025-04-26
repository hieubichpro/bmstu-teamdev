using Store.Models;

namespace Store.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);

        User GetUser(string login);

        void AddUser(User user);

        void DelUser(User user);

        void UpdateUser(User user);

        ICollection<User> GetAll();

        int CountAllUsers();
    }
}
