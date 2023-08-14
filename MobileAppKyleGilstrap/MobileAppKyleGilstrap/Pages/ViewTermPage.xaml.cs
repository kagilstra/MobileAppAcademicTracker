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
    public partial class ViewTermPage : ContentPage
    {
        Term selectedTerm;
        public ViewTermPage(Term term)
        {
            InitializeComponent();

            selectedTerm = term;
            BindingContext = selectedTerm;
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            TermName.Text = selectedTerm.TermName;
            Courselist.ItemsSource = await Database.GetCourses(selectedTerm.TermId);

        }

        async void EditTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTermPage(selectedTerm));
        }

        async void AddCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCoursePage(selectedTerm));
        }

        async void RemoveTerm_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("WARNING", "Are you sure you want to DELETE this term?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(selectedTerm.TermId.ToString());

                await Database.DeleteTerm(id);

                await Navigation.PopAsync();

            }

        }

        async void Courselist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Course course = (Course)e.SelectedItem;

            await Navigation.PushAsync(new ViewCoursePage(course));
        }
    }
}