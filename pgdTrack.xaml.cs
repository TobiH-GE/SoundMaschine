using System;
using System.Collections.Generic;
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

namespace SoundMaschine
{
    /// <summary>
    /// Interaktionslogik für pgdTrack.xaml
    /// </summary>
    public class TrackButton : Button
    {
        public int id;
        public int delay;
        public MediaPlayer sound;

        public TrackButton(int id, int delay, MediaPlayer sound)
        {
            this.id = id;
            this.delay = delay;
            this.sound = sound;
        }
    }
    public partial class pgdTrack : Page
    {
        public List<TrackButton> Track = new List<TrackButton>();
        public pgdTrack()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayTrack(Track);
        }

        async void PlayTrack(List<TrackButton> aTrack)
        {
            bool stillWorking = true;

            //aTextbox.Text = $"sounds: {aTrack.Count}";

            while (stillWorking)
            {
                
                for (int i = 0; i < aTrack.Count; i++)
                {
                    aTrack[i].sound.Play();
                    aTrack[i].Background = Brushes.Green;
                    await wait(1000);
                    aTrack[i].sound.Stop();
                aTrack[i].Background = Brushes.LightGray;
                }
                
                stillWorking = false;
            }
        }

        private async Task wait(int ms)
        {
            await Task.Delay(ms);
        }
    }
}
