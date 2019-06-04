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
    public class ShareholderTypeController : ApiController
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: api/ShareholderType
        public IQueryable<Shareholder_Type> GetShareholder_Type()
        {
            return db.Shareholder_Type;
        }

        // GET: api/ShareholderType/5
        [ResponseType(typeof(Shareholder_Type))]
        public IHttpActionResult GetShareholder_Type(string id)
        {
            Shareholder_Type shareholder_Type = db.Shareholder_Type.Find(id);
            if (shareholder_Type == null)
            {
                return NotFound();
            }

            return Ok(shareholder_Type);
        }

        // PUT: api/ShareholderType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShareholder_Type(string id, Shareholder_Type shareholder_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shareholder_Type.ID)
            {
                return BadRequest();
            }

            db.Entry(shareholder_Type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Shareholder_TypeExists(id))
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

        // POST: api/ShareholderType
        [ResponseType(typeof(Shareholder_Type))]
        public IHttpActionResult PostShareholder_Type(Shareholder_Type shareholder_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shareholder_Type.Add(shareholder_Type);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Shareholder_TypeExists(shareholder_Type.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shareholder_Type.ID }, shareholder_Type);
        }

        // DELETE: api/ShareholderType/5
        [ResponseType(typeof(Shareholder_Type))]
        public IHttpActionResult DeleteShareholder_Type(string id)
        {
            Shareholder_Type shareholder_Type = db.Shareholder_Type.Find(id);
            if (shareholder_Type == null)
            {
                return NotFound();
            }

            db.Shareholder_Type.Remove(shareholder_Type);
            db.SaveChanges();

            return Ok(shareholder_Type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Shareholder_TypeExists(string id)
        {
            return db.Shareholder_Type.Count(e => e.ID == id) > 0;
        }
    }
}