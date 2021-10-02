using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form_Builder.Models
{
    /*
     * Submission Class
     * represents the data submited for a form
     */
    public class Submission
    {
        // the id of the submission
        public int Id { get; set; }
        // the data of the submission
        public String SubmissionData { get; set; }
        // the id of the form that the submission is made for
        public int FormId { get; set; }
        // the form that the submission is made for
        public Form Form { get; set; }
    }
}
