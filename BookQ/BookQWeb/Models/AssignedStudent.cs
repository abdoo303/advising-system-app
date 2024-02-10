using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookQWeb.Models;

public partial class AssignedStudent
    {
        [Key]
        public int student_id { get; set; }
        public string Student_name { get; set; }
        public string major { get; set; }
        public string Course_name { get; set; }
    }


