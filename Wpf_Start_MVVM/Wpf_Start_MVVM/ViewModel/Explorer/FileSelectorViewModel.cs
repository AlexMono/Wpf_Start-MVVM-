using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wpf_Start_MVVM.Traductions;
using Wpf_Start_MVVM.ViewModel.Base;

namespace Wpf_Start_MVVM.ViewModel.Explorer
{
    public enum FileType
    {
        All,
        Csv,
        Jpg,
        Txt,
        Pdf,
        Xls,
        AllImage,
    }

    public enum FileSelectionType
    {
        Selection,
        Creation
    }

    public class FileSelectorViewModel: BaseViewModel
    {
        private const ulong Tera = (ulong)1024 * (ulong)1024 * (ulong)1024 * (ulong)1024;
        private const ulong Giga = 1024 * 1024 * 1024;
        private const ulong Mega = 1024 * 1024;
        private const ulong Kilo = 1024;

        private List<string> _rootFolders;
        private string _filter;
        private string _endWith;
        private string libelle;
        private string selectedText;
        private string filter;
        private FileSelectionType fileSelectionType;
        private ObservableCollection<DirectoryItemViewModel> directories;
        private DirectoryItemViewModel selectedDirectory;
        private ObservableCollection<DirectoryItemViewModel> breabCrumb;
        private FileItemViewModel selectedFile;
        private string fullPath;
        private bool allowEditFileName;

        public string Libelle { get { return libelle; } set { libelle = value; OnPropertyChanged(nameof(Libelle)); } }

        public string SelectedText { get { return selectedText; } set { selectedText = value; OnPropertyChanged(nameof(SelectedText)); } }

        public string Filter { get { return filter; } set { filter = value; OnPropertyChanged(nameof(Filter)); } }

        public FileSelectionType FileSelectionType { get { return fileSelectionType; } set { fileSelectionType = value; OnPropertyChanged(nameof(FileSelectionType)); } }

        public ObservableCollection<DirectoryItemViewModel> Directories { get { return directories; } set { directories = value; OnPropertyChanged(nameof(Directories)); } }

        public DirectoryItemViewModel SelectedDirectory { get { return selectedDirectory; } set { selectedDirectory = value; OnPropertyChanged(nameof(SelectedDirectory)); } }

        public ObservableCollection<DirectoryItemViewModel> Breadcrumb { get { return breabCrumb; } set { breabCrumb = value; OnPropertyChanged(nameof(Breadcrumb)); } }

        public FileItemViewModel SelectedFile { get { return selectedFile; } set { selectedFile = value; OnPropertyChanged(nameof(SelectedFile)); } }

        public string FullPath { get { return fullPath; } set { fullPath = value; OnPropertyChanged(nameof(FullPath)); } }

        public bool AllowEditFileName { get { return allowEditFileName; } set { allowEditFileName = value; OnPropertyChanged(nameof(AllowEditFileName)); } }        

        public ICommand BreadcrumbCommand { get; set; }

        public FileSelectorViewModel(FileSelectionType fileSelectionType, FileType fileType = FileType.All, string title = null, params string[] rootFolders)
        {
            _endWith = null;
            switch (fileSelectionType)
            {
                case FileSelectionType.Selection:                    
                    AllowEditFileName = false;
                    break;
                case FileSelectionType.Creation:                    
                    AllowEditFileName = true;
                    break;
                default:
                    break;
            }

            _rootFolders = new List<string>();

            if (rootFolders != null)
                foreach (var rootFolder in rootFolders)
                    _rootFolders.Add(rootFolder);
            switch (fileType)
            {
                case FileType.All:
                    _filter = null;
                    Filter = "*.";
                    break;
                case FileType.Csv:
                    _filter = "*.csv";
                    //Filter = ResApplication.FichiersCSV;
                    _endWith = ".csv";
                    break;
                case FileType.Jpg:
                    _filter = "*.jpg|*.jpeg";
                    //Filter = ResControls.FichiersJPG;
                    break;
                case FileType.Txt:
                    _filter = "*.txt";
                    _endWith = ".txt";
                    break;
                case FileType.Pdf:
                    _filter = "*.pdf";
                    Filter = "Fichiers .pdf";
                    _endWith = ".pdf";
                    break;
                case FileType.Xls:
                    _filter = "*.xls|*.xlsx";
                    //Filter = ResControls.FichiersXLS;
                    break;
                case FileType.AllImage:
                    _filter = "*.jpg|*.png|*.jpeg|*.gif|*.bmp";
                    Filter = "Fichiers images";
                    break;
                default:
                    break;
            }

            FileSelectionType = fileSelectionType;

            Initialize();
        }

