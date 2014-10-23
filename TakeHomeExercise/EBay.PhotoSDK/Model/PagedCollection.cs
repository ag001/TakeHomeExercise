using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBay.PhotoSDK.Model
{
   public class PagedCollection : System.Collections.ObjectModel.ObservableCollection<Photo>
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
   }
}
