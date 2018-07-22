using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Start_MVVM.ViewModel.Base;

namespace Wpf_Start_MVVM.ViewModel.Popup
{
    public class DialogMessageViewModel: BaseViewModel
    {
        #region Members

        #endregion

        #region Properties

        public string Message { get; set; }

        #endregion

        #region Constructor

        public DialogMessageViewModel(string message)
        {
            Message = message;
        }

        #endregion
    }
}
