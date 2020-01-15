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
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            Detail = new NavigationPage(new MyProfilePage());
        }

        private void btnMyProfile_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MyProfilePage());
            IsPresented = false;
        }

        private void btnMyVouchers_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MyVouchers());
            IsPresented = false;
        }
    }
}