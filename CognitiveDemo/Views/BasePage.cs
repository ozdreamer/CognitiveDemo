using System;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class BasePage : ContentPage
    {
        protected BaseViewModel BaseViewModel;

        public BasePage(BaseViewModel viewModel) : base()
        {
            this.BaseViewModel = viewModel;

            this.BaseViewModel.ShowErrorMessage = (message) =>
            {
                this.DisplayAlert("Error", message, "Cancel");
            };

            this.BaseViewModel.Navigate = async (page) =>
            {
                await Navigation.PushAsync(page);
            };

        }
    }
}
