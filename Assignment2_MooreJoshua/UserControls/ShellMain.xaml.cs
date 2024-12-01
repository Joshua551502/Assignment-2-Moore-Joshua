using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Assignment2_MooreJoshua.Models;
using Assignment2_MooreJoshua.Services;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment2_MooreJoshua.UserControls
{
    /// <summary>
    /// Interaction logic for ShellMainUC.xaml
    /// </summary>
    public partial class ShellMain : UserControl
    {
        private WatchGallery _watchGallery;
        public ShellMain()
        {
            InitializeComponent();
            _watchGallery = new WatchGallery();
            FramePages.Content = _watchGallery;
        }

        #region Main Window Event Handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FramePages.Content = new UserControls.WatchGallery();
        }


        private void CloseBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Page Navigation Buttons

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Content = _watchGallery;
        }

        private void WatchesBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Source = new Uri("/UserControls/WristWatch.xaml", UriKind.Relative);
        }

        private void PocketWatchBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Source = new Uri("/UserControls/PocketWatch.xaml", UriKind.Relative);
        }

        private void WatchBoxBtn_Click(object sender, RoutedEventArgs e)
        {
            FramePages.Source = new Uri("/UserControls/WatchBox.xaml", UriKind.Relative);
        }


        #endregion

    }
}
