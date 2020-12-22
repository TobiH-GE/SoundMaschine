using System.Windows.Controls;
using System.Windows.Media;

namespace SoundMaschine
{
    public class PadButton : Button
    {    
        public int id;
        public MediaPlayer sound;
        public PadButton (int id, MediaPlayer sound)
        {
            Name = "btn_" + id.ToString();

            this.id = id;
            this.sound = sound;

            Content = (char)(id + 65);
            sound.MediaEnded += (o, e) => Background = Brushes.LightGray;
        }
    }
}
