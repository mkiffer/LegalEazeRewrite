using Microsoft.AspNetCore.Authorization;

namespace LegalEazeRewrite.Models.DataModels
{

    public class Court
    {
        public int CourtID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public ICollection<Matter> Matters { get; set; }
    }
}
