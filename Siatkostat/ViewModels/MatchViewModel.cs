using System;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Models;

namespace Siatkostat.ViewModels
{
    public class MatchViewModel
    {

        #region Lazy singleton elements
        private static readonly Lazy<MatchViewModel> LazyInstance = new Lazy<MatchViewModel>(() => new MatchViewModel());

        public static MatchViewModel Instance
        {
            get
            {
                return LazyInstance.Value;
            }
        }

        #endregion

        #region Events
        public delegate void MatchesCollectionLoaded(object sender);

        public event MatchesCollectionLoaded CollectionLoaded;

        protected virtual void OnCollectionLoaded()
        {
            if (CollectionLoaded != null)
                CollectionLoaded(this);
        }

        #endregion

        #region Data Objects
        private readonly IMobileServiceTable<Match> matchesTable = App.MobileService.GetTable<Match>();

        public MobileServiceCollection<Match, Match> MatchesCollection { get; set; }

        public Match CurrentMatch { get; set; }
        #endregion

        #region Constructor
        private MatchViewModel()
        {
            RefreshMatches();
        }
        #endregion

        public async void RefreshMatches()
        {
            if (App.SelectedTeam == null)
            {
                return;
            }

            MobileServiceInvalidOperationException exception = null;
            MatchesCollection = null;
            try
            {
                MatchesCollection = await matchesTable.Where(
                    match => match.TeamId.ToString() == App.SelectedTeam.Id.ToString())
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                OnCollectionLoaded();
            }
        }

        #region DataOperations
        public async void InsertMatch(Match match)
        {
            await matchesTable.InsertAsync(match);
            MatchesCollection.Add(match);
        }
        #endregion
    }
}