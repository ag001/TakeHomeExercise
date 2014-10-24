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
      public DataTemplate NoneItemTemplate { get; set; }
      public DataTemplate StorageItemTemplate { get; set; }

      protected override DataTemplate SelectTemplateCore( object item, DependencyObject container )
      {
         if( item != null )
         {
            if( item is DataProviders.PhotoLibraryProvider.StorageFileWrapper )
            {
               return StorageItemTemplate;
            }
            else if( item is EBay.PhotoSDK.Model.Photo )
            {
               return PhotoItemTemplate;
            }
            else if( item is EBay.PhotoSDK.Model.LoadingButton )
            {
               return LoadingItemTemplate;
            }
            else if( item is EBay.PhotoSDK.Model.MoreButton )
            {
               return MoreItemTemplate;
            }
            else if( item is EBay.PhotoSDK.Model.NoneButton )
            {
               return NoneItemTemplate;
            }
         }

         return base.SelectTemplateCore( item, container );
      }
   }
}
