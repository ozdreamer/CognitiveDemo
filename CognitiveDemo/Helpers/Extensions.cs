using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CognitiveDemo
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require
            return ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
        }
    }

    [TypeConversion(typeof(Color))]
    public class ColorTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            return Color.FromHex(value);
        }
    }
}
