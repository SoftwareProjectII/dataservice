using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Surveys")]
    public class SurveysController : Controller
    {
        private readonly _17SP2G4Context _context;

        public SurveysController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Surveys
        [HttpGet]
        public IEnumerable<Survey> GetSurvey()
        {
            return _context.Survey;
        }

        // GET: api/Surveys/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurvey([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var survey = await _context.Survey.SingleOrDefaultAsync(m => m.SurveyId == id);

            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        // GET: api/Surveys/5/questions
        [HttpGet("{id}/questions")]
        public async Task<IActionResult> GetQuestions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questions = await _context.Survey.Where(m => m.SurveyId == id).Select(m => m.Surveyquestion).ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

        // GET: api/Surveys/5/trainings
        [HttpGet("{id}/trainings")]
        public async Task<IActionResult> GetTrainings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainings = await _context.Trainingsurvey.Where(m => m.SurveyId == id).Select(m => m.Training).ToListAsync();

            if (trainings == null)
            {
                return NotFound();
            }

            return Ok(trainings);
        }

        // PUT: api/Surveys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurvey([FromRoute] int id, [FromBody] Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != survey.SurveyId)
            {
                return BadRequest();
            }

            _context.Entry(survey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
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

        // POST: api/Surveys
        [HttpPost]
        public async Task<IActionResult> PostSurvey([FromBody] Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Survey.Add(survey);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurvey", new { id = survey.SurveyId }, survey);
        }

        // DELETE: api/Surveys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurvey([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var survey = await _context.Survey.SingleOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
            {
                return NotFound();
            }

            _context.Survey.Remove(survey);
            await _context.SaveChangesAsync();

            return Ok(survey);
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.SurveyId == id);
        }
    }
}