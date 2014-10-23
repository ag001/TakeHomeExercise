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

      void DoAuthenticationAsync( Action<bool> fAuthenticated );

      void InitAsync( Action initCompleted );

      Task LoadDataAsync( PhotoSearchParams searchParams, int pageId, int perPage, Action<bool, IReadOnlyList<object>, int> result );
   }
}
