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
    /// Interaction logic for ButtonPanel.xaml
    /// </summary>
    public partial class ButtonPanel : UserControl
    {
        public ButtonPanel()
        {
            InitializeComponent();
        }


        public Memory SharedMemory
        {
            get;set;
        }

        public bool Enter
        {
            get;set;
        }

        public bool Escape
        {
            get; set;
        }

        public bool Panic
        {
            get; set;
        }

        public bool Fire
        {
            get; set;
        }

        public bool ButtonPressed
        {
            get; set;
        }

        private void NumberClick(object sender, RoutedEventArgs e)
        {
            var source = e.OriginalSource as Button;

            if (source == null)
                return;

            SharedMemory.CurrentMessage += source.Content;
            ButtonPressed = true;
        }

        private void EnterClick(object sender, RoutedEventArgs e)
        {
            Enter = true;
        }

        private void EscapeClick(object sender, RoutedEventArgs e)
        {
            Escape = true;
        }

        private void PanicClick(object sender, RoutedEventArgs e)
        {
            Panic = true;
        }
        private void FireClick(object sender, RoutedEventArgs e)
        {
            Fire = true;
        }
    }
}
