using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nomadic.Views.Components.PopupComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedArticlesSavePopup
    {
        public SavedArticlesSavePopup()
        {
            InitializeComponent();
            BindingContext = ViewModels.SavedArticlesViewModels.Instance;
        }
    }
}