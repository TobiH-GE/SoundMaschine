using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoundMaschine
{
    /// <summary>
    /// Interaktionslogik für pgdTrack.xaml
    /// </summary>
    public partial class pgdTrack : Page
    {
        public List<MediaPlayer> Track = new List<MediaPlayer>();
        public pgdTrack()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime timeStart;
            
            for (int i = 0; i < Track.Count; i++)
            {
                timeStart = DateTime.Now;
                Track[i].Play();

                while ((DateTime.Now - timeStart).TotalMilliseconds < 2000)
                {

                }
                Track[i].Stop();
            }
        }
    }
}
