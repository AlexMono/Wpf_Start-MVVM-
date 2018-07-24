using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wpf_Start_MVVM.ObjectValue;
using Wpf_Start_MVVM.Traductions;
using Wpf_Start_MVVM.ViewModel.Base;
using Wpf_Start_MVVM.ViewModel.Explorer;
using Wpf_Start_MVVM.ViewModel.InputUrl;
using Wpf_Start_MVVM.ViewModel.Popup;

namespace Wpf_Start_MVVM.ViewModel.PageExample
{
    public class Page1ViewModel : BaseViewModel
    {
        private ObservableCollection<OVDownload> listDownload;
        private OVDownload selectedDownload;
        private string saveFilePath;

        public ICommand SetSavePathCommand { get; set; }
        public ICommand AddDownloadCommand { get; set; }

        public string SaveFilePath
        {
            get
            {
                return saveFilePath;
            }
            set
            {
                saveFilePath = value;
                OnPropertyChanged(nameof(SaveFilePath));
            }
        }

        public ObservableCollection<OVDownload> ListDownload
        {
            get
            {
                return listDownload;
            }
            set
            {
                listDownload = value;
                OnPropertyChanged(nameof(ListDownload));
            }
        }

        public OVDownload SelectedDownload
        {
            get
            {
                return selectedDownload;
            }
            set
            {
                selectedDownload = value;
                OnPropertyChanged(nameof(SelectedDownload));
            }
        }

        /// <summary>
        /// The constructor
        /// </summary>
        public Page1ViewModel()
        {
            Initialize();
        }


        /// <summary>
        /// Initialize the view model
        /// </summary>
        private void Initialize()
        {
            ListDownload = new ObservableCollection<OVDownload>();  

            if(ListDownload != null && ListDownload.Count() > 0)
                SelectedDownload = ListDownload.FirstOrDefault();

            SetSavePathCommand = new RelayCommand(SetSavePathExecute);
            AddDownloadCommand = new RelayCommand(AddDownloadExecute);
        }

        private void SetSavePathExecute()
        {       
            FileSelectorView fileSelectorView = new FileSelectorView();
            Window popup = new DialogWindow(fileSelectorView, ResApplication.Explorer);            
            popup.ShowDialog();

            if (popup.DialogResult.Value == true)
            {
                var dialogWindowViewModel = popup.DataContext as DialogWindowViewModel;
                var currentPage = dialogWindowViewModel.CurrentPage as FileSelectorView;
                var vm = currentPage.DataContext as FileSelectorViewModel;

                var path = vm.SelectedDirectory.FullPath + "\\";

                SaveFilePath = path;
            }
        }

        private void AddDownloadExecute()
        {

            if (!string.IsNullOrEmpty(SaveFilePath))
            {
                InputUrlView inputUrlView = new InputUrlView();
                Window popup = new DialogWindow(inputUrlView, ResApplication.AddUrl);
                popup.ShowDialog();

                if (popup.DialogResult.Value == true)
                {
                    var dialogWindowViewModel = popup.DataContext as DialogWindowViewModel;
                    var currentPage = dialogWindowViewModel.CurrentPage as InputUrlView;
                    var vm = currentPage.DataContext as InputUrlViewModel;

                    OVDownload download = new OVDownload
                    {
                        Url = vm.Url,
                        Title = vm.Title,
                        SavePath = SaveFilePath
                    };

                    ListDownload.Add(download);

                    Task.Run(() =>
                    {
                        download.Start(vm.IsMp3);
                    });                    
                }
            }
            else
            {
                DialogMessageView dialogMessageView = new DialogMessageView(ResApplication.NeedSaveRepository);
                Window popup = new DialogWindow(dialogMessageView, ResApplication.Error);
                popup.ShowDialog();
            }
        }


    }
}
