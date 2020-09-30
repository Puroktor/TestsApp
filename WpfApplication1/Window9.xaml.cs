using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;
using WpfApplication1;

namespace Test
{
    public partial class Window9 : Window
    {
        private int qestionNumber;
        private string qestionsAll;
        private Taask oldTask;
        private Taask tasks;
        OpenFileDialog fd;
        int corect = 0;
        Stream stream;

        public Window9(Taask task)
        {
            tasks = new Taask();
            oldTask = task;
            tasks.author = task.author;
            tasks.name = task.name;
            InitializeComponent();
            qestionNumber = 1;
            qestionsAll = task.qestions;
            tasks.qestion = new List<string>();
            tasks.answers = new List<string>();
            tasks.pictures = new List<string>();
            tasks.V1 = new List<string>();
            tasks.V2 = new List<string>();
            tasks.V3 = new List<string>();
            tasks.V4 = new List<string>();
            Initial(task);
        }

        public Window9(Taask task,Taask oldTask, int qestionNumber)
        {
            InitializeComponent();
            this.tasks = task;
            this.oldTask = oldTask;
            this.qestionNumber = qestionNumber+1;
            Initial(oldTask);
        }

        public void Initial(Taask task)
        {
            qestionsAll = task.qestions;
            fd = new OpenFileDialog();
            QestionNumber.Text = string.Format("ВОПРОС №{0}", this.qestionNumber);
            try
            {
                this.task.Text = task.qestion[qestionNumber - 1];
                V1.Text = task.V1[qestionNumber - 1];
                V2.Text = task.V2[qestionNumber - 1];
                V3.Text = task.V3[qestionNumber - 1];
                V4.Text = task.V4[qestionNumber - 1];
                if (V1.Text == task.answers[qestionNumber - 1]) v1.IsChecked = true;
                else if (V2.Text == task.answers[qestionNumber - 1]) v2.IsChecked = true;
                else if (V3.Text == task.answers[qestionNumber - 1]) v3.IsChecked = true;
                else if (V4.Text == task.answers[qestionNumber - 1]) v4.IsChecked = true;
                if (task.pictures[qestionNumber - 1] != "")
                {
                    BitmapImage bi = new BitmapImage();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    stream = File.Open(task.pictures[qestionNumber - 1], FileMode.Open);
                    bi.BeginInit();
                    bi.StreamSource = stream;
                    bi.EndInit();
                    Imagee.Source = bi;
                    AddButton.Visibility = Visibility.Collapsed;
                    Buton.Visibility = Visibility.Visible;
                }
            }
            catch { }
        }

