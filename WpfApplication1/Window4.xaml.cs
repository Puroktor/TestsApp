using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApplication1;

namespace Test
{
    public partial class Window4 : Window
    {
        int rights=0;
        int completed=0;
        Taask task;

        public Window4(Taask task)
        {
            InitializeComponent();
            Loaded += Window4_Loaded;
            this.task = task;
        }

        private void Window4_Loaded(object sender, RoutedEventArgs e)
        {
            TextRefresher();
            Name.Text = task.name;
            ObservableCollection<Taask> tasks = new ObservableCollection<Taask>();
            foreach (string a in task.qestion)
            {
                tasks.Add(new Taask { qestions = a, height = 0.5 * task.height, width = Width * 0.2, picture = task.pictures[task.qestion.IndexOf(a)], v1 = task.V1[task.qestion.IndexOf(a)], v2 = task.V2[task.qestion.IndexOf(a)], v3 = task.V3[task.qestion.IndexOf(a)], v4 = task.V4[task.qestion.IndexOf(a)] });
            }
            List.ItemsSource = tasks;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stack = (StackPanel)button.Parent;
            Grid grid = (Grid)stack.Parent;
            UIElementCollection colection = grid.Children;
            TextBlock text = (TextBlock)colection[0];
            if (task.answers[task.qestion.IndexOf(text.Text)] == (string)button.Content) { rights++; completed++; } else completed++;
            grid.IsEnabled = false;
            grid.Background = Brushes.LightGray;
            TextRefresher();
        }

        private void TextRefresher()
        {
            quanty.Text = completed + "/" + task.qestions;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window3 win = new Window3(Convert.ToDouble(rights),Convert.ToDouble(task.qestions), task.name);
            win.Show();
            Close();
        }
    }
}
