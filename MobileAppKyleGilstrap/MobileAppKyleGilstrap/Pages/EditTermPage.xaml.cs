using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAppKyleGilstrap.DataHolders;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppKyleGilstrap.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTermPage : ContentPage
    {
        Term selectedTerm;
        public EditTermPage(Term term)
        {
            InitializeComponent();

            selectedTerm = term;
            BindingContext = selectedTerm;
        }

        async void SaveTerm_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TermName.Text))
            {
                await DisplayAlert("ERROR", "Please enter term name", "OK");
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

            await Database.UpdateTerm(selectedTerm.TermId, TermName.Text, StartDatePicker.Date, EndDatePicker.Date);
            await Navigation.PopAsync();

        }

        async void CancelTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}