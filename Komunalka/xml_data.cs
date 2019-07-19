using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Komunalka
{
    public class XmlData
    {
        private ObservableCollection<string> DataXml { get; set; }
        public IEnumerable<string> RefresheData()
        {
            const string filename = "data.xml";
            var doc = XDocument.Load(filename);
            DataXml = new ObservableCollection<string>();
            foreach (var el in doc.Root.Elements())
            {
                if (el.Attribute("day") == null)
                {
                    el.Add(new XAttribute("day", "1"));
                    doc.Save(filename);
                }
                string item = el.Attribute("month")?.Value + ' ' + el.Attribute("year")?.Value;
                DataXml.Add("За " + item);
            }
            return DataXml;
        }
        public static Tuple<double, double, double, double, double> Energy()
        {
            double count = 0, room1 = 0, room2 = 0, room3 = 0, room4 = 0;
            string filename = "data.xml";
            XDocument xDoc = XDocument.Load(filename);
            XElement attr = xDoc.Root.Elements().LastOrDefault();
            if (attr == null)
            {
                count = 0;
                room1 = 0;
                room2 = 0;
                room3 = 0;
                room4 = 0;
            }
            else
            {
                foreach (XElement el in attr.Elements())
                {
                    if (el.Attribute("id").Value == "energy")
                    {
                        count = double.Parse(el.Element("count")?.Value ?? "0");
                        room1 = double.Parse(el.Element("room1")?.Value ?? "0");
                        room2 = double.Parse(el.Element("room2")?.Value ?? "0");
                        room3 = double.Parse(el.Element("room3")?.Value ?? "0");
                        room4 = double.Parse(el.Element("room4")?.Value ?? "0");
                    }
                }
            }
            return Tuple.Create(count, room1, room2, room3, room4);
        }
        public static double Water()
        {
            double result = 0;
            string filename = "data.xml";
            XDocument xDoc = XDocument.Load(filename);
            XElement attr = xDoc.Root.Elements().LastOrDefault();
            if (attr == null)
            {
                result = 0;
            }
            else
            {
                foreach (XElement el in attr.Elements())
                {
                    if (el.Attribute("id").Value == "water")
                    {
                        var count = el.Element("count").Value;
                        result = double.Parse(count);
                    }
                }
            }
            return result;
        }

        public static Tuple<double, double, double, double, double, double, string> OldData(string id)
        {
            double count = 0, room1 = 0, room2 = 0, room3 = 0, room4 = 0,  water = 0;
            var date = "";
            var prevId = int.Parse(id);
            --prevId;
            var curId = prevId.ToString();
            const string filename = "data.xml";
            var xDoc = XDocument.Load(filename);
            foreach (var el in xDoc.Root.Elements())
            {
                if (el.Attribute("id").Value == curId)
                {
                    date = el.Attribute("day")?.Value + ":" + el.Attribute("month")?.Value + ":" + el.Attribute("year")?.Value;
                    foreach (XElement element in el.Elements())
                    {
                        if (element.Attribute("id").Value == "energy")
                        {
                            count = double.Parse(element.Element("count")?.Value ?? "0");
                            room1 = double.Parse(element.Element("room1")?.Value ?? "0");
                            room2 = double.Parse(element.Element("room2")?.Value ?? "0");
                            room3 = double.Parse(element.Element("room3")?.Value ?? "0");
                            room4 = double.Parse(element.Element("room4")?.Value ?? "0");
                        }
                        if (element.Attribute("id").Value == "water")
                        {
                            water = double.Parse(element.Element("count").Value);
                        }
                    }
                }
            }
            return Tuple.Create(count, room1, room2, room3, room4, water, date);
        }/**/
    }
}
