using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EBay.PhotoSDK.Helpers
{
   public class NotifyPropertyChanged : INotifyPropertyChanged
   {
      public void Notify( [CallerMemberName] string propertyName = "" )
      {
         if( PropertyChanged != null )
         {
            PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
