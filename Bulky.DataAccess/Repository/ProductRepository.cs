using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky.DataAccess.Repository;

public class ProductRepository : Repository<ProductRepository>, IProductRepository
{
    public ProductRepository(ApplicationDbContext db) : base(db)
    {
    }

    public void Update(ProductRepository obj)
    {
        throw new NotImplementedException();
    }
}