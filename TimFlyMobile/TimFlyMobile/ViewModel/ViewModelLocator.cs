/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TimFlyMobile"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using TimFlyMobile.Managers;

namespace TimFlyMobile.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


            SimpleIoc.Default.Register<IGlobalManager, GlobalManager>();

            SimpleIoc.Default.Register<ConnexionViewModel>();
            SimpleIoc.Default.Register<InitializationViewModel>();
            SimpleIoc.Default.Register<FlyViewModel>();
            SimpleIoc.Default.Register<TestViewModel>();
        }

        public ConnexionViewModel ConnexionViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConnexionViewModel>();
            }
        }

        public FlyViewModel FlyViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FlyViewModel>();
            }
        }

        public InitializationViewModel InitializationViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InitializationViewModel>();
            }
        }

        public TestViewModel TestViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TestViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}