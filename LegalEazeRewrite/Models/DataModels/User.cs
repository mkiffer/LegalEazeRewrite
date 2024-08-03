using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace LegalEazeRewrite.Models.DataModels
{
    public class User : IdentityUser
    {
        // Additional properties can be added here
        public Lawyer Lawyer { get; set; }
        public Manager Manager { get; set; }
    }
}
