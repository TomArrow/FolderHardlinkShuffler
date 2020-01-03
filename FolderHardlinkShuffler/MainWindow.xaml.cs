using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ookii.Dialogs;
using Ookii.Dialogs.Wpf;
using System.IO;
using Microsoft.Win32;

namespace FolderHardlinkShuffler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string srcFolder = "";
        string trgtFolder = "";

        private void BtnSourceFolder_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            if(fbd.ShowDialog() == true)
            {
                srcFolder = fbd.SelectedPath;
                sourceFolder.Text = srcFolder;
                enableButton();
            }
        }

        private void BtnTargetFolder_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            if (fbd.ShowDialog() == true)
            {
                trgtFolder = fbd.SelectedPath;
                targetFolder.Text = trgtFolder;
                enableButton();
            }
        }

        private void enableButton()
        {

            if (trgtFolder == "" || srcFolder == "")
            {
                // Nope.
                return;
            } else
            {
                btnDoit.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(trgtFolder == "" || srcFolder == "")
            {
                // Nope.
                return;
            }

        }

        private void BtnDoit_Click(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.GetFiles(srcFolder);
            var rng = new Random();
            rng.Shuffle(files);

            int zeroFill = int.Parse(txtFillZeros.Text);

            string prefix = txtPrefix.Text;

            string batchfile = "";

            int index = 0;

            batchfile += "SET srcFolder="+srcFolder+"\n";
            batchfile += "SET dstFolder="+trgtFolder+"\n";

            string rawFileName;

            foreach(string filename in files)
            {
                rawFileName = System.IO.Path.GetFileName(filename);
                batchfile += "mklink /h \"%dstFolder%" + System.IO.Path.DirectorySeparatorChar + prefix  + (index++).ToString("D"+zeroFill) + System.IO.Path.GetExtension(filename)+"\" \"%srcFolder%" + System.IO.Path.DirectorySeparatorChar + rawFileName + "\" " +"\n";
            }

            batchfile += "pause\n";

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Batch File | *.bat";
            sfd.DefaultExt = "bat";
            sfd.FileName = "shuffledHardlinksCreate.bat";
            if(sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, batchfile);
            }
        }
    }
}
