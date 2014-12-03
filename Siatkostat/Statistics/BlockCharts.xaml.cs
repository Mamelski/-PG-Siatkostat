using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;


namespace Siatkostat.Statistics
{

    public sealed partial class BlockCharts
    {
        private String id;

        private IEnumerable<Set> playerSets;

        private Player player;

        public BlockCharts()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            id = e.Parameter as String;
            MakePunktowy();
        }

        private void MakePunktowy()
        {
            var punktowe = new Dictionary<string, double>();
            var rebound = new Dictionary<string, double>();
            var bladSiatki = new Dictionary<string, double>();

            player = PlayersViewModel.Instance.PlayersCollection.First(p => p.Id.ToString() == id);
            playerSets = SetViewModel.Instance.SetsCollection.Where(set => set.PlayerId.ToString() == player.Id.ToString());

            foreach (var playerSet in playerSets)
            {

                if (!punktowe.ContainsKey(playerSet.MatchId.ToString()))
                {
                    punktowe.Add(playerSet.MatchId.ToString(), playerSet.BlockKill);
                }
                else
                {
                    punktowe[playerSet.MatchId.ToString()] += playerSet.BlockKill;
                }

                if (!rebound.ContainsKey(playerSet.MatchId.ToString()))
                {
                    rebound.Add(playerSet.MatchId.ToString(), playerSet.BlockRebound);
                }
                else
                {
                    rebound[playerSet.MatchId.ToString()] += playerSet.BlockRebound;
                }

                if (!bladSiatki.ContainsKey(playerSet.MatchId.ToString()))
                {
                    bladSiatki.Add(playerSet.MatchId.ToString(), playerSet.BlockFault);
                }
                else
                {
                    bladSiatki[playerSet.MatchId.ToString()] += playerSet.BlockFault;
                }

               
            }
            var punktoweList = new List<SetData>();
            var reboundist = new List<SetData>();
            var bladSiatkiList = new List<SetData>();

            punktoweList = punktowe.Keys.Select(key => new SetData { Percents = punktowe[key] }).ToList();
            reboundist = rebound.Keys.Select(key => new SetData { Percents = rebound[key] }).ToList();
            bladSiatkiList = bladSiatki.Keys.Select(key => new SetData { Percents = bladSiatki[key] }).ToList();

            for (int i = 0; i < punktoweList.Count; ++i)
            {
                punktoweList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < reboundist.Count; ++i)
            {
                reboundist.ElementAt(i).Index = i;
            }

            for (int i = 0; i < bladSiatkiList.Count; ++i)
            {
                bladSiatkiList.ElementAt(i).Index = i;
            }

            (BlokChart.Series[0] as LineSeries).ItemsSource = punktoweList;
            (BlokChart.Series[0] as LineSeries).IndependentValuePath = "Index";
            (BlokChart.Series[0] as LineSeries).DependentValuePath = "Percents";

            (BlokChart.Series[1] as LineSeries).ItemsSource = reboundist;
            (BlokChart.Series[1] as LineSeries).IndependentValuePath = "Index";
            (BlokChart.Series[1] as LineSeries).DependentValuePath = "Percents";

            (BlokChart.Series[2] as LineSeries).ItemsSource = bladSiatkiList;
            (BlokChart.Series[2] as LineSeries).IndependentValuePath = "Index";
            (BlokChart.Series[2] as LineSeries).DependentValuePath = "Percents";

        }
    }
}
