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
    public partial class ViewAssessmentPage : ContentPage
    {
        Assessment selectedAssessment;
        public ViewAssessmentPage(Assessment assessment)
        {
            InitializeComponent();

            selectedAssessment = assessment;
            BindingContext = selectedAssessment;
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            AssessmentName.Text = selectedAssessment.AssessmentName;
            AssessmentType.Text = selectedAssessment.AssessmentType;
            DueDate.Text = selectedAssessment.DueDate.ToString("MM/dd/yy");
            AssessmentNotify.Text = selectedAssessment.AssessmentAlert.ToString();


            if (selectedAssessment.AssessmentAlert == true)
            {
                await DisplayAlert("Alert", "This Assessment is due on " + selectedAssessment.DueDate.ToString("MM/dd/yy"), "OK");
                return;
            }
        }

        async void EditAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAssessmentPage(selectedAssessment));
        }

        async void RemoveAssessment_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("WARNING", "Are you sure you want to DELETE this Assessment?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(selectedAssessment.AssessmentId.ToString());

                await Database.DeleteAssessment(id);

                await Navigation.PopAsync();

            }
        }
    }
}