using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XMLEditor
{
    class Program
    {
        static void Main(string[] args)
        {

#if !vijay


            //XDocument doc = XDocument.Load(@"C:\Users\vijay.siraparapu\Desktop\Attr Presentation xml\Utility\DiagramsAttributePresentation-Fix 1.xml");
            //var elements = doc.Elements().Where(x => x.Name == "Attribute");
            //foreach (XElement xElement in elements)
            //{
            //    //if (xElement.Attributes().Where(y=>y.Name=="Unit").FirstOrDefault() !=null)
            //    //{
                    
            //    //}
            //    xElement.Add(new XAttribute("test","TEst"));
            //}
            //doc.Save(@"C:\Users\vijay.siraparapu\Desktop\Attr Presentation xml\Utility\OutPut.xml");
            //XmlDocument xmldoc = new XmlDocument();
           
            //XmlNodeList nodeList;
            //int i = 0;
            //string str = null;
            //FileStream fs = new FileStream(@"C:\Users\vijay.siraparapu\Desktop\Attr Presentation xml\Utility\DiagramsAttributePresentation-Fix 1.xml", FileMode.Open, FileAccess.ReadWrite);
            //xmldoc.Load(fs);
            //nodeList = xmldoc.GetElementsByTagName("Attribute");
            //bool latest = false;

            //foreach (XmlNode node in nodeList)
            //{
            //    foreach (XmlAttribute att in node.Attributes)
            //    {
            //        if (att.Name == "Test")
            //        {
            //            latest = true;
            //        }
            //    }
            //}
            //if (!latest)
            //{
            //    foreach (XmlNode node in nodeList)
            //    {
            //        XmlAttribute attribute=new XmlAttribute()
            //        node.Attributes.Append()
            //    }
            //}


            XDocument xDocument = XDocument.Load(@"C:\Users\vijay.siraparapu\Desktop\Attr Presentation xml\Utility\DiagramsAttributePresentation-Fix 1.xml");
            IEnumerable<XElement> elements = xDocument.Element("AttributePresentation").Element("ElementType").Elements().Where(x=>x.Name=="Attribute");

            var attributes = elements.Where(x => x.Name == "Attribute");

            

            foreach (XElement element in attributes)
            {
                var attributes1 = element.Attributes().ToDictionary(x => x.Name);
                element.RemoveAttributes();
                element.Add(attributes1["attrID"]);
                element.Add(attributes1["label"]);
                element.Add(new XAttribute("UnitSet", ""));
                element.Add(new XAttribute("Unit", ""));
                element.Add(new XAttribute("Precision", "00"));
                element.Add(attributes1["prompt"]);
                element.Add(attributes1["visible"]);
                element.Add(attributes1["readOnly"]);
                element.Add(attributes1["copy"]);
                element.Add(attributes1["order"]);
                element.Add(attributes1["visibleInItemList"]);
            }

            xDocument.Save(@"C:\Users\vijay.siraparapu\Desktop\Attr Presentation xml\Utility\Output.xml");

            //var firstAttri = attributes.FirstOrDefault();

            //if (firstAttri != null)
            //{
            //    if (firstAttri.Attribute("Test") == null)
            //    {
            //        foreach (XElement element in attributes)
            //        {

            //        }
            //    }
            //}


#else
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList nodeList;
            int i = 0;
            string str = null;
            FileStream fs = new FileStream(@"C:\Users\vijay.siraparapu\Desktop\Attr Presentation xml\Utility\DiagramsAttributePresentation-Fix 1.xml", FileMode.Open, FileAccess.ReadWrite);
            xmldoc.Load(fs);
            nodeList = xmldoc.GetElementsByTagName("Attribute");

            foreach (XmlNode node in nodeList)
            {

                //foreach (XmlAttribute attribute in node)
                //{

                //}
            }
            for (i = 0; i <= nodeList.Count - 1; i++)
            {
                foreach (XmlAttribute att in nodeList[i].Attributes)
                {
                    Console.WriteLine(att.Name + ":" + att.Value);
                }
                //xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                //str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                //Console.WriteLine(str);
            }
#endif
        }
    }
}
