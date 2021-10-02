using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form_Builder.Models
{
    /*
     * Form Class
     * represents the form that the user will build
     */
    public class Form
    {
        // the id of the form
        public int Id { get; set; }
        // the content (fields) of the form
        public String FormContent { get; set; }
        // the submissions that are made to the form
        public ICollection<Submission> Submissions { get; set; }
    }
}
