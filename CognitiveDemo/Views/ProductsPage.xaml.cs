namespace CognitiveDemo
{
	public partial class ProductsPage : BasePage
	{
        public ProductsViewModel ViewModel => this.BindingContext as ProductsViewModel;

        public ProductsPage() : base(new ProductsViewModel())
		{
            this.BindingContext = this.BaseViewModel;
            this.InitializeComponent();
        }
    }
}