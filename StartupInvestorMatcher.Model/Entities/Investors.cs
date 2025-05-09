namespace StartupInvestorMatcher.Model.Entities
{
    public class Investor
    {
        // Constructor to initialize with the associated Member ID
        public Investor(int id)
        {
            InvestorId = id;
            NameInvestor = string.Empty; // Default value
            OverviewInvestor = string.Empty; // Default value
        }

        public Investor(){
            NameInvestor = string.Empty; // Default value
            OverviewInvestor = string.Empty; // Default value
 }
        
        // Foreign key to the Member entity
        public int InvestorId { get; set; }

        // Name of the investor
        public string NameInvestor { get; set; }

        // Description or summary of the investor
        public string OverviewInvestor { get; set; }

        // Country identifier (foreign key or enum reference)
        public int CountryId { get; set; }

        // Industry identifier (foreign key or enum reference)
        public int IndustryId { get; set; }

        // Investment size preference identifier
        public int InvestmentSizeId { get; set; }
}}
