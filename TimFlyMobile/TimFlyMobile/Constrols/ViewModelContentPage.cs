using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimFlyMobile.ViewModel;
using Xamarin.Forms;

namespace TimFlyMobile.Constrols
{
    public class ViewModelContentPage : ContentPage
    {
        public ViewModelContentPage()
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ExtendedViewModel vm = BindingContext as ExtendedViewModel;
            if (vm != null && vm.LoadCommand.CanExecute(null))
                vm.LoadCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ExtendedViewModel vm = BindingContext as ExtendedViewModel;
            if (vm != null && vm.UnloadCommand.CanExecute(null))
                vm.UnloadCommand.Execute(null);
        }
    }
}
