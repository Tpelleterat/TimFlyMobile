using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TimFlyMobile.Constrols
{
    public class CustomControlTest : View
    {
        //public View JoyView;
        public View JoyContainer;

        #region Attributes

        private bool _moveOn;
        private int _moveTouchId;
        private float _initialTouchX;
        private float _InitialTouchY;

        private double _initialJoyPositionX;
        private double _initialJoyPositionY;

        #endregion

        #region Properties

        public ICommand Test1
        {
            get
            {
                return (ICommand)GetValue(Test1Property);
            }
            set
            {
                SetValue(Test1Property, value);
            }
        }
        public static readonly BindableProperty Test1Property = BindableProperty.Create("Test1", typeof(ICommand), typeof(CustomControlTest), null);

        public double MaxX
        {
            get
            {
                return (double)GetValue(MaxXProperty);
            }
            set
            {
                SetValue(MaxXProperty, value);
            }
        }
        public static readonly BindableProperty MaxXProperty = BindableProperty.Create("MaxX", typeof(double), typeof(CustomControlTest), double.NaN);

        public double MaxY
        {
            get
            {
                return (double)GetValue(MaxYProperty);
            }
            set
            {
                SetValue(MaxYProperty, value);
            }
        }
        public static readonly BindableProperty MaxYProperty = BindableProperty.Create("MaxY", typeof(double), typeof(CustomControlTest), double.NaN);


        public double XPosition
        {
            get
            {
                return _xPosition;
            }
        }
        private double _xPosition;

        public double YPosition
        {
            get
            {
                return _yPosition;
            }
        }
        private double _yPosition;

        #endregion

        #region Methods

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            CenterJoy();
        }

        public void OnTouchEvent(TouchStatus touchStatus, int touchId, float x, float y)
        {
            if (touchStatus == TouchStatus.TouchDown && !_moveOn && IsJoyInside(x, y))
            {
                _moveOn = true;
                _moveTouchId = touchId;

                _initialTouchX = x;
                _InitialTouchY = y;
            }
            else if (touchStatus == TouchStatus.TouchMove && _moveOn && _moveTouchId == touchId)
            {
                MoveJoy(x, y);
                FillPositions();
            }
            else if (touchStatus == TouchStatus.TouchUp && _moveOn && _moveTouchId == touchId)
            {
                _moveOn = false;
                CenterJoy();
                FillPositions();
            }
        }

        private bool IsJoyInside(float x, float y)
        {
            bool xInside = TranslationX <= x && TranslationX + Width >= x;

            bool yInside = TranslationY <= y && TranslationY + Height >= y;

           //return xInside && yInside;
            return true;
        }

        private void MoveJoy(float x, float y)
        {
            double workX = _initialJoyPositionX + (x - _initialTouchX) * 0.35;
            if (workX < 0)
                workX = 0;
            else if (workX > JoyContainer.Width - Width)
                workX = JoyContainer.Width - Width;
            TranslationX = workX;

            double workY = _initialJoyPositionY + (y - _InitialTouchY) * 0.35;
            if (workY < 0)
                workY = 0;
            else if (workY > JoyContainer.Height - Height)
                workY = JoyContainer.Height - Height;
            TranslationY = workY;

            if (Test1 != null)
                Test1.Execute(new Point(XPosition, YPosition));
        }

        private void FillPositions()
        {
            double logicPositionXMax = JoyContainer.Width - _initialJoyPositionX - Width;

            if (TranslationX - _initialJoyPositionX > 0)
            {
                _xPosition = MaxX / (logicPositionXMax / (TranslationX - _initialJoyPositionX));
            }
            else if (TranslationX - _initialJoyPositionX < 0)
            {
                _xPosition = -MaxX / (logicPositionXMax / (TranslationX - _initialJoyPositionX));
            }
            else
            {
                _xPosition = 0;
            }

            double logicPositionYMax = JoyContainer.Height - _initialJoyPositionY - Height;

            if (TranslationY - _initialJoyPositionY > 0)
            {
                _yPosition = MaxY / (logicPositionYMax / (TranslationY - _initialJoyPositionY));
            }
            else if (TranslationY - _initialJoyPositionY < 0)
            {
                _yPosition = -MaxY / (logicPositionYMax / (TranslationY - _initialJoyPositionY));
            }
            else
            {
                _yPosition = 0;
            }
        }

        private void CenterJoy()
        {
            TranslationX = (JoyContainer.WidthRequest / 2) - (WidthRequest / 2);
            TranslationY = (JoyContainer.HeightRequest / 2) - (HeightRequest / 2);

            _initialJoyPositionX = TranslationX;
            _initialJoyPositionY = TranslationY;

            if (Test1 != null)
                Test1.Execute(null);
        }

        #endregion
    }

    public enum TouchStatus
    {
        None,
        TouchDown,
        TouchUp,
        TouchMove
    }
}
