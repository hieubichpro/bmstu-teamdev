using Store.Models;

namespace Store.Interfaces
{
    public interface IItemOrderRepository
    {
        ItemOrder GetItemOrder(int id);

        void AddItemOrder(ItemOrder itemOrder);

        void DelItemOrder(ItemOrder itemOrder);

        void UpdateItemOrder(ItemOrder itemOrder);

        bool IsExistItemOrder(ItemOrder itemOrder);

        List<ItemOrder> GetItemOrderByIdOrder(int id);
        List<ItemOrder> GetItemOrders();
    }
}
