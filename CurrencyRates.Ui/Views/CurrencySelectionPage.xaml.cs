using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CurrencyRates.Ui.ViewModels;

namespace CurrencyRates.Ui.Views
{
    public sealed partial class CurrencySelectionPage
    {
        private ICurrencySelectionsViewModel? _currencySelectionsViewModel;

        public CurrencySelectionPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is not ICurrencySelectionsViewModel viewModel || CurrencySelectionListBox.Items is null)
            {
                var messageDialog = new MessageDialog("Невозможно загрузить данные курсов валют");
                await messageDialog.ShowAsync();
                return;
            }

            _currencySelectionsViewModel = viewModel;

            DataContext = viewModel.SecondSelectedCurrency;

            if (viewModel.IsFirstIndexSelecting)
            {
                DataContext = viewModel.FirstSelectedCurrency;
            }

            foreach (var currencyName in viewModel.CurrencyNames)
            {
                CurrencySelectionListBox.Items.Add(new ListBoxItem
                {
                    Content = new TextBlock()
                    {
                        Text = currencyName,
                    }
                });
            }
        }

        private async void CurrencySelectionListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Window.Current.Content is not Frame rootFrame || _currencySelectionsViewModel is null)
            {
                var messageDialog = new MessageDialog("Невозможно загрузить данные курсов валют");
                await messageDialog.ShowAsync();
                return;
            }

            if (_currencySelectionsViewModel.AreEqualIndexes())
            {
                return;
            }

            rootFrame.Navigate(typeof(MainPage), _currencySelectionsViewModel);
        }
    }
}