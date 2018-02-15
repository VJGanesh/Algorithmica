using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TempDelete
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    class Scribble
    {
         [STAThread]
        static void Main()
        {
       
            var files = Directory.GetFiles(@"C:\Users\Public\Documents\AVEVA\Diagrams\Data14.1.1\DFLTS\Diagrams", "*", SearchOption.AllDirectories);

            var fil =
                files.Where(ext => Path.GetExtension(ext).Equals(".xml", StringComparison.InvariantCultureIgnoreCase)).Where(file => Path.GetFileName(file).Contains("DiagramsSettings") ||
                          Path.GetFileName(file).Equals("DiagramsAttributePresentation.xml", StringComparison.OrdinalIgnoreCase));

            if(fil.Count()!=2)


            foreach (var ts in fil)
            {
                Console.WriteLine(ts);
            }

            //foreach (var ext in files)
            //{
            //    Console.WriteLine(Path.GetExtension(ext));
            //}

            //foreach (var file in fil)
            //    {
            //        if (Path.GetFileName(file).Contains("DiagramsSettings") ||
            //              Path.GetFileName(file).Equals("DiagramsAttributePresentation.xml", StringComparison.OrdinalIgnoreCase))
            //        {
            //            Console.WriteLine("test");
            //        }

                   
            //    }


            /*
              //FolderBrowserDialog folder=new FolderBrowserDialog();
           OpenFileDialog dialog=new OpenFileDialog();
           DirectoryInfo settingsDir = new DirectoryInfo(VmdSettingsManager.DiagramsAttributePresPath);
            
           //folder.ShowNewFolderButton = false;
           //folder.SelectedPath = settingsDir.Parent.Parent.FullName;
           dialog.InitialDirectory = settingsDir.Parent.Parent.FullName;
           dialog.Filter = "XML files|*.xml";
           dialog.Multiselect = true;
           if (dialog.ShowDialog() == DialogResult.OK)
           {
               //var  res=Directory.GetFiles(folder.SelectedPath, "*", SearchOption.AllDirectories)
               //    .Where(ext=>Path.GetExtension(ext).Equals(".xml",StringComparison.OrdinalIgnoreCase))
               //    .Where(file => Path.GetFileName(file).Equals(VmdConstants.XML_ATTR_PRESENTATION_FILE) || Path.GetFileName(file).Contains("DiagramsSettings"));

               var res =
                   dialog.FileNames.Where(
                       file =>
                           Path.GetFileName(file).Equals(VmdConstants.XML_ATTR_PRESENTATION_FILE) ||
                           Path.GetFileName(file).Contains("DiagramsSettings"));
                
               foreach (var file in res)
                   File.Copy(file, Path.Combine(VmdSettingsManager.DiagramsAttributePresPath, Path.GetFileName(file)),true);

               VmdApplication.ReInitializeSettingsManager(VmdConstants.XML_DEFAULT_SETTINGS_FILE,VmdConstants.XML_ATTR_PRESENTATION_FILE);
               mDefaultsCtrl.Initialize(defSettStru);
               dgAttrPresentation.Refresh();

               MessageBox.Show("Settings restored successfully.", VmdUserMessage.GetDialogBoxMessage("ID_SETTINGS_MANAGER"), MessageBoxButtons.OK, MessageBoxIcon.Information);
             */

        }
    }
}
