using EasyClean.Resx;
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
            // validate email and password
            if (string.IsNullOrEmpty(entryEmail.Text) || string.IsNullOrEmpty(entryPassword1.Text) || string.IsNullOrEmpty(entryPassword2.Text))
            {
                await DisplayAlert(AppResources.Error, AppResources.EmailAndPasswordCompulsory, AppResources.OK);
                return;
            }
            if (!entryEmail.Text.Contains("@") || !entryEmail.Text.Contains("."))
            {
                await DisplayAlert(AppResources.Error, AppResources.EmailFormatNotVaild, AppResources.OK);
                entryEmail.Text = "";
                return;
            }
            if (entryPassword1.Text != entryPassword2.Text)
            {
                await DisplayAlert(AppResources.Error, AppResources.PasswordsNotMatching, AppResources.OK);
                entryPassword1.Text = "";
                entryPassword2.Text = "";
                return;
            }

            // register the user
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.RegisterUser(entryEmail.Text, entryPassword1.Text, entryPassword2.Text);
            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
            if (response)
            {
                await DisplayAlert(AppResources.Success, AppResources.UserCreated, AppResources.OK);
            }
            else
            {
                await DisplayAlert(AppResources.Error, AppResources.UserNotCreated, AppResources.OK);
            }
        }
    }
}