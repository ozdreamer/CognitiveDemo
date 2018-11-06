using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;

namespace CognitiveDemo
{
	public class ProductsViewModel : BaseViewModel
	{
        public ProductsViewModel(string email)
		{
            this.Email = email;
            this.Title = "Products";

            var assembly = typeof(ProductsViewModel).GetTypeInfo().Assembly;
			this.Products = new ObservableCollection<Product>
			{
                new Product { Name = "Simulate", Title = "Discrete event based simulator to analyze and sumulate the mining operations and manage fleets", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Simulate.png", assembly) },
				new Product { Name = "Talpac", Title = "Single source fleet analzyer and a lightweight version of current simulation product Simulate", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Talpac.png", assembly) },
                new Product { Name = "Xpac", Title = "The scheduling engine to design optimize mine schedules", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Xpac.png", assembly) },
                new Product { Name = "Xeras", Title = "Finanical planning enterprise product which is ueed for", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Xeras.png", assembly) },
                new Product { Name = "Xecute", Title = "The block based mini scehduler to analyze a small block of surface with high precision", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Xecute.png", assembly) },
            };
        }

		private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
		{
            get
            {
                return this.products;
            }

			set
            {
                this.products = value;
                this.OnPropertyChanged(nameof(this.Products));
            }
		}

        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.OnPropertyChanged(nameof(this.Email));
                }
            }
        }
    }
}