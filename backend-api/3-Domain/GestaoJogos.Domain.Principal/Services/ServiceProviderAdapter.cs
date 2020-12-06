using System;

namespace GestaoJogos.Domain.Principal.Services
{
    public static class ServiceProviderAdapter
    {
        private static IServiceProvider ServiceProvider;

        public static void Init(IServiceProvider provider)
        {
            ServiceProvider = provider;
        }

        public static IServiceProvider GetServiceProvider()
        {
            return ServiceProvider;
        }
    }
}
