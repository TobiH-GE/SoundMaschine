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
        public List<TrackButton> Track = new List<TrackButton>();
        public pgdTrack()
        {
            InitializeComponent();

            Grid contentGrid = Content as Grid;
            ColumnDefinition tempColumn;
            for (int i = 0; i < 50; i++)
            {
                tempColumn = new ColumnDefinition();
                contentGrid.ColumnDefinitions.Add(tempColumn);
            }
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
