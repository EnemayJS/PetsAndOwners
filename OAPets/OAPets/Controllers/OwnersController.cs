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
using OAPets.Models;

namespace OAPets.Controllers
{
    public class OwnersController : ApiController
    {
        private OAPetsContext db = new OAPetsContext();

        // GET api/Owners?page={page}&itemsPerPage={itemsPerPage}&reverse={reverse}
        // {page} - number of current page
        // {itemsPerPage} - how many items display in one page      
        public IHttpActionResult GetOwners(
            int page = 1,
            int itemsPerPage = 3
            )
        {
            //getting all owners from db including pets they have and ordering them to allow Skip() usage
            var allOwners = db.Owners.Include(p => p.Pets).OrderBy(o => o.Id);
            // formating owners for pagination 
            //((page - 1) * itemsPerPage) - skip owners from previous pages
            var usersPaged = allOwners.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            // formatting owners data using OwnersDTO data transfer object model
            //to avoid serialization circular reference problem 
            var ownersData = usersPaged.Select(x => new OwnerDTO()
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        PetsCount = x.Pets.Count()
                                    });
            // json result
            var json = new
            {
                // count - is number of all owners in db 
                count = db.Owners.Count(),
                owners = ownersData
            };

            return Ok(json);
        }

        // GET: api/Owners/{id}?page={page}&itemsPerPage={itemsPerPage}&reverse={reverse}
        // {id} - id of current owner
        // {page} - number of current page
        // {itemsPerPage} - how many items display in one page
        
        [ResponseType(typeof(OwnerDetailsDTO))]
        public async Task<IHttpActionResult> GetOwner(
            int id, 
            int page = 1,
            int itemsPerPage = 3
           )
        {   
            // finds owners in db by id and return error if not found
            Owner owner = await db.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            // getting all owner's pets from db by matching their OwnerId property
            // and ordering them to allow Skip() usage
            var allPets = db.Pets.Where(p => p.OwnerId == id).OrderBy(p => p.Id);

            // formating pets for pagination 
            //((page - 1) * itemsPerPage) - skip pets from previous pages
            var petsPaged = allPets.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            // formatting owners data using OwnersPetsDTO data transfer object model
            //to avoid serialization circular reference problem
            var pets = petsPaged.Select(x => new OwnersPetsDTO
                                  {
                                      PetId = x.Id,
                                      PetName = x.Name,                                      
                                  }

                ).ToArray();

            var ownerName = owner.Name;
            var count = allPets.Count();
            // formating response 
            // response.Count - number of all owner's pets in db
            var response = new OwnerDetailsDTO();
            response.OwnerName = ownerName;
            response.Pets = pets;
            response.Count = count;

            return Ok(response);
           
            {

            }
        }

        // PUT: api/Owners/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOwner(int id, Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != owner.Id)
            {
                return BadRequest();
            }

            db.Entry(owner).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
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

        // POST: api/Owners
        [ResponseType(typeof(Owner))]
        public async Task<IHttpActionResult> PostOwner(Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Owners.Add(owner);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = owner.Id }, owner);
        }

        // DELETE: api/Owners/5
        [ResponseType(typeof(Owner))]
        public async Task<IHttpActionResult> DeleteOwner(int id)
        {
            Owner owner = await db.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            db.Owners.Remove(owner);
            await db.SaveChangesAsync();

            return Ok(owner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnerExists(int id)
        {
            return db.Owners.Count(e => e.Id == id) > 0;
        }
    }
}