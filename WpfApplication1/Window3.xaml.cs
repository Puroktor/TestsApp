using System;
using System.IO;
using System.Windows;

namespace WpfApplication1
{
    public partial class Window3 : Window
    {
        string name;
        double b;

        public Window3(double right, double a, string c)
        {
            InitializeComponent();
            b = (right/a)*100;
            Text.Text=Convert.ToString(b)+"%";
            name = c;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("Results.txt",true);
            sw.WriteLine(DateTime.Now+" "+name+" "+b+"%");
            sw.Close();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            w.Show();
            Close();
        }
    }
}
