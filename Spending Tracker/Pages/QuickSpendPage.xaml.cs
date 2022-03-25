using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spending_Tracker.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//10/5 10:08, finish applying API to this page

namespace Spending_Tracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickSpendPage : ContentPage
    {
        public QuickSpendPage()
        {
            InitializeComponent();
        }

        private async void onAddExpenseClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(expenditureName.Text))
            {
                await App.ExpenditureRepo.AddNewExpenditureAsync(expenditureCost.Text);
            }
            else if (!string.IsNullOrWhiteSpace(expenditureName.Text))
            {
                await App.ExpenditureRepo.AddNewExpenditureAsync(expenditureCost.Text, expenditureName.Text);
            }
        }

        private async void OnQuickRefreshClicked(object sender, EventArgs e)
        {
            //Retrieving last 7 expenses to show on splash screen
            List<Expenditure> allExpenditures = await App.ExpenditureRepo.GetAllExpendituresAsync();
            var recentId = allExpenditures.Max(t => t.Id);
            var quickExpendituresQuery = from x in allExpenditures
                                     where x.Id >= (recentId - 7)
                                     orderby x.Id descending
                                     select x;
            List<Expenditure> quickExpenditures = quickExpendituresQuery.ToList<Expenditure>();
            quickExpendituresList.ItemsSource = quickExpenditures;

            //Query to retrive months worth of expenses to show last 30 day spending on splash screen
            //var today = allExpenditures.Max(t => t.expenditureDate.Date);

            DateTime today;
            today = DateTime.Today;
            DateTime pastMonth;
            pastMonth = DateTime.Today.AddMonths(-1);
            var monthExpendituresQuery = from x in allExpenditures
                                         where DateTime.Compare(x.expenditureDate.Date, pastMonth) > 0
                                         orderby x.Id descending
                                         select x;
            List<Expenditure> monthExpenditures = monthExpendituresQuery.ToList<Expenditure>();
           //Logic to get last 30 days worth of values
            List<Day> daysList = new List<Day>();
            DateTime dayItr = DateTime.Today;
            int itr = 0;
            double dayValue = 0;

            //Creates a list of Days for all the expenses in the previous 30 days from today
            try
            {
                while (dayItr != pastMonth && itr < monthExpenditures.Count)
                {
                    //these variable are to check if the listed obj is still on a given day. The goal of the loop
                    //is to add all values of the same date, and create a new Day obj
                    bool itrCheck = false;
                    itrCheck = monthExpenditures[itr].expenditureDate == dayItr;

                    //Keep adding up list item expense values for items of the same date to get a whole days expenditure.
                    //[dayItr] will be compared againt item date
                    //and create a true check condition if the date is the same. List index iterator [itr]
                    //will keep iterating to traverse list until dates do not match. Then the date
                    //will be iterated in [dayItr] until a matching date to the current item is reached
                    if (itrCheck)
                    {
                        dayValue += monthExpenditures[itr].expenditureValue;
                        itr++;
                    }
                    else
                    {
                        if (dayValue > 0)
                        {
                            daysList.Add(new Day() { Date = dayItr.Date, DayValue = dayValue });
                        }
                        dayValue = 0;
                        dayItr = dayItr.AddDays(-1);
                    }                   
                }
            }
            catch(Exception ex)
            {
                DisplayAlert("error", ex.Message, "exit");
            }

            //Indexes a list of days that are 30 days from Today, and adds all of their value
            double monthValue = 0;
            itr = 0;
            while (itr < daysList.Count)
            { 
                monthValue += daysList[itr].DayValue;
                itr++;
            }
            lastMonth.Text = monthValue.ToString("c");
                
            
        }
    }
}