        public void Initialize()
        {
            DirectoryInfo di;
            var directories = new ObservableCollection<DirectoryItemViewModel>();
            foreach (var d in _rootFolders)
            {
                di = new DirectoryInfo(d);
                if (di.Exists)
                {
                    var vm = new DirectoryItemViewModel(di.Name, d);
                    vm.Directories = GetDirectories(d, di.Name);
                    vm.Files = GetFiles(d);
                    vm.Breadcrumb = di.Name;
                    vm.IsHeader = true;
                    directories.Add(vm);
                }
            }
            Directories = directories;

            SelectedDirectory = Directories.FirstOrDefault();
            SelectedFile = SelectedDirectory.Files.FirstOrDefault();
            
            BreadcrumbCommand = new RelayCommand(OnBreadcrumbSelect);
        }

        public FileSelectorViewModel()
        {

        }

        public static ObservableCollection<DirectoryItemViewModel> GetDirectories(string fullPath, string breadcrumb)
        {
            var directories = new ObservableCollection<DirectoryItemViewModel>();
            DirectoryInfo di;
            try
            {
                foreach (var dir in Directory.GetDirectories(fullPath))
                {
                    di = new DirectoryInfo(dir);
                    var vm = new DirectoryItemViewModel(di.Name, dir);
                    vm.Breadcrumb = string.Format("{0}|{1}", breadcrumb, di.Name);
                    directories.Add(vm);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Accès non autorisé à ce dossier				
            }


            return directories;
        }

        public ObservableCollection<FileItemViewModel> GetFiles(string fullPath, bool stop = false)
        {
            var files = new ObservableCollection<FileItemViewModel>();
            FileInfo fi;
            try
            {
                string[] all;

                if (_filter == null)
                    all = Directory.GetFiles(fullPath);
                else
                    all = GetFilesWithFilter(fullPath, _filter).ToArray();

                foreach (var file in all)
                {
                    fi = new FileInfo(file);
                    var vm = new FileItemViewModel(fi.Name, file, GetWeightLibelle((ulong)fi.Length), fi.LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss"));
                    files.Add(vm);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Accès non autorisé à ce dossier
            }

            return files;
        }

        private static string GetWeightLibelle(ulong weight)
        {
            if (weight > Tera)
                return string.Format("{0:# ##0.###} To", weight / Tera);
            if (weight > Giga)
                return string.Format("{0:# ##0.###} Go", weight / Giga);
            if (weight > Mega)
                return string.Format("{0:# ##0.###} Mo", weight / Mega);
            if (weight > Kilo)
                return string.Format("{0:# ##0.###} Ko", weight / Kilo);
            return string.Format("{0:### ##0.###} octets", weight);
        }

        private List<string> GetFilesWithFilter(string fullPath, string _filter)
        {
            List<string> files = new List<string>();

            foreach (var filter in _filter.Split('|'))
                files.AddRange(Directory.GetFiles(fullPath, filter));

            return files;
        }

        private ObservableCollection<DirectoryItemViewModel> ResolveBreadcrumb(DirectoryItemViewModel directory)
        {
            ObservableCollection<DirectoryItemViewModel> list = new ObservableCollection<DirectoryItemViewModel>();

            var dirs = Directories;

            foreach (var dir in directory.Breadcrumb.Split('|'))
            {
                var d = dirs.FirstOrDefault(elt => elt.Libelle == dir);
                if (d != null)
                {
                    list.Add(d);
                    dirs = d.Directories;
                }
                else
                    break;
            }

            return list;
        }

        private void OnBreadcrumbSelect(object parameter)
        {
            var dir = parameter as DirectoryItemViewModel;

            if (dir != null)
            {
                SelectedDirectory = dir;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(SelectedDirectory))
            {
                var selectedDirectory = SelectedDirectory;
                if (selectedDirectory != null)
                {
                    selectedDirectory.IsSelected = true;
                    if (selectedDirectory.Files == null)
                        selectedDirectory.Files = GetFiles(selectedDirectory.FullPath);
                    Breadcrumb = ResolveBreadcrumb(selectedDirectory);
                }
            }

            if (propertyName == nameof(SelectedFile))
            {
                if (SelectedFile != null)
                    SelectedText = SelectedFile.Libelle;
            }

            if (propertyName == nameof(SelectedText) && SelectedText != null)
            {
                // FCH 20170210 - Le renommage n'est pas nécessaire sur une sélection de fichier
                // car ensuite pose prb sur la récupération du fichier puisqu'il a été renommé
                // on réalise ce traitement seulement si ce n'est pas une sélection
                var r = new Regex("[^ a-zA-Z0-9-.]");
                var selectedText = r.Replace(SelectedText, "_");

                if (SelectedDirectory != null)
                    FullPath = Path.Combine(SelectedDirectory.FullPath, FileSelectionType != FileSelectionType.Selection ? selectedText : SelectedText);
                else
                    FullPath = string.Empty;

                // Nom modifié seulement si pas une selection sinon il a été affecté avant.
                if (FileSelectionType != FileSelectionType.Selection)
                    SelectedText = selectedText;
            }
        }
    }
}
