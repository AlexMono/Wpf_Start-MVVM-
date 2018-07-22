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

namespace Wpf_Start_MVVM.ViewModel.InputUrl
{
    /// <summary>
    /// Logique d'interaction pour InputUrlView.xaml
    /// </summary>
    public partial class InputUrlView : UserControl
    {
        public InputUrlView()
        {
            InitializeComponent();

            this.DataContext = new InputUrlViewModel();
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
