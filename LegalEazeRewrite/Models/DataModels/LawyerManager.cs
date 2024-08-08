namespace LegalEazeRewrite.Models.DataModels
{
    public class LawyerManager
    {
        public int LawyerID { get; set; }
        public Lawyer Lawyer { get; set; }
        public int ManagerID { get; set; }
        public Manager Manager { get; set; }
    }
}
