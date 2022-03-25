using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spending_Tracker.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spending_Tracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllExpendituresPage : ContentPage
    {
        public AllExpendituresPage()
        {
            InitializeComponent();

            RefreshAllAsync();
        }

        private async void OnAllRefreshClicked(object sender, EventArgs e)
        {
            List<Expenditure> allExpenditures = await App.ExpenditureRepo.GetAllExpendituresAsync();
            allExpenditures.Reverse();
            allExpendituresList.ItemsSource = allExpenditures;
        }

        private async void RefreshAllAsync()
        {
            List<Expenditure> allExpenditures = await App.ExpenditureRepo.GetAllExpendituresAsync();
            allExpenditures.Reverse();
            allExpendituresList.ItemsSource = allExpenditures;
        }
    }
}