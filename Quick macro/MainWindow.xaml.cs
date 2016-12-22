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
using System.Diagnostics;
using System.Runtime.InteropServices;
using HenoohDeviceEmulator;

namespace Quick_macro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyboardListener test;

        public MainWindow()
        {
            InitializeComponent();
            stop_listen.IsEnabled = false;
        }

        private void Test_KeyDown(object sender, RawKeyEventArgs args)
        {
           // MessageBox.Show("key {0} pressed", args.Key.ToString());
            keyList.Items.Add(args.Key.ToString());
           

        }

        private void Start_Listen_Click(object sender, RoutedEventArgs e)
        {
            Start_Listen.IsEnabled = false;
            stop_listen.IsEnabled = true;
            
            test.KeyDown += Test_KeyDown;
        }

        private void stop_listen_Click(object sender, RoutedEventArgs e)
        {
            Start_Listen.IsEnabled = true;
            stop_listen.IsEnabled = false;

            cleanUp();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            test = new KeyboardListener();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            cleanUp();
        }

        private void cleanUp()
        {
            test.KeyDown -= Test_KeyDown;
            test = null;
        }

        public static void sendKeys(string key)
        {
            KeyboardController kb = new KeyboardController();
            kb.TypeString(key);
        }




    }
}
