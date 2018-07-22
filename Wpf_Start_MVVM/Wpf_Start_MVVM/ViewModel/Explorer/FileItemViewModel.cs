using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Start_MVVM.ViewModel.Base;

namespace Wpf_Start_MVVM.ViewModel.Explorer
{
    public class FileItemViewModel: BaseViewModel
    {
        private bool selected;
        private string libelle;
        private string fullPath;
        private string size;
        private string date;

        public bool Selected { get { return selected; } set { selected = value; OnPropertyChanged(nameof(Selected)); } }
        public string Libelle { get { return libelle; } set { libelle = value; OnPropertyChanged(nameof(Libelle)); } }
        public string FullPath { get { return fullPath; } set { fullPath = value; OnPropertyChanged(nameof(FullPath)); } }
        public string Size { get { return size; } set { size = value; OnPropertyChanged(nameof(Size)); } }
        public string Date { get { return date; } set { date = value; OnPropertyChanged(nameof(Date)); } }

        public FileItemViewModel(string libelle, string fullPath, string size, string date)
        {
            Selected = false;
            Libelle = libelle;
            FullPath = fullPath;
            Size = size;
            Date = date;
        }
    }
}
