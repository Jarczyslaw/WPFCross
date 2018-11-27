using MvvmCross.IoC;

namespace WPFCross.Extensions
{
    public static class IoCProviderExtensions
    {
        public static T RegisterTypeAndResolve<T>(this IMvxIoCProvider iocProvider)
            where T : class
        {
            iocProvider.RegisterType<T>();
            return iocProvider.Resolve<T>();
        }
    }
}
