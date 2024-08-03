using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<ProductRepository>
{
    void Update(ProductRepository obj);
}