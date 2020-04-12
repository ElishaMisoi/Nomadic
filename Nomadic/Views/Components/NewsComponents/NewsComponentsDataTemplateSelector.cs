using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Nomadic.Views.Components.NewsComponents
{
    public class NewsComponentsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NewsView { get; set; }
        public DataTemplate WideNewsView { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (((Models.Article)item).IsWideView)
                return WideNewsView;
            return NewsView;
        }
    }
}
