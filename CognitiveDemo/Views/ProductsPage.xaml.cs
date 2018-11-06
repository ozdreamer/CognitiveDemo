using System;

namespace CognitiveDemo
{
	public partial class ProductsPage : BasePage
	{
        public ProductsViewModel ViewModel => this.BindingContext as ProductsViewModel;

        public ProductsPage(string email) : base(new ProductsViewModel(email))
		{
            this.BindingContext = this.BaseViewModel;
            this.InitializeComponent();
        }
    }
}