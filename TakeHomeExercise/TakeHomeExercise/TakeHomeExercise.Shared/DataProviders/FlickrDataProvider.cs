using FlickrNet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace TakeHomeExercise.DataProviders
{
   public sealed class FlickrDataProvider : EBay.PhotoSDK.IDataProvider
   {
      private const string m_callbackUri = "http://test.com";

      private FlickrNet.Flickr m_flickr;

      private OAuthRequestToken m_token;

      public FlickrDataProvider()
      {
         m_flickr = new FlickrNet.Flickr( "cf933b74598c39c8d4a1ca8fb7546570", "f6c9a280de0cc59e" );
#if true
         // You can comment out this code to see how authentication works.
         m_flickr.OAuthAccessToken = "72157648914745815-53d9747dd41565d3";
         m_flickr.OAuthAccessTokenSecret = "d0ec86315d8fbf79";
#endif
      }

#if WINDOWS_PHONE_APP
      void FlickrDataProvider_AppActivatedEvent( object sender, Windows.ApplicationModel.Activation.WebAuthenticationBrokerContinuationEventArgs e )
      {
         ContinueWithWebAuthenticationResult( e.WebAuthenticationResult );
      }
#endif

      public bool FRequiresAuthentication()
      {
         return string.IsNullOrEmpty( m_flickr.OAuthAccessToken ) || string.IsNullOrEmpty( m_flickr.OAuthAccessTokenSecret );
      }

      public async Task DoAuthenticationAsync( Action<bool> fAuthenticated )
      {
#if WINDOWS_PHONE_APP
         ( ( App )App.Current ).AppActivatedEvent += FlickrDataProvider_AppActivatedEvent;
#endif

         m_token = await m_flickr.OAuthRequestTokenAsync( m_callbackUri );

         string uri = m_flickr.OAuthCalculateAuthorizationUrl( m_token.Token, AuthLevel.Read );
#if WINDOWS_PHONE_APP
         WebAuthenticationBroker.AuthenticateAndContinue( new Uri( uri ), new Uri( m_callbackUri ) );
#else
         WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync( WebAuthenticationOptions.None, new Uri( uri ), new Uri( m_callbackUri ) );
         ContinueWithWebAuthenticationResult( result );
#endif
      }

      public Task InitAsync()
      {
         return Task.Factory.StartNew( () => { } );
      }

      public async Task LoadDataAsync( int pageId, int perPage, Action<bool, IReadOnlyList<EBay.PhotoSDK.Model.Photo>> result )
      {
         PhotoSearchOptions options = new PhotoSearchOptions();
         options.Text = "flowers";
         PhotoCollection coll = await m_flickr.PhotosSearchAsync( options );

         if( coll == null )
         {
            result( false, null );
            return;
         }

         List<EBay.PhotoSDK.Model.Photo> list = new List<EBay.PhotoSDK.Model.Photo>();
         foreach( var item in coll )
         {
            EBay.PhotoSDK.Model.Photo photo = new EBay.PhotoSDK.Model.Photo( item.PhotoId, item );
            list.Add( photo );
         }

         result( true, list );
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

      private async void ContinueWithWebAuthenticationResult( WebAuthenticationResult result )
      {
         string OAuthVerifier = GetQueryParameter( result.ResponseData.ToString(), "oauth_verifier" );

         OAuthAccessToken accessToken = await m_flickr.OAuthAccessTokenAsync( m_token.Token, m_token.TokenSecret, OAuthVerifier );

         m_flickr.OAuthAccessTokenSecret = accessToken.TokenSecret;
         m_flickr.OAuthAccessToken = accessToken.Token;

      }
   }
}
