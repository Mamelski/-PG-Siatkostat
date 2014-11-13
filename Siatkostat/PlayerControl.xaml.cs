using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Newtonsoft.Json.Bson;
using Siatkostat.Data.DataModels;

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
            }
        }

        public PlayerControl()
        {
            this.InitializeComponent();
        }

        private void Ellipse_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Selected)
                Unselect();
            else
                Select();
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
