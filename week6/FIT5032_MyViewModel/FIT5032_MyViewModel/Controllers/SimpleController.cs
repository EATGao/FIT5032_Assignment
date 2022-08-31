﻿using FIT5032_MyViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_MyViewModel.Controllers
{
    public class SimpleController : Controller
    {
        // GET: Simple
        public ActionResult Index()
        {
            return View();
        }

        // GET: Simple/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Simple/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Simple/Create
        [HttpPost]
        public ActionResult Create(FormOneViewModel model)
        {
            try
            {
                String FirstName = model.FirstName;
                String LastName = model.LastName;
                ViewBag.FullName = FirstName + " " + LastName;

                return View();
            }
            catch
            {
                return View();
            }
        }


        // GET: Simple/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Simple/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Simple/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Simple/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
