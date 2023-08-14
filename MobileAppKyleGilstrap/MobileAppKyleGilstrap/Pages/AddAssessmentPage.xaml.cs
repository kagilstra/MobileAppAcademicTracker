using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAppKyleGilstrap.DataHolders;
using MobileAppKyleGilstrap.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppKyleGilstrap.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAssessmentPage : ContentPage
    {
        Course selectedCourse;
        public AddAssessmentPage(Course course)
        {
            InitializeComponent();

            selectedCourse = course;
            BindingContext = selectedCourse;
        }

        async void SaveAssessment_Clicked(object sender, EventArgs e)
        {
            var Objcount = await Database.CheckAssessmentObjective(selectedCourse.CourseId);
            var Percount = await Database.CheckAssessmentPerformance(selectedCourse.CourseId);

            if (Objcount.Count == 1 && AssessmentType.SelectedItem.ToString() == "Objective") 
            {
                await DisplayAlert("ERROR", "Can only have one OBJECTIVE assessment", "OK");
                return;
            }

            if (Percount.Count == 1 && AssessmentType.SelectedItem.ToString() == "Performance")
            {
                await DisplayAlert("ERROR", "Can only have one PERFORMANCE assessment", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(AssessmentName.Text))
            {
                await DisplayAlert("ERROR", "Please enter Assessment name", "OK");
                return;
            }

            if (DueDatePicker.Date < DateTime.Today)
            {
                await DisplayAlert("ERROR", "Due Date cannot be in the past", "OK");
                return;
            }


            await Database.AddAssessment(selectedCourse.CourseId ,AssessmentName.Text, AssessmentType.SelectedItem.ToString(), DueDatePicker.Date, AssessmentNotifications.IsChecked);
            await Navigation.PopAsync();
        }

        private void CancelAssessment_Clicked(object sender, EventArgs e)
        {

        }
    }
}