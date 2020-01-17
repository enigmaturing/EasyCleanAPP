using EasyClean.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyClean.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

        private async void btnCreateProfile_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            if (entryPassword1.Text == entryPassword2.Text)
            {
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;
                bool response = await apiServices.RegisterUser(entryEmail.Text, entryPassword1.Text, entryPassword2.Text);
                if (response)
                {
                    await DisplayAlert("Success", "User profile was successfully created", "OK");
                }
                else
                {
                    await DisplayAlert("ERROR", "User profile could not be created", "OK");
                }
                activityIndicator.IsVisible = false;
                activityIndicator.IsRunning = false;
            }
            else
            {
                await DisplayAlert("WARNING","Passwords do not match", "OK");
            }
        }
    }
}