using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TakeHomeExercise.ValueConverters
{
   public class PhotoDateConverter : IValueConverter
   {
      private const string c_formatString = "date uploaded : {0}";

      public object Convert( object value, Type targetType, object parameter, string culture )
      {
         if( value is EBay.PhotoSDK.Model.Photo )
         {
            EBay.PhotoSDK.Model.Photo photo = ( EBay.PhotoSDK.Model.Photo ) value;

            if( photo.Data is FlickrNet.Photo )
            {
               FlickrNet.Photo p = ( FlickrNet.Photo ) photo.Data;

               return string.Format( c_formatString, p.DateUploaded );
            }
            else if( photo.Data is Slyno.Providers._500px.Photo )
            {
               Slyno.Providers._500px.Photo p = ( Slyno.Providers._500px.Photo ) photo.Data;

               return string.Format( c_formatString, p.created_at );
            }
            else if( photo.Data is StorageFile )
            {
               StorageFile file = ( StorageFile ) photo.Data;

               return string.Format( c_formatString, file.DateCreated );
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
