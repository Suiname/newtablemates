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
using System.Web.Http.Cors;
using NewTableMates.DAL;
using NewTableMates.Models;

namespace NewTableMates.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AttendeeController : ApiController
    {
        private TableMatesContext db = new TableMatesContext();

        // GET: api/Attendee
        public Attendee[] GetAttendees()
        {
            return db.Attendees.ToArray();
        }

        // GET: api/Attendee/5
        [ResponseType(typeof(Attendee))]
        public IHttpActionResult GetAttendee(string id)
        {
            Attendee attendee = db.Attendees.Find(id);
            if (attendee == null)
            {
                return NotFound();
            }

            return Ok(attendee);
        }

        // PUT: api/Attendee/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAttendee(string id, Attendee attendee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attendee.username)
            {
                return BadRequest();
            }

            db.Entry(attendee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendeeExists(id))
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

        // POST: api/Attendee
        [ResponseType(typeof(Attendee))]
        public IHttpActionResult PostAttendee(Attendee attendee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Attendees.Add(attendee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AttendeeExists(attendee.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = attendee.username }, attendee);
        }

        // DELETE: api/Attendee/5
        [ResponseType(typeof(Attendee))]
        public IHttpActionResult DeleteAttendee(string id)
        {
            Attendee attendee = db.Attendees.Find(id);
            if (attendee == null)
            {
                return NotFound();
            }

            db.Attendees.Remove(attendee);
            db.SaveChanges();

            return Ok(attendee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttendeeExists(string id)
        {
            return db.Attendees.Count(e => e.username == id) > 0;
        }
    }
}