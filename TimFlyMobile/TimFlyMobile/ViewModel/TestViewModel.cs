using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TimFlyMobile.Managers;
using System.Windows.Input;
using TimFlyMobile.Entities;
using Xamarin.Forms;

namespace TimFlyMobile.ViewModel
{

    public class TestViewModel : ViewModelBase
    {
        #region Commands

        /// <summary>
        /// Get connect command
        /// </summary>
        public RelayCommand<Point> ConnectCommand
        {
            get
            {
                if (_connectCommand == null)
                    _connectCommand = new RelayCommand<Point>(param => NewPosition(param));

                return _connectCommand;
            }
        }
        private RelayCommand<Point> _connectCommand;

        #endregion

        #region Properties

        private double xPosition;

        public double XPosition
        {
            get
            {
                return xPosition;
            }
            set
            {
                xPosition = value;
                RaisePropertyChanged(() => XPosition);
            }
        }


        #endregion

        public TestViewModel()
        {
        }

        private void NewPosition(Point param)
        {
            XPosition = param.Y;
        }
    }
}