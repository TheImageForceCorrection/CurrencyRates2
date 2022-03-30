using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CurrencyRates.Container;
using CurrencyRates.Ui.ViewModels;
using CurrencyRates.Ui.ViewModels.Factories;
using CurrencyRates.Ui.Views;

namespace CurrencyRates.Ui
{
    sealed partial class App
    {
        private readonly ContainerWrapper _containerWrapper;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            _containerWrapper = new ContainerWrapper();
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (Window.Current.Content is not Frame rootFrame)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    var viewModelFactory = _containerWrapper.Resolve();

                    var viewModel = await GetViewModelOrDefaultWithErrorHandling(viewModelFactory);
                    rootFrame.Navigate(typeof(MainPage), viewModel);
                }

                Window.Current.Activate();
            }
        }

        private async Task<ICurrencySelectionsViewModel?> GetViewModelOrDefaultWithErrorHandling(
            ICurrencySelectionsViewModelFactory factory)
        {
            try
            {
                return await factory.Create();
            }
            catch (UriFormatException)
            {
                var messageDialog =
                    new MessageDialog(
                        "Неверный url с информацией о курсах валют. Пропишите в конфигурационном файле актульный url");
                await messageDialog.ShowAsync();

                return null;
            }
            catch (Exception)
            {
                var messageDialog =
                    new MessageDialog(
                        "Неизвестная ошибка при попытке загрузить курсы валют");
                await messageDialog.ShowAsync();

                return null;
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            _containerWrapper.Dispose();
            deferral.Complete();
        }
    }
}