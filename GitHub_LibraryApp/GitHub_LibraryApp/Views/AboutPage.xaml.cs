using GitHub_LibraryApp.ViewModels;
using PCLStorage;
using System;
using System.Threading.Tasks;
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

        private string id;
        private string pass;

        private void OnSaveCommand(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine($"aaa");
            Task.Run(async () =>
            {
                await SaveTextAsync($"{id},{pass}");
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

        /// <summary>
        /// UserIDの入力が完了したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_Completed_UserID(object sender, EventArgs e)
        {
            id = this.user_entry.Text;
            //Enterを押すと表示
            //DisplayAlert("", this.user_entry.Text, "OK");
        }

        /// <summary>
        /// PassWordの入力が完了したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_Completed_PassWord(object sender, EventArgs e)
        {
            pass = this.pass_entry.Text;

            //Enterを押すと表示
            //DisplayAlert("", this.pass_entry.Text, "OK");
        }
        
    }
}