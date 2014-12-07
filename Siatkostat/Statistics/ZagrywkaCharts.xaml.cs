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

    public sealed partial class ZagrywkaCharts
    {
        private String id;

        private IEnumerable<Set> playerSets;

        private Player player;

        public ZagrywkaCharts()
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
            Frame.Navigate(typeof(StatsWindow),id);
        }

        private void MakePunktowy()
        {
            var punktowe = new Dictionary<string, double>();
            var odrzucająca = new Dictionary<string, double>();
            var reszta = new Dictionary<string, double>();
            var zepsuty = new Dictionary<string, double>();

            player = PlayersViewModel.Instance.PlayersCollection.First(p => p.Id.ToString() == id);
            playerSets = SetViewModel.Instance.SetsCollection.Where(set => set.PlayerId.ToString() == player.Id.ToString());

            foreach (var playerSet in playerSets)
            {

                if (!punktowe.ContainsKey(playerSet.MatchId.ToString()))
                {
                    punktowe.Add(playerSet.MatchId.ToString(), playerSet.ServeAce);
                }
                else
                {
                    punktowe[playerSet.MatchId.ToString()] += playerSet.ServeAce;
                }

                if (!odrzucająca.ContainsKey(playerSet.MatchId.ToString()))
                {
                    odrzucająca.Add(playerSet.MatchId.ToString(), playerSet.ServeHit);
                }
                else
                {
                    odrzucająca[playerSet.MatchId.ToString()] += playerSet.ServeHit;
                }

                if (!reszta.ContainsKey(playerSet.MatchId.ToString()))
                {
                    reszta.Add(playerSet.MatchId.ToString(), playerSet.ServeOther);
                }
                else
                {
                    reszta[playerSet.MatchId.ToString()] += playerSet.ServeOther;
                }

                if (!zepsuty.ContainsKey(playerSet.MatchId.ToString()))
                {
                    zepsuty.Add(playerSet.MatchId.ToString(), playerSet.ServeFault);
                }
                else
                {
                    zepsuty[playerSet.MatchId.ToString()] += playerSet.ServeFault;
                }
            }
            var punktoweList = new List<SetData>();
            var odrzucającaList = new List<SetData>();
            var resztaList = new List<SetData>();
            var zepsutyList = new List<SetData>();

            punktoweList = punktowe.Keys.Select(key => new SetData { Percents = punktowe[key] }).ToList();
            odrzucającaList = odrzucająca.Keys.Select(key => new SetData { Percents = odrzucająca[key] }).ToList();
            resztaList = reszta.Keys.Select(key => new SetData { Percents = reszta[key] }).ToList();
            zepsutyList = zepsuty.Keys.Select(key => new SetData { Percents = zepsuty[key] }).ToList();

            for (int i = 0; i < punktoweList.Count; ++i)
            {
                punktoweList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < odrzucającaList.Count; ++i)
            {
                odrzucającaList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < resztaList.Count; ++i)
            {
                resztaList.ElementAt(i).Index = i;
            }

            for (int i = 0; i < zepsutyList.Count; ++i)
            {
                zepsutyList.ElementAt(i).Index = i;
            }

            (ZagrywkaChart.Series[0] as LineSeries).ItemsSource = punktoweList;
            (ZagrywkaChart.Series[0] as LineSeries).IndependentValuePath = "Index";
            (ZagrywkaChart.Series[0] as LineSeries).DependentValuePath = "Percents";

            (ZagrywkaChart.Series[1] as LineSeries).ItemsSource = odrzucającaList;
            (ZagrywkaChart.Series[1] as LineSeries).IndependentValuePath = "Index";
            (ZagrywkaChart.Series[1] as LineSeries).DependentValuePath = "Percents";

            (ZagrywkaChart.Series[2] as LineSeries).ItemsSource = resztaList;
            (ZagrywkaChart.Series[2] as LineSeries).IndependentValuePath = "Index";
            (ZagrywkaChart.Series[2] as LineSeries).DependentValuePath = "Percents";

            (ZagrywkaChart.Series[3] as LineSeries).ItemsSource = zepsutyList;
            (ZagrywkaChart.Series[3] as LineSeries).IndependentValuePath = "Index";
            (ZagrywkaChart.Series[3] as LineSeries).DependentValuePath = "Percents";
        }
    }
}
