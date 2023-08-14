using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;

namespace MobileAppKyleGilstrap.DataHolders
{
    public static class Database
    {

        #region Database
        private static SQLiteAsyncConnection _db;
        private static SQLiteConnection _dbConnection;

        public static void createDataBase() 
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Final.db");

            _db = new SQLiteAsyncConnection(databasePath);
            _dbConnection = new SQLiteConnection(databasePath);

            _dbConnection.CreateTable<Term>();
            _dbConnection.CreateTable<Course>();
            _dbConnection.CreateTable<Assessment>();
            var termList = _dbConnection.Table<Term>().ToList();
            if (!termList.Any())
            {
                LoadSampleData();
            }
        }
        public static async Task Init()
        {
            if (_db != null)
            {
                return;
            }

            

            
        }


        public static void LoadSampleData() 
        {
           

            Term term = new Term
            {
                TermName = "Term 1",
                TermStart = DateTime.Today.Date,
                TermEnd = DateTime.Today.AddDays(10),
            };

             _dbConnection.Insert(term);

            Course course = new Course
            {
                TermId = term.TermId,
                CourseName = "Course 1",
                CourseStatus = "Ongoing",
                CourseStart = DateTime.Today,
                CourseEnd = DateTime.Today.AddDays(10),
                CourseInstructor = "Kyle Gilstrap",
                CourseInstructorPhone = "4804408988",
                CourseInstructorEmail = "kgilst2@wgu.edu",
                Notes = "These are my course notes.",
                Notifications = true  
            };

            _dbConnection.Insert(course);

            Assessment assessmentObj = new Assessment
            {
                CourseId = course.CourseId,
                AssessmentName = "Assessment 1",
                AssessmentType = "Objective",
                DueDate = DateTime.Today,
                AssessmentAlert = true
            };

            _dbConnection.Insert(assessmentObj);

            Assessment assessmentPer = new Assessment
            {
                CourseId = course.CourseId,
                AssessmentName = "Assessment 2",
                AssessmentType = "Performance",
                DueDate = DateTime.Today.AddDays(10),
                AssessmentAlert = false
            };

            _dbConnection.Insert(assessmentPer);

        }

        #endregion

        #region Term Methods
        public static async Task AddTerm(string name, DateTime startDate, DateTime endDate)
        {
            await Init();
            var term = new Term()
            {
                TermName = name,
                TermStart = startDate,
                TermEnd = endDate
            };
            await _db.InsertAsync(term);

            var id = term.TermId;
        }


        public static async Task<IEnumerable<Term>> GetTerms()
        {
            await Init();
            var terms = await _db.Table<Term>().ToListAsync();
            return terms;
        }

        public static async Task DeleteTerm(int id)
        {
            await Init();

            await _db.DeleteAsync<Term>(id);
        }

        public static async Task UpdateTerm(int id, string Name, DateTime startDate, DateTime endDate)
        {
            await Init();

            var termQuery = await _db.Table<Term>()
                .Where(i => i.TermId == id)
                .FirstOrDefaultAsync();

            if (termQuery != null)
            {
                termQuery.TermName = Name;
                termQuery.TermStart = startDate;
                termQuery.TermEnd = endDate;

                await _db.UpdateAsync(termQuery);
            }
        }

        #endregion

        #region Course Methods

        public static async Task AddCourse(int termID, string name, string status, DateTime start, DateTime end, string instructorName, string instructorPhone, string instructorEmail, string notes, bool notifications)
        {

            await Init();
            var course = new Course
            {
                TermId = termID,
                CourseName = name,
                CourseStatus = status,
                CourseStart = start,
                CourseEnd = end,
                CourseInstructor = instructorName,
                CourseInstructorPhone = instructorPhone,
                CourseInstructorEmail = instructorEmail,
                Notes = notes,
                Notifications = notifications
            };

            await _db.InsertAsync(course);

            var id = course.CourseId;

        }

        public static async Task<IEnumerable<Course>> GetCourses(int id)
        {
            await Init();
            var courses = await _db.QueryAsync<Course>($"Select * From Course Where TermId = '{id}'");
            return courses;
        }

        public static async Task DeleteCourse(int id)
        {
            await Init();

            await _db.DeleteAsync<Course>(id);
        }

        public static async Task UpdateCourse(int courseID, int termID, string name, string status, DateTime start, DateTime end, string instructorName, string instructorPhone, string instructorEmail, string notes, bool notifications) 
        {
            await Init();

            var courseQuery = await _db.Table<Course>()
                .Where(i => i.CourseId == courseID)
                .FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.CourseName = name;
                courseQuery.CourseStatus = status;
                courseQuery.CourseStart = start;
                courseQuery.CourseEnd = end;
                courseQuery.CourseInstructor = instructorName;
                courseQuery.CourseInstructorPhone = instructorPhone;
                courseQuery.CourseInstructorEmail = instructorEmail;
                courseQuery.Notes = notes;
                courseQuery.Notifications = notifications;

                await _db.UpdateAsync(courseQuery);
            }

        }



        #endregion

        #region Assessment Methods

        public static async Task AddAssessment(int courseID, string name, string type, DateTime dueDate, bool notifications)
        {

            await Init();
            var assessment = new Assessment
            {
                CourseId = courseID,
                AssessmentName = name,
                AssessmentType = type,
                DueDate = dueDate,
                AssessmentAlert = notifications
            };

            await _db.InsertAsync(assessment);

            var id = assessment.AssessmentId;

        }

        public static async Task<IEnumerable<Assessment>> GetAssessments(int id)
        {
            await Init();
            var assessments = await _db.QueryAsync<Assessment>($"Select * From Assessment Where CourseId = '{id}'");
            return assessments;
        }

        public static async Task DeleteAssessment(int id)
        {
            await Init();

            await _db.DeleteAsync<Assessment>(id);
        }

        public static async Task UpdateAssessment(int assessmentId, int courseID, string name, string type, DateTime dueDate, bool notifications)
        {
            await Init();

            var assessmentQuery = await _db.Table<Assessment>()
                .Where(i => i.AssessmentId == assessmentId)
                .FirstOrDefaultAsync();

            if (assessmentQuery != null)
            {
                assessmentQuery.AssessmentName = name;
                assessmentQuery.AssessmentType = type;
                assessmentQuery.DueDate = dueDate;
                assessmentQuery.AssessmentAlert = notifications;

                await _db.UpdateAsync(assessmentQuery);
            }
        }

        public static async Task<List<Assessment>> CheckAssessmentObjective(int id) 
        {
            var ObjectiveCount = await _db.QueryAsync<Assessment>($"SELECT AssessmentType FROM Assessment WHERE CourseId = {id} And AssessmentType = 'Objective'");

            return ObjectiveCount;
        }

        public static async Task<List<Assessment>> CheckAssessmentPerformance(int id)
        {
            var PerformanceCount = await _db.QueryAsync<Assessment>($"SELECT AssessmentType FROM Assessment WHERE CourseId = {id} And AssessmentType = 'Performance'");

            return PerformanceCount;
        }

        #endregion
    }
}
