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

        public static void RegisterAndConstruct<TInterface, TType>(this IMvxIoCProvider iocProvider)
            where TType : class, TInterface
            where TInterface : class
        {
            iocProvider.RegisterSingleton<TInterface>(() => iocProvider.IoCConstruct<TType>());
        }
    }
}
