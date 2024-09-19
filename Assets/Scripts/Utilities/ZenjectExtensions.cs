using GTN.Base.Interfaces;
using Zenject;

namespace GTN.Utilities
{
    public static class ZenjectExtensions
    {
        public static void InstallService<TService>(this DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<TService>()
                .AsSingle();
        }
        
        public static void InstallFactory<TService>(this DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<TService>()
                .AsSingle()
                .NonLazy();
        }

        public static void BindServiceToInterface<TInterface, TService>(this DiContainer container)
            where TService : TInterface
        {
            container
                .Bind<TInterface>()
                .To<TService>()
                .AsSingle();
        }

        public static void InstallModel<TModel>(this DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<TModel>()
                .AsSingle();
        }

        public static void InstallGameRule<TGameRule>(this DiContainer container) where TGameRule : IGameRule
        {
            container
                .BindInterfacesAndSelfTo<TGameRule>()
                .AsSingle()
                .NonLazy();
        }
        
        public static void InstallRegistry<TRegistry>(this DiContainer container, TRegistry registry)
        {
            container
                .BindInterfacesAndSelfTo<TRegistry>()
                .FromInstance(registry)
                .AsSingle();
        }
    }
}