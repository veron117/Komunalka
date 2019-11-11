using LingvoNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace Komunalka
{
    /// <summary>
    /// Логика взаимодействия для PrintForm.xaml
    /// </summary>
    public partial class PrintForm : Window
    {
        public PrintForm()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var frm = (MainWindow)Owner;
            hide.Text = frm.hide.Text;
            var x = hide.Text.Replace(" ", ":");
            var arr = x.Split(':');

            Title = "Просмотр коммунальных услуг за " + arr[1] + " " + arr[2];
            info_title.Text = "Общие сведения: коммунальные услуги за " + arr[1] + " " + arr[2];
            try
            {
                var filename = "data.xml";
                var doc = XDocument.Load(filename);
                foreach (var el in doc.Root.Elements())
                {
                    if (el.Attribute("month").Value == arr[1] && el.Attribute("year").Value == arr[2])
                    {
                        var old = new XmlData();
                        var oldData = XmlData.OldData(el.Attribute("id").Value);
                        foreach (var elem in el.Elements())
                        {
                            if (elem.Attribute("id").Value == "tech")
                            {
                                adminTechCount.Text = "11,6";
                                compTechCount.Text = "17,8";
                                barbTechCount.Text = "10,7";
                                seamstressTechCount.Text = "6";

                                techName.Text = elem.Element("namejob").Value;
                                techCount.Text = elem.Element("count").Value;
                                techUnit.Text = elem.Element("unit").Value;
                                techPrice.Text = elem.Element("price").Value;
                                techSum.Text = elem.Element("summa").Value;
                                double techsummroom1 = double.Parse("11,6") * double.Parse(elem.Element("price").Value),
                                       techsummroom2 = double.Parse("17,8") * double.Parse(elem.Element("price").Value),
                                       techsummroom3 = double.Parse(barbTechCount.Text) * double.Parse(elem.Element("price").Value),
                                       techsummroom4 = double.Parse(seamstressTechCount.Text) * double.Parse(elem.Element("price").Value);
                                techsummroom1 = Math.Round(techsummroom1, 2);
                                techsummroom2 = Math.Round(techsummroom2, 2);
                                techsummroom3 = Math.Round(techsummroom3, 2);
                                techsummroom4 = Math.Round(techsummroom4, 2);

                                techSumRoom1.Text = "Администрация: " + techsummroom1 + " руб.";
                                techSumRoom2.Text = "Компьютерный зал: " + techsummroom2 + " руб.";
                                techSumRoom3.Text = "Парикмахерская: " + techsummroom3 + " руб.";
                                techSumRoom4.Text = "Швея: " + techsummroom4 + " руб.";
                                

                                adminTechPrice.Text = elem.Element("price").Value;
                                compTechPrice.Text = elem.Element("price").Value;
                                barbTechPrice.Text = elem.Element("price").Value;
                                seamstressTechPrice.Text = elem.Element("price").Value;

                                adminTechSum.Text = techsummroom1.ToString();
                                compTechSum.Text = techsummroom2.ToString();
                                barbTechSum.Text = techsummroom3.ToString();
                                seamstressTechSum.Text = techsummroom4.ToString();
                            }
                            if (elem.Attribute("id").Value == "energy")
                            {
                                energyName.Text = elem.Element("namejob").Value;
                                var count = double.Parse(elem.Element("count").Value) - oldData.Item1;
                                energyCount.Text = count.ToString();
                                energyUnit.Text = elem.Element("unit").Value;
                                energyPrice.Text = elem.Element("price").Value;
                                energySum.Text = elem.Element("summa").Value;

                                double countroom1 = double.Parse(elem.Element("room1").Value) - oldData.Item2,
                                       countroom2 = double.Parse(elem.Element("room2").Value) - oldData.Item3,
                                       countroom3 = double.Parse(elem.Element("room3").Value) - oldData.Item4,
                                       countroom4 = double.Parse(elem.Element("room4")?.Value ?? "0") - oldData.Item5;
                                countRoom1.Text = "Администрация: " + countroom1 + " кВт";
                                countRoom2.Text = "Компьютерный зал: " + countroom2 + " кВт";
                                countRoom3.Text = "Парикмахерская: " + countroom3 + " кВт";
                                countRoom4.Text = "Швея: " + countroom4 + " кВт";
                                double energysummroom1 = countroom1 * double.Parse(elem.Element("price").Value),
                                       energysummroom2 = countroom2 * double.Parse(elem.Element("price").Value),
                                       energysummroom3 = countroom3 * double.Parse(elem.Element("price").Value),
                                       energysummroom4 = countroom4 * double.Parse(elem.Element("price").Value);
                                energysummroom1 = Math.Round(energysummroom1, 2);
                                energysummroom2 = Math.Round(energysummroom2, 2);
                                energysummroom3 = Math.Round(energysummroom3, 2);
                                energysummroom4 = Math.Round(energysummroom4, 2);
                                energySumRoom1.Text = "Администрация: " + energysummroom1 + " руб.";
                                energySumRoom2.Text = "Компьютерный зал: " + energysummroom2 + " руб.";
                                energySumRoom3.Text = "Парикмахерская: " + energysummroom3 + " руб.";
                                energySumRoom4.Text = "Швея: " + energysummroom4 + " руб.";

                                adminCount.Text = countroom1.ToString();
                                compCount.Text = countroom2.ToString();
                                barbCount.Text = countroom3.ToString();
                                seamstressCount.Text = countroom4.ToString();

                                adminPrice.Text = elem.Element("price").Value;
                                compPrice.Text = elem.Element("price").Value;
                                barbPrice.Text = elem.Element("price").Value;
                                seamstressPrice.Text = elem.Element("price").Value;

                                adminSum.Text = energysummroom1.ToString();
                                compSum.Text = energysummroom2.ToString();
                                barbSum.Text = energysummroom3.ToString();
                                seamstressSum.Text = energysummroom4.ToString();
                            }
                            if (elem.Attribute("id").Value == "heat")
                            {
                                heatName.Text = elem.Element("namejob").Value;
                                heatCount.Text = elem.Element("count").Value;
                                heatUnit.Text = elem.Element("unit").Value;
                                heatPrice.Text = elem.Element("price").Value;
                                heatSum.Text = elem.Element("summa").Value;
                                double heatsummroom1 = double.Parse("11,6") * double.Parse(elem.Element("price").Value),
                                       heatsummroom2 = double.Parse("17,8") * double.Parse(elem.Element("price").Value),
                                       heatsummroom3 = double.Parse("16,7") * double.Parse(elem.Element("price").Value);
                                heatsummroom1 = Math.Round(heatsummroom1, 2);
                                heatsummroom2 = Math.Round(heatsummroom2, 2);
                                heatsummroom3 = Math.Round(heatsummroom3, 2);
                                heatSumRoom1.Text = "Администрация: " + heatsummroom1 + " руб.";
                                heatSumRoom2.Text = "Компьютерный зал: " + heatsummroom2 + " руб.";
                                heatSumRoom3.Text = "Парикмахерская: " + heatsummroom3 + " руб.";

                                adminHeatCount.Text = "11,6";
                                compHeatCount.Text = "17,8";
                                barbHeatCount.Text = "16,7";

                                adminHeatPrice.Text = elem.Element("price").Value;
                                compHeatPrice.Text = elem.Element("price").Value;
                                barbHeatPrice.Text = elem.Element("price").Value;

                                adminHeatSum.Text = heatsummroom1.ToString();
                                compHeatSum.Text = heatsummroom2.ToString();
                                barbHeatSum.Text = heatsummroom3.ToString();
                            }
                            if (elem.Attribute("id").Value == "water")
                            {
                                waterName.Text = elem.Element("namejob").Value;
                                var count = double.Parse(elem.Element("count").Value) - oldData.Item6;
                                waterCount.Text = count.ToString();
                                waterUnit.Text = elem.Element("unit").Value;
                                waterPrice.Text = elem.Element("price").Value;
                                waterSum.Text = elem.Element("summa").Value;
                            }
                        }
                    }
                    else
                    {
                    }
                }
                var total = Math.Round(double.Parse(techSum.Text) + double.Parse(energySum.Text) + double.Parse(heatSum.Text) + double.Parse(waterSum.Text), 2);
                TotalSum.Text = total.ToString();

                //Заполнение даблицы По помещениям
                adminTechTotalSum.Text = adminTechSum.Text;
                adminEnergyTotalSum.Text = adminSum.Text;
                adminHeatTotalSum.Text = adminHeatSum.Text;

                compTechTotalSum.Text = compTechSum.Text;
                compEnergyTotalSum.Text = compSum.Text;
                compHeatTotalSum.Text = compHeatSum.Text;

                barbTechTotalSum.Text = barbTechSum.Text;
                seamstressTechTotalSum.Text = seamstressTechSum.Text;
                barbEnergyTotalSum.Text = barbSum.Text;
                barbHeatTotalSum.Text = barbHeatSum.Text;
                barbWaterTotalSum.Text = waterSum.Text;

                seamstressEnergyTotalSum.Text = seamstressSum.Text;

                adminTotalSum.Text = Math.Round(double.Parse(adminTechTotalSum.Text) + double.Parse(adminEnergyTotalSum.Text) + double.Parse(adminHeatTotalSum.Text), 2).ToString();
                compTotalSum.Text = Math.Round(double.Parse(compTechTotalSum.Text) + double.Parse(compEnergyTotalSum.Text) + double.Parse(compHeatTotalSum.Text), 2).ToString();
                barbTotalSum.Text = Math.Round(double.Parse(barbTechTotalSum.Text) + double.Parse(barbEnergyTotalSum.Text) + double.Parse(barbHeatTotalSum.Text) + double.Parse(waterSum.Text), 2).ToString();
                seamstressTotalSum.Text = seamstressEnergyTotalSum.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Скрыть Grid
                //MainGrid.Visibility = Visibility.Hidden;

                // Увеличить размер в 5 раз
                PrintGrid.LayoutTransform = new ScaleTransform(1, 1);

                // Определить поля
                var pageMargin = 5;

                // Получить размер страницы
                var pageSize = new Size(printDialog.PrintableAreaWidth - pageMargin * 2,
                    printDialog.PrintableAreaHeight - 20);

                // Инициировать установку размера элемента
                PrintGrid.Measure(pageSize);
                PrintGrid.Arrange(new Rect(pageMargin, pageMargin + 10, pageSize.Width, pageSize.Height));

                // Напечатать элемент
                printDialog.PrintVisual(PrintGrid, "Распечатываем платёжку");

                // Удалить трансформацию и снова сделать элемент видимым
                PrintGrid.LayoutTransform = null;
                //MainGrid.Visibility = Visibility.Visible;
            }
        }

        private void PrintMail_OnClick(object sender, RoutedEventArgs e)
        {
            var frm = (MainWindow)Owner;
            hide.Text = frm.hide.Text;
            var x = hide.Text.Replace(" ", ":");
            var arr = x.Split(':');
            try
            {
                const string filename = "data.xml";
                var doc = XDocument.Load(filename);
                if (doc.Root != null)
                    foreach (var el in doc.Root.Elements())
                    {
                        if (el.Attribute("month")?.Value == arr[1] && el.Attribute("year")?.Value == arr[2])
                        {
                            var oldData = XmlData.OldData(el.Attribute("id")?.Value);
                            var oldDate = oldData.Item7 != "" ? oldData.Item7.Split(':') : new []{ "1", "январь", arr[2] };
                            foreach (var elem in el.Elements())
                            {
                                if (elem.Attribute("id")?.Value != "energy") continue;
                                var oldDay = int.Parse(oldDate[0]).ToString("00");
                                var newDay = int.Parse(el.Attribute("day")?.Value ?? throw new InvalidOperationException()).ToString("00");
                                var dataList = new List<DocumentsData>
                                {
                                    //Период
                                    new DocumentsData
                                    {
                                        Data = oldDate[2] == arr[2] ? "с " + oldDay + " " + Nouns.FindOne(oldDate[1])[Case.Genitive] + " по " + newDay + " " + Nouns.FindOne(arr[1])[Case.Genitive]  + " " + arr[2] + " года" 
                                                                    : "с " + oldDay + " " + Nouns.FindOne(oldDate[1])[Case.Genitive] + " " + oldDate[2] + " года" + " по " + newDay + " " + Nouns.FindOne(arr[1])[Case.Genitive]  + " " + arr[2] + " года" ,
                                        Label = "{period}"
                                    },
                                    //Старые показания
                                    new DocumentsData
                                    {
                                        Data = oldData.Item2.ToString(),
                                        Label = "{old_admin}"
                                    },
                                    new DocumentsData
                                    {
                                        Data = oldData.Item3.ToString(),
                                        Label = "{old_comp}"
                                    },
                                    new DocumentsData
                                    {
                                        Data = oldData.Item4.ToString(),
                                        Label = "{old_barb}"
                                    },
                                    new DocumentsData
                                    {
                                        Data = oldData.Item5.ToString(),
                                        Label = "{old_seamstress}"
                                    },
                                    //Новые показания
                                    new DocumentsData
                                    {
                                        Data = elem.Element("room1")?.Value,
                                        Label = "{new_admin}"
                                    },
                                    new DocumentsData
                                    {
                                        Data = elem.Element("room2")?.Value,
                                        Label = "{new_comp}"
                                    },
                                    new DocumentsData
                                    {
                                        Data = elem.Element("room3")?.Value,
                                        Label = "{new_barb}"
                                    },
                                    new DocumentsData
                                    {
                                        Data = elem.Element("room4")?.Value,
                                        Label = "{new_seamstress}"
                                    },
                                    //Результаты по кабинетам
                                    new DocumentsData
                                    {
                                        Data = (double.Parse(elem.Element("room1")?.Value ?? throw new InvalidOperationException()) - oldData.Item2).ToString(),
                                        Label = "{result_admin}"
                                    },
                                    new DocumentsData
                                    {
                                        Data = (double.Parse(elem.Element("room2")?.Value ?? throw new InvalidOperationException()) - oldData.Item3).ToString(),
                                        Label = "{result_comp}"
                                    },
                                    new DocumentsData
                                    {
                                        Data = (double.Parse(elem.Element("room3")?.Value ?? throw new InvalidOperationException()) - oldData.Item4).ToString(),
                                        Label = "{result_barb}"
                                    },
                                    new DocumentsData
                                    {
                                        Data = (double.Parse(elem.Element("room4")?.Value ?? throw new InvalidOperationException()) - oldData.Item5).ToString(),
                                        Label = "{result_seamstress}"
                                    },
                                    //Иотоговый результат
                                    new DocumentsData
                                    {
                                        Data = (double.Parse(elem.Element("count")?.Value ?? throw new InvalidOperationException()) - oldData.Item1).ToString(),
                                        Label = "{result}"
                                    }
                                };
                                var path = AppDomain.CurrentDomain.BaseDirectory + "info_mail.docx";
                                var name = oldDate[2] == arr[2]
                                    ? oldDate[1] + " - " + arr[1] + " " + arr[2] + " года"
                                    : oldDate[1] + " " + oldDate[2] + " года" + " - " + arr[1] + " " + arr[2] + " года";
                                Task.Run(() => new PrintService(dataList, path, name));
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var frm = (MainWindow)Owner;
            var editform = new EditForm
            {
                Owner = this,
                ShowInTaskbar = true
            };
            editform.ShowDialog();
            var data = new XmlData();
            frm.dataXml.ItemsSource = data.RefresheData();
            frm.hide.Text = hide.Text;
            Window_Loaded(sender, e);
            Show();
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var frm = (MainWindow)Owner;
            var x = hide.Text.Replace(" ", ":");
            var arr = x.Split(':');
            var confirm = MessageBox.Show("Вы уверены что хотите удалить запись " + hide.Text, "Удаление", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.Yes)
            {
                var filename = "data.xml";
                var doc = XDocument.Load(filename);
                var period = doc.Root.Descendants("period").Where(t => t.Attribute("month").Value == arr[1] && t.Attribute("year").Value == arr[2]);
                period.Remove();
                doc.Save(filename);
                var data = new XmlData();
                frm.dataXml.ItemsSource = data.RefresheData();
                Close();
            }
        }

        private void ExitWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
