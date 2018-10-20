using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpViewModel ViewModel => this.BindingContext as SignUpViewModel;

        public SignUpPage()
        {
            this.BindingContext = new SignUpViewModel();
            this.InitializeComponent();
            this.ViewModel.ShowErrorMessage = (message) =>
            {
                this.DisplayAlert("Error", message, null);
            };
        }
    }
}