namespace LegalEazeRewrite.Models.DataModels
{
    public class LawyerClient
    {
        public string LawyerID { get; set; }
        public Lawyer Lawyer { get; set; }
        public string ClientID { get; set; }
        public Client Client { get; set; }
    }
}
