using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EBay.PhotoSDK.Model
{
   public class Photo : Helpers.NotifyPropertyChanged
   {
      public object Data { get; private set; }

      public Photo( object data )
      {
         this.Data = data;
      }
   }
}
