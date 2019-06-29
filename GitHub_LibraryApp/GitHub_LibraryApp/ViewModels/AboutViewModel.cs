using Octokit;
using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace GitHub_LibraryApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string userID;
        private string passWord;

        public AboutViewModel()
        {
            Title = "GitHubデータ取得";

            OpenWebCommand = new Command(async () =>
            {
                var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));

                var basicAuth = new Credentials(userID, passWord);
                client.Credentials = basicAuth;

                //// you can also specify a search term here
                //var request = new SearchRepositoriesRequest("bootstrap");

                //var result = await client.Search.SearchRepo(request);

                //foreach (var data in result.Items)
                //{
                //    System.Diagnostics.Trace.WriteLine($"{data.Name}");
                //}
                var dirInfo = await client.Repository.Content.GetAllContents(userID, "TIL");
                
                foreach (RepositoryContent data in dirInfo)
                {
                    System.Diagnostics.Trace.WriteLine($"{data.Path}");
                }

                //foreach (var data in getData)
                //{
                //    System.Diagnostics.Trace.WriteLine($"{data.FullName}");
                //}

            });
        }

        public void CompletedUserID(string st)
        {
            System.Diagnostics.Trace.WriteLine($"{st}が入力されました");
        }

        public void CompletedPassWord(string st)
        {
            System.Diagnostics.Trace.WriteLine($"{st}が入力されました");
        }

        public ICommand OpenWebCommand { get; }
    }
}