using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;


namespace Siatkostat.Statistics
{
    class SetData
    {
        public double Percents { get; set; }

        public int Index { get; set; }
    }

    public sealed partial class AtakCharts
    {
        private String id;

        private IEnumerable<Set> playerSets;

        private Player player;

        public AtakCharts()
        {
            InitializeComponent();
            
        }
        async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof(StatsWindow), id);
        }
  
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            id = e.Parameter as String;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            MakePunktowy();
        }

        private void MakePunktowy()
        {
            var punktowe = new Dictionary<string, double>();
            var pozostałe = new Dictionary<string, double>();
            var zablokowane = new Dictionary<string, double>();
            var zepsuty = new Dictionary<string, double>();

            player = PlayersViewModel.Instance.PlayersCollection.First(p => p.Id.ToString() == id);
            playerSets = SetViewModel.Instance.SetsCollection.Where(set => set.PlayerId.ToString() == player.Id.ToString());

            foreach (var playerSet in playerSets)
            {

                if (!punktowe.ContainsKey(playerSet.MatchId.ToString()))
                {
                    punktowe.Add(playerSet.MatchId.ToString(), playerSet.SpikeKill);
                }
                else
                {
                    punktowe[playerSet.MatchId.ToString()] += playerSet.SpikeKill;
                }

                if (!pozostałe.ContainsKey(playerSet.MatchId.ToString()))
                {
                    pozostałe.Add(playerSet.MatchId.ToString(), playerSet.SpikeOther);
                }
                else
                {
                    pozostałe[playerSet.MatchId.ToString()] += playerSet.SpikeOther  ;
                }

                if (!zablokowane.ContainsKey(playerSet.MatchId.ToString()))
                {
                    zablokowane.Add(playerSet.MatchId.ToString(), playerSet.SpikeBlocked);
                }
                else
                {
                    zablokowane[playerSet.MatchId.ToString()] += playerSet.SpikeBlocked;
                }

                if (!zepsuty.ContainsKey(playerSet.MatchId.ToString()))
                {
                    zepsuty.Add(playerSet.MatchId.ToString(), playerSet.SpikeFault);
                }
                else
                {
                    zepsuty[playerSet.MatchId.ToString()] += playerSet.SpikeFault;
                }
            }
            var punktoweList = new List<SetData>();
            var pozostałeList = new List<SetData>();
            var zablokowaneList = new List<SetData>();
            var zepsutyList = new List<SetData>();

             punktoweList = punktowe.Keys.Select(key => new SetData {Percents = punktowe[key]}).ToList();
             pozostałeList = pozostałe.Keys.Select(key => new SetData { Percents = pozostałe[key] }).ToList();
             zablokowaneList = zablokowane.Keys.Select(key => new SetData { Percents = zablokowane[key] }).ToList();
             zepsutyList = zepsuty.Keys.Select(key => new SetData { Percents = zepsuty[key] }).ToList();

            for (int i = 0; i < punktoweList.Count; ++i)
            {
                punktoweList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < pozostałeList.Count; ++i)
            {
                pozostałeList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < zablokowaneList.Count; ++i)
            {
                zablokowaneList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < zepsutyList.Count; ++i)
            {
                zepsutyList.ElementAt(i).Index = i;
            }

            (PunktowyChart.Series[0] as LineSeries).ItemsSource = punktoweList;
            (PunktowyChart.Series[0] as LineSeries).IndependentValuePath = "Index";
            (PunktowyChart.Series[0] as LineSeries).DependentValuePath = "Percents";

            (PunktowyChart.Series[1] as LineSeries).ItemsSource = pozostałeList;
            (PunktowyChart.Series[1] as LineSeries).IndependentValuePath = "Index";
            (PunktowyChart.Series[1] as LineSeries).DependentValuePath = "Percents";

            (PunktowyChart.Series[2] as LineSeries).ItemsSource = zablokowaneList;
            (PunktowyChart.Series[2] as LineSeries).IndependentValuePath = "Index";
            (PunktowyChart.Series[2] as LineSeries).DependentValuePath = "Percents";

            (PunktowyChart.Series[3] as LineSeries).ItemsSource = zepsutyList;
            (PunktowyChart.Series[3] as LineSeries).IndependentValuePath = "Index";
            (PunktowyChart.Series[3] as LineSeries).DependentValuePath = "Percents";
        }
    }
}
