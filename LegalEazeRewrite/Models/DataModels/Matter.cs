namespace LegalEazeRewrite.Models.DataModels
{
    public class Matter
    {
        public string MatterID { get; set; }
        public string ClientID { get; set; }
        public Client Client { get; set; }
        public string CourtID { get; set; }
        public Court Court { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
