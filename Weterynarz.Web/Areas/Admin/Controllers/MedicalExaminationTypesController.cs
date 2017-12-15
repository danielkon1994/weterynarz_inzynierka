using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Weterynarz.Web.Areas.Admin.Controllers
{
    public class MedicalExaminationTypesController : AdminBaseController
    {
        // GET: MedicalExaminationTypes
        public ActionResult Index()
        {
            return View();
        }

        // GET: MedicalExaminationTypes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MedicalExaminationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalExaminationTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedicalExaminationTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MedicalExaminationTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedicalExaminationTypes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MedicalExaminationTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}