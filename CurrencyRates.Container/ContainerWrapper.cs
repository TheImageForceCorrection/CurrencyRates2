using System;
using Autofac;
using CurrencyRates.BusinessLogic;
using CurrencyRates.BusinessLogic.Implementation;
using CurrencyRates.Configuration;
using CurrencyRates.Configuration.Implementation;
using CurrencyRates.Data;
using CurrencyRates.Data.Implementation;
using CurrencyRates.Ui.ViewModels;
using CurrencyRates.Ui.ViewModels.Factories;
using CurrencyRates.Ui.ViewModels.Implementation;
using CurrencyRates.Ui.ViewModels.Implementation.Factories;

namespace CurrencyRates.Container
{
    public sealed class ContainerWrapper : IDisposable
    {
        private readonly IContainer _container;

        public ContainerWrapper()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>();
            containerBuilder.RegisterType<CurrenciesProvider>().As<ICurrenciesProvider>();
            containerBuilder.RegisterType<RelativeCurrencyRateCalculator>().As<IRelativeCurrencyRateCalculator>();
            containerBuilder.RegisterType<RelativeCurrencyRatesProvider>().As<IRelativeCurrencyRatesProvider>();
            containerBuilder.RegisterType<CurrencySelectionsViewModel>()
                .As<ICurrencySelectionsViewModel>().InstancePerDependency();
            containerBuilder.RegisterType<RelativeCurrencyRateViewModel>()
                .As<IRelativeCurrencyRateViewModel>();
            containerBuilder.RegisterType<CurrencySelectionsViewModelFactory>()
                .As<ICurrencySelectionsViewModelFactory>();

            _container = containerBuilder.Build();
        }

        public ICurrencySelectionsViewModelFactory Resolve()
        {
            return _container.Resolve<ICurrencySelectionsViewModelFactory>();
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}