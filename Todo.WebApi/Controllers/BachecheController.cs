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
using Todo.WebApi.Models;
using Todo.WebApi.Models.EF_DataBase;

namespace Todo.WebApi.Controllers
{
    public class BachecheController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Bacheche
        public IQueryable<Bacheca> GetBachecas()
        {
            return db.Bachecas;
        }

        // GET: api/Bacheche/5
        [ResponseType(typeof(Bacheca))]
        public async Task<IHttpActionResult> GetBacheca(string id)
        {
            Bacheca bacheca = await db.Bachecas.FindAsync(id);
            if (bacheca == null)
            {
                return NotFound();
            }

            return Ok(bacheca);
        }

        // PUT: api/Bacheche/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBacheca(string id, Bacheca bacheca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bacheca.NomeBacheca)
            {
                return BadRequest();
            }

            db.Entry(bacheca).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BachecaExists(id))
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

        // POST: api/Bacheche
        [ResponseType(typeof(Bacheca))]
        public async Task<IHttpActionResult> PostBacheca(Bacheca bacheca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bachecas.Add(bacheca);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BachecaExists(bacheca.NomeBacheca))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bacheca.NomeBacheca }, bacheca);
        }

        // DELETE: api/Bacheche/5
        [ResponseType(typeof(Bacheca))]
        public async Task<IHttpActionResult> DeleteBacheca(string id)
        {
            Bacheca bacheca = await db.Bachecas.FindAsync(id);
            if (bacheca == null)
            {
                return NotFound();
            }

            db.Bachecas.Remove(bacheca);
            await db.SaveChangesAsync();

            return Ok(bacheca);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BachecaExists(string id)
        {
            return db.Bachecas.Count(e => e.NomeBacheca == id) > 0;
        }
    }
}