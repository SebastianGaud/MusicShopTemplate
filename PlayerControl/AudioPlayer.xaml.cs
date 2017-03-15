using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MusicShop.PlayerControl
{
    /// <summary>
    /// Interaction logic for AudioPlayer.xaml
    /// </summary>
    public partial class AudioPlayer : UserControl
    {
        
        MediaPlayer _mp;

        public AudioPlayer()
        {
            InitializeComponent();

            _mp = new MediaPlayer();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (_mp.Source != null)
            {
                var duration = _mp.NaturalDuration;
                if (duration.HasTimeSpan)
                {
                    lblTimer.Text = string.Format("{0} / {1}", _mp.Position.ToString(@"mm\:ss"), _mp.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }
            }
            else
            {
                lblTimer.Text = "00:00 / 00:00";
                lblTraccia.Text = "Nessuna canzone in riproduzione";
            }
        }

        public void RiproduciTraccia(string titoloTraccia, string urlTraccia)
        {
            lblTraccia.Text = titoloTraccia;

            //hack
            var url = urlTraccia.Replace("https://", "http://");

            _mp.Open(new Uri(url));
            _mp.Play();
        }

        private void btnPlay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mp.Play();
        }

        private void btnPause_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mp.Pause();
        }

        private void btnStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mp.Stop();
        }
    }
}
