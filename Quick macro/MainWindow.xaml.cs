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
using HenoohDeviceEmulator.Native;

namespace Quick_macro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyboardListener kbListener;
        inputProcessor iProcessor;
       

        public MainWindow()
        {
            InitializeComponent();
            stop_listen.IsEnabled = false;
           
        }

        private void keyDownEvent(object sender, RawKeyEventArgs args)
        {
            keyList.Items.Add(args.Key.ToString());
            iProcessor.processInput(args.Key.ToString());
        }

        private void Start_Listen_Click(object sender, RoutedEventArgs e)
        {
            Start_Listen.IsEnabled = false;
            stop_listen.IsEnabled = true;
            kbListener.KeyDown += keyDownEvent;
        }

        private void stop_listen_Click(object sender, RoutedEventArgs e)
        {
            Start_Listen.IsEnabled = true;
            stop_listen.IsEnabled = false;
            cleanUp();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            kbListener = new KeyboardListener();
            iProcessor = new inputProcessor();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            cleanUp();
            kbListener = null;
            
        }

        private void cleanUp()
        {
            kbListener.KeyDown -= keyDownEvent;
            keyList.Items.Clear();

        }

        

    }
}
