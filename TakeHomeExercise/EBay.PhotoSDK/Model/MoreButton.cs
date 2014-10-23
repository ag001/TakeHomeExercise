using System.Windows.Input;

namespace EBay.PhotoSDK.Model
{
   public sealed class MoreButton
   {
      public ICommand IMoreCommand { get; private set; }

      public MoreButton( ICommand cmd )
      {
         this.IMoreCommand = cmd;
      }
   }
}
