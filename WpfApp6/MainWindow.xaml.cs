using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp6
{
    public partial class MainWindow : Window
    {

        OpenFileDialog openFileDialog = new OpenFileDialog();
        DispatcherTimer timer = new DispatcherTimer();

        string chooseFile;
        int countImages = 6;

        
        public MainWindow()
        {
            InitializeComponent();

            lbox_file.Items.Add(@"C:\Users\OMEN\source\repos\WpfApp6\WpfApp6\Images\antarktida.jpg");
            lbox_file.Items.Add(@"C:\Users\OMEN\source\repos\WpfApp6\WpfApp6\Images\central florida.jpg");
            lbox_file.Items.Add(@"C:\Users\OMEN\source\repos\WpfApp6\WpfApp6\Images\florida.jpg");
            lbox_file.Items.Add(@"C:\Users\OMEN\source\repos\WpfApp6\WpfApp6\Images\italy.jpg");
            lbox_file.Items.Add(@"C:\Users\OMEN\source\repos\WpfApp6\WpfApp6\Images\spain.jpg");

        }


        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (lbox_file.SelectedIndex == countImages - 1)
                lbox_file.SelectedIndex = 0;

            else if (lbox_file.SelectedIndex < countImages)
                lbox_file.SelectedIndex = lbox_file.SelectedIndex + 1;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (lbox_file.SelectedIndex == 0)
                lbox_file.SelectedIndex = countImages - 1;

            else if (lbox_file.SelectedIndex > 0)
                lbox_file.SelectedIndex = lbox_file.SelectedIndex - 1;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
            }

        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            foreach (string fullPath in openFileDialog.FileNames)
                lbox_file.Items.Add(System.IO.Path.GetFullPath(fullPath));

            countImages = lbox_file.Items.Count;

        }

        private void lbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chooseFile = lbox_file.SelectedValue.ToString();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(chooseFile);
            bitmap.EndInit();
            ImageViewer1.Source = bitmap;

            imageInfoLBL.Content = "Name: " + System.IO.Path.GetFileName(chooseFile) + "\n" +
                "Original size: " + bitmap.PixelWidth + " x " + bitmap.PixelHeight;
        }
        
        private void ComboBoxItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (chooseFile != null)
            {
                BitmapImage bitmap2 = new BitmapImage();
                bitmap2.BeginInit();
                bitmap2.UriSource = new Uri(chooseFile);
                bitmap2.Rotation = (Rotation)Enum.Parse(typeof(Rotation), rotateValueCB.SelectionBoxItemStringFormat);
                bitmap2.EndInit();
                ImageViewer1.Source = bitmap2;
            }

        }

        
    }
}
