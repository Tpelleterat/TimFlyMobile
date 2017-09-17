using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TimFlyMobile.Managers;
using System.Windows.Input;
using TimFlyMobile.Entities;
using Xamarin.Forms;

namespace TimFlyMobile.ViewModel
{

    public class InitializationViewModel : ViewModelBase
    {
        #region Properties

        #endregion

        #region Commands

        /// <summary>
        /// Get connect command
        /// </summary>
        public RelayCommand SendInitializationCommand
        {
            get
            {
                if (_sendInitializationCommand == null)
                    _sendInitializationCommand = new RelayCommand(SendInitialization);

                return _sendInitializationCommand;
            }
        }
        private RelayCommand _sendInitializationCommand;

        #endregion

        private void SendInitialization()
        {

        }

    }
}