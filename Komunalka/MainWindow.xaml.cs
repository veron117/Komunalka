using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace Komunalka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> _dataxml;
        public MainWindow()
        {
            InitializeComponent();
            var data = new XmlData();
            dataXml.ItemsSource = data.RefresheData();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Addform addform = new Addform
            {
                Owner = this,
                ShowInTaskbar = true
            };
            addform.ShowDialog();
            var data = new XmlData();
            dataXml.ItemsSource = data.RefresheData();
        }
        private void dataXml_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                hide.Text = dataXml.SelectedItem.ToString();
                if (hide.Text != "")
                {
                    PrintForm printform = new PrintForm();
                    printform.Owner = this;
                    printform.ShowInTaskbar = true;
                    printform.Show();
                    dataXml.UnselectAll();
                }
            }
            catch { }
        }
    }
}
