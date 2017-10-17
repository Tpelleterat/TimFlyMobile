using TimFlyMobile.Constrols;
using Xamarin.Forms.Xaml;

namespace TimFlyMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Fly : ViewModelContentPage
    {
        public Fly()
        {
            BindingContext = App.Locator.FlyViewModel;

            InitializeComponent();

            TouchView.JoyContainer = JoyView;
            TouchView2.JoyContainer = JoyView2;
        }
    }
}