using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBay.PhotoSDK.Model;

namespace EBay.PhotoSDK
{
   public interface IDataProvider
   {
      bool FRequiresAuthentication();

      Task DoAuthenticationAsync( Action<bool> fAuthenticated );

      Task InitAsync();

      Task LoadDataAsync( int pageId, int perPage, Action<bool, IReadOnlyList<Photo>> result );
   }
}
