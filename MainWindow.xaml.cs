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
        public MediaPlayer[] Sounds = new MediaPlayer[25];
        List<Pad> Pads = new List<Pad>();
        pgdTrack pgdTrack = new pgdTrack();

        public int selectedSoundID = 0;

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
            Sounds[9] = new MediaPlayer(); Sounds[9].Open(new Uri("./Sounds/Pad/pad-FuzzyMosquito 1.wav", UriKind.Relative));

            Sounds[10] = new MediaPlayer(); Sounds[10].Open(new Uri("./Sounds/Bass/Bass-Atmozzz.wav", UriKind.Relative));
            Sounds[11] = new MediaPlayer(); Sounds[11].Open(new Uri("./Sounds/Bass/Bass-Beanbass.wav", UriKind.Relative));
            Sounds[12] = new MediaPlayer(); Sounds[12].Open(new Uri("./Sounds/Bass/Bass-Knispy.wav", UriKind.Relative));
            Sounds[13] = new MediaPlayer(); Sounds[13].Open(new Uri("./Sounds/Bass/bass-Lavabass 1.wav", UriKind.Relative));
            Sounds[14] = new MediaPlayer(); Sounds[14].Open(new Uri("./Sounds/Bass/bass-Moerasbas 1.wav", UriKind.Relative));
            Sounds[15] = new MediaPlayer(); Sounds[15].Open(new Uri("./Sounds/Bass/bass-Zandwezen 1.wav", UriKind.Relative));
            Sounds[16] = new MediaPlayer(); Sounds[16].Open(new Uri("./Sounds/Bass/bass-Magmabass 1.wav", UriKind.Relative));
            Sounds[17] = new MediaPlayer(); Sounds[17].Open(new Uri("./Sounds/Pad/Pad-Fefifish.wav", UriKind.Relative));
            Sounds[18] = new MediaPlayer(); Sounds[18].Open(new Uri("./Sounds/Pad/pad-FuzzyMosquito 1.wav", UriKind.Relative));
            Sounds[19] = new MediaPlayer(); Sounds[19].Open(new Uri("./Sounds/Pad/pad-FuzzyMosquito 1.wav", UriKind.Relative));

            Sounds[20] = new MediaPlayer(); Sounds[20].Open(new Uri("./Sounds/Bass/Bass-Atmozzz.wav", UriKind.Relative));
            Sounds[21] = new MediaPlayer(); Sounds[21].Open(new Uri("./Sounds/Bass/Bass-Beanbass.wav", UriKind.Relative));
            Sounds[22] = new MediaPlayer(); Sounds[22].Open(new Uri("./Sounds/Bass/Bass-Knispy.wav", UriKind.Relative));
            Sounds[23] = new MediaPlayer(); Sounds[23].Open(new Uri("./Sounds/Bass/bass-Lavabass 1.wav", UriKind.Relative));
            Sounds[24] = new MediaPlayer(); Sounds[24].Open(new Uri("./Sounds/Bass/bass-Moerasbas 1.wav", UriKind.Relative));


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
