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
    public class SeriesController : ApiController
    {
        private StockManagementEntities db = new StockManagementEntities();

        // GET: api/Series
        public IQueryable<Series> GetSeries()
        {
            return db.Series;
        }

        // GET: api/Series/5
        [ResponseType(typeof(Series))]
        public IHttpActionResult GetSeries(string id)
        {
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return NotFound();
            }

            return Ok(series);
        }

        // PUT: api/Series/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeries(string id, Series series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != series.ID)
            {
                return BadRequest();
            }

            db.Entry(series).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeriesExists(id))
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

        // POST: api/Series
        [ResponseType(typeof(Series))]
        public IHttpActionResult PostSeries(Series series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Series.Add(series);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SeriesExists(series.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = series.ID }, series);
        }

        // DELETE: api/Series/5
        [ResponseType(typeof(Series))]
        public IHttpActionResult DeleteSeries(string id)
        {
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return NotFound();
            }

            db.Series.Remove(series);
            db.SaveChanges();

            return Ok(series);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeriesExists(string id)
        {
            return db.Series.Count(e => e.ID == id) > 0;
        }
    }
}