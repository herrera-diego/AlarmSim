using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistSeguridad.Simulator
{
    /// <summary>
    /// Interaction logic for CallDisplay.xaml
    /// </summary>
    public partial class CallDisplay : UserControl
    {
        public CallDisplay()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextBlockTextProperty =
           DependencyProperty.Register("CallInfo", typeof(string), typeof(CallDisplay), new UIPropertyMetadata(""));

        public string CallInfo
        {
            get { return (string)GetValue(TextBlockTextProperty); }
            set { SetValue(TextBlockTextProperty, value); }
        }
    }

    public class CallEventArgs : EventArgs
    {
        public CallEventArgs()
        {
            
        }

        public string UserNumber
        {
            set; get;
        }

        public string CompanyPhoneNumber
        {
            get; set;
        }

        public bool Fire
        {
            get; set;
        }

        public bool Panic
        {
            get; set;
        }

        public bool Alarm
        {
            get; set;
        }

        public string SensorName
        {
            get; set;
        }

        public string Zone
        {
            get; set;
        }




    }
}
