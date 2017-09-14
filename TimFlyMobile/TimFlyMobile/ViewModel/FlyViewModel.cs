using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TimFlyMobile.Managers;
using Xamarin.Forms;
using System.Threading.Tasks;

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
    public class FlyViewModel : ViewModelBase
    {
        #region Attributes

        private readonly IGlobalManager _globalManager;
        private int _elevationWorker;

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

        public RelayCommand InitializationCommand
        {
            get
            {
                if (_initializationCommand == null)
                    _initializationCommand = new RelayCommand(Initialization);

                return _initializationCommand;
            }
        }
        private RelayCommand _initializationCommand;

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
            Task.Run(async () =>
            {
                bool elevationLoopOk = true;

                while (elevationLoopOk)
                {
                    if (ElevationValue + _elevationWorker < 0)
                    {
                        ElevationValue = 0;
                    }
                    else
                    {
                        ElevationValue += _elevationWorker;
                    }

                    _globalManager.ChangeElevation(Convert.ToInt32(ElevationValue));

                    await Task.Delay(100);
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

        private async void Initialization()
        {
            _globalManager.SendInitialization();

            await Task.Delay(2000);

            ElevationLoop();

            _globalManager.SendCommandsLoop();
        }

        #endregion
    }
}