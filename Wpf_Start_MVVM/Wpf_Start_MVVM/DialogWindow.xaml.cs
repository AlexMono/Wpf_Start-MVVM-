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
using Wpf_Start_MVVM.ViewModel;

namespace Wpf_Start_MVVM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow(UserControl currentPage, string title)
        {
            InitializeComponent();

            this.DataContext = new DialogWindowViewModel(this, currentPage, title);
        }
    }
}
