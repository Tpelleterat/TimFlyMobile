using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TimFlyMobile.Constrols
{
    public class PanView : View
    {
        public View JoyView;

        public double ParentWidth
        {
            get
            {
                return _parentWidth;
            }
            set
            {
                _parentWidth = value;
            }
        }
        private double _parentWidth;

        public double ParentHeight
        {
            get
            {
                return _parentHeight;
            }
            set
            {
                _parentHeight = value;
            }
        }
        private double _parentHeight;

        public PanView()
        {
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            panGesture.TouchPoints = 1;
            GestureRecognizers.Add(panGesture);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            this.TranslationX = (ParentWidth / 2) - (WidthRequest / 2);
            this.TranslationY = (ParentHeight / 2) - (HeightRequest / 2);

            CenterJoyView();
        }

        private void CenterJoyView()
        {
            JoyView.TranslationX = TranslationX;
            JoyView.TranslationY = TranslationY;
        }
        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    break;
                case GestureStatus.Running:
                    if (true)
                    {
                        double workX = TranslationX + e.TotalX;
                        if (workX < 0)
                            workX = 0;
                        if (workX > ParentWidth - WidthRequest)
                            workX = ParentWidth - WidthRequest;
                        double workY = TranslationY + e.TotalY;
                        if (workY < 0)
                            workY = 0;
                        if (workY > ParentHeight - HeightRequest)
                            workY = ParentHeight - HeightRequest;

                        JoyView.TranslationX = workX;
                        JoyView.TranslationY = workY;
                    }
                    else
                    {
                        CenterJoyView();
                    }
                    break;
                case GestureStatus.Completed:
                    CenterJoyView();
                    break;
            }
        }
    }
}
