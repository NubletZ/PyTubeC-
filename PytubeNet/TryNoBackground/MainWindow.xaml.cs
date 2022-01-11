using Microsoft.Win32;
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
            string curPath = Path.GetFullPath(".");
            Installer.InstallPath = @$"{curPath}"; //to declare workdir
            Debug.WriteLine($"Working directory : {curPath}");
            await Installer.SetupPython();
            PythonEngine.PythonPath = @$"{curPath}\python-3.7.3-embed-amd64;{curPath}\python-3.7.3-embed-amd64\python37.zip;{curPath}\python-3.7.3-embed-amd64\Lib\site-packages;";
            Debug.WriteLine($"python path = { PythonEngine.PythonPath}");
            PythonEngine.Initialize();
            if (Installer.IsPipInstalled()) Debug.WriteLine("Pip has been installed :)");

            pytube = PythonEngine.ImportModule("pytube");
        }

        public static bool pyInit { get; set; } = false;
        public static dynamic pytube { get; set; } = null;
        //function for test only
        public static void startPy()
        {
            Installer.InstallPath = Path.GetFullPath("."); //to declare workdir
            Debug.WriteLine($"Working directory : {Path.GetFullPath(".")}");
            Installer.SetupPython();
            string curPath = Directory.GetCurrentDirectory();
            PythonEngine.PythonPath = @$"{curPath}\python-3.7.3-embed-amd64\python37.zip;{curPath}\python-3.7.3-embed-amd64;{curPath}\python-3.7.3-embed-amd64\Lib\site-packages;{curPath}\python-3.7.3-embed-amd64\Lib\site-packages\numpy\core";
            PythonEngine.Initialize();
            if (Installer.IsPipInstalled()) Debug.WriteLine("Pip has been installed :)");
            pyInit = PythonEngine.IsInitialized;

            pytube = PythonEngine.ImportModule("pytube");
            recordpath = Directory.GetCurrentDirectory();
            string tmp = "https://www.youtube.com/watch?v=sHD_z90ZKV0";
            pytube.__main__.YouTube(tmp).streams.get_highest_resolution().download(recordpath);
        }

        public static void pyVersion()
        {
            Console.WriteLine(PythonEngine.Version);
        }


        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            //cv2.destroyAllWindows();
        }

        private void urlLabel_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tmp = urlLabel.Text;
            //test.Content = tmp;
            if(urlLabel.Text == "")
            {
                UrlCheck.Content = "Please input the URL";
            }
            else if(pathLabel.Text == "")
            {
                PathCheck.Content = "Please select the folder";
            }
            else
            {
                UrlCheck.Content = "";
                PathCheck.Content = "";
                pytube.__main__.YouTube(tmp).streams.get_highest_resolution().download(recordpath);
                MessageBox.Show(pytube.__main__.YouTube(tmp).streams.filter());
            }
        }


        public static string recordpath { get; set; }
        private void openFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.ValidateNames = false;
            openFile.CheckFileExists = false;
            openFile.CheckPathExists = true;
            openFile.FileName = "Folder Selection.";
            if (openFile.ShowDialog() == true)
            {
                string folderPath = Path.GetDirectoryName(openFile.FileName);
                recordpath = Path.GetFullPath(folderPath);
                pathLabel.Text = recordpath;
            }
        }
    }
}
