using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAppKyleGilstrap.DataHolders;
using MobileAppKyleGilstrap.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace MobileAppKyleGilstrap.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCoursePage : ContentPage
    {
        Course selectedCourse;
        public ViewCoursePage(Course course)
        {
            InitializeComponent();

            selectedCourse = course;
            BindingContext = selectedCourse;
        }

        protected override async void OnAppearing() 
        {

            base.OnAppearing();

            CourseName.Text = selectedCourse.CourseName;
            CourseStatus.Text = selectedCourse.CourseStatus;
            StartDate.Text = selectedCourse.CourseStart.ToString("MM/dd/yy");
            EndDatePicker.Text = selectedCourse.CourseEnd.ToString("MM/dd/yy");
            InstructorName.Text = selectedCourse.CourseInstructor;
            InstructorPhone.Text = selectedCourse.CourseInstructorPhone;
            InstructorEmail.Text = selectedCourse.CourseInstructorEmail;
            CourseNotes.Text = selectedCourse.Notes;
            Notifications.Text = selectedCourse.Notifications.ToString();

            Assessmentlist.ItemsSource = await Database.GetAssessments(selectedCourse.CourseId);

            if (selectedCourse.Notifications == true && (selectedCourse.CourseStart.ToString("MM/dd/yy") == DateTime.Today.ToString("MM/dd/yy")))
            {
                await DisplayAlert("Alert", "This course starts today", "OK");
                return;
            }
            if (selectedCourse.Notifications == true && (selectedCourse.CourseEnd.ToString("MM/dd/yy") == DateTime.Today.ToString("MM/dd/yy")))
            {
                await DisplayAlert("Alert", "This course ends today", "OK");
                return;
            }

            

        }

        async void EditCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCoursePage(selectedCourse));
        }


        async void RemoveCourse_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("WARNING", "Are you sure you want to DELETE this Course?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(selectedCourse.CourseId.ToString());

                await Database.DeleteCourse(id);

                await Navigation.PopAsync();

            }
        }

        async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessmentPage(selectedCourse));
        }

        async void Assessmentlist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Assessment assessment = (Assessment)e.SelectedItem;

            await Navigation.PushAsync(new ViewAssessmentPage(assessment));
        }

        async void ShareNotes_Clicked(object sender, EventArgs e)
        {
            var text = CourseNotes.Text;
            await Share.RequestAsync(new ShareTextRequest { Text = text, Title = "Share Text" });
        }
    }
}