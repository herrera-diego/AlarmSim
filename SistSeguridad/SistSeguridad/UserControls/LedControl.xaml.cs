using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SistSeguridad.UserControls
{
    /// <summary>
    /// Interaction logic for Leds.xaml
    /// </summary>
    public partial class LedControl : UserControl
    {
        private bool _enableStatus = false;

        public LedControl()
        {
            InitializeComponent();
        }


        public void LedOn()
        {
            RadialGradientBrush brush = new RadialGradientBrush(new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(0,128,0),0.2),
                        new GradientStop(Color.FromRgb(173,255,47),0.8)

                    });

            Bulb.Fill = brush;
        }

        public void LedOff()
        {
            RadialGradientBrush brush = new RadialGradientBrush(new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(0,128,0),0.4),
                        new GradientStop(Color.FromRgb(173,255,47),0)

                    });

            Bulb.Fill = brush;
        }
    }
}
