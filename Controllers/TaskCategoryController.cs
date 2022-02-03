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
using System.Linq;

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

            return View(categoryList);
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
            else if (User.IsInRole("Manager, User"))
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
            
            return View();
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
           var  taskCategory1=  _context.TaskCategories.Find(id);
            var taskCatogories = _context.TaskCategories.ToList();

            foreach (var item in taskCatogories)
            {
                var managers = _context.Users.Find(item.ManagerId);
                item.Manager = managers;
            }
            return View(taskCategory1);
        }

        // POST: TaskCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TaskCategory taskCategory)
        {
            _context.TaskCategories.Remove(taskCategory);
            _context.SaveChanges();
            return RedirectToAction("ViewTaskCategory");
            
        }

    }
}
