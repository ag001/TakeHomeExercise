using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBay.PhotoSDK.Model;

namespace EBay.PhotoSDK
{
   public delegate void AuthenticationCompleted( bool e );
   public delegate void InitCompleted();
   public delegate void LoadCompleted( bool fCompleted, IReadOnlyList<object> list, int num );

   public interface IDataProvider
   {
      bool FRequiresAuthentication();

      void DoAuthenticationAsync( AuthenticationCompleted fAuthenticated );

      void InitAsync( InitCompleted initCompleted );

      void LoadDataAsync( PhotoSearchParams searchParams, int pageId, int perPage, LoadCompleted result );
   }
}
