using System;
using System.Collections.Generic;
using System.Text;
using Slyno.Providers._500px;
using System.Threading.Tasks;

namespace TakeHomeExercise.DataProviders
{
    internal class _500pxDataProvider: EBay.PhotoSDK.IDataProvider
   {
      ApiService m_service;

      public _500pxDataProvider()
      {
         m_service = new ApiService( "FFkYQtLZKc5Px3u80GpPNbupuOqWBT0Zd3YTyCSt" );
      }

      public bool FRequiresAuthentication()
      {
         return false;
      }

      public Task DoAuthenticationAsync( Action<bool> fAuthenticated )
      {
         throw new NotImplementedException();
      }

      public System.Threading.Tasks.Task InitAsync()
      {
         return Task.Factory.StartNew( () => { } );
      }

      public async System.Threading.Tasks.Task LoadDataAsync( int pageId, int perPage, Action<bool, IReadOnlyList<EBay.PhotoSDK.Model.Photo>> result )
      {
         Query query = new Query();
         query.Tags.Add( "Flowers" );
         query.ResultSize = perPage;
         SearchResult searchResult = await m_service.Search( query );
         if( searchResult == null )
         {
            result( false, null );
            return;
         }

         List<EBay.PhotoSDK.Model.Photo> list = new List<EBay.PhotoSDK.Model.Photo>();
         foreach( var item in searchResult.photos )
         {
            EBay.PhotoSDK.Model.Photo photo = new EBay.PhotoSDK.Model.Photo( item.id.ToString(), item );
            list.Add( photo );
         }

         result( true, list );
      }

   }
}
