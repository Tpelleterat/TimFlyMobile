using MobilePrototype.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimFlyMobile.Entities;
using Xamarin.Forms;

namespace TimFlyMobile.Managers
{
    //TODO Créer un évènement Disconnect qui redirige vers l'écran de connexion

    public class GlobalManager : IGlobalManager
    {
        #region Attributes

        XamarinSocketService _socketService;
        BrainStatusEnum _brainStatusEnum;

        private int _elevation;
        private int _pitch;
        private int _roll;

        bool _commandsLoopOk;

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
                await _socketService.SendMessage(Constants.ASKSTATUS_COMMAND);
            }
            return success;

            //await Task.Delay(3000);
            //return false;
        }

        public void SendCommandsLoop()
        {
            Task.Run(async () =>
            {
                _commandsLoopOk = true;

                while (_commandsLoopOk)
                {
                    SendCommands();

                    await Task.Delay(50);
                }
            });
        }

        private async void SendCommands()
        {
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.Append(string.Concat(Constants.ELEVATION_COMMAND, Constants.COMMAND_SEPARATOR, _elevation));
            messageBuilder.Append(";");
            messageBuilder.Append(string.Concat(Constants.ROLL_COMMAND, Constants.COMMAND_SEPARATOR, _roll));
            messageBuilder.Append(";");
            messageBuilder.Append(string.Concat(Constants.PITCH_COMMAND, Constants.COMMAND_SEPARATOR, _pitch));

            await _socketService.SendMessage(messageBuilder.ToString());
        }

        public void ChangeElevation(int value)
        {
            _elevation = value;
        }

        public void ChangePitch(int value)
        {
            _pitch = value;
        }

        public void ChangeRoll(int value)
        {
            _roll = value;
        }

        public async void SendInitialization()
        {
            await _socketService.SendMessage(Constants.INITIALIZATION_COMMAND);
        }

        public void ReceiveNewStatus(string status)
        {
            BrainStatusEnum NewStatus = (BrainStatusEnum)Enum.Parse(typeof(BrainStatusEnum), status, true);

            if (NewStatus != _brainStatusEnum)
            {
                _brainStatusEnum = NewStatus;
                if (_brainStatusEnum == BrainStatusEnum.Fly && App.Current.MainPage.GetType() != typeof(Pages.Fly))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        App.Current.MainPage = new Pages.Fly();
                    });
                }
                else if (_brainStatusEnum == BrainStatusEnum.Initialisation)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        App.Current.MainPage = new Pages.Initialization();
                    });
                }
            }
        }

        #endregion

        #region Handlers

        private void OnSocketServiceMessageReceived(object sender, string messageData)
        {
            List<string> commands = messageData.Split(';')?.ToList();

            foreach (var command in commands)
            {
                string data = command.Split('|')?.ToList().Last();

                if (command.Contains(Constants.STATUS_COMMAND))
                {
                    ReceiveNewStatus(data);
                }
            }
        }


        #endregion
    }
}
