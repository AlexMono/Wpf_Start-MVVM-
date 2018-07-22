using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf_Start_MVVM.ViewModel.Base;
using Wpf_Start_MVVM.ViewModel.Explorer;
using Wpf_Start_MVVM.ViewModel.InputUrl;
using Wpf_Start_MVVM.ViewModel.PageExample;

namespace Wpf_Start_MVVM.ViewModel
{
    class DialogWindowViewModel: WindowViewModel
    {
        #region Public Properties

        /// <summary>
        /// The title of this dialog window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The content of the window
        /// </summary>
        public Control Content { get; set; }

        public UserControl CurrentPage { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="window"></param>
        public DialogWindowViewModel(Window window, UserControl currentPage, string title): base(window)
        {
            CurrentPage = currentPage;
            Title = title;
        }

        #endregion
    }
}
