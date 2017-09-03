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
    public partial class Fly : ContentPage
    {
        public Fly()
        {
            BindingContext = App.Locator.FlyViewModel;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            TouchView.JoyContainer = JoyView;
            TouchView2.JoyContainer = JoyView2;
        }
    }
}