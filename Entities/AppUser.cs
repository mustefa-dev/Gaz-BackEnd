using System.ComponentModel.DataAnnotations;
using Gaz_BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndStructuer.Entities
{
    public class AppUser : BaseEntity<Guid>
    {
        public string? PhoneNumber { get; set; }
        
        public string? Name { get; set; }
        
        public string? Password { get; set; }
    
        public UserRole? Role { get; set; }
       
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Station>? Stations { get; set; }
    }
    
    public enum UserRole
    {
        None = 0,
        SuperAdmin=1,
        Admin = 2,
        User = 3,
        Provider = 4
    }
}