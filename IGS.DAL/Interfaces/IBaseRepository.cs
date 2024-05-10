namespace IGS.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        Task<List<T>> Select();

        Task<bool> Delete(T entity);
    }
}
