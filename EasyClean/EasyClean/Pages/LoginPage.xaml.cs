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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            // Validate email and password
            if(string.IsNullOrEmpty(entryMail.Text) || string.IsNullOrEmpty(entryPassword.Text))
            {
                await DisplayAlert(AppResources.Warning, AppResources.EmailAndPasswordCompulsory, AppResources.OK);
                return;
            }
            // Log user in
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.LoginUser(entryMail.Text, entryPassword.Text);
            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
            if (response)
            {
                App.Current.MainPage = new MasterPage();
            }
            else
            {
                await DisplayAlert(AppResources.Warning, AppResources.WrongEmailOrPassword, AppResources.OK);
                entryMail.Text = "";
            }
        }

        private void Register_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new RegisterPage();
        }
    }
}