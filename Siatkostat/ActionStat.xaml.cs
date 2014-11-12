﻿using System;
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
using Siatkostat.Data.DataProviders;
using Siatkostat.Data.DataModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Siatkostat
{
    public enum Grade
    {
        Perfect,
        Positive,
        Bad,
        Broken
    }

    public sealed partial class ActionStat : Page
    {
        private MatchProvider matchProvider = new MatchProvider();
        private SetProvider setProvider = new SetProvider();
        private TeamsProvider teamProvider = new TeamsProvider();
        private PlayersProvider playerProvider = new PlayersProvider();

        public ActionStat()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ActionTypeButton_Click(object sender, RoutedEventArgs e)
        {
            ServeButton.IsChecked = false;
            PrzyjecieButton.IsChecked = false;
            AttackButton.IsChecked = false;
            BlockButton.IsChecked = false;
            AnotherFaultButton.IsChecked = false;

            ToggleButton activeButton = sender as ToggleButton;
            activeButton.IsChecked = true;
        }

        private void ServeButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            ServeButton.IsChecked = true;

            HideGradeStackPanels();
            ServeStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void HideGradeStackPanels()
        {
            ServeStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            PrzyjecieStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AttackStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            BlockStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AnotherFaultStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void UncheckAllActionTypeButtons()
        {
            ServeButton.IsChecked = false;
            PrzyjecieButton.IsChecked = false;
            AttackButton.IsChecked = false;
            BlockButton.IsChecked = false;
            AnotherFaultButton.IsChecked = false;

            /*// Zagrywka
            PointServeButton.IsChecked = false;
            OdrzucajacaButton.IsChecked = false;
            ResztaServeButton.IsChecked = false;
            BrokenServeButton.IsChecked = false;

            // Przyjecie
            PerfectSaveButton.IsChecked = false;
            PositiveSaveButton.IsChecked = false;
            BadSaveButton.IsChecked = false;
            BrokenSaveButton.IsChecked = false;

            // Atak
            PointAttackButton.IsChecked = false;
            OtherAttackButton.IsChecked = false;
            BlockedAttackButton.IsChecked = false;
            BrokenAttackButton.IsChecked = false;*/

            //
        }

        private void PrzyjecieButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            PrzyjecieButton.IsChecked = true;

            HideGradeStackPanels();
            PrzyjecieStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            AttackButton.IsChecked = true;

            HideGradeStackPanels();
            AttackStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            BlockButton.IsChecked = true;

            HideGradeStackPanels();
            BlockStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void AnotherFaultButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            AnotherFaultButton.IsChecked = true;

            HideGradeStackPanels();
            AnotherFaultStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
