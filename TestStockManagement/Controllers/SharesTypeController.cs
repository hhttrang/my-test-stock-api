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
    public class SharesTypeController : ApiController
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: api/SharesType
        public IQueryable<Shares_Type> GetShares_Type()
        {
            return db.Shares_Type;
        }

        // GET: api/SharesType/5
        [ResponseType(typeof(Shares_Type))]
        public IHttpActionResult GetShares_Type(string id)
        {
            Shares_Type shares_Type = db.Shares_Type.Find(id);
            if (shares_Type == null)
            {
                return NotFound();
            }

            return Ok(shares_Type);
        }

        // PUT: api/SharesType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShares_Type(string id, Shares_Type shares_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shares_Type.ID)
            {
                return BadRequest();
            }

            db.Entry(shares_Type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Shares_TypeExists(id))
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

        // POST: api/SharesType
        [ResponseType(typeof(Shares_Type))]
        public IHttpActionResult PostShares_Type(Shares_Type shares_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shares_Type.Add(shares_Type);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Shares_TypeExists(shares_Type.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shares_Type.ID }, shares_Type);
        }

        // DELETE: api/SharesType/5
        [ResponseType(typeof(Shares_Type))]
        public IHttpActionResult DeleteShares_Type(string id)
        {
            Shares_Type shares_Type = db.Shares_Type.Find(id);
            if (shares_Type == null)
            {
                return NotFound();
            }

            db.Shares_Type.Remove(shares_Type);
            db.SaveChanges();

            return Ok(shares_Type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Shares_TypeExists(string id)
        {
            return db.Shares_Type.Count(e => e.ID == id) > 0;
        }
    }
}