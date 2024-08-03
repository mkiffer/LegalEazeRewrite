﻿namespace LegalEazeRewrite.Models.DataModels
{
    public class Lawyer
    {
        public string LawyerID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string LawFirmID { get; set; }
        public LawFirm LawFirm { get; set; }

        public ICollection<LawyerClient> LawyerClients { get; set; }
        public ICollection<LawyerManager> LawyerManagers { get; set; }
    }
}
