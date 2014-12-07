using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace Siatkostat.Statistics
{
 
    public sealed partial class PrzyjęcieCharts 
    {
        private String id;

        private IEnumerable<Set> playerSets;

        private Player player;


        public PrzyjęcieCharts()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            id = e.Parameter as String;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            MakePunktowy();
        }
        async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof(StatsWindow), id);
        }
        private void MakePunktowy()
        {
            var perfekcyjne = new Dictionary<string, double>();
            var pozytywne = new Dictionary<string, double>();
            var złe = new Dictionary<string, double>();
            var zepsute = new Dictionary<string, double>();

            player = PlayersViewModel.Instance.PlayersCollection.First(p => p.Id.ToString() == id);
            playerSets = SetViewModel.Instance.SetsCollection.Where(set => set.PlayerId.ToString() == player.Id.ToString());

            foreach (var playerSet in playerSets)
            {

                if (!perfekcyjne.ContainsKey(playerSet.MatchId.ToString()))
                {
                    perfekcyjne.Add(playerSet.MatchId.ToString(), playerSet.ReceivePerfect);
                }
                else
                {
                    perfekcyjne[playerSet.MatchId.ToString()] += playerSet.ReceivePerfect;
                }

                if (!pozytywne.ContainsKey(playerSet.MatchId.ToString()))
                {
                    pozytywne.Add(playerSet.MatchId.ToString(), playerSet.ReceiveGood);
                }
                else
                {
                    pozytywne[playerSet.MatchId.ToString()] += playerSet.ReceiveGood;
                }

                if (!złe.ContainsKey(playerSet.MatchId.ToString()))
                {
                    złe.Add(playerSet.MatchId.ToString(), playerSet.ReceiveBad);
                }
                else
                {
                    złe[playerSet.MatchId.ToString()] += playerSet.ReceiveBad;
                }

                if (!zepsute.ContainsKey(playerSet.MatchId.ToString()))
                {
                    zepsute.Add(playerSet.MatchId.ToString(), playerSet.ReceiveFault);
                }
                else
                {
                    zepsute[playerSet.MatchId.ToString()] += playerSet.ReceiveFault;
                }
            }
            var perfekcyjneList = new List<SetData>();
            var pozytywneList = new List<SetData>();
            var złeList = new List<SetData>();
            var zepsuteList = new List<SetData>();

            perfekcyjneList = perfekcyjne.Keys.Select(key => new SetData { Percents = perfekcyjne[key] }).ToList();
            pozytywneList = pozytywne.Keys.Select(key => new SetData { Percents = pozytywne[key] }).ToList();
            złeList = złe.Keys.Select(key => new SetData { Percents = złe[key] }).ToList();
            zepsuteList = zepsute.Keys.Select(key => new SetData { Percents = zepsute[key] }).ToList();

            for (int i = 0; i < perfekcyjneList.Count; ++i)
            {
                perfekcyjneList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < pozytywneList.Count; ++i)
            {
                pozytywneList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < złeList.Count; ++i)
            {
                złeList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < zepsuteList.Count; ++i)
            {
                zepsuteList.ElementAt(i).Index = i;
            }

            (PrzyjęcieChart.Series[0] as LineSeries).ItemsSource = perfekcyjneList;
            (PrzyjęcieChart.Series[0] as LineSeries).IndependentValuePath = "Index";
            (PrzyjęcieChart.Series[0] as LineSeries).DependentValuePath = "Percents";

            (PrzyjęcieChart.Series[1] as LineSeries).ItemsSource = pozytywneList;
            (PrzyjęcieChart.Series[1] as LineSeries).IndependentValuePath = "Index";
            (PrzyjęcieChart.Series[1] as LineSeries).DependentValuePath = "Percents";

            (PrzyjęcieChart.Series[2] as LineSeries).ItemsSource = złeList;
            (PrzyjęcieChart.Series[2] as LineSeries).IndependentValuePath = "Index";
            (PrzyjęcieChart.Series[2] as LineSeries).DependentValuePath = "Percents";

            (PrzyjęcieChart.Series[3] as LineSeries).ItemsSource = zepsuteList;
            (PrzyjęcieChart.Series[3] as LineSeries).IndependentValuePath = "Index";
            (PrzyjęcieChart.Series[3] as LineSeries).DependentValuePath = "Percents";
        }
    }
}
