using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TakeHomeExercise.TemplateSelectors
{
   public class PhotoItemTemplateSelector : DataTemplateSelector
   {
      public DataTemplate LoadingItemTemplate { get; set; }
      public DataTemplate MoreItemTemplate { get; set; }
      public DataTemplate PhotoItemTemplate { get; set; }

      protected override DataTemplate SelectTemplateCore( object item, DependencyObject container )
      {
         if( item != null )
         {
            if( item is EBay.PhotoSDK.Model.Photo )
            {
               return PhotoItemTemplate;
            }
         }

         return base.SelectTemplateCore( item, container );
      }
   }
}
