using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Start_MVVM.ViewModel.Base;

namespace Wpf_Start_MVVM.ViewModel.PageExample
{
    public class Page1ViewModel : BaseViewModel
    {
        private string nameButton;

        /// <summary>
        /// Property example
        /// </summary>
        public string NameButtonExample
        {
            get
            {
                return this.nameButton;
            }
            set
            {
                this.nameButton = value;
                OnPropertyChanged(nameof(NameButtonExample));
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
            //Load your list/objects here...
            NameButtonExample = "Example Button";
        }
    }
}
