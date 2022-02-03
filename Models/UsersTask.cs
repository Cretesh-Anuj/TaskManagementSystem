using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class UsersTask
    {
        
        [Key]
        [Column(Order = 1)]
        public int TaskId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string ApplicationUserId { get; set; }


    
        public Task Task { get; set; }

        
        public ApplicationUser ApplicationUser { get; set; }
    }
}
