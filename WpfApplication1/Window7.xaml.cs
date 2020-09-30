using System.Windows;
using WpfApplication1;

namespace Test
{
    public partial class Window7 : Window
    {
        public Window7()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            w.Show();
            Close();
        }
    }
}
