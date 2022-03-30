using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CurrencyRates.Ui.ViewModels;

namespace CurrencyRates.Ui.Views
{
    public sealed partial class MainPage
    {
        private ICurrencySelectionsViewModel? _currencySelectionsViewModel;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is not ICurrencySelectionsViewModel viewModel)
            {
                var messageDialog = new MessageDialog("Невозможно загрузить данные курсов валют");
                await messageDialog.ShowAsync();
                return;
            }

            _currencySelectionsViewModel = viewModel;
            DataContext = viewModel.GetSelectedRelativeCurrencyRateViewModel();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Content is not Frame rootFrame ||
                _currencySelectionsViewModel is null ||
                sender is not Button button || button.Tag is not string tagString ||
                DataContext is not IRelativeCurrencyRateViewModel relativeCurrencyRateViewModel)
            {
                var messageDialog = new MessageDialog("Данные курсов валют недоступны");
                await messageDialog.ShowAsync();
                return;
            }

            relativeCurrencyRateViewModel.ClearCurrencyAmounts();

            _currencySelectionsViewModel.IsFirstIndexSelecting = tagString.Equals("0", StringComparison.Ordinal);

            rootFrame.Navigate(typeof(CurrencySelectionPage), _currencySelectionsViewModel);
        }
    }
}