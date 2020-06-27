using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using InViBuS.API.Models;

namespace InViBuS.API.Controllers
{
    public class FilterController : ApiController
    {
        private InViBuSContext db = new InViBuSContext();

        // GET: api/Filter/getallfilters
        [HttpGet]
        [Authorize]
        [ActionName("getallfilters")]
        public IQueryable<Filter> GetFilters()
        {
            return db.Filters;
        }

        // GET: api/Filter/getfilter/{id}
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(Filter))]
        [ActionName("getfilter")]
        public async Task<IHttpActionResult> GetFilter(int id)
        {
            Filter filter = await db.Filters.FindAsync(id);
            if (filter == null)
            {
                return NotFound();
            }

            return Ok(filter);
        }

        // PUT: api/Filter/editfilter/{id}
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(void))]
        [ActionName("editfilter")]
        public async Task<IHttpActionResult> PutFilter(int id, Filter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filter.IdFilter)
            {
                return BadRequest();
            }

            db.Entry(filter).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilterExists(id))
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

        // POST: api/Filter/postfilter
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(Filter))]
        [ActionName("postfilter")]
        public async Task<IHttpActionResult> PostFilter(Filter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Filters.Add(filter);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = filter.IdFilter }, filter);
        }

        // DELETE: api/Filter/deletefilter/{id}
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(Filter))]
        [ActionName("deletefilter")]
        public async Task<IHttpActionResult> DeleteFilter(int id)
        {
            Filter filter = await db.Filters.FindAsync(id);
            if (filter == null)
            {
                return NotFound();
            }

            db.Filters.Remove(filter);
            await db.SaveChangesAsync();

            return Ok(filter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilterExists(int id)
        {
            return db.Filters.Count(e => e.IdFilter == id) > 0;
        }
    }
}