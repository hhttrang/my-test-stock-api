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
    public class TransactionTypeController : ApiController
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: api/TransactionType
        public IQueryable<Transaction_Type> GetTransaction_Type()
        {
            return db.Transaction_Type;
        }

        // GET: api/TransactionType/5
        [ResponseType(typeof(Transaction_Type))]
        public IHttpActionResult GetTransaction_Type(string id)
        {
            Transaction_Type transaction_Type = db.Transaction_Type.Find(id);
            if (transaction_Type == null)
            {
                return NotFound();
            }

            return Ok(transaction_Type);
        }

        // PUT: api/TransactionType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaction_Type(string id, Transaction_Type transaction_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction_Type.ID)
            {
                return BadRequest();
            }

            db.Entry(transaction_Type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Transaction_TypeExists(id))
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

        // POST: api/TransactionType
        [ResponseType(typeof(Transaction_Type))]
        public IHttpActionResult PostTransaction_Type(Transaction_Type transaction_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transaction_Type.Add(transaction_Type);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Transaction_TypeExists(transaction_Type.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = transaction_Type.ID }, transaction_Type);
        }

        // DELETE: api/TransactionType/5
        [ResponseType(typeof(Transaction_Type))]
        public IHttpActionResult DeleteTransaction_Type(string id)
        {
            Transaction_Type transaction_Type = db.Transaction_Type.Find(id);
            if (transaction_Type == null)
            {
                return NotFound();
            }

            db.Transaction_Type.Remove(transaction_Type);
            db.SaveChanges();

            return Ok(transaction_Type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Transaction_TypeExists(string id)
        {
            return db.Transaction_Type.Count(e => e.ID == id) > 0;
        }
    }
}