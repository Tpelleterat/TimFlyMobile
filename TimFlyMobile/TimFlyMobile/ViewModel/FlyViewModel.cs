using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TimFlyMobile.Managers;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace TimFlyMobile.ViewModel
{
    public class FlyViewModel : ExtendedViewModel
    {
        #region Attributes

        private readonly IGlobalManager _globalManager;
        private int _elevationWorker;
        bool _elevationLoopOk;

        #endregion

        #region Properties

        public string ElevationValueLabel
        {
            get
            {
                return _elevationValue.ToString();
            }
        }

        public double ElevationValue
        {
            get
            {
                return _elevationValue;
            }
            set
            {
                _elevationValue = value;
                RaisePropertyChanged(() => ElevationValueLabel);
            }
        }
        private double _elevationValue;

        public string PitchValueLabel
        {
            get
            {
                return _pitchValue.ToString();
            }
        }

        public double PitchValue
        {
            get
            {
                return _pitchValue;
            }
            set
            {
                _pitchValue = value;
                RaisePropertyChanged(() => PitchValueLabel);
            }
        }
        private double _pitchValue;

        public string RollValueLabel
        {
            get
            {
                return RollValue.ToString();
            }
        }

        public double RollValue
        {
            get
            {
                return _rollValue;
            }
            set
            {
                _rollValue = value;
                RaisePropertyChanged(() => RollValueLabel);
            }
        }
        private double _rollValue;

        #endregion

        #region Commands

        /// <summary>
        /// Get roll pitch command
        /// </summary>
        public RelayCommand<Point> ElevationYawJoyCommand
        {
            get
            {
                if (_elevationYawCommand == null)
                    _elevationYawCommand = new RelayCommand<Point>(point => ElevationYawJoy(point));

                return _elevationYawCommand;
            }
        }
        private RelayCommand<Point> _elevationYawCommand;

        /// <summary>
        /// Get roll pitch command
        /// </summary>
        public RelayCommand<Point> RollPitchJoyCommand
        {
            get
            {
                if (_rollPitchJoyCommand == null)
                    _rollPitchJoyCommand = new RelayCommand<Point>(point => RollPitchJoy(point));

                return _rollPitchJoyCommand;
            }
        }
        private RelayCommand<Point> _rollPitchJoyCommand;

        /// <summary>
        /// Get stop command
        /// </summary>
        public RelayCommand StopCommand
        {
            get
            {
                if (_stopCommand == null)
                    _stopCommand = new RelayCommand(Stop);

                return _stopCommand;
            }
        }
        private RelayCommand _stopCommand;


        #endregion

        /// <summary>
        /// Initialise new instance
        /// </summary>
        /// <param name="globalManager">Global manager</param>
        public FlyViewModel(IGlobalManager globalManager)
        {
            _globalManager = globalManager;
        }

        #region Methods

        private void ElevationLoop()
        {
            if (_elevationLoopOk)
                return;

            Task.Run(async () =>
            {
                _elevationLoopOk = true;

                while (_elevationLoopOk)
                {
                    double newValue = ElevationValue + _elevationWorker;
                    if (newValue < 0)
                    {
                        ElevationValue = 0;
                    }
                    else if (newValue <= Constants.ELEVATION_COMMAND_MAX_VALUE)
                    {
                        ElevationValue = newValue;
                    }

                    _globalManager.ChangeElevation(Convert.ToInt32(ElevationValue));

                    await Task.Delay(Constants.FREQUENCE_UPDATE_ELEVATION);
                }
            });
        }

        private void RollPitchJoy(Point point)
        {
            RollValue = Convert.ToInt32(point.X);
            PitchValue = Convert.ToInt32(point.Y);

            _globalManager.ChangeRoll((int)RollValue);
            _globalManager.ChangePitch((int)PitchValue);
        }

        private void ElevationYawJoy(Point point)
        {
            _elevationWorker = Convert.ToInt32(point.Y);
        }

        private void Stop()
        {
            _elevationValue = 0;
            _globalManager.ChangeElevation(0);
        }

        protected override void Load()
        {
            ElevationLoop();

            _globalManager.StartSendCommandsLoop();
        }

        protected override void Unload()
        {
            _elevationLoopOk = false;

            _globalManager.StopSendCommandsLoop();
        }

        #endregion
    }
}