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

namespace Komunalka
{
    /// <summary>
    /// Логика взаимодействия для EditForm.xaml
    /// </summary>
    public partial class EditForm : Window
    {
        #region Public members

        public DateTime? PeriodDate { get; set; }

        #endregion

        #region Private members

        private double count, price, sum;

        #endregion

        #region Constructor

        public EditForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var form = (PrintForm)Owner;
            hide.Text = form.hide.Text;
            var x = hide.Text.Replace(" ", ":");
            var arr = x.Split(':');
            Title = "Редактирование записи периода за " + arr[1] + " " + arr[2];
            try
            {
                const string filename = "data.xml";
                var doc = XDocument.Load(filename);
                if (doc.Root == null) return;
                foreach (var el in doc.Root.Elements())
                {
                    if (el.Attribute("month")?.Value != arr[1] || el.Attribute("year")?.Value != arr[2]) continue;
                    hideId.Text = el.Attribute("id")?.Value;
                    var date = DateTime.Parse(el.Attribute("day")?.Value + " " + el.Attribute("month")?.Value + " " +
                                                el.Attribute("year")?.Value);

                    PeriodDate = date;
                    DataContext = this;
                    foreach (var elem in el.Elements())
                    {
                        if (elem.Attribute("id").Value == "tech")
                        {
                            techCount.Text = elem.Element("count").Value;
                            techPrice.Text = elem.Element("price").Value;
                            techSum.Text = elem.Element("summa").Value;
                        }

                        if (elem.Attribute("id").Value == "energy")
                        {
                            energyRoom1.Text = elem.Element("room1").Value;
                            energyRoom2.Text = elem.Element("room2").Value;
                            energyRoom3.Text = elem.Element("room3").Value;
                            energyPrice.Text = elem.Element("price").Value;
                            energySum.Text = elem.Element("summa").Value;
                        }

                        if (elem.Attribute("id").Value == "heat")
                        {
                            heatCount.Text = elem.Element("count").Value;
                            heatPrice.Text = elem.Element("price").Value;
                            heatSum.Text = elem.Element("summa").Value;
                        }

                        if (elem.Attribute("id").Value == "water")
                        {
                            waterCount.Text = elem.Element("count").Value;
                            waterPrice.Text = elem.Element("price").Value;
                            waterSum.Text = elem.Element("summa").Value;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Text_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Double x;

                var conv = new KeyConverter();
                var key = conv.ConvertToString(e.Key).Replace("NumPad", "");
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
                var old_count = old.OldData(hideId.Text);
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
                var old_count = old.OldData(hideId.Text);
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
                allSum.Text = result.ToString() + " руб.";
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
                var old_count = old.OldData(hideId.Text);
                waterCount.Text = waterCount.Text.Replace(".", ",");
                waterCount.SelectionStart = waterCount.Text.Length;
                count = double.Parse(waterCount.Text);
                price = double.Parse(waterPrice.Text);
                sum = Math.Round(((count - old_count.Item5) * price), 2);
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
                var old_count = old.OldData(hideId.Text);
                waterPrice.Text = waterPrice.Text.Replace(".", ",");
                waterPrice.SelectionStart = waterPrice.Text.Length;
                count = double.Parse(waterCount.Text);
                price = double.Parse(waterPrice.Text);
                sum = Math.Round(((count - old_count.Item5) * price), 2);
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
                    var form = (PrintForm)Owner;
                    hide.Text = form.hide.Text;
                    var x = hide.Text.Replace(" ", ":");
                    var arr = x.Split(new char[] { ':' });
                    var filename = "data.xml";
                    var xDoc = XDocument.Load(filename);
                    foreach (var el in xDoc.Root.Elements())
                    {
                        if (el.Attribute("month").Value == arr[1] && el.Attribute("year").Value == arr[2])
                        {
                            el.Attribute("day").Value = PeriodDate.Value.Day.ToString();
                            el.Attribute("month").Value = PeriodDate.Value.ToString("MMMM");
                            el.Attribute("year").Value = PeriodDate.Value.Year.ToString();
                            foreach (var elem in el.Elements())
                            {
                                if (elem.Attribute("id").Value == "tech")
                                {
                                    elem.Element("namejob").Value = tech.Text;
                                    elem.Element("count").Value = techCount.Text;
                                    elem.Element("unit").Value = "Месяц";
                                    elem.Element("price").Value = techPrice.Text;
                                    elem.Element("summa").Value = techSum.Text;
                                }
                                if (elem.Attribute("id").Value == "energy")
                                {
                                    elem.Element("namejob").Value = energy.Text;
                                    elem.Element("count").Value = energyCount.Text;
                                    elem.Element("room1").Value = energyRoom1.Text;
                                    elem.Element("room2").Value = energyRoom2.Text;
                                    elem.Element("room3").Value = energyRoom3.Text;
                                    elem.Element("unit").Value = "кВт";
                                    elem.Element("price").Value = energyPrice.Text;
                                    elem.Element("summa").Value = energySum.Text;
                                }
                                if (elem.Attribute("id").Value == "heat")
                                {
                                    elem.Element("namejob").Value = heat.Text;
                                    elem.Element("count").Value = heatCount.Text;
                                    elem.Element("unit").Value = "Месяц";
                                    elem.Element("price").Value = heatPrice.Text;
                                    elem.Element("summa").Value = heatSum.Text;
                                }
                                if (elem.Attribute("id").Value == "water")
                                {
                                    elem.Element("namejob").Value = water.Text;
                                    elem.Element("count").Value = waterCount.Text;
                                    elem.Element("unit").Value = "Месяц";
                                    elem.Element("price").Value = waterPrice.Text;
                                    elem.Element("summa").Value = waterSum.Text;
                                }
                            }

                            form.hide.Text = "За " + el.Attribute("month")?.Value + " " + el.Attribute("year")?.Value;
                        }
                    }
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
