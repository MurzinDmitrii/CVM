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
using CVM_API.Models;

namespace CVM_API.Controllers
{
    public class MobileClientViewsController : ApiController
    {
        private CVMEntities db = new CVMEntities();

        // GET: api/MobileClientViews
        public IHttpActionResult GetMobileClientView()
        {
            return Ok(db.MobileClientView.ToList());
        }

        // GET: api/MobileClientViews/5
        [ResponseType(typeof(MobileClientView))]
        public IHttpActionResult GetMobileClientView(int id)
        {
            MobileClientView mobileClientView = db.MobileClientView.Find(id);
            if (mobileClientView == null)
            {
                return NotFound();
            }

            return Ok(mobileClientView);
        }

        // PUT: api/MobileClientViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMobileClientView(int id, MobileClientView mobileClientView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mobileClientView.ClientId)
            {
                return BadRequest();
            }

            db.Entry(mobileClientView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobileClientViewExists(id))
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

        // POST: api/MobileClientViews
        [ResponseType(typeof(MobileClientView))]
        public IHttpActionResult PostMobileClientView(MobileClientView mobileClientView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MobileClientView.Add(mobileClientView);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mobileClientView.ClientId }, mobileClientView);
        }

        // DELETE: api/MobileClientViews/5
        [ResponseType(typeof(MobileClientView))]
        public IHttpActionResult DeleteMobileClientView(int id)
        {
            MobileClientView mobileClientView = db.MobileClientView.Find(id);
            if (mobileClientView == null)
            {
                return NotFound();
            }

            db.MobileClientView.Remove(mobileClientView);
            db.SaveChanges();

            return Ok(mobileClientView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MobileClientViewExists(int id)
        {
            return db.MobileClientView.Count(e => e.ClientId == id) > 0;
        }
    }
}