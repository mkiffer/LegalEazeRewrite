namespace LegalEazeRewrite.Models.DataModels
{
    public class LawyerManager
    {
        public string LawyerID { get; set; }
        public Lawyer Lawyer { get; set; }
        public string ManagerID { get; set; }
        public Manager Manager { get; set; }
    }
}
