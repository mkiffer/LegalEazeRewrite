namespace LegalEazeRewrite.Models.DataModels
{
    public class Client
    {
        public string ClientID { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public ICollection<LawyerClient> LawyerClients { get; set; }
        public ICollection<Matter> Matters { get; set; }
    }
}
