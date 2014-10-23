using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EBay.PhotoSDK.Model
{
   public sealed class Photo : INotifyPropertyChanged
   {
      public object Data { get; private set; }

      public Photo( object data )
      {
         this.Data = data;
      }

      private void Notify( [CallerMemberName] string propertyName = "" )
      {
         if( PropertyChanged != null )
         {
            PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
