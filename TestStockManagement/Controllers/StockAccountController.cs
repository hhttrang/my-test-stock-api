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
    public class StockAccountController : ApiController
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: api/StockAccount
        public IQueryable<Stock_Account> GetStock_Account()
        {
            return db.Stock_Account;
        }

        // GET: api/StockAccount/5
        [ResponseType(typeof(Stock_Account))]
        public IHttpActionResult GetStock_Account(string id)
        {
            Stock_Account stock_Account = db.Stock_Account.Find(id);
            if (stock_Account == null)
            {
                return NotFound();
            }

            return Ok(stock_Account);
        }

        // PUT: api/StockAccount/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStock_Account(string id, Stock_Account stock_Account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock_Account.ID)
            {
                return BadRequest();
            }

            db.Entry(stock_Account).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stock_AccountExists(id))
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

        // POST: api/StockAccount
        [ResponseType(typeof(Stock_Account))]
        public IHttpActionResult PostStock_Account(Stock_Account stock_Account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stock_Account.Add(stock_Account);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Stock_AccountExists(stock_Account.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = stock_Account.ID }, stock_Account);
        }

        // DELETE: api/StockAccount/5
        [ResponseType(typeof(Stock_Account))]
        public IHttpActionResult DeleteStock_Account(string id)
        {
            Stock_Account stock_Account = db.Stock_Account.Find(id);
            if (stock_Account == null)
            {
                return NotFound();
            }

            db.Stock_Account.Remove(stock_Account);
            db.SaveChanges();

            return Ok(stock_Account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Stock_AccountExists(string id)
        {
            return db.Stock_Account.Count(e => e.ID == id) > 0;
        }
    }
}