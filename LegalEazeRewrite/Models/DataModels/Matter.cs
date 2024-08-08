namespace LegalEazeRewrite.Models.DataModels
{
    public class Matter
    {
        public int MatterID { get; set; }
        public int ClientID { get; set; }
        public Client Client { get; set; }
        public int CourtID { get; set; }
        public Court Court { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
