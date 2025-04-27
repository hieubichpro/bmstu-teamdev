using Store.Interfaces;
using Store.Models;

namespace Store.BL
{
    public class ItemOrderService
    {
        private readonly IItemOrderRepository _itemOrderRepository;

        public ItemOrderService(IItemOrderRepository itemOrderRepository)
        {
            _itemOrderRepository = itemOrderRepository;
        }

        public ItemOrder GetItemOrderById(int id)
        {
            return _itemOrderRepository.GetItemOrder(id);
        }

        public List<ItemOrder> GetItemOrderByIdOrder(int id)
        {
            return _itemOrderRepository.GetItemOrderByIdOrder(id);
        }
        public List<ItemOrder> GetItemOrders()
        {
            return _itemOrderRepository.GetItemOrders();
        }

        public void AddItemOrder(ItemOrder itemOrder)
        {
            _itemOrderRepository.AddItemOrder(itemOrder);
        }

        public void DelItemOrder(int id)
        {
            if (_itemOrderRepository.GetItemOrder(id) == null)
            {
                throw new Exception("Деталь корзины не существует");
            }
            else
                _itemOrderRepository.DelItemOrder(_itemOrderRepository.GetItemOrder(id));
        }

        public void UpdateItemOrder(ItemOrder itemOrder)
        {
            if (_itemOrderRepository.IsExistItemOrder(itemOrder) == false)
            {
                throw new Exception("Деталь корзины не существует");
            }
            else
                _itemOrderRepository.UpdateItemOrder(itemOrder);
        }
    }
}
