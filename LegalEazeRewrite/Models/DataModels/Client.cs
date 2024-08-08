using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegalEazeRewrite.Models.DataModels
{
    public class Client
    {
        
        public int ClientID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string ContactInfo { get; set; }

        public ICollection<LawyerClient>? LawyerClients { get; set; }
        public ICollection<Matter>? Matters { get; set; }
    }
}
