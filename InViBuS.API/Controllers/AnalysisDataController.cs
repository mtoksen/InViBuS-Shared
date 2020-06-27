using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using InViBuS.API.Models;

namespace InViBuS.API.Controllers
{
    public class AnalysisDataController : ApiController
    {
        private InViBuSContext db = new InViBuSContext();

        // GET: api/AnalysisData/getallanalysisdata
        [HttpGet]
        [Authorize]
        [ActionName("getallanalysisdata")]
        public IQueryable<AnalysisData> GetAnalysisData()
        {
            return db.AnalysisDatas;
        }

        // GET: api/AnalysisData/getanalysisdata/{id}
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(AnalysisData))]
        [ActionName("getanalysisdata")]
        public async Task<IHttpActionResult> GetAnalysisData(int id)
        {
            AnalysisData analysisData = await db.AnalysisDatas.FindAsync(id);
            if (analysisData == null)
            {
                return NotFound();
            }

            return Ok(analysisData);
        }

        // PUT: api/AnalysisData/editanalysisdata/{id}
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(void))]
        [ActionName("editanalysisdata")]
        public async Task<IHttpActionResult> PutAnalysisData(int id, AnalysisData analysisData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != analysisData.IdAnalysisData)
            {
                return BadRequest();
            }

            db.Entry(analysisData).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalysisDataExists(id))
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

        // POST: api/AnalysisData/postanalysisdata
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(AnalysisData))]
        [ActionName("postanalysisdata")]
        public async Task<IHttpActionResult> PostAnalysisData(AnalysisData analysisData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AnalysisDatas.Add(analysisData);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = analysisData.IdAnalysisData }, analysisData);
        }

        // DELETE: api/AnalysisData/deleteanalysisdata/{id}
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(AnalysisData))]
        [ActionName("deleteanalysisdata")]
        public async Task<IHttpActionResult> DeleteAnalysisData(int id)
        {
            AnalysisData analysisData = await db.AnalysisDatas.FindAsync(id);
            if (analysisData == null)
            {
                return NotFound();
            }

            db.AnalysisDatas.Remove(analysisData);
            await db.SaveChangesAsync();

            return Ok(analysisData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnalysisDataExists(int id)
        {
            return db.AnalysisDatas.Count(e => e.IdAnalysisData == id) > 0;
        }
    }
}