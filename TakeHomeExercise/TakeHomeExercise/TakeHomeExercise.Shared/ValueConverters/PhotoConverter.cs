using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace TakeHomeExercise.ValueConverters
{
   public class PhotoConverter : IValueConverter
   {
      public object Convert( object value, Type targetType, object parameter, string culture )
      {
         if( value is EBay.PhotoSDK.Model.Photo )
         {
            EBay.PhotoSDK.Model.Photo photo = ( EBay.PhotoSDK.Model.Photo ) value;

            if( photo.Data is FlickrNet.Photo )
            {
               FlickrNet.Photo p = ( FlickrNet.Photo ) photo.Data;

               if( parameter == null )
                  return p.ThumbnailUrl;
               else
                  return p.LargeUrl;
            }
            else if( photo.Data is Slyno.Providers._500px.Photo )
            {
               Slyno.Providers._500px.Photo p = ( Slyno.Providers._500px.Photo ) photo.Data;
               return p.image_url;
            }
            else if( photo.Data is StorageFile )
            {
               StorageFile file = ( StorageFile ) photo.Data;

               var asyncTaskResult = file.GetThumbnailAsync( Windows.Storage.FileProperties.ThumbnailMode.PicturesView, 100 );
               asyncTaskResult.AsTask().Wait();
               StorageItemThumbnail thumbnail = asyncTaskResult.GetResults();

               BitmapImage image = new BitmapImage();
               image.SetSource( thumbnail );
               return image;
            }
            else if( photo.Data is DataProviders.PhotoLibraryProvider.StorageFileWrapper )
            {
               DataProviders.PhotoLibraryProvider.StorageFileWrapper p = ( DataProviders.PhotoLibraryProvider.StorageFileWrapper ) photo.Data;
               if( p != null )
               {
                  if( parameter == null )
                  {
                     return p.PhotoSource;
                  }
                  else
                  {
                     return p.PhotoSourceDetailed;
                  }
               }
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
