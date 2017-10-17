using GalaSoft.MvvmLight.Command;
using TimFlyMobile.Managers;

namespace TimFlyMobile.ViewModel
{

    public class InitializationViewModel : ExtendedViewModel
    {
        #region Attributes

        private bool _initPending;
        private readonly IGlobalManager _globalManager;

        #endregion

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
                    _sendInitializationCommand = new RelayCommand(SendInitialization, CanSendInitialization);

                return _sendInitializationCommand;
            }
        }
        private RelayCommand _sendInitializationCommand;

        #endregion

        /// <summary>
        /// Initialise new instance
        /// </summary>
        /// <param name="globalManager">Global manager</param>
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