        private void Ending()
        {
            if (tasks.qestions != "0")
            {
                XDocument xdocum = XDocument.Load("XMLBAS.xml");
                foreach (XElement TestElement in xdocum.Root.Elements())
                {
                    if (TestElement.Attribute("Name").Value == tasks.name && TestElement.Attribute("Author").Value == tasks.author) TestElement.Remove();
                }
                xdocum.Save("XMLBAS.xml");

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(@"XMLBAS.xml");
                XmlElement testElement = xdoc.CreateElement("test");
                testElement.SetAttribute("Name", tasks.name);
                testElement.SetAttribute("Author", tasks.author);
                testElement.SetAttribute("Qestions", qestionsAll);
                testElement.SetAttribute("pas", tasks.password);
                foreach (string a in tasks.qestion)
                {
                    XmlElement qestionElememnt = xdoc.CreateElement("question");
                    qestionElememnt.SetAttribute("qes", a);
                    qestionElememnt.SetAttribute("ans", tasks.answers[tasks.qestion.IndexOf(a)]);
                    qestionElememnt.SetAttribute("pic", tasks.pictures[tasks.qestion.IndexOf(a)]);
                    qestionElememnt.SetAttribute("v1", tasks.V1[tasks.qestion.IndexOf(a)]);
                    qestionElememnt.SetAttribute("v2", tasks.V2[tasks.qestion.IndexOf(a)]);
                    qestionElememnt.SetAttribute("v3", tasks.V3[tasks.qestion.IndexOf(a)]);
                    qestionElememnt.SetAttribute("v4", tasks.V4[tasks.qestion.IndexOf(a)]);
                    testElement.AppendChild(qestionElememnt);
                }
                XmlElement el = xdoc.DocumentElement;
                el.AppendChild(testElement);
                xdoc.Save("XMLBAS.xml");

                Window1 w = new Window1();
                w.Show();

                this.Close();
            }
            else
            {
                XDocument xdocum = XDocument.Load("XMLBAS.xml");
                foreach (XElement TestElement in xdocum.Root.Elements())
                {
                    if (TestElement.Attribute("Name").Value == tasks.name && TestElement.Attribute("Author").Value == tasks.author) TestElement.Remove();
                }
                xdocum.Save("XMLBAS.xml");
                Window1 w = new Window1();
                w.Show();

                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fd.Filter = "Pictures|*.png ; *.jpg; *.bmp; *.jpeg";
            fd.ShowDialog();
            var button = sender as Button;
            if (fd.FileName != "")
            {
                switch (Convert.ToString(button.Content))
                {
                    case "Добавить изображение":
                        button.Visibility = Visibility.Collapsed;
                        Buton.Visibility = Visibility.Visible;
                        try { stream.Dispose(); } catch { }
                        try
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            stream = File.Open(fd.FileName, FileMode.Open);
                            bi.BeginInit();
                            bi.StreamSource = stream;
                            bi.EndInit();
                            Imagee.Source = bi;
                        }
                        catch { }
                        break;
                    default:
                        try { stream.Dispose(); } catch { }
                        try
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            stream = File.Open(fd.FileName, FileMode.Open);
                            bi.BeginInit();
                            bi.StreamSource = stream;
                            bi.EndInit();
                            Imagee.Source = bi;
                        }
                        catch { }
                        break;
                }
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (corect == 0)
            {
                if (V1.Text != "" && V2.Text != "" && V3.Text != "" && V4.Text != "" && task.Text != "" && (v1.IsChecked == true || v3.IsChecked == true || (v2.IsChecked == true) || v4.IsChecked == true))
                {
                    if (v1.IsChecked == true) tasks.answers.Add(V1.Text);
                    else if (v2.IsChecked == true) tasks.answers.Add(V2.Text);
                    else if (v3.IsChecked == true) tasks.answers.Add(V3.Text);
                    else if (v4.IsChecked == true) tasks.answers.Add(V4.Text);
                    tasks.V1.Add(V1.Text);
                    tasks.V2.Add(V2.Text);
                    tasks.V3.Add(V3.Text);
                    tasks.V4.Add(V4.Text);
                    tasks.qestion.Add(task.Text);
                    try { stream.Dispose(); } catch { }

                    try
                    {
                        FileInfo f = new FileInfo(fd.FileName);
                        DirectoryInfo dir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "Data\\" + tasks.name);
                        try
                        {
                            f.CopyTo(AppDomain.CurrentDomain.BaseDirectory + "Data\\" + tasks.name + "\\" + qestionNumber + f.Extension, true);
                        }
                        catch { dir.Create(); f.CopyTo(System.AppDomain.CurrentDomain.BaseDirectory + "Data\\" + tasks.name + "\\" + qestionNumber + f.Extension,true); }
                        tasks.pictures.Add(String.Format("\\Data\\" + tasks.name + "\\" + qestionNumber + f.Extension));
                    }
                    catch { tasks.pictures.Add(""); }

                    if (this.qestionNumber == int.Parse(this.qestionsAll))
                    {
                        Ending();
                    }
                    else
                    {
                        Window9 w = new Window9(tasks,oldTask,qestionNumber);
                        w.Show();
                        Close();
                    }

                }
                else { if (v1.IsChecked == false && v3.IsChecked == false && v2.IsChecked == false && v4.IsChecked == false) choose.Text = "Выбирете правильный вар. ответа!"; if (task.Text == "") task.Background = Brushes.OrangeRed; if (V1.Text == "") V1.Background = Brushes.OrangeRed; if (V2.Text == "") V2.Background = Brushes.OrangeRed; if (V3.Text == "") V3.Background = Brushes.OrangeRed; if (V4.Text == "") V4.Background = Brushes.OrangeRed; }
            }

        }

