using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DiagramXMLUpgrade
{
    public partial class Form1 : Form
    {
        private string defaultsPath = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1=new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                defaultsPath = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           AttributeUpgrade.Upgrade(defaultsPath);
        }
    }

    public class AttributeUpgrade
    {
        public static void Upgrade(string defaultsPath)
        {


            if (File.Exists(defaultsPath + "\\DiagramsAttributePresentation.xml"))
            {
                //Load Attribute file
                XDocument xDocument = XDocument.Load(defaultsPath + "\\DiagramsAttributePresentation.xml");

                //Get Required elements for modification elements
                IEnumerable<XElement> elements = xDocument.Element("AttributePresentation").Element("ElementType").Elements().Where(x => x.Name == "Attribute");

                var attributes = elements.Where(x => x.Name == "Attribute");

                foreach (XElement element in attributes)
                {
                    var attributes1 = element.Attributes().ToDictionary(x => x.Name);
                    element.RemoveAttributes();
                    element.Add(attributes1["attrID"]);
                    element.Add(attributes1["label"]);

                    if (attributes1.ContainsKey("UnitSet"))
                        element.Add(attributes1["UnitSet"]);
                    else
                        element.Add(new XAttribute("UnitSet", ""));

                    if (attributes1.ContainsKey("Unit"))
                        element.Add(attributes1["Unit"]);
                    else
                        element.Add(new XAttribute("Unit", ""));

                    if (attributes1.ContainsKey("Precision"))
                        element.Add(attributes1["Precision"]);
                    else
                        element.Add(new XAttribute("Precision", "00"));

                    element.Add(attributes1["prompt"]);
                    element.Add(attributes1["visible"]);
                    element.Add(attributes1["readOnly"]);
                    element.Add(attributes1["copy"]);
                    element.Add(attributes1["order"]);
                    element.Add(attributes1["visibleInItemList"]);
                }

                xDocument.Save(defaultsPath + "\\DiagramsAttributePresentation.xml");
                MessageBox.Show("Upgrade successful", "Diagrams Upgrade", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Attribute presentation file not found. Please Select the correct folder ", "Attribute presentation file not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
