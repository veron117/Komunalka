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
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Data;
using System.Globalization;
using System.Xml;

namespace Komunalka
{
    /// <summary>
    /// Логика взаимодействия для addform.xaml
    /// </summary>
    public partial class addform : Window
    {
        #region Public members

        public DateTime? PeriodDate { get; set; }

        #endregion

        #region Private members

        private double count, price, sum;

        #endregion

        #region Constructor

        public addform()
        {
            InitializeComponent();
            DataContext = this;
        }

        #endregion

        #region Methods

        private void Text_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Double x;

                KeyConverter conv = new KeyConverter();
                string key = conv.ConvertToString(e.Key).Replace("NumPad", "");
                if (key == "OemPeriod" || key == "OemComma" || key == "Decimal")
                {
                    key = "0";
                }
                if (double.TryParse(key, out x))
                {
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void techCount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                techCount.Text = techCount.Text.Replace(".", ",");
                techCount.SelectionStart = techCount.Text.Length;
                count = double.Parse(techCount.Text);
                price = double.Parse(techPrice.Text);
                sum = Math.Round((count * price), 2);
                techSum.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void techPrice_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                techPrice.Text = techPrice.Text.Replace(".", ",");
                techPrice.SelectionStart = techPrice.Text.Length;
                count = double.Parse(techCount.Text);
                price = double.Parse(techPrice.Text);
                sum = Math.Round((count * price), 2);
                techSum.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void Room_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double admin, comp, barb, allRoom;

                if (energyRoom1.Text == "" || energyRoom1.Text.Substring(0,1) == ",")
                {
                    energyRoom1.Text = "0";
                }
                if (energyRoom2.Text == "" || energyRoom2.Text.Substring(0, 1) == ",")
                {
                    energyRoom2.Text = "0";
                }
                if (energyRoom3.Text == "" || energyRoom3.Text.Substring(0, 1) == ",")
                {
                    energyRoom3.Text = "0";
                }

                energyRoom1.Text = energyRoom1.Text.Replace(".", ",").Replace(" ", "");
                energyRoom1.SelectionStart = energyRoom1.Text.Length;
                energyRoom2.Text = energyRoom2.Text.Replace(".", ",").Replace(" ", "");
                energyRoom2.SelectionStart = energyRoom2.Text.Length;
                energyRoom3.Text = energyRoom3.Text.Replace(".", ",").Replace(" ", "");
                energyRoom3.SelectionStart = energyRoom3.Text.Length;
                
                admin = double.Parse(energyRoom1.Text);
                comp = double.Parse(energyRoom2.Text);
                barb = double.Parse(energyRoom3.Text);
                
                allRoom = Math.Round((admin + comp + barb), 2);
                energyCount.Text = allRoom.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void energyCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var old = new Xml_data();
                var old_count = old.Energy();
                energyCount.Text = energyCount.Text.Replace(".", ",");
                energyCount.SelectionStart = energyCount.Text.Length;
                count = double.Parse(energyCount.Text);
                price = double.Parse(energyPrice.Text);
                sum = Math.Round(((count - old_count.Item1) * price), 2);
                energySum.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void energyPrice_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var old = new Xml_data();
                var old_count = old.Energy();
                energyPrice.Text = energyPrice.Text.Replace(".", ",");
                energyPrice.SelectionStart = energyPrice.Text.Length;
                count = double.Parse(energyCount.Text);
                price = double.Parse(energyPrice.Text);
                sum = Math.Round(((count - old_count.Item1) * price), 2);
                energySum.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void AddOldData_Click(object sender, RoutedEventArgs e)
        {
            var old = new Xml_data();
            var old_count = old.Energy();
            energyCount.Text = old_count.Item1.ToString();
            energyRoom1.Text = old_count.Item2.ToString();
            energyRoom2.Text = old_count.Item3.ToString();
            energyRoom3.Text = old_count.Item4.ToString();
        }

        private void Sum_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double techSumm, energySumm, heatSumm, waterSumm, result;
                if (techSum.Text == "")
                {
                    techSum.Text = "0";
                }
                if (energySum.Text == "")
                {
                    energySum.Text = "0";
                }
                if (heatSum.Text == "")
                {
                    heatSum.Text = "0";
                }
                if (waterSum.Text == "")
                {
                    waterSum.Text = "0";
                }
                techSumm = double.Parse(techSum.Text);
                energySumm = double.Parse(energySum.Text);
                heatSumm = double.Parse(heatSum.Text);
                waterSumm = double.Parse(waterSum.Text);
                result = Math.Round((techSumm + energySumm + heatSumm + waterSumm), 2);
                allSum.Text = result + " руб.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void heatCount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                heatCount.Text = heatCount.Text.Replace(".", ",");
                heatCount.SelectionStart = heatCount.Text.Length;
                count = double.Parse(heatCount.Text);
                price = double.Parse(heatPrice.Text);
                sum = Math.Round((count * price), 2);
                heatSum.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void heatPrice_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                heatPrice.Text = heatPrice.Text.Replace(".", ",");
                heatPrice.SelectionStart = heatPrice.Text.Length;
                count = double.Parse(heatCount.Text);
                price = double.Parse(heatPrice.Text);
                sum = Math.Round((count * price), 2);
                heatSum.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void waterCount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var old = new Xml_data();
                double old_count = old.Water();
                waterCount.Text = waterCount.Text.Replace(".", ",");
                waterCount.SelectionStart = waterCount.Text.Length;
                count = double.Parse(waterCount.Text);
                price = double.Parse(waterPrice.Text);
                sum = Math.Round(((count - old_count) * price), 2);
                waterSum.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void waterPrice_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var old = new Xml_data();
                double old_count = old.Water();
                waterPrice.Text = waterPrice.Text.Replace(".", ",");
                waterPrice.SelectionStart = waterPrice.Text.Length;
                count = double.Parse(waterCount.Text);
                price = double.Parse(waterPrice.Text);
                sum = Math.Round(((count - old_count) * price), 2);
                waterSum.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PeriodDate != null)
                {
                    var day = PeriodDate.Value.Day;
                    var month = PeriodDate.Value.ToString("MMMM");
                    var year = PeriodDate.Value.Year;
                    int maxId = 0;
                    MainWindow form = (MainWindow)Owner;
                    string filename = "data.xml";
                    XDocument xDoc = XDocument.Load(filename);
                    XNode node = xDoc.Root.FirstNode;
                    if (node != null)
                    {
                        maxId = xDoc.Root.Elements("period").Max(t => Int32.Parse(t.Attribute("id").Value));
                    }
                    else
                    {
                        maxId = 0;
                    }
                    XElement element = new XElement("period",
                                            new XAttribute("day", day),
                                            new XAttribute("month", month),
                                            new XAttribute("year", year),
                                            new XAttribute("id", ++maxId),
                                            new XElement("job",
                                                new XAttribute("id", "tech"),
                                                new XElement("namejob", tech.Text),
                                                new XElement("count", techCount.Text),
                                                new XElement("unit", "месяц"),
                                                new XElement("price", techPrice.Text),
                                                new XElement("summa", techSum.Text)),
                                            new XElement("job",
                                                new XAttribute("id", "energy"),
                                                new XElement("namejob", energy.Text),
                                                new XElement("count", energyCount.Text),
                                                new XElement("room1", energyRoom1.Text),
                                                new XElement("room2", energyRoom2.Text),
                                                new XElement("room3", energyRoom3.Text),
                                                new XElement("unit", "кВт"),
                                                new XElement("price", energyPrice.Text),
                                                new XElement("summa", energySum.Text)),
                                            new XElement("job",
                                                new XAttribute("id", "heat"),
                                                new XElement("namejob", heat.Text),
                                                new XElement("count", heatCount.Text),
                                                new XElement("unit", "Гкал"),
                                                new XElement("price", heatPrice.Text),
                                                new XElement("summa", heatSum.Text)),
                                            new XElement("job",
                                                new XAttribute("id", "water"),
                                                new XElement("namejob", water.Text),
                                                new XElement("count", waterCount.Text),
                                                new XElement("unit", "м3"),
                                                new XElement("price", waterPrice.Text),
                                                new XElement("summa", waterSum.Text))
                                        );
                    xDoc.Root.Add(element);
                    xDoc.Save(filename);
                    Close();
                }
                else
                {
                    MessageBox.Show("Поле даты должно быть заполнено!!!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        #endregion
    }
}
