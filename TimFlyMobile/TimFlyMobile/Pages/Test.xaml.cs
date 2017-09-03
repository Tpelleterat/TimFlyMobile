using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimFlyMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        public Test()
        {
            BindingContext = App.Locator.TestViewModel;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            TouchView.JoyView = JoyView;
            TouchView2.JoyView = JoyView2;
        }
    }
}