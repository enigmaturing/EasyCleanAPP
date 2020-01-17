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
            if(string.IsNullOrEmpty(entryMail.Text) || string.IsNullOrEmpty(entryPassword.Text))
            {
                await DisplayAlert("WARNING", "Email and password are necessary to log in", "OK");
                return;
            }
            ApiServices apiServices = new ApiServices();
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;
            bool response = await apiServices.LoginUser(entryMail.Text, entryPassword.Text);
            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
            if (response)
            {
                App.Current.MainPage = new MasterPage();
            }
            else
            {
                await DisplayAlert("WARNING", "Wrong email or password", "OK");
            }
        }

        private void Register_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new RegisterPage();
        }
    }
}