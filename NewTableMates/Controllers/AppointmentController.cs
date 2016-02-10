using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Http.Cors;
using System.Web.Mvc;
using NewTableMates.DAL;
using NewTableMates.Models;

namespace NewTableMates.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class AppointmentController : ApiController
    {
        private TableMatesContext db = new TableMatesContext();

        // GET: api/Appointment
        public Appointment[] GetAppointments()
        {
            return db.Appointments.ToArray();
        }

        // GET: api/Appointment/5
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult GetAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        // PUT: api/Appointment/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppointment(int id, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.ID)
            {
                return BadRequest();
            }

            db.Entry(appointment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Appointment
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult PostAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointment.ID }, appointment);
        }

        // DELETE: api/Appointment/5
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult DeleteAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointment);
            db.SaveChanges();

            return Ok(appointment);
        }
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult PutAttend(int id, string attendeeName)
        {
            //Appointment appointment = db.Appointments.Find(id);
            //if (appointment == null)
            //{
            //    return NotFound();
            //}
            //var attendee = new Attendee[1];
            //attendee[0] = db.Attendees.Find(attendeeName);
            //if (attendee[0] == null)
            //{
            //    return NotFound();
            //}
            //if (db.Appointments.Find(id).Attendees == null)
            //{
            //    appointment.Attendees = attendee;
            //    db.Entry(appointment).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
            //else
            //{
            //    appointment.Attendees.Concat(attendee);
            //    db.Entry(appointment).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
            var appointment = db.Appointments.Single(p => p.ID == id);
            appointment.Attendees.Add(db.Attendees.Find(attendeeName));
            db.SaveChanges();
            return Ok(appointment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentExists(int id)
        {
            return db.Appointments.Count(e => e.ID == id) > 0;
        }
    }
}