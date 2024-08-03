namespace LegalEazeRewrite.Models.DataModels
{
    public class Manager
    {
        public string ManagerID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string LawFirmID { get; set; }
        public LawFirm LawFirm { get; set; }

        public ICollection<LawyerManager> LawyerManagers { get; set; }
    }
}
