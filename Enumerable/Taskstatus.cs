using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Enumerable
{
    public enum Taskstatus
    {
        [Display(Name = "Not Started" )]
        NotStarted,

        [Display(Name = "In Progress")]
        InProgress,

        [Display(Name = "Not Completed")]
        NotCompleted,

        [Display(Name = "Completed")]
        Completed
    }
}
