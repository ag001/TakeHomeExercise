using EBay.PhotoSDK.Model;
using System;
using Windows.UI.Core;

namespace EBay.PhotoSDK.ViewModel
{
   public sealed class PhotoStoreViewModel
   {
      private const int PicturesPerPage = 10;

      public PagedCollection Photos { get; private set; }

      public event EventHandler InitializationCompletedEvent;

      public int CurrentPage { get; private set; }

      public PhotoStoreViewModel( IDataProvider provider )
      {
         Photos = new PagedCollection();
         m_loadCommand = new MoreButtonCommand( this );

         if( provider == null )
            throw new ArgumentNullException();

         m_provider = provider;
         CurrentPage = 1;
      }

      public void SetProvider( IDataProvider provider, CoreDispatcher dispatcher )
      {
         if( provider == null || dispatcher == null )
            throw new ArgumentNullException();

         SetDispatcher( dispatcher );

         m_provider = provider;

         m_initDone = false;

         this.Photos.Clear();
         InitAsync();
      }

      public void SetDispatcher( CoreDispatcher dispatcher )
      {
         this.m_dispatcher = new WeakReference( dispatcher );
      }

      public void InitAsync()
      {
         if( m_initDone == false )
         {
            m_provider.InitAsync( () =>
            {
               if( m_provider.FRequiresAuthentication() )
               {
                  m_provider.DoAuthenticationAsync( result =>
                  {
                     if( result != true )
                     {
                        throw new UnauthorizedAccessException();
                     }
                     else
                     {
                        m_initDone = result;

                        if( InitializationCompletedEvent != null )
                           InitializationCompletedEvent( this, null );
                     }
                  } );
               }
               else
               {
                  m_initDone = true;

                  if( InitializationCompletedEvent != null )
                     InitializationCompletedEvent( this, null );
               }
            } );
         }
      }

      public void Load( PhotoSearchParams searchParams )
      {
         if( searchParams == null )
         {
            throw new ArgumentNullException();
         }

         if( m_searchParams != searchParams )
         {
            m_searchParams = searchParams;

            CurrentPage = 0;
            Photos.Clear();

            Photos.HideMoreButton();
            Photos.ShowLoading();
         }
         else
         {
            Photos.HideMoreButton();
            Photos.ShowLoading();
         }

         m_provider.LoadDataAsync( searchParams, CurrentPage, PicturesPerPage, ( fSuccess, result, total ) =>
         {
            CoreDispatcher dispatcher = m_dispatcher.Target as CoreDispatcher;
            if( dispatcher != null )
            {
               var ignore = dispatcher.RunAsync( CoreDispatcherPriority.Normal, () =>
               {
                  Photos.HideLoading();

                  //
                  if( fSuccess && result != null && result.Count > 0 )
                  {
                     foreach( var item in result )
                     {
                        EBay.PhotoSDK.Model.Photo photo = new EBay.PhotoSDK.Model.Photo( item );
                        Photos.Add( photo );
                     }
                  }

                  //
                  if( total > Photos.Count )
                  {
                     Photos.ShowMoreButton( this.m_loadCommand );
                  }
               } );
            }
         } );
      }

      private void LoadMore()
      {
         CurrentPage++;
         Load( m_searchParams );
      }

      private PhotoSearchParams m_searchParams;
      private IDataProvider m_provider;
      private bool m_initDone = false;
      private WeakReference m_dispatcher;
      private MoreButtonCommand m_loadCommand;

      private class MoreButtonCommand : System.Windows.Input.ICommand
      {
         PhotoStoreViewModel m_parent;

         public MoreButtonCommand( PhotoStoreViewModel parent )
         {
            this.m_parent = parent;
         }

         public bool CanExecute( object parameter )
         {
            return true;
         }

         public event EventHandler CanExecuteChanged
         {
            add { }
            remove { }
         }

         public void Execute( object parameter )
         {
            m_parent.LoadMore();
         }
      }
   }
}
