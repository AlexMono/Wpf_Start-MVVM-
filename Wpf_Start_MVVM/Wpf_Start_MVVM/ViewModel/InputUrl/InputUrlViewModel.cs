using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Start_MVVM.ViewModel.Base;

namespace Wpf_Start_MVVM.ViewModel.InputUrl
{
    public class InputUrlViewModel: BaseViewModel
    {
        #region Members

        private string title;
        private string url;        

        #endregion

        #region Properties

        public string Title { get { return title; } set { title = value; OnPropertyChanged(nameof(Title)); } }
        public string Url { get { return url; } set { url = value; OnPropertyChanged(nameof(Url)); } }        

        #endregion

        #region Constructor

        public InputUrlViewModel()
        {
            Initalize();
        }

        #endregion

        #region Private Methods        

        #endregion

        #region Public Methods

        public void Initalize()
        {

        }

        #endregion
    }
}
