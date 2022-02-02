using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PrestamoController : Controller
    {
        private bdVideoExplicativoEntitiesEntities db = new bdVideoExplicativoEntitiesEntities();

        // GET: Prestamo
        public ActionResult Index()
        {
            var prestamo = db.Prestamo.Include(p => p.Equipo).Include(p => p.Usuario);
            return View(prestamo.ToList());
        }

        // GET: Prestamo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // GET: Prestamo/Create
        public ActionResult Create()
        {
            ViewBag.IdEquipo = new SelectList(db.Equipo, "IdEquipo", "NombreEquipo");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Usuario1");
            return View();
        }

        // POST: Prestamo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrestamo,FechaInicio,FechaFin,FechaEntrega,Multa,Estado,IdUsuario,IdEquipo")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Prestamo.Add(prestamo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEquipo = new SelectList(db.Equipo, "IdEquipo", "NombreEquipo", prestamo.IdEquipo);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Usuario1", prestamo.IdUsuario);
            return View(prestamo);
        }

        // GET: Prestamo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEquipo = new SelectList(db.Equipo, "IdEquipo", "NombreEquipo", prestamo.IdEquipo);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Usuario1", prestamo.IdUsuario);
            return View(prestamo);
        }

        // POST: Prestamo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrestamo,FechaInicio,FechaFin,FechaEntrega,Multa,Estado,IdUsuario,IdEquipo")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEquipo = new SelectList(db.Equipo, "IdEquipo", "NombreEquipo", prestamo.IdEquipo);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Usuario1", prestamo.IdUsuario);
            return View(prestamo);
        }

        // GET: Prestamo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: Prestamo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamo prestamo = db.Prestamo.Find(id);
            db.Prestamo.Remove(prestamo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
