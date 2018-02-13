using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab4.Data;
using lab4.Data.Entities;
using lab4.Models.View;

namespace lab4.Controllers
{
    public class DescendantsController : Controller
    {
        // GET: Descendants
        public ActionResult Index()
        {
            return View();
        }

        private Descendants GetDescendants(int descendantId)
        {
            var descendantsDbContext = new AppDbContext();
            return descendantsDbContext.Descendants.Find(descendantId);
        }

        private Descendants MapToDescendants(DescendantsViewModel descendantViewModel)
        {
            return new Descendants
            {
                Id = descendantViewModel.Id,
                FirstName = descendantViewModel.FirstName,
                LastName = descendantViewModel.LastName,
                UserId = descendantViewModel.UserId
            };
        }

        private DescendantsViewModel MapToDescendantsViewModel(Descendants descendants)
        {
            return new DescendantsViewModel
            {
                Id = descendants.Id,
                FirstName = descendants.FirstName,
                LastName = descendants.LastName,
                UserId = descendants.UserId,
            };
        }

        private ICollection<DescendantsViewModel> GetDescendantsFromUser(int userId)
        {
            var descendantsViewModel = new List<DescendantsViewModel>();
            var descendantDbContext = new AppDbContext();
            var descendants = descendantDbContext.Descendants.Where(descendant => descendant.UserId == userId).ToList();
            foreach (var descendant in descendants)
            {
                var descViewModel = MapToDescendantsViewModel(descendant);
                descendantsViewModel.Add(descViewModel);
            }

            return descendantsViewModel;
        }

        private void Save(DescendantsViewModel descendantsViewModel)
        {
            var descendantDbContext = new AppDbContext();
            var descendant = MapToDescendants(descendantsViewModel);
            descendantDbContext.Descendants.Add(descendant);
            descendantDbContext.SaveChanges();
        }

        private void UpdateDescendant(DescendantsViewModel descendantsViewModel)
        {
            var descendantDbContext = new AppDbContext();
            var descendant = descendantDbContext.Descendants.Find(descendantsViewModel);
            CopyToDesc(descendantsViewModel, descendant);
            descendantDbContext.SaveChanges();
        }

        public ActionResult Delete(int id)
        {
            var descendant = GetDescendants(id);

            DeleteDescendant(id);

            return RedirectToAction("List", new { UserId = descendant.UserId });
        }

        private void DeleteDescendant(int id)
        {
            var dbContext = new AppDbContext();
            var descendant = dbContext.Descendants.Find(id);
            if (descendant != null)
            {
                dbContext.Descendants.Remove(descendant);
                dbContext.SaveChanges();
            }

        }

        [HttpGet]
        public ActionResult Create(int userId)
        {
            ViewBag.UserId = userId;

            return View();
        }

        [HttpPost]
        public ActionResult Create(DescendantsViewModel descendantsViewModel)
        {
            if (ModelState.IsValid)
            {
                Save(descendantsViewModel);
                return RedirectToAction("List", new { UserId = descendantsViewModel.UserId });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var descendant = GetDescendants(id);

            return View(descendant);
        }

        public ActionResult List(int userId)
        {
            ViewBag.UserId = userId;
            var descendants = GetDescendantsFromUser(userId);
            return View(descendants);
        }

        public void CopyToDesc(DescendantsViewModel descendantsViewModel, Descendants descendants)
        {
            descendants.Id = descendantsViewModel.Id;
            descendants.FirstName = descendantsViewModel.FirstName;
            descendants.LastName = descendantsViewModel.LastName;
            descendants.UserId = descendantsViewModel.UserId;

        }

    }
}