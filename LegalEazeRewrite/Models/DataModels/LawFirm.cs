namespace LegalEazeRewrite.Models.DataModels
{
    public class LawFirm
    {
        public string LawFirmID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<Lawyer> Lawyers { get; set; }
        public ICollection<Manager> Managers { get; set; }
    }
}
