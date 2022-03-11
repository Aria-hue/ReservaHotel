using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReservaHotel.Models;

namespace ReservaHotel.Controllers
{
    public class reservasController : Controller
    {
        private sistemahotelEntities db = new sistemahotelEntities();

        // GET: reservas
        public ActionResult Index()
        {
            var reservas = db.reservas.Include(r => r.habitacion1);
            return View(reservas.ToList());
        }

        // GET: reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservas reservas = db.reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            return View(reservas);
        }

        // GET: reservas/Create
        public ActionResult Create()
        {
            ViewBag.habitacion = new SelectList(db.habitacion, "id_habitacion", "nombre");
            return View();
        }

        // POST: reservas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_reserva,habitacion,fecha_entrada,fecha_salida")] reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.reservas.Add(reservas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.habitacion = new SelectList(db.habitacion, "id_habitacion", "nombre", reservas.habitacion);
            return View(reservas);
        }

        // GET: reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservas reservas = db.reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            ViewBag.habitacion = new SelectList(db.habitacion, "id_habitacion", "nombre", reservas.habitacion);
            return View(reservas);
        }

        // POST: reservas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_reserva,habitacion,fecha_entrada,fecha_salida")] reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.habitacion = new SelectList(db.habitacion, "id_habitacion", "nombre", reservas.habitacion);
            return View(reservas);
        }

        // GET: reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reservas reservas = db.reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            return View(reservas);
        }

        // POST: reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            reservas reservas = db.reservas.Find(id);
            db.reservas.Remove(reservas);
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
