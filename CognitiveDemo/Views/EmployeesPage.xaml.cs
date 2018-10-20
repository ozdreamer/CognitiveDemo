using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CognitiveDemo
{
	public partial class EmployeesPage : ContentPage
	{
        public EmployeesViewModel ViewModel => this.BindingContext as EmployeesViewModel;

        public EmployeesPage()
		{
            this.BindingContext = new EmployeesViewModel();
            this.InitializeComponent();
		}
	}
}