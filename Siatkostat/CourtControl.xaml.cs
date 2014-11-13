using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Siatkostat
{
    public sealed partial class CourtControl : UserControl
    {
        public List<PlayerControl> Players = new List<PlayerControl>();
        public CourtControl()
        {
            this.InitializeComponent();
            Players.Add(Player1);
            Players.Add(Player2);
            Players.Add(Player3);
            Players.Add(Player4);
            Players.Add(Player5);
            Players.Add(Player6);
        }
    }
}
