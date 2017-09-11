using MobilePrototype.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimFlyMobile.Entities;

namespace TimFlyMobile.Managers
{
    //TODO Créer un évènement Disconnect qui redirige vers l'écran de connexion

    public class GlobalManager : IGlobalManager
    {
        #region Attributes

        XamarinSocketService _socketService;
        BrainStatusEnum _brainStatusEnum;

        private int _elevation;
        private bool _newElevation;
        private int _pich;
        private bool _newPich;
        private int _roll;
        private bool _newRoll;

        #endregion

        public GlobalManager()
        {
            _socketService = new XamarinSocketService();
            _socketService.OnMessageReceived += OnSocketServiceMessageReceived;
        }

        #region Methods

        public async Task<bool> Connect(string address, int port)
        {
#warning debug, code à réactiver
            bool success = await _socketService.Connect(address, port);

            if (success)
            {
                await _socketService.SendMessage(Constants.STATUS_COMMAND);

#warning Voir meilleurs solution
                StartSocketLoop();
            }
            return success;

            //await Task.Delay(3000);
            //return false;
        }

        /// <summary>
        /// Send socket message with
        /// </summary>
        /// <param name="value">New elevation value</param>
        public void ChangeElevation(int value)
        {
            if (value != _elevation)
            {
                _elevation = value;
                _newElevation = true;
            }
        }

        public void ChangePich(int value)
        {
            if (value != _pich)
            {
                _pich = value;
                _newPich = true;
            }
        }

        public void ChangeRoll(int value)
        {
            if (value != _roll)
            {
                _roll = value;
                _newRoll = true;
            }
        }

        public async void StartSocketLoop()
        {
            await Task.Run(async () =>
            {
                bool connected = false;

                while (!connected)
                {
                    SendElevation();
                    SendPich();
                    SendRoll();

                    if (!connected)
                        await Task.Delay(500);
                }
            });
        }

        private async void SendElevation()
        {
            if (_newElevation)
            {
                await _socketService.SendMessage(string.Concat(Constants.ELEVATION_COMMAND, Constants.COMMAND_SEPARATOR, _elevation));
                _newElevation = false;
            }
        }

        private async void SendPich()
        {
            if (_newPich)
            {
                await _socketService.SendMessage(string.Concat(Constants.PICH_COMMAND, Constants.COMMAND_SEPARATOR, _pich));
                _newPich = false;
            }
        }

        private async void SendRoll()
        {
            if (_newRoll)
            {
                await _socketService.SendMessage(string.Concat(Constants.ROLL_COMMAND, Constants.COMMAND_SEPARATOR, _elevation));
                _newRoll = false;
            }
        }

        #endregion

        #region Handlers

        private void OnSocketServiceMessageReceived(object sender, string e)
        {

        }

        #endregion
    }
}
