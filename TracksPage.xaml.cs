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
    public partial class TracksPage : Page
    {
        public List<List<TrackButton>> Track = new List<List<TrackButton>>();
        bool isPlaying = false;
        public TracksPage()
        {
            InitializeComponent();

            Grid contentGrid = TracksGrid;

            for (int y = 0; y < 5; y++)
            {
                Track.Add(new List<TrackButton>());

                for (int x = 0; x < 1000; x++)
                {
                    contentGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    var temp = new TrackButton(0, 0, new MediaPlayer());
                    temp.Style = FindResource("TrackButtons") as Style;
                    contentGrid.Children.Add(temp);
                    Track[y].Add(temp);
                    Grid.SetColumn(temp, x);
                    Grid.SetRow(temp, y);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((TrackButton)sender).Content == null)
            {
                MainWindow wnd = (MainWindow)Application.Current.MainWindow;

                ((TrackButton)sender).Content = (char)(wnd.selectedSoundID + 65);
                ((TrackButton)sender).sound = wnd.Sounds[wnd.selectedSoundID];
            }
            else
            {
                ((TrackButton)sender).Content = null;
                ((TrackButton)sender).sound = null;
            }
        }


        async void PlayTrack()
        {
            isPlaying = true;
            int bpm;

            MainWindow wnd = (MainWindow)Application.Current.MainWindow;

            int.TryParse(wnd.bpm.Text, out bpm);

            for (int i = 0; i < Track[0].Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Track[j][i].sound != null) Track[j][i].sound.Play();
                    Track[j][i].Background = Brushes.Green;
                }
                
                await wait(1000 * 60 / bpm);
                for (int j = 0; j < 5; j++)
                {
                    if (Track[j][i].sound != null) Track[j][i].sound.Stop();
                    Track[j][i].Background = Brushes.LightGray;
                }
                
                if (isPlaying == false) break;
            }
        }

        private async Task wait(int ms)
        {
            await Task.Delay(ms);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying == false)
                PlayTrack();
            else
                isPlaying = false;
        }
    }
}
