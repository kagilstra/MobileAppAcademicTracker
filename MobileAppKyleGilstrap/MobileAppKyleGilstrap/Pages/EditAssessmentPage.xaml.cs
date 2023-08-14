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
    public partial class EditAssessmentPage : ContentPage
    {

        Assessment selectedAssessment;
        public EditAssessmentPage(Assessment assessment)
        {
            InitializeComponent();

            selectedAssessment = assessment;
            BindingContext = selectedAssessment;
        }

        async void SaveAssessment_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(AssessmentName.Text))
            {
                await DisplayAlert("ERROR", "Please enter Assessment name", "OK");
                return;
            }

            if (AssessmentDue.Date < DateTime.Today)
            {
                await DisplayAlert("ERROR", "Due Date cannot be in the past", "OK");
                return;
            }

            

            await Database.UpdateAssessment(selectedAssessment.AssessmentId ,selectedAssessment.CourseId, AssessmentName.Text, selectedAssessment.AssessmentType.ToString(), selectedAssessment.DueDate.Date, selectedAssessment.AssessmentAlert);
            await Navigation.PopAsync();
        }

        async void CancelAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}