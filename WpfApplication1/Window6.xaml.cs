using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Xml;

namespace Test
{
    public partial class Window6 : Window
    {
        private int qestionNumber;
        private string qestionsAll;
        private string name;
        private string author;
        private string password;
        private List<string> tasks; 
        private List<string> answers; 
        private List<string> pictures;
        private List<string> lV1;
        private List<string> lV2;
        private List<string> lV3;
        private List<string> lV4;
        OpenFileDialog fd;
        int corect = 0;
        Stream stream;

        public Window6(string name, string author, string qestionsAll,string password)
        {
            InitializeComponent();
            qestionNumber = 1;
            this.name = name;
            this.password = password;
            this.author = author;
            this.qestionsAll = qestionsAll;
            tasks = new List<string>();
            pictures = new List<string>();
            answers = new List<string>();
            lV1 = new List<string>();
            lV2 = new List<string>();
            lV3 = new List<string>();
            lV4 = new List<string>();
            QestionNumber.Text = string.Format("ВОПРОС №{0}", this.qestionNumber);
            fd = new OpenFileDialog();
        }

        public Window6(string name, string author, string qestionsAll, List<string> tasks, List<string> answers, List<string> pictures, int qestionNumber,string password, List<string> lV1, List<string> lV2, List<string> lV3, List<string> lV4)
        {
            InitializeComponent();
            this.password = password;
            this.name = name;
            this.author = author;
            this.qestionsAll = qestionsAll;
            this.tasks = new List<string>();
            this.tasks.AddRange(tasks);
            this.pictures = new List<string>();
            this.pictures.AddRange(pictures);
            this.answers = new List<string>();
            this.answers.AddRange(answers);
            this.lV1 = new List<string>();
            this.lV2 = new List<string>();
            this.lV3 = new List<string>();
            this.lV4 = new List<string>();
            this.lV1.AddRange(lV1);
            this.lV2.AddRange(lV2);
            this.lV3.AddRange(lV3);
            this.lV4.AddRange(lV4);
            this.qestionNumber = qestionNumber + 1;
            QestionNumber.Text = string.Format("ВОПРОС №{0}", this.qestionNumber);
            fd = new OpenFileDialog();
        }

        private void Ending()
        {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(@"XMLBAS.xml");
                XmlElement testElement = xdoc.CreateElement("test");
                testElement.SetAttribute("Name", name);
                testElement.SetAttribute("Author", author);
                testElement.SetAttribute("Qestions",qestionsAll);
                testElement.SetAttribute("pas", password);
                foreach (string a in tasks)
                {
                    XmlElement qestionElememnt = xdoc.CreateElement("question");
                    qestionElememnt.SetAttribute("qes", a);
                    qestionElememnt.SetAttribute("ans", answers[tasks.IndexOf(a)]);
                    qestionElememnt.SetAttribute("pic", pictures[tasks.IndexOf(a)]);
                    qestionElememnt.SetAttribute("v1", lV1[tasks.IndexOf(a)]);
                    qestionElememnt.SetAttribute("v2", lV2[tasks.IndexOf(a)]);
                    qestionElememnt.SetAttribute("v3", lV3[tasks.IndexOf(a)]);
                    qestionElememnt.SetAttribute("v4", lV4[tasks.IndexOf(a)]);
                    testElement.AppendChild(qestionElememnt);
                }
                XmlElement el = xdoc.DocumentElement;
                el.AppendChild(testElement);
                xdoc.Save("XMLBAS.xml");

                Window7 w = new Window7();
                w.Show();
              
                this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                fd.Filter = "Pictures|*.png ; *.jpg; *.bmp; *.jpeg";
                fd.ShowDialog();
                var button = sender as Button;
            if(fd.FileName != "") {
                switch (Convert.ToString(button.Content))
                {
                    case "Добавить изображение":
                        button.Visibility = Visibility.Collapsed;
                        Buton.Visibility = Visibility.Visible;
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
                    if (v1.IsChecked == true) answers.Add(V1.Text);
                    else if (v2.IsChecked == true) answers.Add(V2.Text);
                    else if (v3.IsChecked == true) answers.Add(V3.Text);
                    else if (v4.IsChecked == true) answers.Add(V4.Text);
                    lV1.Add(V1.Text);
                    lV2.Add(V2.Text);
                    lV3.Add(V3.Text);
                    lV4.Add(V4.Text);
                    tasks.Add(task.Text);
                    try { stream.Dispose(); } catch { }
                    try
                    {
                        FileInfo f = new FileInfo(fd.FileName);
                        DirectoryInfo dir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\Data\\" + name);
                        try
                        {
                            f.CopyTo(System.AppDomain.CurrentDomain.BaseDirectory + "\\Data\\" + name + "\\" + qestionNumber + f.Extension);
                        }
                        catch { dir.Create(); f.CopyTo(System.AppDomain.CurrentDomain.BaseDirectory + "\\Data\\" + name + "\\" + qestionNumber + f.Extension); }
                        pictures.Add(String.Format("\\Data\\" + name + "\\" + qestionNumber + f.Extension));
                    }
                    catch { pictures.Add(""); }

                    if (this.qestionNumber == int.Parse(this.qestionsAll))
                    {
                        Ending();
                    }
                    else
                    {
                        Window6 w = new Window6(name, author, qestionsAll, tasks, answers, pictures, qestionNumber, password, lV1, lV2, lV3, lV4);
                        w.Show();
                        Close();
                    }

                }
                else { if (v1.IsChecked == false && v3.IsChecked == false && v2.IsChecked == false && v4.IsChecked == false) choose.Text = "Выбирете правильный вар. ответа!"; if (task.Text == "") task.Background = Brushes.OrangeRed; if (V1.Text == "") V1.Background = Brushes.OrangeRed; if (V2.Text == "") V2.Background = Brushes.OrangeRed; if (V3.Text == "") V3.Background = Brushes.OrangeRed; if (V4.Text == "") V4.Background = Brushes.OrangeRed; }
            }

        }

        private void task_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool b= false;
            var textBox = sender as TextBox;
            foreach (string a in  tasks)
            {
                if (a.Equals(textBox.Text)) { b = true; break; }
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
    }
}
