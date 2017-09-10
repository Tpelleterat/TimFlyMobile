using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TimFlyMobile.Managers;
using Xamarin.Forms;

namespace TimFlyMobile.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ConnexionViewModel : ViewModelBase
    {
        #region Attributes

        private readonly IGlobalManager _globalManager;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set socket server address
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }
        private string _address;

        /// <summary>
        /// Get or set socket server port
        /// </summary>
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                RaisePropertyChanged(() => Port);
            }
        }
        private string _port;

        /// <summary>
        /// Get or set connection pending indicator
        /// </summary>
        public bool ConnectionPending
        {
            get
            {
                return _connectionPending;
            }
            set
            {
                _connectionPending = value;
                RaisePropertyChanged(() => ConnectionPending);
                RaisePropertyChanged(() => ReversConnectionPending);
            }
        }
#warning A remplacer dans le Xaml avec le converter
        public bool ReversConnectionPending
        {
            get
            {
                return !_connectionPending;
            }
        }
        private bool _connectionPending;

        #endregion

        #region Commands

        /// <summary>
        /// Get connect command
        /// </summary>
        public RelayCommand ConnectCommand
        {
            get
            {
                if (_connectCommand == null)
                    _connectCommand = new RelayCommand(Connect);

                return _connectCommand;
            }
        }
        private RelayCommand _connectCommand;

        #endregion

        public ConnexionViewModel(IGlobalManager globalManager)
        {
            _globalManager = globalManager;

            Address = Constants.DEFAULT_ADDRESS;
            Port = Constants.DEFAULT_PORT;
        }

        #region Methods

        /// <summary>
        /// Call connect procedure
        /// </summary>
        public async void Connect()
        {
            ConnectionPending = true;

#warning int parse exception
            bool success = await _globalManager.Connect(Address, int.Parse(Port));

            if (success)
            {
                ConnectionPending = false;
                Device.BeginInvokeOnMainThread(() => {
                    App.Current.MainPage = new Pages.Fly();
                });
            }
        }

        #endregion
    }
}