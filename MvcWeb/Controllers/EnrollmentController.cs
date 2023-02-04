using System;
using System.Collections.Generic;
using System.Data;
 
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWeb.Data;
using MvcWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MvcWeb.SchoolModels;
using MvcWeb.SchoolModels.SchoolViewModels;

namespace MvcWeb.Controllers
{
    public class EnrollmentsController : Controller
    {

        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }
 
        // GET: Enrollment
        public ActionResult Index()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.

            int ct = ipHostInfo.AddressList.Count();
            StringBuilder sb = new StringBuilder();

            foreach (IPAddress ipx in ipHostInfo.AddressList)
            {
                sb.Append(ipx.ToString());
                sb.Append("  ");
            }
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            ViewData["ip"] = sb.ToString();
            var enrollments = _context.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(enrollments.ToList());
        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Enrollment enrollment = _context.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound(); //HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(_context.Courses, "CourseID", "Title");
            ViewBag.StudentID = new SelectList(_context.Students, "ID", "LastName");
            return View();
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598. [Bind("EnrollmentDate,FirstMidName,LastName")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("EnrollmentID, CourseID, StudentID, Grade")] Enrollment enrollment)
        //public ActionResult Create([Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(_context.Students, "ID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Enrollment enrollment = _context.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewBag.CourseID = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(_context.Students, "ID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }
 

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(enrollment).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(_context.Students, "ID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SchoolModels.Enrollment enrollment = _context.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolModels.Enrollment enrollment = _context.Enrollments.Find(id);
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
