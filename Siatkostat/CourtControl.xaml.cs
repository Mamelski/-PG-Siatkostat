using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Input;
using Siatkostat.Models;
using Siatkostat.ViewModels;


namespace Siatkostat
{
    public sealed partial class CourtControl
    {
        public enum Position
        {
            LeftAttack,
            MiddleAttack,
            RightAttack,
            Serve,
            LeftDefense,
            MiddleDefense
        };

        public enum CurrentTeam
        {
            Team,
            Opponent
        };

        private static CurrentTeam? currentTeam = null;

        public delegate void PlayerSelect(object sender);

        public event PlayerSelect OnPlayerSelect;

        public List<PlayerControl> Players = new List<PlayerControl>();

        public PlayerControl SelectedPlayer { get; set; }
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
            {
                playerControl.Tapped += ChangeSelection;
                playerControl.Tapped += PlayerSelected;
            }
        }

        //TODO: set current team before match begins!!!!
        public void PointFor(CurrentTeam team)
        {
            if (currentTeam == CurrentTeam.Opponent && team == CurrentTeam.Team)
            {
                RotatePlayers();
            }
            currentTeam = team;
        }

        public void SetPlayersOnCourt(Collection<Player> players)
        {
            for (int i = 0; i < 6; i++)
            {
                Players[i].player = players[i];
            }
        }

        private void PlayerSelected(object sender, TappedRoutedEventArgs e)
        {
            SelectedPlayer = sender as PlayerControl;
            if(OnPlayerSelect != null)
                OnPlayerSelect.Invoke(sender);
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

            PlayersViewModel.Instance.SetPlayersOnCourt(Players.Select(p => p.player));
        }

        public bool ValidatePositions()
        {
            if (Player1.player.IsLibero || Player2.player.IsLibero || Player3.player.IsLibero)
                return false;
            return true;
        }

        public void SelectPlayerOnPosition(Position position)
        {
            switch (position)
            {
                case Position.LeftAttack:
                    Players[0].Select();
                    SelectedPlayer = Players[0];
                    break;
                case Position.MiddleAttack:
                    Players[1].Select();
                    SelectedPlayer = Players[1];
                    break;
                case Position.RightAttack:
                    Players[2].Select();
                    SelectedPlayer = Players[2];
                    break;
                case Position.Serve:
                    Players[3].Select();
                    SelectedPlayer = Players[3];
                    break;
                case Position.MiddleDefense:
                    Players[4].Select();
                    SelectedPlayer = Players[4];
                    break;
                case Position.LeftDefense:
                    Players[5].Select();
                    SelectedPlayer = Players[5];
                    break;
            }
        }
    }
}
