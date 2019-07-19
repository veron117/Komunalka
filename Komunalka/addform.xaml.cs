using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace Komunalka
{
    /// <summary>
    /// Логика взаимодействия для addform.xaml
    /// </summary>
    public partial class Addform : Window
    {
        #region Public members

        public DateTime? PeriodDate { get; set; }

        #endregion

        #region Private members

        private double _count, _price, _sum;

        #endregion

        #region Constructor

        public Addform()
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
                var conv = new KeyConverter();
                var key = conv.ConvertToString(e.Key)?.Replace("NumPad", "");
                if (key == "OemPeriod" || key == "OemComma" || key == "Decimal")
                {
                    key = "0";
                }
                if (double.TryParse(key, out _))
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
                _count = double.Parse(techCount.Text);
                _price = double.Parse(techPrice.Text);
                _sum = Math.Round(_count * _price, 2);
                techSum.Text = _sum.ToString();
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
                _count = double.Parse(techCount.Text);
                _price = double.Parse(techPrice.Text);
                _sum = Math.Round(_count * _price, 2);
                techSum.Text = _sum.ToString();
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
                double admin, comp, barb, seamstress, allRoom;

                if (energyRoom1.Text == "" || energyRoom1.Text.Substring(0, 1) == ",")
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
                if (energyRoom4.Text == "" || energyRoom4.Text.Substring(0, 1) == ",")
                {
                    energyRoom4.Text = "0";
                }

                energyRoom1.Text = energyRoom1.Text.Replace(".", ",").Replace(" ", "");
                energyRoom1.SelectionStart = energyRoom1.Text.Length;
                energyRoom2.Text = energyRoom2.Text.Replace(".", ",").Replace(" ", "");
                energyRoom2.SelectionStart = energyRoom2.Text.Length;
                energyRoom3.Text = energyRoom3.Text.Replace(".", ",").Replace(" ", "");
                energyRoom3.SelectionStart = energyRoom3.Text.Length;
                energyRoom4.Text = energyRoom4.Text.Replace(".", ",").Replace(" ", "");
                energyRoom4.SelectionStart = energyRoom4.Text.Length;

                admin = double.Parse(energyRoom1.Text);
                comp = double.Parse(energyRoom2.Text);
                barb = double.Parse(energyRoom3.Text);
                seamstress = double.Parse(energyRoom4.Text);

                allRoom = Math.Round(admin + comp + barb + seamstress, 2);
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
                var old = new XmlData();
                var oldCount = XmlData.Energy();
                energyCount.Text = energyCount.Text.Replace(".", ",");
                energyCount.SelectionStart = energyCount.Text.Length;
                _count = double.Parse(energyCount.Text);
                _price = double.Parse(energyPrice.Text);
                _sum = Math.Round((_count - oldCount.Item1) * _price, 2);
                energySum.Text = _sum.ToString();
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
                var old = new XmlData();
                var oldCount = XmlData.Energy();
                energyPrice.Text = energyPrice.Text.Replace(".", ",");
                energyPrice.SelectionStart = energyPrice.Text.Length;
                _count = double.Parse(energyCount.Text);
                _price = double.Parse(energyPrice.Text);
                _sum = Math.Round((_count - oldCount.Item1) * _price, 2);
                energySum.Text = _sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void AddOldData_Click(object sender, RoutedEventArgs e)
        {
            var oldCount = XmlData.Energy();
            energyCount.Text = oldCount.Item1.ToString();
            energyRoom1.Text = oldCount.Item2.ToString();
            energyRoom2.Text = oldCount.Item3.ToString();
            energyRoom3.Text = oldCount.Item4.ToString();
            energyRoom4.Text = oldCount.Item5.ToString();
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
                result = Math.Round(techSumm + energySumm + heatSumm + waterSumm, 2);
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
                _count = double.Parse(heatCount.Text);
                _price = double.Parse(heatPrice.Text);
                _sum = Math.Round(_count * _price, 2);
                heatSum.Text = _sum.ToString();
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
                _count = double.Parse(heatCount.Text);
                _price = double.Parse(heatPrice.Text);
                _sum = Math.Round(_count * _price, 2);
                heatSum.Text = _sum.ToString();
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
                var old = new XmlData();
                double oldCount = XmlData.Water();
                waterCount.Text = waterCount.Text.Replace(".", ",");
                waterCount.SelectionStart = waterCount.Text.Length;
                _count = double.Parse(waterCount.Text);
                _price = double.Parse(waterPrice.Text);
                _sum = Math.Round((_count - oldCount) * _price, 2);
                waterSum.Text = _sum.ToString();
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
                var old = new XmlData();
                double oldCount = XmlData.Water();
                waterPrice.Text = waterPrice.Text.Replace(".", ",");
                waterPrice.SelectionStart = waterPrice.Text.Length;
                _count = double.Parse(waterCount.Text);
                _price = double.Parse(waterPrice.Text);
                _sum = Math.Round((_count - oldCount) * _price, 2);
                waterSum.Text = _sum.ToString();
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
                    var maxId = 0;
                    var form = (MainWindow)Owner;
                    const string filename = "data.xml";
                    var xDoc = XDocument.Load(filename);
                    var node = xDoc.Root.FirstNode;
                    if (node != null)
                    {
                        maxId = xDoc.Root.Elements("period").Max(t => int.Parse(t.Attribute("id").Value));
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
                                                new XElement("room4", energyRoom4.Text),
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
