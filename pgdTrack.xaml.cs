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
    public partial class pgdTrack : Page
    {
        public List<List<TrackButton>> Track = new List<List<TrackButton>>();
        bool isPlaying = false;
        public pgdTrack()
        {
            InitializeComponent();

            Grid contentGrid = TracksGrid;
            ColumnDefinition tempColumn;

            for (int i = 0; i < 5; i++)
            {
                Track.Add(new List<TrackButton>());

                for (int j = 0; j < 1000; j++)
                {
                    tempColumn = new ColumnDefinition();
                    contentGrid.ColumnDefinitions.Add(tempColumn);

                    var tempButton = new TrackButton(0, 0, new MediaPlayer());
                    tempButton.Style = FindResource("TrackButtons") as Style;
                    contentGrid.Children.Add(tempButton);
                    Track[i].Add(tempButton);
                    Grid.SetColumn(tempButton, j);
                    Grid.SetRow(tempButton, i);
                }
            }
        }
        public void AddSoundButton(int id, int delay, MediaPlayer sound)
        {
            
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
            //aTextbox.Text = $"sounds: {aTrack.Count}";
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
