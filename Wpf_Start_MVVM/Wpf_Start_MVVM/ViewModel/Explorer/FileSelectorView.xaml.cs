using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Start_MVVM.ViewModel.Explorer
{
    /// <summary>
    /// Logique d'interaction pour FileSelectorView.xaml
    /// </summary>
    public partial class FileSelectorView : UserControl
    {
        public FileSelectorView()
        {
            InitializeComponent();

            this.DataContext = new FileSelectorViewModel(FileSelectionType.Selection, FileType.All, "FileSelector", string.Format("C:\\Users\\{0}", Environment.UserName));            
        }

        private void TreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = TreeExplorer.SelectedItem as DirectoryItemViewModel;
            var vm = DataContext as FileSelectorViewModel;

            if (selectedItem != null)
                vm.SelectedDirectory = selectedItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            if (parentWindow != null)
            {
                parentWindow.DialogResult = true;

                parentWindow.Close();
            }
        }
    }
}
