using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MobileAppKyleGilstrap.DataHolders;
using MobileAppKyleGilstrap.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppKyleGilstrap.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCoursePage : ContentPage
    {
        public Course selectedCourse;
        //public event EventHandler SuccessfulEdit;

        #region DataCheckers
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^([0-9]{10})$").Success;
        }
        #endregion
        public EditCoursePage(Course course)
        {
            InitializeComponent();

            selectedCourse = course;
            BindingContext = selectedCourse;
        }

        async void CancelCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void SaveCourse_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CourseName.Text))
            {
                await DisplayAlert("ERROR", "Please enter Course name", "OK");
                return;
            }

            if (CourseStatus == null)
            {
                await DisplayAlert("ERROR", "Please Select Course Status", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CourseInstuctorName.Text))
            {
                await DisplayAlert("ERROR", "Please enter Instructor name", "OK");
                return;
            }

            if (CourseStart.Date < DateTime.Today)
            {
                await DisplayAlert("ERROR", "Start Date cannot be in the past", "OK");
                return;
            }

            if (CourseStart.Date >= CourseEnd.Date)
            {
                await DisplayAlert("ERROR", "End Date must be after Start Date", "OK");
                return;
            }

            if (CourseInstructorEmail.Text == null)
            {
                await DisplayAlert("ERROR", "Please enter an Email address", "OK");
                return;
            }

            if (IsValidEmail(CourseInstructorEmail.Text) == false)
            {
                await DisplayAlert("ERROR", "Invalid email address", "OK");
                return;
            }

            if (CourseInstructorPhone.Text == null)
            {
                await DisplayAlert("ERROR", "Please enter an Phone Number", "OK");
                return;
            }

            if (IsPhoneNumber(CourseInstructorPhone.Text) == false)
            {
                await DisplayAlert("ERROR", "Invalid Phone Number.Phone Number must be ten digits ex: 1112223333", "OK");
                return;
            }

            await Database.UpdateCourse(selectedCourse.CourseId, selectedCourse.TermId, CourseName.Text, CourseStatus.SelectedItem.ToString(), CourseStart.Date, CourseEnd.Date, CourseInstuctorName.Text, CourseInstructorPhone.Text, CourseInstructorEmail.Text, CourseNotes.Text, Notifications.IsChecked);

            await Navigation.PopAsync();

        }
    }
}