using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Form_Builder.Models;

namespace Form_Builder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FormBuilderDBContext _context;

        public HomeController(ILogger<HomeController> logger, FormBuilderDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forms()
        {
            var forms = _context.Forms.ToList();
            return View(forms);
        }

        public IActionResult FillForm(int id)
        {
            var form = _context.Forms.Where(f => f.Id == id).FirstOrDefault();
            return View(form);
        }
        public async Task<bool> SubmitFormAsync(int formId, String submissionData)
        {
            try
            {
                var submission = new Submission();
                submission.Form = _context.Forms.Where(f => f.Id == formId).FirstOrDefault();
                submission.SubmissionData = submissionData;
                _context.Add(submission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveFormTemplateAsync(String formData)
        {
            try
            {
                Form form = new Form();
                form.FormContent = formData;
                _context.Add(form);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
