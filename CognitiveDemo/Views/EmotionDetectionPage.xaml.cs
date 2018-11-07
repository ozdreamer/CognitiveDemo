using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CognitiveDemo
{
    public partial class EmotionDetectionPage : BasePage
    {
        private EmotionDetectionViewModel PageViewModel => this.BaseViewModel as EmotionDetectionViewModel;

        public EmotionDetectionPage() : base(new EmotionDetectionViewModel())
        {
            this.BindingContext = this.BaseViewModel;
            this.InitializeComponent();
        }
    }
}
