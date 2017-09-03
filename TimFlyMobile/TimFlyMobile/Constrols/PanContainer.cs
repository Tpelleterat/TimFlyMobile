using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TimFlyMobile.Constrols
{
    public class PanContainer : ContentView
    {
        double x, y;
        public View JoyView;


        public PanContainer()
        {
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            CenterJoyView();
        }

        private void CenterJoyView()
        {
            if (JoyView != null)
            {
                JoyView.TranslationX = (this.WidthRequest / 2) - (JoyView.WidthRequest / 2);
                JoyView.TranslationY = (this.HeightRequest / 2) - (JoyView.HeightRequest / 2);

                x = JoyView.TranslationX;
                y = JoyView.TranslationY;
            }
        }



        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:


                case GestureStatus.Running:
                    if (JoyView != null)
                    {
                        double workX = x + e.TotalX;
                        if (workX < 0)
                            workX = 0;
                        if (workX > this.WidthRequest - JoyView.WidthRequest)
                            workX = this.WidthRequest - JoyView.WidthRequest;
                        double workY = y + e.TotalY;
                        if (workY < 0)
                            workY = 0;
                        if (workY > this.HeightRequest - JoyView.HeightRequest)
                            workY = this.HeightRequest - JoyView.HeightRequest;

                        JoyView.TranslationX = workX;
                        JoyView.TranslationY = workY;
                    }
                    break;
                case GestureStatus.Completed:
                    CenterJoyView();
                    break;
            }
        }
    }
}
