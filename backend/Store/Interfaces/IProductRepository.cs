using Store.Models;

namespace Store.Interfaces
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        ICollection<Product> GetProduct(string name);
        void AddProduct(Product product);
        void DelProduct(Product product);

        void UpdateProduct(Product product);

        bool IsExistProduct(Product product);

        ICollection<Product> GetAllProducts();
    }
}
