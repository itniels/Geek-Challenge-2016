using System.Collections.Generic;

namespace TheGeekStore.Core.Interfaces
{
    public interface IProductRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetTop3Sold();
        TEntity GetFeaturedProduct();

        IEnumerable<TEntity> GetFeaturedProducts();
    }
}