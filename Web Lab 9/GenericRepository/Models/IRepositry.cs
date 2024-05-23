namespace Tassk2.Models
{
    public interface IRepository<TEntity>
    {
        TEntity FindById(int id);
         public void DeleteById(int id);
        public void Add(TEntity entity);
    }
}
