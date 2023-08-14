using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppKyleGilstrap.DataHolders
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }
        public int TermId { get; set; } //ForeignKey
        public string CourseName { get; set; }
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }
        public string CourseStatus { get; set; }
        public string Notes { get; set; }
        public string CourseInstructor { get; set; }
        public string CourseInstructorPhone { get; set; }
        public string CourseInstructorEmail { get; set; }
        public bool Notifications { get; set; }
    }
}
