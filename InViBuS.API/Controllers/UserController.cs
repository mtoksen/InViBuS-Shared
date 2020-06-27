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
using InViBuS.API.Services;
using Microsoft.AspNet.Identity;

namespace InViBuS.API.Controllers
{
    public class UserController : ApiController
    {
        private InViBuSContext db = new InViBuSContext();

        private UserManager<User> _userManager; 

        public UserController()
            : this(new UserManager<User>(new UserStoreService())) { }

        public UserController(UserManager<User> userManager)
        {
            userManager = this._userManager;
        }

        [HttpGet]
        [Authorize]
        [ActionName("getusers")]
        // GET: api/Users/getusers
        public async Task<IHttpActionResult> GetUsers()
        {
            var users = await db.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Authorize]
        [ActionName("getuser")]
        // GET: api/Users/getuser/{id}
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            List<User> user = null;
            try
            {
                 user = await db.Users.Where(x => x.UserName == id).ToListAsync();
                 if (user == null)
                 {
                     return NotFound();
                 }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok(user);
        }

        // PUT: api/Users/edituser
        [HttpPut]
        [Authorize]
        [ActionName("edituser")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> EditUser(string id, User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.UserName)
            {
                return BadRequest();
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        // POST: api/Users/register
        [HttpPost]
        [AllowAnonymous]
        [ActionName("register")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> RegisterUser(User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(model);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (UserExists(model.Id))
                {
                    return Conflict();
                }
                else
                {
                    Console.WriteLine(e.Message);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        [HttpDelete]
        [ActionName("deleteuser")]
        [Authorize]
        // DELETE: api/Users/deleteuser/{id}
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}