using Xamarin.Forms;

namespace CognitiveDemo
{
    public class BasePage : ContentPage
    {
        protected BaseViewModel BaseViewModel;

        public BasePage(BaseViewModel viewModel) : base()
        {
            this.BaseViewModel = viewModel;

            this.BaseViewModel.Navigate = async (page) =>
            {
                await Navigation.PushAsync(page);
            };

        }
    }
}
