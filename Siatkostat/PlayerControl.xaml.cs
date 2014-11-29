using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Siatkostat.Models;

namespace Siatkostat
{
    public sealed partial class PlayerControl : UserControl
    {
        private readonly SolidColorBrush highlightBrush = new SolidColorBrush(Colors.Cyan);

        private readonly SolidColorBrush normalBrush = new SolidColorBrush(Colors.White);

        public bool Selected { get; set; }

        private Player _player;
        public Player player
        {
            get { return _player; }
            set
            {
                _player = value;
                Number.Text = _player.Number.ToString();
                Unselect();
            }
        }

        public PlayerControl()
        {
            this.InitializeComponent();
        }

        public void Select()
        {
            ellipse.Fill = highlightBrush;
            Selected = true;
        }

        public void Unselect()
        {
            ellipse.Fill = normalBrush;
            Selected = false;
        }
    }
}
