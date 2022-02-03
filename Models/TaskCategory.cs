using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace TaskManagementSystem.Models
{
    public class TaskCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify the Name of the Task Category")]
        public string Name { get; set; }

        
        public ApplicationUser Manager { get; set; }


        [Required(ErrorMessage = "Please specify the Manager of the Task Category")]
        [Display(Name = "Manager")]
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }


        public List<Task> Tasks { get; set; }

      
    }
}
