using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModel;
using TaskManagementSystem.ViewModels;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        public ApplicationDbContext _context;
        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: TaskController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TaskController/Details/5
        public ActionResult Details(int id)
        {
            var task1 = _context.Tasks.Find(id);
            var TaskLists = _context.Tasks.ToList();
            foreach (var item in TaskLists)
            {
                var assignedby = _context.Users.Find(item.AssignedById);

                item.AssignedBy = assignedby;

                var taskcategory = _context.TaskCategories.Find(item.TaskCategoryId);
                item.TaskCategory = taskcategory;
            }
            return View(task1);
        }

        [Authorize]

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            var userList = _context.Users.AsNoTracking().ToList();
            var users = new List<SelectListItem>();

            foreach (var item in userList)
            {
                SelectListItem userLists = new SelectListItem { Value = item.Id, Text = item.UserName };
                users.Add(userLists);
            }


            var categoriesList = _context.TaskCategories.ToList();
            var categories = new List<SelectListItem>();
            foreach (var items in categoriesList)
            {
                SelectListItem categorylist = new SelectListItem { Value = items.Id.ToString(), Text = items.Name };
                categories.Add(categorylist);
            }
            var assignedToList = _context.Users.AsNoTracking().ToList();
            var assignedToLists = new List<SelectListItem>();

            foreach (var i in assignedToList)
            {
                SelectListItem userListss = new SelectListItem { Value = i.Id, Text = i.UserName };
                assignedToLists.Add(userListss);
            }

            TaskViewModel tvm = new TaskViewModel()
            {
                Assignedby = users,
                AssignedTo = assignedToLists,
                TaskCategories = categories

            };

            return View(tvm);
        }





        //For assigned by field. To access data from database and show it in dropdownlist


        // POST: TaskController/Create

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(TaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                Task task = new Task()
                {
                    TaskName = taskViewModel.TaskName,
                    TaskDescription = taskViewModel.TaskDescription,
                    TaskCategoryId = taskViewModel.TaskCatogoriesId,
                    Taskstatus = taskViewModel.Taskstatus,
                    AssignedById = taskViewModel.AssignedById,
                    AssignedDate = taskViewModel.AssignedDate,
                    DueDate = taskViewModel.DueDate,
                    TaskCompletedDate = taskViewModel.TaskCompletedDate,
                    CreatedDate = taskViewModel.CreatedDate
                };

                var response = _context.Tasks.Add(task);
                _context.SaveChanges();

                if (response.Entity.Id != 0)
                {

                    foreach (var item in taskViewModel.AssignedToIds)
                    {
                        UsersTask tsk = new UsersTask(); 
                        tsk.TaskId = response.Entity.Id;
                        tsk.ApplicationUserId = item;
                        _context.UsersTasks.Add(tsk);
                        _context.SaveChanges();
                    }

                }

            return RedirectToAction("ViewTask");
            }

         
            return View(taskViewModel);

        }
        public ActionResult ViewTask()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            if (User.IsInRole("Admin"))
            {
                var TaskLists = _context.Tasks.Include(a => a.UserTasks).ToList();

                var allTaskList = TaskLists.Select(c => new ListViewModel()
                {
                    Id = c.Id,
                    TaskName = c.TaskName,
                    TaskDescription = c.TaskDescription,
                    Taskstatus = c.Taskstatus,
                    TaskCatogoryId = c.TaskCategoryId,
                    AssignedDate = c.AssignedDate,
                    AssignedById = c.AssignedById,
                    DueDate = c.DueDate,
                    TaskCompletedDate = c.TaskCompletedDate,
                    CreatedDate = c.CreatedDate,
                    AssignedToIds = c.UserTasks.Select(c => c.ApplicationUserId).ToArray(),

                }).ToList();

                foreach (var item in allTaskList)
                {
                    var assignedby = _context.Users.Find(item.AssignedById);

                    item.Assignedby = assignedby;

                    var taskcategory = _context.TaskCategories.Find(item.TaskCatogoryId);
                    item.TaskCategory = taskcategory;


                    foreach (var items in item.AssignedToIds)
                    {

                        var users = _context.Users.Find(items);
                        item.AssignedTo.Add(users);


                    }


                }
                return View(allTaskList);
            }
            else if (User.IsInRole("Manager"))
            {
                
                var TaskLists = from cust in _context.Tasks
                                                     where cust.AssignedById == userId
                                                     select cust;

                var allTaskList = TaskLists.Select(c => new ListViewModel()
                {
                    Id = c.Id,
                    TaskName = c.TaskName,
                    TaskDescription = c.TaskDescription,
                    Taskstatus = c.Taskstatus,
                    TaskCatogoryId = c.TaskCategoryId,
                    AssignedDate = c.AssignedDate,
                    AssignedById = c.AssignedById,
                    DueDate = c.DueDate,
                    TaskCompletedDate = c.TaskCompletedDate,
                    CreatedDate = c.CreatedDate,
                    AssignedToIds = c.UserTasks.Select(c => c.ApplicationUserId).ToArray(),

                }).ToList();

                foreach (var item in allTaskList)
                {
                    var assignedby = _context.Users.Find(item.AssignedById);

                    item.Assignedby = assignedby;

                    var taskcategory = _context.TaskCategories.Find(item.TaskCatogoryId);
                    item.TaskCategory = taskcategory;


                    foreach (var items in item.AssignedToIds)
                    {

                        var users = _context.Users.Find(items);
                        item.AssignedTo.Add(users);


                    }


                }
                return View(allTaskList);

            }
            else
            {
                var TaskLists = _context.Tasks.Include(a => a.UserTasks).ToList();
                var allTaskList = TaskLists.Select(c => new ListViewModel()
                {
                    Id = c.Id,
                    TaskName = c.TaskName,
                    TaskDescription = c.TaskDescription,
                    Taskstatus = c.Taskstatus,
                    TaskCatogoryId = c.TaskCategoryId,
                    AssignedDate = c.AssignedDate,
                    AssignedById = c.AssignedById,
                    DueDate = c.DueDate,
                    TaskCompletedDate = c.TaskCompletedDate,
                    CreatedDate = c.CreatedDate,
                    AssignedToIds = c.UserTasks.Select(c => c.ApplicationUserId).ToArray(),

                }).ToList();
                foreach (var item in allTaskList)
                {
                    var assignedby = _context.Users.Find(item.AssignedById);

                    item.Assignedby = assignedby;

                    var taskcategory = _context.TaskCategories.Find(item.TaskCatogoryId);
                    item.TaskCategory = taskcategory;


                    foreach (var items in item.AssignedToIds)
                    {

                        var users = _context.Users.Find(items);
                        item.AssignedTo.Add(users);

                    }
                   
                }
                 
            }
            return View();
        }

        // GET: TaskController/Edit/5
        public ActionResult Edit(int id)
        {
            //var editTaskViewModel = new EditTaskViewModel();
            //var tasks = _context.Tasks.Find(id);
            //var users = _context.UsersTasks.ToList();
            //var dropdown = new List<SelectListItem>();
            //foreach (var item in users)
            //{
            //    SelectListItem selectListItem = new SelectListItem
            //    {
            //        Value = item.ApplicationUserId,
            //        Text = item.ApplicationUserId

            //    };
            //    dropdown.Add(selectListItem);
            //}

            //editTaskViewModel.Task.TaskName = tasks.TaskName;
            //editTaskViewModel.Task.Taskstatus = tasks.Taskstatus;
            //editTaskViewModel.Task.TaskType = tasks.TaskType;
            //editTaskViewModel.Task.TaskDescription = tasks.TaskDescription;
            //editTaskViewModel.Task.TaskCompletedDate = tasks.TaskCompletedDate;
            //editTaskViewModel.Task.TaskCategoryId = tasks.TaskCategoryId;
            //editTaskViewModel.Task.DueDate = tasks.DueDate;
            //editTaskViewModel.Task.CreatedDate = tasks.CreatedDate;
            //editTaskViewModel.Task.AssignedDate = tasks.AssignedDate;
            //editTaskViewModel.Task.AssignedById = tasks.AssignedById;





            var taskViewModel = new TaskViewModel();
            var assignedTolist = _context.Tasks.Include(m => m.UserTasks).Where(x => x.Id == id).FirstOrDefault();
            
            
            var userList = _context.Users.ToList();
            var users = new List<SelectListItem>();

            foreach (var item in userList)
            {
                SelectListItem userLists = new SelectListItem { Value = item.Id, Text = item.UserName };
                users.Add(userLists);
            }
           
            var categoriesList = _context.TaskCategories.ToList();
            var categories = new List<SelectListItem>();
            foreach (var items in categoriesList)
            {
                SelectListItem categorylist = new SelectListItem { Value = items.Id.ToString(), Text = items.Name };
                categories.Add(categorylist);
            }
            var assignedToList = _context.Users.ToList();
            var assignedToLists = new List<SelectListItem>();

            foreach (var i in assignedToList)
            {
                SelectListItem userListss = new SelectListItem { Value = i.Id, Text = i.UserName };
                assignedToLists.Add(userListss);
            }

            taskViewModel.TaskCategories = categories;
            taskViewModel.Assignedby = users;
            taskViewModel.AssignedTo = assignedToLists;
            taskViewModel.TaskName = assignedTolist.TaskName;
            taskViewModel.TaskDescription = assignedTolist.TaskDescription;
            taskViewModel.TaskCatogoriesId = assignedTolist.TaskCategoryId;
            taskViewModel.Taskstatus = assignedTolist.Taskstatus;
            taskViewModel.AssignedById = assignedTolist.AssignedById;
            taskViewModel.AssignedDate = assignedTolist.AssignedDate;
            taskViewModel.DueDate = assignedTolist.DueDate;
            taskViewModel.TaskCompletedDate = assignedTolist.TaskCompletedDate;
            taskViewModel.CreatedDate = assignedTolist.CreatedDate;
            taskViewModel.AssignedToIds = assignedTolist.UserTasks.Select(x => x.ApplicationUserId).ToArray();

           
           
            
            //TaskViewModel taskViewModel = new TaskViewModel();

            ////foreach (var item in allUsers)
            ////{

            ////}
            //taskViewModel.TaskName = assignedTolist.TaskName;
            //taskViewModel.TaskDescription = assignedTolist.TaskDescription;
            //taskViewModel.TaskCatogoriesId = assignedTolist.TaskCategoryId;
            //assignedTolist.Taskstatus = taskViewModel.Taskstatus;
            //taskViewModel.TaskType = assignedTolist.TaskType;
            //taskViewModel.AssignedById = assignedTolist.AssignedById;
            //taskViewModel.AssignedDate = assignedTolist.AssignedDate;
            //taskViewModel.DueDate = assignedTolist.DueDate;
            //taskViewModel.TaskCompletedDate = assignedTolist.TaskCompletedDate;
            //taskViewModel.CreatedDate = assignedTolist.CreatedDate;

            //taskViewModel.AssignedTo= PopulateAssinedTo(assignedTolist);
            //return View(taskViewModel);
            //ViewBag.assignedToLists = assignedToLists;

            //var task1 = _context.Tasks.Find(id);

            //var userList = _context.Users.ToList();
            //var users = new List<SelectListItem>();

            //foreach (var item in userList)
            //{
            //    SelectListItem userLists = new SelectListItem { Value = item.Id, Text = item.UserName };
            //    users.Add(userLists);
            //}
            //ViewBag.users = users;


            //var categoriesList = _context.TaskCategories.ToList();
            //var categories = new List<SelectListItem>();
            //foreach (var items in categoriesList)
            //{
            //    SelectListItem categorylist = new SelectListItem { Value = items.Id.ToString(), Text = items.Name };
            //    categories.Add(categorylist);
            //}
            //ViewBag.categories = categories;


            return View(taskViewModel);

      




        }

        //private TaskViewModel PopulateAssinedTo(Task task)
        //{
        //    var allUsers = _context.Users;
        //    var userTasks = new HashSet<string>(task.UserTasks.Select(c => c.ApplicationUserId));
        //    var taskViewModel = new List<UsersTask>();

        //    foreach (var item in allUsers)
        //    {
        //        taskViewModel.Add(new UsersTask()
        //        {
        //             ApplicationUserId = item.Id,
        //             TaskId = task.Id
        //        });
        //    }
        //    ViewData["AssignedTo"] = taskViewModel;
        //}

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                var editTask = _context.Tasks.AsNoTracking().Include(m => m.UserTasks).Where(x => x.Id == taskViewModel.Id).FirstOrDefault();

                editTask.TaskName = taskViewModel.TaskName;
                editTask.TaskDescription = taskViewModel.TaskDescription;
                editTask.TaskCategoryId = taskViewModel.TaskCatogoriesId;
                editTask.Taskstatus = taskViewModel.Taskstatus;
                editTask.AssignedById = taskViewModel.AssignedById;
                editTask.AssignedDate = taskViewModel.AssignedDate;
                editTask.DueDate = taskViewModel.DueDate;
                editTask.TaskCompletedDate = taskViewModel.TaskCompletedDate;
                editTask.CreatedDate = taskViewModel.CreatedDate;
                
                
                var response = _context.Tasks.Update(editTask);

                _context.SaveChanges();
                _context.Entry(editTask).State = EntityState.Detached;


                if (response.Entity.Id != 0)
                {
                                      
                        var editAssignedTo = _context.UsersTasks.AsNoTracking().Where(x => x.TaskId == response.Entity.Id).ToArray();

                   
                    _context.UsersTasks.RemoveRange(editAssignedTo);
                        _context.SaveChanges();
                   



                    foreach (var item in taskViewModel.AssignedToIds)
                        {
                            UsersTask tsk = new UsersTask();
                            tsk.TaskId = response.Entity.Id;
                            tsk.ApplicationUserId = item;
                            _context.UsersTasks.Add(tsk);
                            _context.SaveChanges();
                        }
                    
                        
                    
                    

                    _context.SaveChanges();
                }

                return RedirectToAction("ViewTask");
            }


            return RedirectToAction("ViewTask");
        }

        // GET: TaskController/Delete/5
        public ActionResult Delete(int id)
        {
            var task1 = _context.Tasks.Find(id);
            var TaskLists = _context.Tasks.ToList();
            foreach (var item in TaskLists)
            {
                var assignedby = _context.Users.Find(item.AssignedById);

                item.AssignedBy = assignedby;

                var taskcategory = _context.TaskCategories.Find(item.TaskCategoryId);
                item.TaskCategory = taskcategory;
            }
            return View(task1);

        }

        // POST: TaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Task task1)
        {
            _context.Tasks.Remove(task1);
            _context.SaveChanges();
            return RedirectToAction("ViewTask");
        }
    }
}
