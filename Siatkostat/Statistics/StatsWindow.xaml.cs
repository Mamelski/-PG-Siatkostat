using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Playback;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;

namespace Siatkostat.Statistics
{

    public sealed partial class StatsWindow
    {

        #region Fields
        public StatusBarProgressIndicator ProgresIndicator = StatusBar.GetForCurrentView().ProgressIndicator;

        private IEnumerable<Set> playerSets;

        private Player player;
        private string Id;
        #endregion

        #region Constructor
        public StatsWindow()
        {
            InitializeComponent();
            Loaded += StatsWindow_Loaded;
        }
        #endregion


        async void StatsWindow_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
               if (SetViewModel.Instance.SetsCollection != null)
                return;

            ProgresIndicator.Text = "Trwa łączenie z bazą danych";
            await ProgresIndicator.ShowAsync();
        }

        async void Instance_CollectionLoaded(object sender)
        {
            await ProgresIndicator.HideAsync();
            SetViewModel.Instance.CollectionLoaded -= Instance_CollectionLoaded;
            SetData();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Id = e.Parameter as String;
            if (SetViewModel.Instance.SetsCollection == null)
            {
                SetViewModel.Instance.CollectionLoaded += Instance_CollectionLoaded;
            }
            else
            {
                Instance_CollectionLoaded(this);
            }
           
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            await ProgresIndicator.HideAsync();
            Frame.Navigate(typeof(ChoosePlayerForStats));
        }

        private void SetData()
        {
            player = PlayersViewModel.Instance.PlayersCollection.First(p => p.Id.ToString() == Id);
            PlayerInfo.Text = String.Format("{0} {1} {2}",player.FirstName, player.LastName, player.Number);
            playerSets = SetViewModel.Instance.SetsCollection.Where(set => set.PlayerId.ToString() == player.Id.ToString());

            SetAtack();
            SetBlock();
            SetZagrywka();
            SetPrzyjęcie();

        }

        private void SetAtack()
        {
            double atakAcions;
            double spikeBlocked = 0;
            double spikeFault = 0;
            double spikeKill = 0;
            double spikeOther = 0;

            foreach (var playerSet in playerSets)
            {
                spikeBlocked += playerSet.SpikeBlocked;
                spikeFault += playerSet.SpikeFault;
                spikeKill += playerSet.SpikeKill;
                spikeOther += playerSet.SpikeOther;
            }

            atakAcions = spikeBlocked + spikeFault + spikeKill + spikeOther;

            if (atakAcions == 0)
            {
                ZablokowaneAtak.Text += String.Format(" {0} - {1:0.00}%", 0,0);
                ZepsutyAtak.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                PozostałeAtak.Text += String.Format(" {0} - {1:0.00}%",  0,0);
                PunktowyAtak.Text += String.Format(" {0} - {1:0.00}%",  0,0);
                return;
            }
          
            ZablokowaneAtak.Text += String.Format(" {0} - {1:0.00}%", spikeBlocked, spikeBlocked * 100 / atakAcions);
            ZepsutyAtak.Text += String.Format(" {0} - {1:0.00}%", spikeFault, spikeFault * 100 / atakAcions);
            PozostałeAtak.Text += String.Format(" {0} - {1:0.00}%", spikeOther, spikeOther * 100 / atakAcions);
            PunktowyAtak.Text += String.Format(" {0} - {1:0.00}%", spikeKill, spikeKill * 100 / atakAcions);
        }

        private void SetBlock()
        {
            double blokAcions;
            double blockFault = 0;
            double blockKill = 0;
            double blockRebound = 0;

            foreach (var playerSet in playerSets)
            {
                blockFault += playerSet.BlockFault;
                blockKill += playerSet.BlockKill;
                blockRebound += playerSet.BlockRebound;
            }

            blokAcions = blockFault + blockKill + blockRebound;

            if (blokAcions == 0)
            {
                PunktowyBlok.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                ReboundBlok.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                BłądBlok.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
               
                return;
            }

           
            PunktowyBlok.Text += String.Format(" {0} - {1:0.00}%", blockKill, blockKill * 100 / blokAcions);
            ReboundBlok.Text += String.Format(" {0} - {1:0.00}%", blockRebound, blockRebound * 100 / blokAcions);
            BłądBlok.Text += String.Format(" {0} - {1:0.00}%", blockFault, blockFault * 100 / blokAcions);
        }

        private void SetZagrywka()
        {
            double zagrywkaAcions;
            double serveAce = 0;
            double serveFault = 0;
            double serveHit = 0;
            double serveOther = 0;

            foreach (var playerSet in playerSets)
            {
                serveAce += playerSet.ServeAce;
                serveFault += playerSet.ServeFault;
                serveHit += playerSet.ServeHit;
                serveOther += playerSet.ServeOther;
            }



            zagrywkaAcions = serveAce + serveFault + serveHit + serveOther;

            if (zagrywkaAcions == 0)
            {
                PunktowaZagrywka.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                ZepsutaZagrywka.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                OdrzucającaZagrywka.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                ResztaZagrywka.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                return;
            }

            PunktowaZagrywka.Text += String.Format(" {0} - {1:0.00}%", serveAce, serveAce * 100 / zagrywkaAcions);
            ZepsutaZagrywka.Text += String.Format(" {0} - {1:0.00}%", serveFault, serveFault * 100 / zagrywkaAcions);
            OdrzucającaZagrywka.Text += String.Format(" {0} - {1:0.00}%", serveHit, serveHit * 100 / zagrywkaAcions);
            ResztaZagrywka.Text += String.Format(" {0} - {1:0.00}%", serveOther, serveOther * 100 / zagrywkaAcions); 
        }

        private void SetPrzyjęcie()
        {
            double przyjęcieAcions;
            double receiveBad = 0;
            double receiveFault = 0;
            double receiveGood = 0;
            double receivePerfect = 0;

            foreach (var playerSet in playerSets)
            {
                receiveBad += playerSet.ReceiveBad;
                receiveFault += playerSet.ReceiveFault;
                receiveGood += playerSet.ReceiveGood;
                receivePerfect += playerSet.ReceivePerfect;
            }

            przyjęcieAcions = receiveBad + receiveFault + receiveGood + receivePerfect;

            if (przyjęcieAcions == 0)
            {
                ZłePrzyjęcie.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                ZepsutePrzyjęcie.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                PozytywnePrzyjęcie.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                PerfekcyjnePrzyjęcie.Text += String.Format(" {0} - {1:0.00}%", 0, 0);
                return;
            }

            ZłePrzyjęcie.Text += String.Format(" {0} - {1:0.00}%", receiveBad, receiveBad * 100 / przyjęcieAcions);
            ZepsutePrzyjęcie.Text += String.Format(" {0} - {1:0.00}%", receiveFault, receiveFault * 100 / przyjęcieAcions);
            PozytywnePrzyjęcie.Text += String.Format(" {0} - {1:0.00}%", receiveGood, receiveGood * 100 / przyjęcieAcions);
            PerfekcyjnePrzyjęcie.Text += String.Format(" {0} - {1:0.00}%", receivePerfect, receivePerfect * 100 / przyjęcieAcions);
        }

        private void Atak_OnClick(object sender, RoutedEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof (AtakCharts), Id);
        }

        private void Blok_OnClick(object sender, RoutedEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof(BlockCharts), Id);
        }

        private void Zagrywka_OnClick(object sender, RoutedEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof(ZagrywkaCharts), Id);
        }

        private void Przyjęcie_OnClick(object sender, RoutedEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof(PrzyjęcieCharts), Id);
        }
    }
}
