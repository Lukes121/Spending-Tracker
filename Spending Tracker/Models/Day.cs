using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Spending_Tracker.Models
{
    //as of 10/14 unsure of whether or not to make seperate table. However this is implemented
    //will likely be a base for Week class
    class Day
    {
        public int Id { get; set; }
        
        public double DayValue { get; set; }
        public DateTime Date { get; set; }
    }
}
