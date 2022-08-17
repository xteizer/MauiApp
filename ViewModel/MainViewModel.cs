
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ToDoApp.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IConnectivity Connectivity;
        public MainViewModel(IConnectivity connectivity)
        {
            taskItems = new();
            Connectivity = connectivity;
        }

        [ObservableProperty]
        ObservableCollection<string> taskItems;

        [ObservableProperty]
        string taskText;

        [RelayCommand]
        async Task Add()
        {
            if (string.IsNullOrWhiteSpace(TaskText))
                return;

            if (Connectivity?.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet Issue","No internet connection!","OK");
                return;
            }
            
            TaskItems.Add(TaskText);
            TaskText = string.Empty;
        }

        [RelayCommand]
        void Delete(string s)
        {
            if (TaskItems.Contains(s))
            {
                TaskItems.Remove(s);
            }
        }

        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?TaskName={s}");
        }
    }
}
