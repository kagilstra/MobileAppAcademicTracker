using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MobileAppKyleGilstrap.Pages;
using MobileAppKyleGilstrap.DataHolders;

namespace MobileAppKyleGilstrap
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing() 
        {
            base.OnAppearing();
            
            
            Termlist.ItemsSource = await Database.GetTerms();
            


        }

        async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTermPage());
        }

        async void Termlist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Term term = (Term)e.SelectedItem;

            await Navigation.PushAsync(new ViewTermPage(term));
        }
    }
}
