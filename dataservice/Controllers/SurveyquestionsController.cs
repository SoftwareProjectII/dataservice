using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Surveyquestions")]
    public class SurveyquestionsController : Controller
    {
        private readonly _17SP2G4Context _context;

        public SurveyquestionsController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Surveyquestions
        [HttpGet]
        public IEnumerable<Surveyquestion> GetSurveyquestion()
        {
            return _context.Surveyquestion;
        }

        // GET: api/Surveyquestions/5
        [HttpGet("{id}", Name = "Surveyquestion_Single")]
        public async Task<IActionResult> GetSurveyquestion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveyquestion = await _context.Surveyquestion.SingleOrDefaultAsync(m => m.QuestionId == id);

            if (surveyquestion == null)
            {
                return NotFound();
            }

            return Ok(surveyquestion);
        }

        // GET: api/Surveyquestions/5/survey
        [HttpGet("{id}/survey")]
        public async Task<IActionResult> GetSurvey([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var survey = await _context.Surveyquestion.Where(m => m.QuestionId == id).Select(m => m.Survey).FirstOrDefaultAsync();

            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        // GET: api/Surveyquestions/5/answers
        [HttpGet("{id}/answers")]
        public async Task<IActionResult> GetAnswers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var answers = await _context.Surveyanswer.Where(m => m.QuestionId == id).ToListAsync();

            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }

        // PUT: api/Surveyquestions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyquestion([FromRoute] int id, [FromBody] Surveyquestion surveyquestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != surveyquestion.QuestionId)
            {
                return BadRequest();
            }

            _context.Entry(surveyquestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyquestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Surveyquestions
        [HttpPost]
        public async Task<IActionResult> PostSurveyquestion([FromBody] Surveyquestion surveyquestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Surveyquestion.Add(surveyquestion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveyquestion", new { id = surveyquestion.QuestionId }, surveyquestion);
        }

        // DELETE: api/Surveyquestions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyquestion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveyquestion = await _context.Surveyquestion.SingleOrDefaultAsync(m => m.QuestionId == id);
            if (surveyquestion == null)
            {
                return NotFound();
            }

            _context.Surveyquestion.Remove(surveyquestion);
            await _context.SaveChangesAsync();

            return Ok(surveyquestion);
        }

        private bool SurveyquestionExists(int id)
        {
            return _context.Surveyquestion.Any(e => e.QuestionId == id);
        }
    }
}