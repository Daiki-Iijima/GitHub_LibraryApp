using System;
using System.Threading.Tasks;
using PCLStorage;

namespace GitHub_LibraryApp.Services
{
    public static class TextController
    {

        public static async Task<string> SaveTextAsync(string text)
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

        public static async Task<string> LoadTextAsync()
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
