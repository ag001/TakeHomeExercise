using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TakeHomeExercise.ValueConverters
{
   public class PhotoTitleConverter : IValueConverter
   {
      private const string c_noTitle = "no title";

      public object Convert( object value, Type targetType, object parameter, string culture )
      {
         if( value is EBay.PhotoSDK.Model.Photo )
         {
            EBay.PhotoSDK.Model.Photo photo = ( EBay.PhotoSDK.Model.Photo ) value;

            if( photo.Data is FlickrNet.Photo )
            {
               FlickrNet.Photo p = ( FlickrNet.Photo ) photo.Data;

               if( string.IsNullOrEmpty( p.Title ) )
                  return c_noTitle;

               return p.Title;
            }
            else if( photo.Data is Slyno.Providers._500px.Photo )
            {
               Slyno.Providers._500px.Photo p = ( Slyno.Providers._500px.Photo ) photo.Data;

               if( string.IsNullOrEmpty( p.name ) )
                  return c_noTitle;

               return p.name;
            }
            else if( photo.Data is StorageFile )
            {
               StorageFile file = ( StorageFile ) photo.Data;

               return file.Name;
            }
         }

         return DependencyProperty.UnsetValue;
      }

      public object ConvertBack( object value, Type targetType, object parameter, string culture )
      {
         throw new NotImplementedException();
      }
   }
}
