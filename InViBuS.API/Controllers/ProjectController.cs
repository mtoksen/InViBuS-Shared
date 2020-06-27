using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using InViBuS.API.Models;
using Newtonsoft.Json.Bson;

namespace InViBuS.API.Controllers
{
    public class ProjectController : ApiController
    {
        private InViBuSContext db = new InViBuSContext();

        // GET: api/Projects/getprojects
        [HttpGet]
        [Authorize]
        [ActionName("getprojects")]
        //public IQueryable<Project> GetProjects()
        public async Task<List<Project>> GetProjects()
        {
            List<Project> projects = db.Projects.ToList();
            return projects;
        }

        // GET: api/Project/getproject/{id}
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(Project))]
        [ActionName("getproject")]
        public async Task<IHttpActionResult> GetProject(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Project/editproject/{id}
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(void))]
        [ActionName("editproject")]
        public async Task<IHttpActionResult> PutProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.IdProject)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Project
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(Project))]
        [ActionName("postproject")]
        public async Task<IHttpActionResult> PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(project);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = project.IdProject }, project);
        }

        // DELETE: api/Project/
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(Project))]
        [ActionName("deleteproject")]
        public async Task<IHttpActionResult> DeleteProject(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.IdProject == id) > 0;
        }
    }
}