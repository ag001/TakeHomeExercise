using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;

namespace TakeHomeExerciseUnitTest
{
   [TestClass]
   public class UnitTest1
   {
      public bool TestPassed;

      [TestMethod]
      public void TestMethod1()
      {
         TestPassed = false;
         EBay.PhotoSDK.ViewModel.PhotoStoreViewModel store = new EBay.PhotoSDK.ViewModel.PhotoStoreViewModel( new DummyProvider( this ) );

         store.InitAsync();

         Assert.IsTrue( TestPassed );
      }
   }

   class DummyProvider : EBay.PhotoSDK.IDataProvider
   {
      UnitTest1 m_parent;
      public DummyProvider( UnitTest1 parent )
      {
         m_parent = parent;
      }

      public bool FRequiresAuthentication()
      {
         return true;
      }

      public void DoAuthenticationAsync( EBay.PhotoSDK.AuthenticationCompleted fAuthenticated )
      {
         m_parent.TestPassed = true;
         fAuthenticated( true );
      }

      public void InitAsync( EBay.PhotoSDK.InitCompleted initCompleted )
      {
         initCompleted();
      }

      public void LoadDataAsync( EBay.PhotoSDK.Model.PhotoSearchParams searchParams, int pageId, int perPage, EBay.PhotoSDK.LoadCompleted result )
      {
         throw new NotImplementedException();
      }
   }
}
