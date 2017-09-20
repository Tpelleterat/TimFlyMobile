using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimFlyMobile.ViewModel
{
    public class ExtendedViewModel : ViewModelBase
    {
        #region Commands

        /// <summary>
        /// Get load command
        /// </summary>
        public RelayCommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                    _loadCommand = new RelayCommand(Load);

                return _loadCommand;
            }
        }
        private RelayCommand _loadCommand;

        /// <summary>
        /// Get unload command
        /// </summary>
        public RelayCommand UnloadCommand
        {
            get
            {
                if (_unloadCommand == null)
                    _unloadCommand = new RelayCommand(Unload);

                return _unloadCommand;
            }
        }
        private RelayCommand _unloadCommand;

        #endregion

        protected virtual void Load()
        {

        }

        protected virtual void Unload()
        {
        }
    }
}
