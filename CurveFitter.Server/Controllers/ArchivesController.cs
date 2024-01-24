using CurveFitter.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity;
using System.Linq;


namespace CurveFitter.Server.Controllers
{
    public class ArchivesController : Controller
    {
        // GET: ArchivesController/Users/5
        public async ActionResult ActionResult<T>(int id)
        {
            await using var db = new ArchiveContext();

            List<Archive> archiveList =
                await (from archive in db.Archives
                    join user in db.Users on archive.UserId equals user.Id into tmp
                    from m in tmp.DefaultIfEmpty()

                    select new Archive
                    {
                        Id = sollIst.Id,
                        CustomerId = sollIst.CustomerId,
                        User = m,
                    }
  ).ToListAsync();


            return queryResults.AsAsyncEnumerable();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
