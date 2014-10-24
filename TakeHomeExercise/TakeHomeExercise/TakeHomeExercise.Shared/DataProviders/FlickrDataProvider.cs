using EBay.PhotoSDK.Model;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace TakeHomeExercise.DataProviders
{
   // EbayTakeHomeaccount123@yahoo.com
   // Helloworld123
   public sealed class FlickrDataProvider : EBay.PhotoSDK.IDataProvider
   {
      private const string m_callbackUri = "http://test.com";

      private FlickrNet.Flickr m_flickr;

      private OAuthRequestToken m_token;

      EBay.PhotoSDK.AuthenticationCompleted m_fAuthenticated;

      public FlickrDataProvider()
      {
         m_flickr = new FlickrNet.Flickr( "74a20ca76c95a380d18518f97b8f8228", "2d4668334771517c" );

#if true
         // You can comment out this code to see how authentication works.
         m_flickr.OAuthAccessToken = "72157648515254950-b539f15478069911";
         m_flickr.OAuthAccessTokenSecret = "8c5aa3f6d0b3c2dd";
#endif
      }

#if WINDOWS_PHONE_APP
      void FlickrDataProvider_AppActivatedEvent( object sender, Windows.ApplicationModel.Activation.WebAuthenticationBrokerContinuationEventArgs e )
      {
         var ignore = ContinueWithWebAuthenticationResultAsync( e.WebAuthenticationResult );
      }
#endif

      public bool FRequiresAuthentication()
      {
         return string.IsNullOrEmpty( m_flickr.OAuthAccessToken ) || string.IsNullOrEmpty( m_flickr.OAuthAccessTokenSecret );
      }

      public void InitAsync( EBay.PhotoSDK.InitCompleted initCompleted )
      {
         initCompleted();
      }

      public async void DoAuthenticationAsync( EBay.PhotoSDK.AuthenticationCompleted fAuthenticated )
      {
         m_fAuthenticated = fAuthenticated;

#if WINDOWS_PHONE_APP
         ( ( App )App.Current ).AppActivatedEvent += FlickrDataProvider_AppActivatedEvent;
#endif

         m_token = await m_flickr.OAuthRequestTokenAsync( m_callbackUri );

         string uri = m_flickr.OAuthCalculateAuthorizationUrl( m_token.Token, AuthLevel.Read );

#if WINDOWS_PHONE_APP
         WebAuthenticationBroker.AuthenticateAndContinue( new Uri( uri ), new Uri( m_callbackUri ) );
#else
         WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync( WebAuthenticationOptions.None, new Uri( uri ), new Uri( m_callbackUri ) );
         await ContinueWithWebAuthenticationResultAsync( result );
#endif
      }

      private async Task ContinueWithWebAuthenticationResultAsync( WebAuthenticationResult result )
      {
         string OAuthVerifier = GetQueryParameter( result.ResponseData.ToString(), "oauth_verifier" );

         OAuthAccessToken accessToken = await m_flickr.OAuthAccessTokenAsync( m_token.Token, m_token.TokenSecret, OAuthVerifier );

         m_flickr.OAuthAccessTokenSecret = accessToken.TokenSecret;
         m_flickr.OAuthAccessToken = accessToken.Token;

         m_fAuthenticated( true );
      }

      public async void LoadDataAsync( PhotoSearchParams searchParams, int pageId, int perPage, EBay.PhotoSDK.LoadCompleted result )
      {
         PhotoSearchOptions options = new PhotoSearchOptions();
         options.Text = searchParams.SearchText;
         options.PerPage = perPage;
         options.Page = pageId;
         PhotoCollection coll = await m_flickr.PhotosSearchAsync( options );

         if( coll == null )
         {
            result( false, null, 0 );
            return;
         }

         result( true, coll, coll.Pages * coll.Count );
      }

      private static string GetQueryParameter( string input, string parameterName )
      {
         foreach( string item in input.Split( '&' ) )
         {
            var parts = item.Split( '=' );
            if( parts[ 0 ] == parameterName )
            {
               return parts[ 1 ];
            }
         }
         return String.Empty;
      }
   }
}
