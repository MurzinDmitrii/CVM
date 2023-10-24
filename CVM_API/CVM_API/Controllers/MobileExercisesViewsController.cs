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
    public class MobileExercisesViewsController : ApiController
    {
        private CVMEntities db = new CVMEntities();

        // GET: api/MobileExercisesViews
        public IQueryable<MobileExercisesView> GetMobileExercisesView()
        {
            return db.MobileExercisesView;
        }

        // GET: api/MobileExercisesViews/5
        [ResponseType(typeof(MobileExercisesView))]
        public IHttpActionResult GetMobileExercisesView(int id)
        {
            var mobileExercisesView = db.MobileExercisesView.Where(c => c.ClientId == id).ToList();
            if (mobileExercisesView == null)
            {
                return NotFound();
            }

            return Ok(mobileExercisesView);
        }

        // PUT: api/MobileExercisesViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMobileExercisesView(int id, MobileExercisesView mobileExercisesView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mobileExercisesView.ClientId)
            {
                return BadRequest();
            }

            db.Entry(mobileExercisesView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobileExercisesViewExists(id))
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

        // POST: api/MobileExercisesViews
        [ResponseType(typeof(MobileExercisesView))]
        public IHttpActionResult PostMobileExercisesView(MobileExercisesView mobileExercisesView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MobileExercisesView.Add(mobileExercisesView);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MobileExercisesViewExists(mobileExercisesView.ClientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mobileExercisesView.ClientId }, mobileExercisesView);
        }

        // DELETE: api/MobileExercisesViews/5
        [ResponseType(typeof(MobileExercisesView))]
        public IHttpActionResult DeleteMobileExercisesView(int id)
        {
            MobileExercisesView mobileExercisesView = db.MobileExercisesView.Find(id);
            if (mobileExercisesView == null)
            {
                return NotFound();
            }

            db.MobileExercisesView.Remove(mobileExercisesView);
            db.SaveChanges();

            return Ok(mobileExercisesView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MobileExercisesViewExists(int id)
        {
            return db.MobileExercisesView.Count(e => e.ClientId == id) > 0;
        }
    }
}