using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
/// Interaction logic for MainWindow.xaml
/// </summary>
    public partial class MainWindow : Window
    {
        public MediaPlayer[] Sounds = new MediaPlayer[25];
        List<Pad> Pads = new List<Pad>();
        pgdTrack pgdTrack = new pgdTrack();

        public int selectedSoundID = 0;

        public MainWindow()
        {
            InitializeComponent();

            string[] soundFiles = Directory.GetFiles(@"./Sounds/", "*.wav", SearchOption.AllDirectories);

            Random rnd = new Random();

            for (int i = 0; i < 25; i++)
            {
                Sounds[i] = new MediaPlayer(); Sounds[i].Open(new Uri(soundFiles[rnd.Next(0,soundFiles.Length)], UriKind.Relative));
            }

            int counter = 0;
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Pad tempButton = new Pad(new Button(), counter, Sounds[counter]);

                    tempButton.button.Name = "btn_" + counter.ToString();
                    tempButton.button.Style = FindResource("PadButtons") as Style;
                    PadsGrid.Children.Add(tempButton.button);
                    Pads.Add(tempButton);
                    Grid.SetColumn(tempButton.button, x);
                    Grid.SetRow(tempButton.button, y);

                    counter++;
                }
            }

            fraTrack.Content = pgdTrack;
        }

        public int getPadID(object sender)
        {
            int tempNumber = 0;

            Int32.TryParse((sender as Button).Name.Substring(4), out tempNumber);

            return tempNumber;
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Pads[selectedSoundID].button.BorderBrush = Brushes.Black;
            Pads[selectedSoundID].button.BorderThickness = new Thickness(1);

            Pads[getPadID(sender)].button.BorderBrush = Brushes.Green;
            Pads[getPadID(sender)].button.BorderThickness = new Thickness(3);

            Pads[getPadID(sender)].button.Background = Brushes.Blue;
            Pads[getPadID(sender)].sound.Position = TimeSpan.Zero;
            Pads[getPadID(sender)].sound.Play();

            selectedSoundID = getPadID(sender);
        }

        private void btn_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Pads[getPadID(sender)].sound.Stop();
            Pads[getPadID(sender)].button.Background = Brushes.LightGray;
        }
    }

    public class Pad
    {
        public Button button;
        public int id;
        public MediaPlayer sound;
        public Pad (Button button, int id, MediaPlayer sound)
        {
            this.button = button;
            this.id = id;
            this.sound = sound;

            button.Content = (char)(id + 65);
            sound.MediaEnded += (o, e) => button.Background = Brushes.LightGray;
        }
    }
}
