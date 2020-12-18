using System;
using System.Collections.Generic;
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
        MediaPlayer[] Sounds = new MediaPlayer[9];
        Pad[] Pads = new Pad[9];
        pgdTrack pgdTrack = new pgdTrack();

        public List<List<MediaPlayer>> Tracks = new List<List<MediaPlayer>>();

        public List<MediaPlayer> getTrack()
        {
            return Tracks[0];
        }

        public MainWindow()
        {
            InitializeComponent();

            Sounds[0] = new MediaPlayer(); Sounds[0].Open(new Uri("./Sounds/Bass/Bass-Atmozzz.wav", UriKind.Relative));
            Sounds[1] = new MediaPlayer(); Sounds[1].Open(new Uri("./Sounds/Bass/Bass-Beanbass.wav", UriKind.Relative));
            Sounds[2] = new MediaPlayer(); Sounds[2].Open(new Uri("./Sounds/Bass/Bass-Knispy.wav", UriKind.Relative));
            Sounds[3] = new MediaPlayer(); Sounds[3].Open(new Uri("./Sounds/Bass/bass-Lavabass 1.wav", UriKind.Relative));
            Sounds[4] = new MediaPlayer(); Sounds[4].Open(new Uri("./Sounds/Bass/bass-Moerasbas 1.wav", UriKind.Relative));
            Sounds[5] = new MediaPlayer(); Sounds[5].Open(new Uri("./Sounds/Bass/bass-Zandwezen 1.wav", UriKind.Relative));
            Sounds[6] = new MediaPlayer(); Sounds[6].Open(new Uri("./Sounds/Bass/bass-Magmabass 1.wav", UriKind.Relative));
            Sounds[7] = new MediaPlayer(); Sounds[7].Open(new Uri("./Sounds/Pad/Pad-Fefifish.wav", UriKind.Relative));
            Sounds[8] = new MediaPlayer(); Sounds[8].Open(new Uri("./Sounds/Pad/pad-FuzzyMosquito 1.wav", UriKind.Relative));

            Pads[0] = new Pad(btn0, 0, Sounds[0]);
            Pads[1] = new Pad(btn1, 1, Sounds[1]);
            Pads[2] = new Pad(btn2, 2, Sounds[2]);
            Pads[3] = new Pad(btn3, 3, Sounds[3]);
            Pads[4] = new Pad(btn4, 4, Sounds[4]);
            Pads[5] = new Pad(btn5, 5, Sounds[5]);
            Pads[6] = new Pad(btn6, 6, Sounds[6]);
            Pads[7] = new Pad(btn7, 7, Sounds[7]);
            Pads[8] = new Pad(btn8, 8, Sounds[8]);

            fraTrack.Content = pgdTrack;
            
        }

        private int getPadID(object sender) //TODO: optimize
        {
            switch (((Button)sender).Name)
            {
                case "btn0": { return 0;}
                case "btn1": { return 1;}
                case "btn2": { return 2;}
                case "btn3": { return 3;}
                case "btn4": { return 4;}
                case "btn5": { return 5;}
                case "btn6": { return 6;}
                case "btn7": { return 7;}
                case "btn8": { return 8;}
                default:
                    return -1;
            }
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Pads[getPadID(sender)].button.Background = Brushes.Green;
            Pads[getPadID(sender)].sound.Position = TimeSpan.Zero;
            Pads[getPadID(sender)].sound.Play();

            //pgdTrack.Track.Add(new TrackButton(0, 1000, Sounds[getPadID(sender)]));

            var temp = new TrackButton(0, 1000, Sounds[getPadID(sender)]);
            Grid contentGrid = pgdTrack.Content as Grid;
            Grid.SetColumn(temp, contentGrid.Children.Count);
            contentGrid.Children.Add(temp);
            pgdTrack.Track.Add(temp);
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
            sound.MediaEnded += (o, e) => button.Background = Brushes.LightGray;
        }
    }
}
