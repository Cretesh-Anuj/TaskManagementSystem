using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Enumerable;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class ListViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify the Name of the Task")]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }



        [Required(ErrorMessage = "Please specify the Task description")]
        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; }



        [Required(ErrorMessage = "Please specify the Assigned date of the task")]

        [Display(Name = "Assigned Date")]
        [DataType(DataType.Date)]
        public DateTime AssignedDate { get; set; } = DateTime.Now;



        [Required(ErrorMessage = "Please specify the Due date of the task")]

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; } = DateTime.Now;



     


        [Required(ErrorMessage = "Please specify the task status")]
        [Display(Name = "Task Status")]
        public Taskstatus Taskstatus { get; set; }



        [Required(ErrorMessage = "Please specify the Task Completed Date")]
        [Display(Name = "Task Completed Date")]
        [DataType(DataType.Date)]
        public DateTime TaskCompletedDate { get; set; } = DateTime.Now;



        [Required(ErrorMessage = "Please fill out this field")]

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;








        [Display(Name = " Assigned By")]
        public string AssignedById { get; set; }


        public ApplicationUser Assignedby { get; set; }


        [Display(Name = "Task Categories")]
        public int? TaskCatogoryId { get; set; }
        public TaskCategory TaskCategory { get; set; }


        public List<ApplicationUser> AssignedTo { get; set; } = new List<ApplicationUser>();

        [Display(Name = "Assigned To")]
        public string[] AssignedToIds { get; set; }
    }
}
