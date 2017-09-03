using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimFlyMobile.Pages
{
    public partial class Connexion : ContentPage
    {
        public Connexion()
        {
            BindingContext = App.Locator.ConnexionViewModel;

            InitializeComponent();
        }
    }
} 