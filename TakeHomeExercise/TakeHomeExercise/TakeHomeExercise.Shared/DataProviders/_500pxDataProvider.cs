﻿using System;
using System.Collections.Generic;
using System.Text;
using Slyno.Providers._500px;
using System.Threading.Tasks;
using EBay.PhotoSDK.Model;

namespace TakeHomeExercise.DataProviders
{
   internal class _500pxDataProvider : EBay.PhotoSDK.IDataProvider
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

      public void DoAuthenticationAsync( Action<bool> fAuthenticated )
      {
         throw new NotImplementedException();
      }

      public void InitAsync( Action initCompleted )
      {
         initCompleted();
      }

      public async void LoadDataAsync( PhotoSearchParams searchParams, int pageId, int perPage, Action<bool, IReadOnlyList<object>, int> result )
      {
         Query query = new Query();
         query.Tags.Add( searchParams.SearchText );
         query.ResultSize = perPage;
         SearchResult searchResult = await m_service.Search( query );
         if( searchResult == null )
         {
            result( false, null, 0 );
            return;
         }

         result( true, searchResult.photos, searchResult.total_items );
      }

   }
}
