namespace LegalEazeRewrite.Models.DataModels
{
    public class LawyerClient
    {
        public int LawyerID { get; set; }
        public Lawyer Lawyer { get; set; }
        public int ClientID { get; set; }
        public Client Client { get; set; }
    }
}
