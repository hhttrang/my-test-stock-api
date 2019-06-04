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
    public class UserAccountController : ApiController
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: api/UserAccount
        public IQueryable<User_Account> GetUser_Account()
        {
            return db.User_Account;
        }

        // GET: api/UserAccount/5
        [ResponseType(typeof(User_Account))]
        public IHttpActionResult GetUser_Account(string id)
        {
            User_Account user_Account = db.User_Account.Find(id);
            if (user_Account == null)
            {
                return NotFound();
            }

            return Ok(user_Account);
        }

        // PUT: api/UserAccount/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser_Account(string id, User_Account user_Account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user_Account.ID)
            {
                return BadRequest();
            }

            db.Entry(user_Account).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_AccountExists(id))
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

        // POST: api/UserAccount
        [ResponseType(typeof(User_Account))]
        public IHttpActionResult PostUser_Account(User_Account user_Account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User_Account.Add(user_Account);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (User_AccountExists(user_Account.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user_Account.ID }, user_Account);
        }

        // DELETE: api/UserAccount/5
        [ResponseType(typeof(User_Account))]
        public IHttpActionResult DeleteUser_Account(string id)
        {
            User_Account user_Account = db.User_Account.Find(id);
            if (user_Account == null)
            {
                return NotFound();
            }

            db.User_Account.Remove(user_Account);
            db.SaveChanges();

            return Ok(user_Account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool User_AccountExists(string id)
        {
            return db.User_Account.Count(e => e.ID == id) > 0;
        }
    }
}