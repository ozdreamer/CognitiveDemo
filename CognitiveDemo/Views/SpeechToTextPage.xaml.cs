using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CognitiveDemo
{
    public partial class SpeechToTextPage : BasePage
    {
        private SpeechToTextViewModel PageViewModel => this.BaseViewModel as SpeechToTextViewModel;

        public SpeechToTextPage() : base(new SpeechToTextViewModel())
        {
            this.BindingContext = this.BaseViewModel;
            this.InitializeComponent();
        }
    }
}
