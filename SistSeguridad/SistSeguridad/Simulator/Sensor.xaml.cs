﻿using System;
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
    /// Interaction logic for Sensor.xaml
    /// </summary>
    public partial class Sensor : UserControl
    {
        public Sensor()
        {
            InitializeComponent();
            DataContext = this;
            Zona0.IsChecked = true;
            Zona1.IsChecked = false;
        }

        public string SensorName
        {
            get { return (string)GetValue(TextBlockTextProperty); }
            set { SetValue(TextBlockTextProperty, value); }
        }

        public int Zone
        {
            get;set;
        }

        public bool Alarm
        {
            get;set;
        }


        public static readonly DependencyProperty TextBlockTextProperty =
            DependencyProperty.Register("SensorName", typeof(string), typeof(Sensor), new UIPropertyMetadata(""));

    }
}