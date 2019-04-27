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
            Panic = false;
            Fire = false;
            InitializeComponent();
            this.DataContext = this;

        }

        public static readonly DependencyProperty CompanyPhoneProperty =
           DependencyProperty.Register("CompanyPhone", typeof(string), typeof(CallDisplay), new UIPropertyMetadata(""));

        public string CompanyPhone
        {
            get { return (string)GetValue(CompanyPhoneProperty); }
            set { SetValue(CompanyPhoneProperty, value); }
        }

        public static readonly DependencyProperty UserNumberProperty =
          DependencyProperty.Register("UserNumber", typeof(string), typeof(CallDisplay), new UIPropertyMetadata(""));

        public string UserNumber
        {
            get { return (string)GetValue(UserNumberProperty); }
            set { SetValue(UserNumberProperty, value); }
        }

        public static readonly DependencyProperty SensorNameProperty =
          DependencyProperty.Register("SensorName", typeof(string), typeof(CallDisplay), new UIPropertyMetadata(""));

        public string SensorName
        {
            get { return (string)GetValue(SensorNameProperty); }
            set { SetValue(SensorNameProperty, value); }
        }

        public static readonly DependencyProperty ZoneProperty =
          DependencyProperty.Register("Zone", typeof(string), typeof(CallDisplay), new UIPropertyMetadata(""));

        public string Zone
        {
            get { return (string)GetValue(ZoneProperty); }
            set { SetValue(ZoneProperty, value); }
        }

        public static readonly DependencyProperty PanicProperty =
          DependencyProperty.Register("Panic", typeof(bool), typeof(CallDisplay), new UIPropertyMetadata(false));

        public bool Panic
        {
            get { return (bool)GetValue(PanicProperty); }
            set { SetValue(PanicProperty, value); }
        }

        public static readonly DependencyProperty FireProperty =
          DependencyProperty.Register("Fire", typeof(bool), typeof(CallDisplay), new UIPropertyMetadata(false));

        public bool Fire
        {
            get { return (bool)GetValue(FireProperty); }
            set { SetValue(FireProperty, value); }
        }
    }

    public class CallEventArgs : EventArgs
    {
        public CallEventArgs()
        {
            
        }

        public CallInfo Information
        { get; set; }
    }

    public class CallInfo
    {
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
