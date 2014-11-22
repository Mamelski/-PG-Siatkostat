using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Siatkostat.Models;


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

            foreach (var playerControl in Players)
                playerControl.Tapped += ChangeSelection;
        }

        private void ChangeSelection(object sender, TappedRoutedEventArgs e)
        {
            foreach (var playerControl in Players)
                playerControl.Unselect();

            (sender as PlayerControl).Select();
        }

        public void RotatePlayers()
        {
            List<Player> playersFromCourt = Players.Select(p => p.player).ToList();
            playersFromCourt = playersFromCourt.Skip(5).Concat(playersFromCourt.Take(5)).ToList();

            for(int i = 0; i < 6; i++)
            {
                Players[i].player = playersFromCourt[i];
            }
        }
    }
}
