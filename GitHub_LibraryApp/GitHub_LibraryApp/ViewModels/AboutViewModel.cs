using GitHub_LibraryApp.Models;
using Octokit;
using PCLStorage;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace GitHub_LibraryApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ICommand GitLoadCommand { get; }
        public ICommand SaveCommand { get; }

        public string UserID { get; set; }
        public string PassWord { get; set; }

        public AboutViewModel()
        {
            Title = "GitHubデータ取得";
            
            GitLoadCommand = new Command(async () =>
            {
                var LoadAcountData = await LoadTextAsync();

                var spritData = LoadAcountData.Split(',');

                UserID = spritData[0];
                PassWord = spritData[1];

                var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));

                var basicAuth = new Credentials(UserID, PassWord);
                client.Credentials = basicAuth;

                var dirInfo = await client.Repository.Content.GetAllContents(UserID, "TIL");

                foreach (RepositoryContent data in dirInfo)
                {
                    System.Diagnostics.Trace.WriteLine($"{data.Path}");

                    var dirInfo2 = await client.Repository.Content.GetAllContents(UserID, "TIL", data.Path);

                    var item = 
                        new Item {
                            Id = Guid.NewGuid().ToString(),
                            Text = data.Path,
                            Description = $"説明\n{Guid.NewGuid().ToString()}"
                        };
                    
                    MessagingCenter.Send(this, "AddItem", item);

                    foreach (RepositoryContent data2 in dirInfo2)
                    {
                        System.Diagnostics.Trace.WriteLine($"{data2.Path}");

                        var item2 = new Item
                        {
                            Id = Guid.NewGuid().ToString(),
                            Text = data2.Path,
                            Description = $"説明\n{Guid.NewGuid().ToString()}"
                        };

                        MessagingCenter.Send(this, "AddItem", item2);
                    }
                }
            });


            
            SaveCommand = new Command(async () =>
            {
                await SaveTextAsync($"{UserID},{PassWord}");
            });
        }

        async Task<string> SaveTextAsync(string text)
        {
            // フォルダ名、ファイル名を作成
            var SubFolderName = "GitUserData";
            var TextFileName = "gitUser.txt";

            // ユーザーデータ保存フォルダー
            PCLStorage.IFolder localFolder = PCLStorage.FileSystem.Current.LocalStorage;

            // サブフォルダーを作成、または、取得する
            PCLStorage.IFolder subFolder
               = await localFolder.CreateFolderAsync(SubFolderName,
                                        PCLStorage.CreationCollisionOption.OpenIfExists);

            // ファイルを作成、または、取得する
            PCLStorage.IFile file
                = await subFolder.CreateFileAsync(TextFileName,
                                  PCLStorage.CreationCollisionOption.ReplaceExisting);

            // テキストをファイルに書き込む
            // ※冒頭に「using PCLStorage;」が必要
            await file.WriteAllTextAsync(text);

            return file.Path;
        }

        async Task<string> LoadTextAsync()
        {
            // フォルダ名、ファイル名を作成
            var SubFolderName = "GitUserData";
            var TextFileName = "gitUser.txt";

            // ユーザーデータ保存フォルダー
            PCLStorage.IFolder localFolder = PCLStorage.FileSystem.Current.LocalStorage;

            // サブフォルダーを作成、または、取得する
            PCLStorage.IFolder subFolder
              = await localFolder.CreateFolderAsync(SubFolderName,
                                    PCLStorage.CreationCollisionOption.OpenIfExists);

            // ファイルを取得する
            PCLStorage.IFile file = await subFolder.GetFileAsync(TextFileName);

            // テキストファイルを読み込む
            // ※ファイル冒頭に「using PCLStorage;」が必要
            return await file.ReadAllTextAsync();
        } 
    }
}