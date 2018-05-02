using System;
using Prism.Mvvm;

namespace PrismIntro.Models
{
    public class User : BindableBase
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
    }
}
