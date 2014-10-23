using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBay.PhotoSDK.Model
{
   public struct PhotoSearchParams
   {
      public string SearchText { get; set; }

      public override bool Equals( object obj )
      {
         if( obj == null )
            return false;

         if( obj.GetType() != this.GetType() )
            return false;

         PhotoSearchParams p = ( PhotoSearchParams ) obj;
         if( p.SearchText != this.SearchText )
            return false;

         return base.Equals( obj );
      }

      public override int GetHashCode()
      {
         return base.GetHashCode() ^ SearchText.GetHashCode();
      }

      public static bool operator ==( PhotoSearchParams c1, PhotoSearchParams c2 )
      {
         return c1.Equals( c2 );
      }

      public static bool operator !=( PhotoSearchParams c1, PhotoSearchParams c2 )
      {
         return !c1.Equals( c2 );
      }
   }
}
