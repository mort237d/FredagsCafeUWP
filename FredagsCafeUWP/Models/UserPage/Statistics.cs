namespace FredagsCafeUWP
{
    public class Statistics
    {
        public double TotalIncome { get; set; }
        public string ProfitType { get; set; }

        public Statistics(double totalIncome, string profitType)
        {
            TotalIncome = totalIncome;
            ProfitType = profitType;
        }

        public Statistics()
        {
            
        }
    }
}
