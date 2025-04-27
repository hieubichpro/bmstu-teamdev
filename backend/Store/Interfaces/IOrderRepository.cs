using Store.Models;

namespace Store.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);
        ICollection<Order> GetAllOrders();
        ICollection<Order> GetAllOrdersByIdUser(int id_user);
        void AddOrder(Order order);
        void DelOrder(Order order);

        void UpdateOrder(Order order);

        bool IsExistOrder(Order order);
    }
}
