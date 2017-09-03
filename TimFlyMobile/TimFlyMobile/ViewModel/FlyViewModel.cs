using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TimFlyMobile.Managers;

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

        #endregion

        public FlyViewModel(IGlobalManager globalManager)
        {
            _globalManager = globalManager;
        }

        #region Methods

        #endregion
    }
}