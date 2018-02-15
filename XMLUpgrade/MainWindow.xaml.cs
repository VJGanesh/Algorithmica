using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using  System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using XMLUpgrade.Properties;
using MessageBox = System.Windows.Forms.MessageBox;

namespace XMLUpgrade
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private FolderBrowserDialog dialog;
        private XmlUpgradeViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            //this.label.Foreground=new bru
            DataContext = _viewModel= new XmlUpgradeViewModel();
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class AttributeUpgradeModel
    {
       public static bool Upgrade(string defaultsPath)
        {
           if (defaultsPath == string.Empty || defaultsPath == Resources.DefaultPathPrompt)
            {
                MessageBox.Show(Resources.NoSelectionMessage, Resources.FolderNotSelectedCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (File.Exists(defaultsPath + Resources.AttributeFileName))
            {
                
                XDocument xDocument = XDocument.Load(defaultsPath + Resources.AttributeFileName);
                
                
                var attributePresentation = xDocument.Element("AttributePresentation");
                if (attributePresentation != null)
                {
                    var elementType = attributePresentation.Elements("ElementType");
                    if (elementType != null)
                    {
                        var elements = elementType.Elements().Where(attribute => attribute.Name == "Attribute");

                        var attributes = elements.Where(attribute => attribute.Name == "Attribute");

                        foreach (XElement element in attributes)
                        {
                            var attributes1 = element.Attributes().ToDictionary(x => x.Name);
                            element.RemoveAttributes();
                            element.Add(attributes1["attrID"]);
                            element.Add(attributes1["label"]);
                            element.Add(attributes1.ContainsKey("UnitSet")? attributes1["UnitSet"]: new XAttribute("UnitSet", ""));
                            element.Add(attributes1.ContainsKey("UnitLabel") ? attributes1["UnitLabel"] : new XAttribute("UnitLabel", ""));
                            element.Add(attributes1.ContainsKey("Precision")? attributes1["Precision"]: new XAttribute("Precision", "00"));
                            element.Add(attributes1["prompt"]);
                            element.Add(attributes1["visible"]);
                            element.Add(attributes1["readOnly"]);
                            element.Add(attributes1["copy"]);
                            element.Add(attributes1["order"]);
                            element.Add(attributes1["visibleInItemList"]);
                        }
                    }
                }

                xDocument.Save(defaultsPath + Resources.AttributeFileName);
                MessageBox.Show(Resources.SuccessMessage, Resources.DiagramsUpgrade, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Resources.IncorrectFolderMessage, Resources.IncorrectFolderCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }

    public class XmlUpgradeViewModel:INotifyPropertyChanged
    {
        private string _defaultsPath = Resources.DefaultPathPrompt;
        private bool _isEnabled = false;
        public event PropertyChangedEventHandler PropertyChanged;
        private string upgragemessage = string.Empty;
        
        private ICommand _selectCommand;
        private ICommand _upgaredeCommand;

        public string UpgradeMessage {
            get { return upgragemessage; }
            set
            {
                upgragemessage = value;
                OnPropertyChanged();
            } 
        }

        public ICommand SelectCommand
        {
            get { return _selectCommand ?? (_selectCommand = new SelectCommand(this)); }
            set{_selectCommand = value;}
        }

        public ICommand UpgradeCommand
        {
            get { return _upgaredeCommand ?? (_upgaredeCommand = new UpgradeCommand(this)); }
            set{_selectCommand = value;}
        }
        public string DefaultsPath
        {
            set
            {
                _defaultsPath = value;
                IsEnabled = _defaultsPath != Resources.DefaultPathPrompt;
                OnPropertyChanged();
            }
            get { return _defaultsPath; }
        }

        public bool IsEnabled
        {
            private set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
            get { return _isEnabled; }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SelectCommand:ICommand
    {
        private readonly XmlUpgradeViewModel _xUpgradeView;
        private FolderBrowserDialog _dialog;
        public SelectCommand(XmlUpgradeViewModel xmlUpgradeView)
        {
            _xUpgradeView = xmlUpgradeView;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _dialog = new FolderBrowserDialog();
            if (_dialog.ShowDialog() != DialogResult.OK) return;
            if (File.Exists(_dialog.SelectedPath + Resources.AttributeFileName))
                _xUpgradeView.DefaultsPath = _dialog.SelectedPath;
            else if (File.Exists(_dialog.SelectedPath + Resources.Diagrams + Resources.AttributeFileName))
                _xUpgradeView.DefaultsPath = _dialog.SelectedPath + Resources.Diagrams;
            else
                MessageBox.Show(Resources.FileNotFoundMessage, Resources.FileNotFoundCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    class UpgradeCommand:ICommand
    {
        private readonly XmlUpgradeViewModel _xUpgradeView;

        public UpgradeCommand(XmlUpgradeViewModel xUpgradeView)
        {
            this._xUpgradeView = xUpgradeView;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _xUpgradeView.UpgradeMessage = string.Empty;
            if (AttributeUpgradeModel.Upgrade(_xUpgradeView.DefaultsPath))
                _xUpgradeView.UpgradeMessage = Resources.SuccessMessage;
        }
    }
    
}
