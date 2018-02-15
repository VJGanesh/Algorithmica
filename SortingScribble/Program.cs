using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SortingScribble
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] array=new int[]{7,1,3,2,4,14,5,20};
            //Random rn=new Random(10);
           
           
            XDocument xDocument = XDocument.Load(@"C:\Users\vijay.siraparapu\Desktop\New folder\DiagramsDefaultSettings.xml");
            //Get Required elements for modification elements
            XElement element= xDocument.Element("VmdSettingsStucture").Element("Defaults");
            //read
            string SnapValue = element.Element("Snap").Value;
            string GlueValue = element.Element("Glue").Value;
            //write
            element.Element("Snap").Value = "SnapModified";
            element.Element("Glue").Value = "GlueModified";
            xDocument.Save(@"C:\Users\vijay.siraparapu\Desktop\New folder\DiagramsDefaultSettings.xml");
        }
    }
}
