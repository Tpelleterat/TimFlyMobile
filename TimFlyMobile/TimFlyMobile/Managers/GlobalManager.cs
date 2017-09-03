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
            //bool success = await _socketService.Connect(address, port);

            //if (success)
            //{
            //    await _socketService.SendMessage(Constants.STATUS_COMMAND);
            //}
            //return success;

            await Task.Delay(3000);
            return false;
        }

        #endregion

        #region Handlers

        private void OnSocketServiceMessageReceived(object sender, string e)
        {

        }

        #endregion
    }
}
