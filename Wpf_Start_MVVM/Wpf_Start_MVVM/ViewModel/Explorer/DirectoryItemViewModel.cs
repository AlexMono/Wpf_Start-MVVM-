using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wpf_Start_MVVM.ViewModel.Base;

namespace Wpf_Start_MVVM.ViewModel.Explorer
{
    public class DirectoryItemViewModel: BaseViewModel
    {
        private object _lock = new object();
        private string libelle;
        private string fullPath;
        private string breadcrumb;
        private bool isExpanded;
        private bool isSelected;
        private bool isHeader;
        private ObservableCollection<DirectoryItemViewModel> directories;
        private ObservableCollection<FileItemViewModel> files;

        public string Libelle { get { return libelle; } set { libelle = value; OnPropertyChanged(nameof(Libelle)); } }

        public string FullPath { get { return fullPath; } set { fullPath = value; OnPropertyChanged(nameof(FullPath)); } }

        public string Breadcrumb { get { return breadcrumb; } set { breadcrumb = value; OnPropertyChanged(nameof(Breadcrumb)); } }

        public bool IsExpanded { get { return isExpanded; } set { isExpanded = value; OnPropertyChanged(nameof(IsExpanded)); } }

        public bool IsSelected { get { return isSelected; } set { isSelected = value; OnPropertyChanged(nameof(IsSelected)); } }

        public bool IsHeader { get { return isHeader; } set { isHeader = value; OnPropertyChanged(nameof(IsHeader)); } }

        public ObservableCollection<DirectoryItemViewModel> Directories { get { return directories; } set { directories = value; OnPropertyChanged(nameof(Directories)); } }

        public ObservableCollection<FileItemViewModel> Files { get { return files; } set { files = value; OnPropertyChanged(nameof(Files)); } }

        public DirectoryItemViewModel()
        {

        }

        public DirectoryItemViewModel(string libelle, string fullPath)
        {
            Libelle = libelle;
            FullPath = fullPath;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "IsExpanded" && IsExpanded && Directories != null)
            {
                Task.Factory.StartNew(() =>
                {
                    lock (_lock)
                    {
                        foreach (var d in Directories)
                        {
                            if (d.Directories == null)
                            {
                                d.Directories = FileSelectorViewModel.GetDirectories(d.FullPath, d.Breadcrumb);
                            }
                        }
                    }
                });
            }
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}][{2}]", Libelle, Directories != null ? Directories.Count() : 0, Files != null ? Files.Count() : 0);
        }
    }
}
