using EBay.PhotoSDK.Model;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace EBay.PhotoSDK.ViewModel
{
   public class PhotoStoreViewModel
   {
      public PagedCollection Photos { get; private set; }

      public PhotoStoreViewModel( IDataProvider provider )
      {
         Photos = new PagedCollection();

         if( provider == null )
            throw new ArgumentNullException();

         m_provider = provider;
      }

      //public void SetProvider( IDataProvider provider, CoreDispatcher dispatcher )
      //{
      //   if( provider == null || dispatcher == null )
      //      throw new ArgumentNullException();

      //   SetDispatcher( dispatcher );

      //   m_provider = provider;
      //}

      public void SetDispatcher( CoreDispatcher dispatcher )
      {
         this.Dispatcher = new WeakReference( dispatcher );
      }

      public async Task InitAsync()
      {
         if( m_initDone == false )
         {
            await m_provider.InitAsync();

            m_initDone = true;

            if( m_provider.FRequiresAuthentication() )
            {
               await m_provider.DoAuthenticationAsync( result =>
               {
                  if( result != true )
                  {
                     throw new UnauthorizedAccessException();
                  }
               } );
            }
         }
      }

      public void Load()
      {
         m_provider.LoadDataAsync( 1, 20, ( fSuccess, result ) =>
            {
               CoreDispatcher dispatcher = Dispatcher.Target as CoreDispatcher;
               if( dispatcher != null )
               {
                  var ignore = dispatcher.RunAsync( CoreDispatcherPriority.Normal, () =>
                     {
                        Photos.Append( fSuccess, result );
                     } );
               }
            } );
      }

      private IDataProvider m_provider;
      private bool m_initDone = false;
      private WeakReference Dispatcher;
   }
}
