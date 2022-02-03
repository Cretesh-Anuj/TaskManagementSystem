using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Enumerable;

namespace TaskManagementSystem.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }


        [Required]  
        public string TaskName { get; set; }

        [Required]
        public string TaskDescription { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime AssignedDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; } = DateTime.Now;



        public ApplicationUser AssignedBy { get; set; } //manager
        
        public string AssignedById { get; set; }

        [Required]
        public Taskstatus Taskstatus { get; set; }



        [Required]
        [DataType(DataType.Date)]
        public DateTime TaskCompletedDate { get; set; } = DateTime.Now;



        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;



        [ForeignKey("TaskCategory")]
        public int? TaskCategoryId { get; set; }
        public virtual TaskCategory TaskCategory { get; set; }


        [Display(Name = "Assigned To")]
        public ICollection<UsersTask> UserTasks { get; set; } 


    }
}
