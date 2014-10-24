﻿using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.UI.Xaml.Media.Imaging;

namespace TakeHomeExercise.DataProviders
{
   public class PhotoLibraryProvider : EBay.PhotoSDK.IDataProvider
   {
      public PhotoLibraryProvider()
      {
      }

      public void InitAsync( EBay.PhotoSDK.InitCompleted initCompleted )
      {
         initCompleted();
      }

      public bool FRequiresAuthentication()
      {
         // we dont need authentication
         return false;
      }

      void EBay.PhotoSDK.IDataProvider.DoAuthenticationAsync( EBay.PhotoSDK.AuthenticationCompleted fAuthenticated )
      {
         throw new NotImplementedException();
      }

      public async void LoadDataAsync( EBay.PhotoSDK.Model.PhotoSearchParams searchParams, int pageId, int perPage, EBay.PhotoSDK.LoadCompleted result )
      {
         int startId = ( pageId - 1 ) * perPage;
         var list = await KnownFolders.PicturesLibrary.GetFilesAsync( CommonFileQuery.OrderByDate, ( uint ) startId, ( uint ) perPage );

         var count = ( await KnownFolders.PicturesLibrary.GetFilesAsync( CommonFileQuery.OrderByDate ) ).Count;
         if( list == null )
         {
            result( false, null, 0 );
            return;
         }

         List<EBay.PhotoSDK.Model.Photo> newList = new List<EBay.PhotoSDK.Model.Photo>();
         foreach( var item in list )
         {
            EBay.PhotoSDK.Model.Photo photo = new EBay.PhotoSDK.Model.Photo( new StorageFileWrapper( item ) );

            newList.Add( photo );
         }

         result( true, newList, count );
      }

      public class StorageFileWrapper
      {
         private StorageFile m_file;

         private BitmapImage bmp;

         public BitmapImage PhotoSource
         {
            get
            {
               if( bmp == null )
               {
                  bmp = new BitmapImage();

                  LoadThumbnail();
               }

               return bmp;
            }
         }

         public StorageFileWrapper( StorageFile file )
         {
            m_file = file;
         }

         public async void LoadThumbnail()
         {
            bmp.SetSource( await m_file.OpenAsync( FileAccessMode.Read ) );
         }
      }
   }
}

