using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using SQLite;

namespace Spending_Tracker.Models
{
    [Table("expenditures")]
    public class Expenditure 
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [Column("_expenditureDate")]
        public DateTime expenditureDate { get; set; }
        public double expenditureValue { get; set; }
        public string expenditureName { get; set; }
        public string FormattedDate
        {
            get { return expenditureDate.ToString("d"); }          
        }

    }
}
