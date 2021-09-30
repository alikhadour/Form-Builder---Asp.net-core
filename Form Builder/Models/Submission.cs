using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form_Builder.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public String SubmissionData { get; set; }
        public int FormId { get; set; }
        public Form Form { get; set; }
    }
}
