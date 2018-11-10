using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CognitiveDemo
{
    public partial class DescriptionPage : BasePage
    {
        private DescriptionViewModel PageViewModel => this.BaseViewModel as DescriptionViewModel;

        public DescriptionPage() : base(new DescriptionViewModel())
        {
            this.BindingContext = this.BaseViewModel;
            this.InitializeComponent();
        }
    }
}
