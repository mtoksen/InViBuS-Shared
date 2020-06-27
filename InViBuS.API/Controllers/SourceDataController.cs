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
    public class SourceDataController : ApiController
    {
        private InViBuSContext db = new InViBuSContext();

        // GET: api/SourceData/getallsourcedata
        [HttpGet]
        [Authorize]
        [ActionName("getallsourcedata")]
        public IQueryable<SourceData> GetSourceData()
        {
            return db.SourceDatas;
        }

        // GET: api/SourceData/getsourcedata/{id}
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(SourceData))]
        [ActionName("getsourcedata")]
        public async Task<IHttpActionResult> GetSourceData(int id)
        {
            SourceData sourceData = await db.SourceDatas.FindAsync(id);
            if (sourceData == null)
            {
                return NotFound();
            }

            return Ok(sourceData);
        }

        // PUT: api/SourceData/editsourcedata/{id}
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(void))]
        [ActionName("editsourcedata")]
        public async Task<IHttpActionResult> PutSourceData(int id, SourceData sourceData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sourceData.IdSourceData)
            {
                return BadRequest();
            }

            db.Entry(sourceData).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceDataExists(id))
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

        // POST: api/SourceData/postsourcedata
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(SourceData))]
        [ActionName("postsourcedata")]
        public async Task<IHttpActionResult> PostSourceData(SourceData sourceData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SourceDatas.Add(sourceData);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sourceData.IdSourceData }, sourceData);
        }

        // DELETE: api/SourceData/deletesourcedata/{id}
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(SourceData))]
        [ActionName("deletesourcedata")]
        public async Task<IHttpActionResult> DeleteSourceData(int id)
        {
            SourceData sourceData = await db.SourceDatas.FindAsync(id);
            if (sourceData == null)
            {
                return NotFound();
            }

            db.SourceDatas.Remove(sourceData);
            await db.SaveChangesAsync();

            return Ok(sourceData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SourceDataExists(int id)
        {
            return db.SourceDatas.Count(e => e.IdSourceData == id) > 0;
        }
    }
}