using Form_Builder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form_Builder.ViewModels
{
    // Form Details
    // the form and its submissions
    public class FormDetails
    {
        public Form Form { get; set; }
        public List<Submission> Submissions { get; set; }
    }
}
