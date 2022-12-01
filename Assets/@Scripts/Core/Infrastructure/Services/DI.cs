using System;

namespace Core.Infrastructure.Services
{
    public class DI
    {
        private static readonly Lazy<DI> LazyLoader = new Lazy<DI>(() => new DI());
        public static DI Container => LazyLoader.Value;

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService =>
            Implementation<TService>.ServiceInstance;

        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}