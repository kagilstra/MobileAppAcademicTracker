using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppKyleGilstrap.DataHolders
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentId { get; set; }
        public int CourseId { get; set; } //foreign Key
        public string AssessmentType { get; set; }
        public string AssessmentName { get; set; }
        public bool AssessmentAlert { get; set; }
        public DateTime DueDate { get; set; }

    }
}
