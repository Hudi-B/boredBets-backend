namespace boredBets.Repositories.Interface
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
}
