using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using Siatkostat.Models;

namespace Siatkostat.ViewModels
{
    public class SetViewModel
    {

        #region Lazy singleton elements
        private static readonly Lazy<SetViewModel> LazyInstance = new Lazy<SetViewModel>(() => new SetViewModel());

        public static SetViewModel Instance
        {
            get
            {
                return LazyInstance.Value;
            }
        }

        #endregion

        #region Events
        public delegate void SetsCollectionLoaded(object sender);

        public event SetsCollectionLoaded CollectionLoaded;

        protected virtual void OnCollectionLoaded()
        {
            if (CollectionLoaded != null)
                CollectionLoaded(this);
        }

        #endregion

        #region Data Objects
        private readonly IMobileServiceTable<Set> setsTable = App.MobileService.GetTable<Set>();

        public MobileServiceCollection<Set, Set> SetsCollection { get; set; }
        #endregion

        #region Constructor
        private SetViewModel()
        {
            RefreshSets();
        }
        #endregion

        #region Properties
        public List<Set> CurrentMatchSets = new List<Set>(); 
        #endregion

        public async void RefreshSets()
        {
            if (App.SelectedTeam == null)
            {
                return;
            }

            MobileServiceInvalidOperationException exception = null;
            SetsCollection = null;
            try
            {
                SetsCollection = await setsTable
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

        public async void InsertMatch(Set set)
        {
            await setsTable.InsertAsync(set);
            SetsCollection.Add(set);
        }
        #endregion

        public void InsertSets(int currentSet)
        {
            foreach (var set in CurrentMatchSets.FindAll(s => s.SetNumber == currentSet))
            {
                InsertMatch(set);
            }
        }
    }
}