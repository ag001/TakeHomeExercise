using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBay.PhotoSDK.Model
{
   public sealed class PagedCollection : System.Collections.ObjectModel.ObservableCollection<object>
   {
      public PagedCollection()
      {
      }

      internal void Append( bool fSuccess, IReadOnlyList<Photo> result )
      {
         if( fSuccess )
         {
            foreach( var item in result )
            {
               this.Add( item );
            }
         }
      }

      internal void ShowLoading()
      {
         if( this.Count == 0 )
         {
            this.Add( new PhotoSDK.Model.LoadingButton() );
         }
         else if( ( this[ this.Count - 1 ] is PhotoSDK.Model.LoadingButton ) == false )
         {
            this.Add( new PhotoSDK.Model.LoadingButton() );
         }
      }

      internal void HideLoading()
      {
         if( this.Count > 0 )
         {
            if( this[ this.Count - 1 ] is PhotoSDK.Model.LoadingButton )
            {
               this.RemoveAt( this.Count - 1 );
            }
         }
      }

      internal void ShowMoreButton( System.Windows.Input.ICommand cmd )
      {
         if( this.Count == 0 )
         {
            this.Add( new PhotoSDK.Model.MoreButton( cmd ) );
         }
         else if( ( this[ this.Count - 1 ] is PhotoSDK.Model.MoreButton ) == false )
         {
            this.Add( new PhotoSDK.Model.MoreButton( cmd ) );
         }
      }

      internal void HideMoreButton()
      {
         if( this.Count > 0 )
         {
            if( this[ this.Count - 1 ] is PhotoSDK.Model.MoreButton )
            {
               this.RemoveAt( this.Count - 1 );
            }
         }
      }

      internal void ShowNone()
      {
         if( this.Count == 0 )
         {
            this.Add( new PhotoSDK.Model.NoneButton() );
         }
         else if( ( this[ this.Count - 1 ] is PhotoSDK.Model.NoneButton ) == false )
         {
            this.Add( new PhotoSDK.Model.NoneButton() );
         }
      }

      internal void HideNone()
      {
         if( this.Count > 0 )
         {
            if( this[ this.Count - 1 ] is PhotoSDK.Model.NoneButton )
            {
               this.RemoveAt( this.Count - 1 );
            }
         }
      }
   }
}
