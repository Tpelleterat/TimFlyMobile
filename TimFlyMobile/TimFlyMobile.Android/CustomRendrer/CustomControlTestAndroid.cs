using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TimFlyMobile.Constrols;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using TimFlyMobile.Droid.CustomRendrer;

[assembly: ExportRenderer(typeof(CustomControlTest), typeof(CustomControlTestAndroid))]
namespace TimFlyMobile.Droid.CustomRendrer
{
    public class CustomControlTestAndroid : ViewRenderer
    {

        public override bool OnTouchEvent(MotionEvent e)
        {
            var xamarinControl = Element as CustomControlTest;

            TouchStatus touchStatus = TouchStatus.None;

            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                case MotionEventActions.PointerDown:
                    touchStatus = TouchStatus.TouchDown;
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.PointerUp:
                    touchStatus = TouchStatus.TouchUp;
                    break;
                case MotionEventActions.Move:
                    touchStatus = TouchStatus.TouchMove;
                    break;
            }

            try
            {
                xamarinControl.OnTouchEvent(touchStatus, e.GetPointerId(e.ActionIndex), e.GetX(e.ActionIndex), e.GetY(e.ActionIndex));
            }
            catch (Exception)
            {
            }

            return true;
        }



    }
}