        private void task_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool b = false;
            var textBox = sender as TextBox;
            if (tasks.qestion != null)
            {
                foreach (string a in tasks.qestion)
                {
                    if (a.Equals(textBox.Text)) { b = true; break; }
                }
            }
            if (b) { textBox.Background = Brushes.OrangeRed; corect = 1; QestionNumber.Text = "Придумайте другое задание!"; QestionNumber.Foreground = Brushes.Red; } else { textBox.Background = Brushes.White; corect = 0; QestionNumber.Text = string.Format("ВОПРОС №{0}", this.qestionNumber); QestionNumber.Foreground = Brushes.Black; }
        }

        private void ans_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t.Text != "") { t.Background = Brushes.White; } else { t.Background = Brushes.OrangeRed; }
        }

        private void v4_Click(object sender, RoutedEventArgs e)
        {
            RadioButton b = sender as RadioButton;
            choose.Text = "";  
                switch (b.Name)
                {
                    case "v1":
                        if (v2.IsChecked == true || v3.IsChecked == true || v4.IsChecked == true) b.IsChecked = false;
                        break;
                    case "v2":
                        if (v1.IsChecked == true || v3.IsChecked == true || v4.IsChecked == true) b.IsChecked = false;
                        break;
                    case "v3":
                        if (v2.IsChecked == true || v1.IsChecked == true || v4.IsChecked == true) b.IsChecked = false;
                        break;
                    case "v4":
                        if (v2.IsChecked == true || v3.IsChecked == true || v1.IsChecked == true) b.IsChecked = false;
                        break;
                
                }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            qestionsAll= Convert.ToString(int.Parse(qestionsAll)+1);
            tasks.qestions = qestionsAll;
            oldTask.qestions = qestionsAll;
            try { adedeQestions.Text = "+" + Convert.ToString(int.Parse(adedeQestions.Text) + 1); } catch { adedeQestions.Text = "+" + Convert.ToString(1); }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            XDocument xdocum = XDocument.Load("XMLBAS.xml");
            foreach (XElement TestElement in xdocum.Root.Elements())
            {
                if (TestElement.Attribute("Name").Value == tasks.name && TestElement.Attribute("Author").Value == tasks.author) TestElement.Remove();
            }
            xdocum.Save("XMLBAS.xml");
            Window1 w = new Window1();
            w.Show();
            Close();
         }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try { stream.Dispose(); } catch { }
            try
            {
                oldTask.answers.RemoveAt(qestionNumber - 1);
                oldTask.V1.RemoveAt(qestionNumber - 1);
                oldTask.V2.RemoveAt(qestionNumber - 1);
                oldTask.V3.RemoveAt(qestionNumber - 1);
                oldTask.V4.RemoveAt(qestionNumber - 1);
                oldTask.qestion.RemoveAt(qestionNumber - 1);
                oldTask.pictures.RemoveAt(qestionNumber - 1);
            }
            catch { }
            oldTask.qestions = Convert.ToString(int.Parse(qestionsAll) - 1);
            tasks.qestions = Convert.ToString(int.Parse(qestionsAll) - 1);
            qestionNumber--;
            qestionsAll = Convert.ToString(int.Parse(qestionsAll) - 1);
            if (this.qestionNumber == int.Parse(this.qestionsAll))
            {
                Ending();
            }
            else
            {
                Window9 w = new Window9(tasks, oldTask, qestionNumber);
                w.Show();
                Close();
            }
        }
    }   
}