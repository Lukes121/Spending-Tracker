using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Spending_Tracker.Models;
using System.Threading.Tasks;

namespace Spending_Tracker
{
    public class ExpenditureRepository
    {
        SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }

        public ExpenditureRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Expenditure>().Wait();
        }

        //Add expenditure methods
        public async Task AddNewExpenditureAsync(string expenditureValue)
        {
            double expenditure = 0;

            try
            {

                if (double.TryParse(expenditureValue, out expenditure))
                {
                    await conn.InsertAsync(new Expenditure { expenditureDate = DateTime.Today, expenditureName = "", expenditureValue = expenditure });

                    StatusMessage = string.Format("Expenditure: {0}\nNo Name", expenditure);
                }

                //unsure if necessary, check back for usefulness
                else
                {
                    StatusMessage = string.Format("Invalid Value");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add. Error: {0}", ex.Message);
            }
        }
        //Overload for assigning a Name to the expenditure
        public async Task AddNewExpenditureAsync(string expenditureValue, string expenditureName)
        {
            double expenditure = 0;

            try
            {
                if (double.TryParse(expenditureValue, out expenditure))
                {
                    await conn.InsertAsync(new Expenditure { expenditureDate = DateTime.Today, expenditureName = expenditureName, expenditureValue = expenditure });

                    StatusMessage = string.Format("Expenditure: {0}\n{1}", expenditure, expenditureName);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed. Error: {0}", ex.Message);
            }
        }

        public async Task<List<Expenditure>> GetAllExpendituresAsync()
        {
            try
            {
                return await conn.Table<Expenditure>().ToListAsync();
            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Failed. Error: {0}", ex.Message);
            }
            return new List<Expenditure>();
        }

        public async Task DeleteExpenditureAsync(Expenditure expenditure)
        {
            try
            {
                await conn.DeleteAsync(expenditure);
            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Failed. Error: {0}", ex.Message);
            }
        }
    }
}
