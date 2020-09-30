using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace Test
{
    public partial class Window5 : Window
    {
        int a = 0;
        int correct = 0;
        int corect = 0;
        public Window5()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (correct == 0 && corect==0)
            {
                if (Author.Text == "")
                {
                    if (Names.Text == "")
                    {
                        if (Qestions.Text == "")
                        {
                            TexT.Text = "Укажите название, автора и кол-во вопросов";
                        }
                        else TexT.Text = "Укажите название и автора";
                    }
                    else if (Qestions.Text == "") TexT.Text = "Укажите автора  и кол-во вопросов"; else TexT.Text = "Укажите автора";
                }
                else if (Names.Text == "")
                {
                    if (Qestions.Text == "") TexT.Text = "Укажите название и кол-во вопросов";else TexT.Text = "Укажите название";
                }
                else if(Qestions.Text == "") { TexT.Text = "Укажите кол-во вопросов"; } else { Window8 win = new Window8(Names.Text,Author.Text,Qestions.Text); win.Show();Close(); }

            }
            else { TexT.Text = "Некорректное значение поля \"кол-во вопросов\""; }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        { 
                var textBox = sender as TextBox;

                try

                {
                Convert.ToInt32(textBox.Text);
                correct = 0;
                textBox.Background = Brushes.White;
                TexT.Text = null;
                if (a == 0)
                {
                    a = 1;
                    Names_TextChanged(Names, e);
                }
                a = 0;
                }

                catch
                {
                if (textBox.Text != "")
                {
                    textBox.Background = Brushes.OrangeRed;
                    correct = 1;
                    if (!TexT.Text.Contains("Некорректное значение поля \"кол-во вопросов\""))
                    {
                        TexT.Text += "\nНекорректное значение поля \"кол-во вопросов\"";
                    }
                }
                else
                {
                    correct = 0;
                    textBox.Background = Brushes.White;
                    TexT.Text = null;
                    if (a == 0)
                    {
                        a = 1;
                        Names_TextChanged(Names, e);
                    }
                    a = 0;
                }
            }
       
        }

        private void Names_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            XDocument doc = XDocument.Load(@"XMLBAS.xml");
            List<string> names = new List<string>();
            foreach (var testElement in doc.Root.Elements())
            {
                names.Add(testElement.Attribute("Name").Value);
            }
            bool coint=false;
            foreach (string a in names)
            {
                if (a.Equals(textBox.Text)) { coint = true; break; }
            }
            if(coint)
            {
                corect = 1;
                textBox.Background = Brushes.OrangeRed;
                if (!TexT.Text.Contains("Данное имя теста уже занято"))
                {
                    TexT.Text += "\n Данное имя теста уже занято";
                }
                coint = false;
            }
            else
            {
                corect = 0;
                textBox.Background = Brushes.White;
                TexT.Text = null;
                if (a == 0)
                {
                    a = 1;
                    TextBox_TextChanged(Qestions, e);
                }
                a = 0;
            }

        }
    }
}
