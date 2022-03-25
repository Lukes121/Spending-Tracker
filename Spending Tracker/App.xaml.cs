using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Spending_Tracker.Pages;

namespace Spending_Tracker
{
    public partial class App : Application
    {
        string dbPath => FileAccessHelper.GetLocalFilePath("spendingtracker.db3");

        public static ExpenditureRepository ExpenditureRepo { get; private set; }

        public App()
        {
            InitializeComponent();

            ExpenditureRepo = new ExpenditureRepository(dbPath);

            MainPage = new Spending_Tracker.MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
/* TODO List
 * (Event Handler inheritance? There's going to be refresh buttons on both pages, with similar functionality)
 * Finish Aggregation of day values, decide whether extra table needed
 * Design week value aggregation. Should be a pretty straightforward linq query
 * Bind appropriate values to corresponding UI
 * Finish CRUD functionality
 * Add settings page, design UI, add main feature = remove all expenses, request biometric/password authentication
 * Clean up Code
 */
