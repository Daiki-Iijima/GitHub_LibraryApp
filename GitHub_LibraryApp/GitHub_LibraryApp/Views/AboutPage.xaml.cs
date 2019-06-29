using GitHub_LibraryApp.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GitHub_LibraryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// UserIDの入力が完了したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_Completed_UserID(object sender, EventArgs e)
        {
            //Enterを押すと表示
            DisplayAlert("", this.user_entry.Text, "OK");
        }

        /// <summary>
        /// PassWordの入力が完了したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_Completed_PassWord(object sender, EventArgs e)
        {
            //Enterを押すと表示
            DisplayAlert("", this.pass_entry.Text, "OK");
        }
    }
}