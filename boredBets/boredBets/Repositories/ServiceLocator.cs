using boredBets.Repositories.Interface;

namespace boredBets.Repositories
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
