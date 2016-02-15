using System;
using System.Windows;
using System.Windows.Controls;
using VideoLibrary;

namespace TestingUI
{
    /// <summary>
    /// Interaction logic for Youtube.xaml
    /// </summary>
    public partial class Youtube : Page
    {
        private VLCPlayer Player;

        public Youtube(ref VLCPlayer player)
        {
            InitializeComponent();
            if (player == null)
            {
                player = new VLCPlayer();
                player.ShowInTaskbar = false;
            }
            Player = player;
        }

        public void MainWindowLocationChanged()
        {
            RelocatePlayer();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            RelocatePlayer();
            Player.Show();
            string uri = "https://www.youtube.com/watch?v=XoLJutPfbP0";
            var youTube = YouTube.Default;
            var video = youTube.GetVideo(uri);

            Player.vlcPlayer.MediaPlayer.Play(new Uri(video.Uri));
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Player.vlcPlayer.MediaPlayer.Stop();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RelocatePlayer();
            Player.Visibility = Visibility.Visible;

            if (Player.vlcPlayer.MediaPlayer.State == Vlc.DotNet.Core.Interops.Signatures.MediaStates.Paused)
            {
                Player.vlcPlayer.MediaPlayer.Play();
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Player.Visibility = Visibility.Hidden;
            Player.vlcPlayer.MediaPlayer.Pause();
        }

        private void RelocatePlayer()
        {
            Point locationFromScreen = playerLocationRectangle.PointToScreen(new Point(0, 0));
            Point locationFromWindow = playerLocationRectangle.TranslatePoint(new Point(0, 0), this);

            Player.Height = playerLocationRectangle.ActualHeight;
            Player.Width = playerLocationRectangle.ActualWidth;
            Player.Top = locationFromScreen.Y;
            Player.Left = locationFromScreen.X;
            Player.Focus();
        }
    }
}