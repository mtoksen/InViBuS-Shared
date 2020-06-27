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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InViBuS.API.Controllers
{
    public class AnalysisMetadataController : ApiController
    {
        private InViBuSContext db = new InViBuSContext();

        // GET: api/AnalysisMetadata/getanalysismetadata
        [HttpGet]
        [Authorize]
        [ActionName("getanalysismetadata")]
        public IQueryable<AnalysisMetadata> GetAnalysisMetadata()
        {
            return db.AnalysisMetadatas.Include(x => x.AnalysisData);
        }

        // GET: api/AnalysisMetadata/getanalysismetadata/{id}
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(List<AnalysisMetadata>))]
        [ActionName("getanalysismetadataforproject")]
        public async Task<IHttpActionResult> GetAnalysisMetadataForProject(int id)
        {
            List<AnalysisMetadata> analysisMetadata = await db.AnalysisMetadatas.Include(amd => amd.AnalysisData)
                .Include(amd => amd.Filters)
                .Where(amd => amd.IdProject == id)
                .ToListAsync();
            if (analysisMetadata == null)
            {
                return NotFound();
            }

            return Ok(analysisMetadata);
        }

        // GET: api/AnalysisMetadata/getanalysismetadata/{id}
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(AnalysisMetadata))]
        [ActionName("getanalysismetadata")]
        public async Task<IHttpActionResult> GetAnalysisMetadata(int id)
        {
            AnalysisMetadata analysisMetadata = await db.AnalysisMetadatas.Include(amd => amd.AnalysisData)
                .Include(amd => amd.Filters)
                .SingleOrDefaultAsync(amd => amd.IdAnalysisMetadata == id);
            if (analysisMetadata == null)
            {
                return NotFound();
            }

            return Ok(analysisMetadata);
        }

        // PUT: api/AnalysisMetadata/editanalysismetadata/{id}
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(void))]
        [ActionName("editanalysismetadata")]
        public async Task<IHttpActionResult> PutAnalysisMetadata(int id, AnalysisMetadata analysisMetadata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != analysisMetadata.IdAnalysisMetadata)
            {
                return BadRequest();
            }

            db.Entry(analysisMetadata).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalysisMetadataExists(id))
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

        // POST: api/AnalysisMetadata/postanalysismetadata
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(AnalysisMetadata))]
        [ActionName("postanalysismetadata")]
        public async Task<IHttpActionResult> PostAnalysisMetadata([FromBody] JObject data, int id)
        {
            AnalysisMetadata analysisMetadata = new AnalysisMetadata
            {
                Iteration = (data.SelectToken("iteration") != null ?
                    Int32.Parse(data.SelectToken("iteration").ToString()) : 0),
                Conclusion = (data.SelectToken("conclusion") != null ?
                    data.SelectToken("conclusion").ToString() : "N/A"),
                DescriptionLong = (data.SelectToken("descriptionLong") != null ?
                data.SelectToken("descriptionLong").ToString() : "N/A"),
                NumIn = (data.SelectToken("ParamIn") != null ?
                    data.SelectToken("ParamIn").Count() : 0),
                NumOut = (data.SelectToken("ParamOut") != null ?
                    data.SelectToken("ParamOut").Count() : 0),
                NumSim = (data.SelectToken("AnalysisOut") != null ?
                    data.SelectToken("AnalysisOut").Count() : 0),
                SourceFile = (data.SelectToken("sourceFile") != null ?
                    data.SelectToken("sourceFile").ToString() : "N/A"),
                TimeSim = (data.SelectToken("timeSim") != null ?
                    Int32.Parse(data.SelectToken("timeSim").ToString()) : 0),
                Subject = (data.SelectToken("subject") != null ?
                    data.SelectToken("subject").ToString() : "N/A"),
                UploadDate = (data.SelectToken("uploadDate") != null ?
                    data.SelectToken("uploadDate").ToString() : "N/A"),
                AnalysisData = new AnalysisData
                {
                    AnalysisDataJson = data.ToString()
                },
                IdProject = id
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AnalysisMetadatas.Add(analysisMetadata);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(analysisMetadata);
        }

        // DELETE: api/AnalysisMetadata/deleteanalysismetadata/{id}
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(AnalysisMetadata))]
        [ActionName("deleteanalysismetadata")]
        public async Task<IHttpActionResult> DeleteAnalysisMetadata(int id)
        {
            AnalysisMetadata analysisMetadata = await db.AnalysisMetadatas.FindAsync(id);
            if (analysisMetadata == null)
            {
                return NotFound();
            }

            db.AnalysisMetadatas.Remove(analysisMetadata);
            await db.SaveChangesAsync();

            return Ok(analysisMetadata);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnalysisMetadataExists(int id)
        {
            return db.AnalysisMetadatas.Count(e => e.IdAnalysisMetadata == id) > 0;
        }
    }
}