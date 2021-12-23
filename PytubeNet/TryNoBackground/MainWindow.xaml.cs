using Python.Included;
using Python.Runtime;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace TryNoBackground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Installer.InstallPath = Path.GetFullPath("."); //to declare workdir
            Debug.WriteLine($"Working directory : {Path.GetFullPath(".")}");
            await Installer.SetupPython();
            string curPath = Directory.GetCurrentDirectory();
            PythonEngine.PythonPath = @$"{curPath}\python-3.7.3-embed-amd64\python37.zip;{curPath}\python-3.7.3-embed-amd64\Lib\site-packages;{curPath}\python-3.7.3-embed-amd64\Lib\site-packages\numpy\core";
            PythonEngine.Initialize();
        }

        public static bool pyInit { get; set; } = false;
        public static void startPy()
        {
            Installer.InstallPath = Path.GetFullPath("."); //to declare workdir
            Debug.WriteLine($"Working directory : {Path.GetFullPath(".")}");
            Installer.SetupPython();
            string curPath = Directory.GetCurrentDirectory();
            PythonEngine.PythonPath = @$"{curPath}\python-3.7.3-embed-amd64\python37.zip;{curPath}\python-3.7.3-embed-amd64\Lib\site-packages;{curPath}\python-3.7.3-embed-amd64\Lib\site-packages\numpy\core";
            PythonEngine.Initialize();
            pyInit = PythonEngine.IsInitialized;
        }

        public static void pyVersion()
        {
            Console.WriteLine(PythonEngine.Version);
        }


        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            //cv2.destroyAllWindows();
        }
    }
}
