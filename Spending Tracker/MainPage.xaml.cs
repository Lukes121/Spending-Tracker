using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Spending_Tracker.Pages;

namespace Spending_Tracker
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.Children.Add(new QuickSpendPage());
            this.Children.Add(new AllExpendituresPage());
            this.Children.Add(new SettingsPage());
        }
    }
}
