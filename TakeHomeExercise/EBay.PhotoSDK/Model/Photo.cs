using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBay.PhotoSDK.Model
{
   public class Photo : Helpers.NotifyPropertyChanged
   {
      public string ID { get; private set; }
      public object Data { get; private set; }

      public Photo( string id, object data )
      {
         this.ID = id;
         this.Data = data;
      }
   }
}
