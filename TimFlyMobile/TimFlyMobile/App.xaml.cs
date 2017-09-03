using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimFlyMobile.Pages;
using TimFlyMobile.ViewModel;
using Xamarin.Forms;

namespace TimFlyMobile
{
    public partial class App : Application
    {
        public static ViewModelLocator Locator
        {
            get
            {
                return _locator ?? (_locator = new ViewModelLocator());
            }
        }
        private static ViewModelLocator _locator;

        public App()
        {
            //MainPage = new Connexion();
            MainPage = new Fly();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
