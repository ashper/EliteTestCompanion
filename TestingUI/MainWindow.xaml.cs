using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestingUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static VLCPlayer player;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBookmarks_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new Bookmarks());
        }

        private void btnYoutube_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new Youtube(ref player));
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            if (_mainFrame.HasContent)
            {
                if (_mainFrame.Content.GetType() == typeof(Youtube))
                {
                    Youtube youtube = (Youtube)_mainFrame.Content;
                    youtube.MainWindowLocationChanged();
                }
            }
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}