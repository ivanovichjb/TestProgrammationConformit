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
    [Route("api/commentaire")]
    [ApiController]
    [Produces("application/json")]
    public class CommentaireController : Controller
    {
        private readonly ConformitContext _context;
        public CommentaireController(ConformitContext context)
        {
            _context = context;
        }

        // GET: api/<CommentaireController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CommentaireDto>>> GetCommentaireAll()
        {
            try
            {
                var response = await _context.Commentaire.Select(x => CommentaireToDTO(x)).ToListAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Error { Message = e.Message });
            }
        }

        // GET api/<CommentaireController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommentaireDto>> GetCommentaireById(int id)
        {
            try
            {
                var response = await _context.Commentaire.FindAsync(id);
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

        // POST api/<CommentaireController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommentaireDto>> CreateCommentaire([FromBody] CommentaireDto commentaireDto)
        {
            try
            {
                var commentaire = new Commentaire
                {
                    CommentaireId = commentaireDto.CommentaireId,
                    Description = commentaireDto.Description,
                    Date = commentaireDto.Date,
                    EvenementId = commentaireDto.EvenementId
                };

                _context.Commentaire.Add(commentaire);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetCommentaireById),
                    new { CommentaireId = commentaire.CommentaireId },
                    CommentaireToDTO(commentaire));

            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Error { Message = e.Message });
            }
        }

        // PUT api/<CommentaireController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCommentaires(int id, [FromBody] CommentaireDto commentaireDto)
        {
            if (id != commentaireDto.CommentaireId)
            {
                return BadRequest();
            }

            var response = await _context.Commentaire.FindAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            response.Description = commentaireDto.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CommentaireExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<ComentaireController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCommentaireById(int id)
        {
            var response = await _context.Commentaire.FindAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            try
            {
                _context.Commentaire.Remove(response);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Error { Message = e.Message });
            }

            return NoContent();
        }

        private static CommentaireDto CommentaireToDTO(Commentaire commentaire) => new CommentaireDto
        {
            CommentaireId = commentaire.CommentaireId,
            Description = commentaire.Description,
            Date = commentaire.Date,
            EvenementId = commentaire.EvenementId,
            Evenement = commentaire.Evenement
        };

        private bool CommentaireExists(int id) => _context.Commentaire.Any(e => e.CommentaireId == id);
    }
}
