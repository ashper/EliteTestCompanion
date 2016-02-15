using System.IO;
using System.Reflection;
using System.Windows;

namespace TestingUI
{
    /// <summary>
    /// Interaction logic for VLCPlayer.xaml
    /// </summary>
    public partial class VLCPlayer : Window
    {
        public VLCPlayer()
        {
            InitializeComponent();
            vlcPlayer.MediaPlayer.VlcLibDirectoryNeeded += MediaPlayer_VlcLibDirectoryNeeded;
            vlcPlayer.MediaPlayer.EndInit();
        }

        private void MediaPlayer_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return;

            //TODO: Remove this as 64bit support for VLC is flakey atm?
            //if (AssemblyName.GetAssemblyName(currentAssembly.Location).ProcessorArchitecture == ProcessorArchitecture.X86)
            e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, @"VLCLib\VLC86\"));
            //else
            //    e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, @"lib\x64\"));
        }
    }
}