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
    public class CustomControlTestAndroid : ViewRenderer//, Android.Views.View.IOnTouchListener
    {
        public CustomControlTestAndroid()
        {
            SetOnTouchListener(this);
        }

        //public bool OnTouch(Android.Views.View v, MotionEvent e)
        //{

        //    int[] viewCoords = new int[2];
        //    int[] viewCoords2 = new int[2];
        //    GetLocationOnScreen(viewCoords);
        //    GetLocationInWindow(viewCoords2);
        //    int touchX = (int)e.RawX;
        //    int touchY = (int)e.RawY;

        //    int positionOnViewX = touchX - viewCoords[0];
        //    int positionOnViewY = touchY - viewCoords[1];

        //    return true;
        //}


        public override bool OnTouchEvent(MotionEvent e)
        {
            var xamarinControl = Element as CustomControlTest;

            TouchStatus touchStatus = TouchStatus.None;

            //int[] viewCoords = new int[2];
            //GetLocationOnScreen(viewCoords);

            //int touchX = (int)e.RawX;
            //int touchY = (int)e.RawY;

            //int positionOnViewX = touchX - viewCoords[0];
            //int positionOnViewY = touchY - viewCoords[1];

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
                xamarinControl.OnTouchEvent(touchStatus, e.GetPointerId(e.ActionIndex), (int)e.RawX, (int)e.RawY);
            }
            catch (Exception)
            {
            }

            return true;
        }

    }
}