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
using Siatkostat.ViewModels;

namespace Siatkostat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EfficiencyThreshold : Page
    {
        public EfficiencyThreshold()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void AttackSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (AttackSlider == null || AttackValue == null) return;
            AttackValue.Text = String.Format("{0}%", (int)AttackSlider.Value);
        }

        private void DefenceSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (ServeSlider == null || ServeValue == null) return;
            ServeValue.Text = String.Format("{0}%", (int)ServeSlider.Value);
        }

        private void ReboundSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (ReboundSlider == null || ReboundValue == null) return;
            ReboundValue.Text = String.Format("{0}%", (int)ReboundSlider.Value);
        }

        private void BlockSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (BlockSlider == null || BlockValue == null) return;
            BlockValue.Text = String.Format("{0}%", (int)BlockSlider.Value);
        }

        private void DalejButton_Click(object sender, RoutedEventArgs e)
        {
            MatchViewModel.Instance.CurrentMatch.AttackThreshold = (int)AttackSlider.Value;
            MatchViewModel.Instance.CurrentMatch.ReboundThreshold = (int)ReboundSlider.Value;
            MatchViewModel.Instance.CurrentMatch.BlockThreshold = (int)BlockSlider.Value;
            MatchViewModel.Instance.CurrentMatch.ServeThreshold = (int)ServeSlider.Value;
            Frame.Navigate(typeof(MainMatch));
        }
    }
}
