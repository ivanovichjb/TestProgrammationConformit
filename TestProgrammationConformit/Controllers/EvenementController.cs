using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestProgrammationConformit.Dtos;
using TestProgrammationConformit.ExceptionsHnadler;
using TestProgrammationConformit.Infrastructures;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProgrammationConformit.Controllers
{
    [Route("api/evenement")]
    [ApiController]
    [Produces("application/json")]
    public class EvenementController : Controller
    {
        private readonly ConformitContext _context;
        public EvenementController(ConformitContext context) {
            _context = context;
        }

        // GET: api/<EvenementController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EvenementDto>>> GetEvenemenAll()
        {
            try {
                var response = await _context.Evenement.Select(x => EvenementToDTO(x)).ToListAsync();
                return Ok(response);
            }
            catch (Exception e) {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Error { Message = e.Message });
            }
        }

        // GET api/<EvenementController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EvenementDto>> GetEvenementById(int id)
        {
            try
            {
                var response = await _context.Evenement.FindAsync(id);
                if (response != null) { return Ok(response); }
                else
                {
                    return NotFound(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Error { Message = e.Message });
            }
        }

        // POST api/<EvenementController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EvenementDto>> CreateEvenements([FromBody] EvenementDto evenementDto)
        {
            try
            {
                var evenement = new Evenement
                {
                    EvenementId = evenementDto.EvenementId,
                    Titre = evenementDto.Titre,
                    Personne = evenementDto.Titre,
                    Commentaire = evenementDto.Commentaire 
                };

                _context.Evenement.Add(evenement);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetEvenementById),
                    new { EvenementId = evenement.EvenementId },
                    EvenementToDTO(evenement));

            }catch (Exception e){
                return StatusCode((int)HttpStatusCode.InternalServerError, new Error { Message = e.Message });
            }
        }

        // PUT api/<EvenementController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEvenements(int id, [FromBody] EvenementDto evenementDto)
        {
            if (id != evenementDto.EvenementId)
            {
                return BadRequest();
            }

            var response = await _context.Evenement.FindAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            response.Titre = evenementDto.Titre;
            response.Personne = evenementDto.Personne;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EvenementExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<EvenementController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEvenementById(int id) 
        { 
            var response = await _context.Evenement.FindAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            try
            {
                _context.Evenement.Remove(response);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Error { Message = e.Message });
            }

            return NoContent();
        }

        private static EvenementDto EvenementToDTO(Evenement evenement) => new EvenementDto {
            EvenementId = evenement.EvenementId,
            Titre = evenement.Titre,
            Personne = evenement.Titre,
            Commentaire = evenement.Commentaire
        };

        private bool EvenementExists(int id) => _context.Evenement.Any(e => e.EvenementId == id);
    }
}