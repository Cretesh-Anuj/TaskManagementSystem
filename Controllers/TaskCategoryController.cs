using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.ViewModels;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class TaskCategoryController : Controller
    {
        public readonly ApplicationDbContext _context;
        public TaskCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: TaskCategoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TaskCategoryController/Details/5
        public ActionResult Details(int id)
        {

            var categoryList = _context.TaskCategories.Find(id);
            var taskCatogories = _context.TaskCategories.ToList();
            foreach (var item in taskCatogories)
            {
                var managers = _context.Users.Find(item.ManagerId);
                item.Manager = managers;
            }

            return PartialView("_DetailTaskCategory", categoryList);
        }
        [Authorize]
        // GET: TaskCategoryController/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            var userList = _context.Users.ToList();
            var users = new List<SelectListItem>();

            foreach (var item in userList)
            {
                SelectListItem userLists = new SelectListItem { Value = item.Id, Text = item.UserName };
                users.Add(userLists);
            }
            ViewBag.users = users;
            return View();
        }

        // POST: TaskCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, TaskCategory context)
        {
            _context.TaskCategories.Add(context);
            _context.SaveChanges();
            return RedirectToAction("ViewTaskCategory");
            
        }

        public ActionResult ViewTaskCategory()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            

            if (User.IsInRole("Admin"))
            {
                var taskCatogories = _context.TaskCategories.ToList();

                foreach (var item in taskCatogories)
                {
                    var managers = _context.Users.Find(item.ManagerId);
                    item.Manager = managers;
                }

                return View(taskCatogories);
            }
            else if (User.IsInRole("Manager"))
            {
                
                var taskCategories = from cust in _context.TaskCategories
                where cust.ManagerId == userId
                select cust;

                foreach (var item in taskCategories)
                {
                    var managers = _context.Users.Find(item.ManagerId);
                    item.Manager = managers;
                }
                return View(taskCategories);
            }
            else {
                var TaskLists = _context.UsersTasks.Include(a => a.ApplicationUser).Include(a => a.Task).Where(a => a.ApplicationUserId == userId)
                   .Select(c => new TaskCategory()
                   {
                       Name = c.Task.TaskCategory.Name,
                       Manager = c.Task.TaskCategory.Manager

                   }).Distinct();

                return View(TaskLists);
            }
            
        }

        // GET: TaskCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var taskcategories = _context.TaskCategories.Find(id);
            var userList = _context.Users.ToList();
            var users = new List<SelectListItem>();

            foreach (var item in userList)
            {
                SelectListItem userLists = new SelectListItem { Value = item.Id, Text = item.UserName };
                users.Add(userLists);
            }
            ViewBag.users = users;
            return View(taskcategories);
        }

        // POST: TaskCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskCategory taskCategory)
        {
            _context.TaskCategories.Update(taskCategory);
            _context.SaveChanges();
            return RedirectToAction("ViewTaskCategory");
        }

        // GET: TaskCategoryController/Delete/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(int ids)
        //{
        //   var  taskCategory1=  _context.TaskCategories.Find(ids);
        //    var taskCatogories = _context.TaskCategories.ToList();

        //    foreach (var item in taskCatogories)
        //    {
        //        var managers = _context.Users.Find(item.ManagerId);
        //        item.Manager = managers;
        //    }
        //    return PartialView("_DeletePartialView", taskCatogories);
        //}

        // POST: TaskCategoryController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var taskCategory = _context.TaskCategories.Where(a => a.Id == id).FirstOrDefault();
                _context.TaskCategories.Remove(taskCategory);
                _context.SaveChanges();
                //TempData["Success"] = "Cateogry deleted successfully";
                return Json(true);
            }
            catch
            {
                //ViewBag.ErrorMessage = "This category is linked with task hence cannot be deleted";
                return Json(false);
            }


        }

    }
}
