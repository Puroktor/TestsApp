using System.Windows;
using WpfApplication1;

namespace Test
{
    public partial class Window8 : Window
    {
        Taask task;
        private string qestionsAll;
        private string name;
        private string author;

        public Window8(Taask task)
        {
            this.task = task;
            Loaded += Window8_Loaded; 
            InitializeComponent();
            Escape.Content = "Назад";
            ForUser.Text = "Введите ключ редактирования:";
        }

        private void Window8_Loaded(object sender, RoutedEventArgs e)
        {
            if (task.password == "") { Window9 w = new Window9(task); w.Show(); Close(); }
        }

        public Window8(string name, string author, string qestionsAll)
        {
            InitializeComponent();
            Escape.Content = "Пропустить";
            ForUser.Text = "Придумайте ключ редактирования";
            this.name = name;
            this.author = author;
            this.qestionsAll = qestionsAll;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((string)Escape.Content) {
                case "Назад":
                    if (TextGetter.Text == task.password) { Window9 w = new Window9(task); w.Show(); Close(); }
                    else Erorr.Text = "Неверный ключ";
                    break;
                case "Пропустить":
                    if (TextGetter.Text == "") Erorr.Text = "Пустой ключ";
                    else { Window6 w = new Window6(name, author, qestionsAll, TextGetter.Text); w.Show(); Close(); }
                    break;
            }
        }

        private void Escape_Click(object sender, RoutedEventArgs e)
        {
            switch ((string)Escape.Content)
            {
                case "Назад":
                    Window1 w = new Window1();
                    w.Show();
                    Close();
                    break;
                case "Пропустить":
                    Window6 w6 = new Window6(name, author, qestionsAll,""); w6.Show(); Close();
                    break;
            }
        }
    }
}
