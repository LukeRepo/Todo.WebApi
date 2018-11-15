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
    public class TasksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Tasks
        public IQueryable<Lavoro> GetLavoroes()
        {
            return db.Lavoroes;
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Lavoro))]
        public async Task<IHttpActionResult> GetLavoro(string id)
        {
            Lavoro lavoro = await db.Lavoroes.FindAsync(id);
            if (lavoro == null)
            {
                return NotFound();
            }

            return Ok(lavoro);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLavoro(string id, Lavoro lavoro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lavoro.NomeTask)
            {
                return BadRequest();
            }

            db.Entry(lavoro).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LavoroExists(id))
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

        // POST: api/Tasks
        [ResponseType(typeof(Lavoro))]
        public async Task<IHttpActionResult> PostLavoro(Lavoro lavoro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lavoroes.Add(lavoro);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LavoroExists(lavoro.NomeTask))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lavoro.NomeTask }, lavoro);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Lavoro))]
        public async Task<IHttpActionResult> DeleteLavoro(string id)
        {
            Lavoro lavoro = await db.Lavoroes.FindAsync(id);
            if (lavoro == null)
            {
                return NotFound();
            }

            db.Lavoroes.Remove(lavoro);
            await db.SaveChangesAsync();

            return Ok(lavoro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LavoroExists(string id)
        {
            return db.Lavoroes.Count(e => e.NomeTask == id) > 0;
        }
    }
}