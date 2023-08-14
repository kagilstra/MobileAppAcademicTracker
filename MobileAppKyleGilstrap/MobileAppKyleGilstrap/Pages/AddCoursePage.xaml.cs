using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileAppKyleGilstrap.DataHolders;

namespace MobileAppKyleGilstrap.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class AddCoursePage : ContentPage
    {
        Term selectedTerm;
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
        public AddCoursePage(Term term)
        {
            InitializeComponent();
            selectedTerm = term;
            BindingContext = selectedTerm;
        }

        async void SaveCourse_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CourseName.Text))
            {
                await DisplayAlert("ERROR", "Please enter Course name", "OK");
                return;
            }

            if (CourseStatus.SelectedItem == null)
            {
                await DisplayAlert("ERROR", "Please Select Course Status", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(InstructorName.Text))
            {
                await DisplayAlert("ERROR", "Please enter Instructor name", "OK");
                return;
            }

            if (StartDatePicker.Date < DateTime.Today)
            {
                await DisplayAlert("ERROR", "Start Date cannot be in the past", "OK");
                return;
            }

            if (StartDatePicker.Date >= EndDatePicker.Date)
            {
                await DisplayAlert("ERROR", "End Date must be after Start Date", "OK");
                return;
            }
            
            if (InstructorEmail.Text == null) 
            {
                await DisplayAlert("ERROR", "Please enter an Email address", "OK");
                return;
            }

            if (IsValidEmail(InstructorEmail.Text) == false) 
            {
                await DisplayAlert("ERROR", "Invalid email address", "OK");
                return;
            }

            if (InstructorPhone.Text == null)
            {
                await DisplayAlert("ERROR", "Please enter an Phone Number", "OK");
                return;
            }

            if (IsPhoneNumber(InstructorPhone.Text) == false)
            {
                await DisplayAlert("ERROR", "Invalid Phone Number. Phone Number must be ten digits ex:1112223333", "OK");
                return;
            }

            await Database.AddCourse(selectedTerm.TermId, CourseName.Text, CourseStatus.SelectedItem.ToString(), StartDatePicker.Date, EndDatePicker.Date, InstructorName.Text, InstructorPhone.Text, InstructorEmail.Text, Notes.Text, Notifications.IsChecked);

            await Navigation.PopAsync();
        }

        async void CancelCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}