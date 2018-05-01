using System;
using Prism.Mvvm;

namespace PrismIntro.Models
{
    /// <summary>
    /// Simple model class for a Person. This is the data type our ListView contains.
    /// </summary>
    public class Category : BindableBase
    {
        private string _categoryname;
        public string CategoryName
        {
            get { return _categoryname; }
            set { SetProperty(ref _categoryname, value); }
        }

        public override string ToString()
        {
            return CategoryName;
        }
        
    
    }
}