namespace VideoCollection.Model.DataAccess
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}