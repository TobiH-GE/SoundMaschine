using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
}
