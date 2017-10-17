using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimFlyMobile.Constrols;
using Xamarin.Forms;

namespace TimFlyMobile.Pages
{
    public partial class Initialization : ViewModelContentPage
    {
        public Initialization()
        {
            BindingContext = App.Locator.InitializationViewModel;

            InitializeComponent();
        }
    }
}
