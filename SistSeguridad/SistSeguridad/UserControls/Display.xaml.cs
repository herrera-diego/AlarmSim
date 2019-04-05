using SistSeguridad.DataHandling;
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

namespace SistSeguridad.UserControls
{
    /// <summary>
    /// Interaction logic for Display.xaml
    /// </summary>
    public partial class Display : UserControl
    {
        public Display()
        {
            InitializeComponent();
            Clear();
        }

        public Memory SharedMemory
        {
            get;set;
        }

        public void Show()
        {
            MainDisplay.Text = SharedMemory.CurrentMessage; 
        }

        public void LightUp()
        {
            MainDisplay.Text = SharedMemory.CurrentMessage;
        }

        public void Clear()
        {
            MainDisplay.Text = string.Empty;
        }
    }
}
