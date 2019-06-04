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
using TestStockManagement;

namespace TestStockManagement.Controllers
{
    public class ShareholdersController : ApiController
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: api/Shareholders
        public IQueryable<Shareholder> GetShareholders()
        {
            return db.Shareholders;
        }

        // GET: api/Shareholders/5
        [ResponseType(typeof(Shareholder))]
        public IHttpActionResult GetShareholder(string id)
        {
            Shareholder shareholder = db.Shareholders.Find(id);
            if (shareholder == null)
            {
                return NotFound();
            }

            return Ok(shareholder);
        }

        // PUT: api/Shareholders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShareholder(string id, Shareholder shareholder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shareholder.ID)
            {
                return BadRequest();
            }

            db.Entry(shareholder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShareholderExists(id))
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

        // POST: api/Shareholders
        [ResponseType(typeof(Shareholder))]
        public IHttpActionResult PostShareholder(Shareholder shareholder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shareholders.Add(shareholder);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ShareholderExists(shareholder.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shareholder.ID }, shareholder);
        }

        // DELETE: api/Shareholders/5
        [ResponseType(typeof(Shareholder))]
        public IHttpActionResult DeleteShareholder(string id)
        {
            Shareholder shareholder = db.Shareholders.Find(id);
            if (shareholder == null)
            {
                return NotFound();
            }

            db.Shareholders.Remove(shareholder);
            db.SaveChanges();

            return Ok(shareholder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShareholderExists(string id)
        {
            return db.Shareholders.Count(e => e.ID == id) > 0;
        }
    }
}