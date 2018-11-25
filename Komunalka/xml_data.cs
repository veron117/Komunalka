using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Komunalka
{
    class Xml_data
    {
        ObservableCollection<string> dataxml { get; set; }
        public ObservableCollection<string> RefresheData()
        {
            string filename = "data.xml";
            XDocument doc = XDocument.Load(filename);
            dataxml = new ObservableCollection<string>();
            foreach (XElement el in doc.Root.Elements())
            {
                if (el.Attribute("day") == null)
                {
                    el.Add(new XAttribute("day", "1"));
                    doc.Save(filename);
                }
                string item = el.Attribute("month")?.Value + ' ' + el.Attribute("year")?.Value;
                dataxml.Add("За " + item);
            }
            return dataxml;
        }
        public Tuple<double, double, double, double> Energy()
        {
            double count = 0, room1 = 0, room2 = 0, room3 = 0;
            string filename = "data.xml";
            XDocument xDoc = XDocument.Load(filename);
            XElement attr = xDoc.Root.Elements().LastOrDefault();
            if (attr == null)
            {
                count = 0;
                room1 = 0;
                room2 = 0;
                room3 = 0;
            }
            else
            {
                foreach (XElement el in attr.Elements())
                {
                    if (el.Attribute("id").Value == "energy")
                    {
                        count = double.Parse(el.Element("count").Value);
                        room1 = double.Parse(el.Element("room1").Value);
                        room2 = double.Parse(el.Element("room2").Value);
                        room3 = double.Parse(el.Element("room3").Value);
                    }
                }
            }
            return Tuple.Create(count, room1, room2, room3);
        }
        public double Water()
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
        public Tuple<double, double, double, double, double, string> OldData(string id)
        {
            double count = 0, room1 = 0, room2 = 0, room3 = 0,  water = 0;
            string date = "";
            int prevId = Int32.Parse(id);
            --prevId;
            string curId = prevId.ToString();
            string filename = "data.xml";
            XDocument xDoc = XDocument.Load(filename);
            foreach (XElement el in xDoc.Root.Elements())
            {
                if (el.Attribute("id").Value == curId)
                {
                    date = el.Attribute("day")?.Value + ":" + el.Attribute("month")?.Value + ":" + el.Attribute("year")?.Value;
                    foreach (XElement element in el.Elements())
                    {
                        if (element.Attribute("id").Value == "energy")
                        {
                            count = double.Parse(element.Element("count").Value);
                            room1 = double.Parse(element.Element("room1").Value);
                            room2 = double.Parse(element.Element("room2").Value);
                            room3 = double.Parse(element.Element("room3").Value);
                        }
                        if (element.Attribute("id").Value == "water")
                        {
                            water = double.Parse(element.Element("count").Value);
                        }
                    }
                }
            }
            return Tuple.Create(count, room1, room2, room3, water, date);
        }/**/
    }
}
