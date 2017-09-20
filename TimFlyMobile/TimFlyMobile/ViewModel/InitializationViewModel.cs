using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TimFlyMobile.Managers;
using System.Windows.Input;
using TimFlyMobile.Entities;
using Xamarin.Forms;

namespace TimFlyMobile.ViewModel
{

    public class InitializationViewModel : ExtendedViewModel
    {
        #region Properties

        private IGlobalManager _globalManager;
        private bool _initPending;

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
                    _sendInitializationCommand = new RelayCommand(SendInitialization, CanSendInitialization);

                return _sendInitializationCommand;
            }
        }
        private RelayCommand _sendInitializationCommand;

        #endregion

        public InitializationViewModel(IGlobalManager globalManager)
        {
            _globalManager = globalManager;
        }

        private void SendInitialization()
        {
            _globalManager.SendInitialization();
            _initPending = true;
        }

        private bool CanSendInitialization()
        {
            return !_initPending;
        }

        protected override void Unload()
        {
            _initPending = false;
        }
    }
}