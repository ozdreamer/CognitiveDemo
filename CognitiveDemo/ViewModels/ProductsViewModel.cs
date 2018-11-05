using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;

namespace CognitiveDemo
{
	public class ProductsViewModel : BaseViewModel
	{
		public ProductsViewModel()
		{
            var assembly = typeof(ProductsViewModel).GetTypeInfo().Assembly;
            Title = "Products";
			Products = new ObservableCollection<Product>
			{
                new Product { Name = "Simulate", Title = "Discrete event based simulator", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Simulate.png", assembly) },
				new Product { Name = "Talpac", Title = "Single source fleet analzyer", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Talpac.png", assembly) },
                new Product { Name = "Xpac", Title = "The scheduling engine to design optimize mine schedules", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Xpac.png", assembly) },
                new Product { Name = "Xeras", Title = "Finanical planning enterprise product", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Xeras.png", assembly) },
                new Product { Name = "Xecute", Title = "The block based mini scehduler", Image = ImageSource.FromResource("CognitiveDemo.Resources.Product_Images.Xecute.png", assembly) },
            };
		}

		ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
		{
            get { return this.products; }
			set { this.products = value; OnPropertyChanged("Products"); }
		}
	}
}