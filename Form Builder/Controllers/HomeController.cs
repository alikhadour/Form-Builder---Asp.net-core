using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Form_Builder.Models;
using Form_Builder.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Form_Builder.Controllers
{
    /**
     * Home Controller
     */
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FormBuilderDBContext _context;
        private readonly IConfiguration _configuration;

        /*
         * Home controller construcor
         */
        public HomeController(ILogger<HomeController> logger, FormBuilderDBContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        // Callback for the index page
        public IActionResult Index()
        {
            return View();
        }

        // Callback for the forms page
        public IActionResult Forms()
        {
            // get all the forms from the database, and send them to the forms page
            var forms = _context.Forms.ToList();
            return View(forms);
        }

        // Callback for the form rendering page
        // @param {int} id: the id of the form
        public IActionResult FillForm(int id)
        {
            // get the data of the form with the requested id, 
            // and send it to the form page to be rendered
            var form = _context.Forms.Find(id);
            if (form == null)
            {
                _logger.LogError("Requested form with id " + id + " was not found");
                return RedirectToAction("PageNotFound");
            }
            return View(form);
        }

        // Callback for the page that lists submissions of a form
        // @param {int} id: the id of the form
        public IActionResult ListFormSubmissions(int id)
        {
            // get the data of the submissions with the requested form id, 
            var form = _context.Forms.Find(id);
            if (form == null)
            {
                _logger.LogError("Requested form with id " + id + " was not found");
                return RedirectToAction("PageNotFound");
            }
            var submissions = _context.Submissions.Where(s => s.FormId == id).ToList();
            var formDetails = new FormDetails();
            formDetails.Form = form;
            formDetails.Submissions = submissions;
            return View(formDetails);
        }

        // Callback to save a submission of a form in the database
        // @param {int} formId: the id of the submited form
        // @param {String} submissionData: the submited data
        public async Task<bool> SubmitFormAsync(int formId, String submissionData)
        {
            try
            {
                // create the submission and save it to the database
                var submission = new Submission();
                submission.Form = _context.Forms.Where(f => f.Id == formId).FirstOrDefault();
                submission.SubmissionData = submissionData;
                _context.Add(submission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogInformation("Saving a new form.");
                _logger.LogError(e.Message);
                return false;
            }
        }

        // Callback to save a new form in the database
        // @param {String} formData: the content of the new form
        public async Task<bool> SaveFormTemplateAsync(String formData)
        {
            try
            {
                // create the form instance and save it to the database
                Form form = new Form();
                form.FormContent = formData;
                form.FormContent = formData;
                _context.Add(form);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogInformation("Saving a new form submission.");
                _logger.LogError(e.Message);
                return false;
            }
        }

        // Callback to return a map window content with locations marked on it
        // @param {String[]} selectedLocations: the selected location from the data grid
        public IActionResult ShowPointsOnMap(String[] selectedLocations)
        {
            var mapAccessToken = _configuration.GetValue<String>("MapAccessToken");
            try
            {
                List<Location> locations = new List<Location>();
                foreach (String location in selectedLocations)
                {
                    Location l = new Location();
                    l.lat = Double.Parse(location.Split(',')[0]);
                    l.lng = Double.Parse(location.Split(',')[1]);
                    locations.Add(l);
                }
                ViewData["MapAccessToken"] = mapAccessToken;
                return View(locations);
            }
            catch(Exception e)
            {
                _logger.LogInformation("Requesting map to show locations");
                _logger.LogError("Some location values are not correct");
                return RedirectToAction("BadRequest: " + e.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Page not found action
        [Route("/NotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        // Bad request action
        public IActionResult BadRequest()
        {
            return View();
        }
    }
}
