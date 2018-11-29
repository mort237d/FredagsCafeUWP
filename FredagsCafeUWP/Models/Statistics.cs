using System.Collections.ObjectModel;

namespace FredagsCafeUWP.Models
{
    public class Statistics
    {
        
        public double totalIncome;
        public string profitType;


        public Statistics(double totalIncome, string profitType)
        {
            this.totalIncome = totalIncome;
            this.ProfitType = profitType;
        }


        public double TotalIncome
        {
            get { return totalIncome; }
            set { totalIncome = value; }
        }


        public string ProfitType
        {
            get { return profitType; }
            set { profitType = value; }
        }
    }
}
