﻿using GitHub_LibraryApp.Models;
using GitHub_LibraryApp.Services;
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
                var LoadAcountData = await TextController.LoadTextAsync();

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
                await TextController.SaveTextAsync($"{UserID},{PassWord}");
            });
        }

        
    }
}