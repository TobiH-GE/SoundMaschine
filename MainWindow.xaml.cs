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
        List<PadButton> Pads = new List<PadButton>();

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
                    PadButton temp = new PadButton(counter, Sounds[counter]);

                    temp.Style = FindResource("PadButtons") as Style;
                    PadsGrid.Children.Add(temp);
                    Pads.Add(temp);
                    Grid.SetColumn(temp, x);
                    Grid.SetRow(temp, y);

                    counter++;
                }
            }
        }

        public int getPadID(object sender)
        {
            int tempNumber = 0;

            Int32.TryParse((sender as Button).Name.Substring(4), out tempNumber);

            return tempNumber;
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Pads[selectedSoundID].BorderBrush = Brushes.Black;
            Pads[selectedSoundID].BorderThickness = new Thickness(1);

            Pads[getPadID(sender)].BorderBrush = Brushes.Green;
            Pads[getPadID(sender)].BorderThickness = new Thickness(3);

            Pads[getPadID(sender)].Background = Brushes.Blue;
            Pads[getPadID(sender)].sound.Position = TimeSpan.Zero;
            Pads[getPadID(sender)].sound.Play();
            changeColor(Pads[getPadID(sender)]);


            selectedSoundID = getPadID(sender);
        }

        private void btn_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Pads[getPadID(sender)].sound.Stop();
            Pads[getPadID(sender)].doGradientEffect();
        }

        async void changeColor(PadButton pad)
        {
            for (int i = 0; i < 20; i++)
            {
                await wait(100);
                pad.Background = doGradientEffect(i * 0.05);
            }
        }
        private async Task wait(int ms)
        {
            await Task.Delay(ms);
        }

        private LinearGradientBrush doGradientEffect(double value = 0.75)
        {
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();
            myLinearGradientBrush.StartPoint = new Point(0, 0);
            myLinearGradientBrush.EndPoint = new Point(1, 1);
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Yellow, 0.0));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Red, 0.25));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Blue, value));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.LimeGreen, 1.0));

            return myLinearGradientBrush;
        }
    }
}
