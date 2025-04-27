using Store.Models;

namespace Store.Interfaces
{
    public interface ICartRepository
    {
        ICollection<Cart> GetAllCarts();
        Cart GetCart(int id);

        void AddCart(Cart cart);
        void DelCart(Cart cart);
        void UpdateCart(Cart cart);

        bool IsExistCart(Cart cart);
    }
}
