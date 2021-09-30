using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form_Builder.Models
{
    public class Form
    {
        public int Id { get; set; }
        public String FormContent { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
