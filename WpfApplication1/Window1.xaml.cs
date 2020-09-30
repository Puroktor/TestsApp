using System.Collections.Generic;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Collections.ObjectModel;
using Test;
using System.IO;

namespace WpfApplication1
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Loaded += Window1_Loaded;
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            rehim.SelectedItem = testirovanie;
            XmlTextReader reader = new XmlTextReader(@"XMLBAS.xml");
            double width = 0.557 * Width;
            double height = 0.2 * Height;
            ObservableCollection<Taask> tasks = new ObservableCollection<Taask>();
            while (reader.Read())
            {
                if (reader.LocalName == "test")
                {
                    string a = reader.GetAttribute("Name");
                    string b = reader.GetAttribute("Author");
                    string c = reader.GetAttribute("Qestions");
                    if(a!=null && b!=null && c!=null)
                        tasks.Add(new Taask { name = a, author = b, qestions = c, width = width, height = height });     
                }
            }
            
            List.ItemsSource = tasks;
            reader.Close();
        }
        
        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                ListBox list = (ListBox)sender;
                Taask task = (Taask)list.SelectedItem;
                task.qestion = new List<string>();
                task.answers = new List<string>();
                task.pictures = new List<string>();
                task.V1 = new List<string>();
                task.V2 = new List<string>();
                task.V3 = new List<string>();
                task.V4 = new List<string>();
                XDocument doc = XDocument.Load(@"XMLBAS.xml");
                foreach (var testElement in doc.Root.Elements())
                {
                    if (testElement.Attribute("Name").Value == task.name && testElement.Attribute("Author").Value == task.author && testElement.Attribute("Qestions").Value == task.qestions)
                    {
                    task.password = testElement.Attribute("pas").Value;
                        foreach (var questionElement in testElement.Elements())
                        {
                            task.answers.Add(questionElement.Attribute("ans").Value);
                            task.qestion.Add(questionElement.Attribute("qes").Value);
                            task.V1.Add(questionElement.Attribute("v1").Value);
                            task.V2.Add(questionElement.Attribute("v2").Value);
                            task.V3.Add(questionElement.Attribute("v3").Value);
                            task.V4.Add(questionElement.Attribute("v4").Value);
                        task.pictures.Add(Directory.GetCurrentDirectory() + questionElement.Attribute("pic").Value);
                        }
                    }
                }

            if (rehim.SelectedItem!=testirovanie)
            {
                Window8 w = new Window8(task);
                w.Show();
                Close();
            }

            else
            {
                Window4 win = new Window4(task);
                win.Show();
                Close();
            }
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window5 w = new Window5();
            w.Show();
            Close();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            grid.Background = (LinearGradientBrush)grid.FindResource("NLeftBac");
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            grid.Background = (LinearGradientBrush)grid.FindResource("LeftBac");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

      