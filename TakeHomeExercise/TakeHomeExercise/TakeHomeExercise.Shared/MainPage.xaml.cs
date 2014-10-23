using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TakeHomeExercise
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class MainPage : Page
   {
      App App
      {
         get
         {
            return ( App ) App.Current;
         }
      }

      public MainPage()
      {
         this.InitializeComponent();

         this.NavigationCacheMode = NavigationCacheMode.Required;

         if( App.ViewModel == null )
         {
            // create viewmodel
            App.ViewModel = new EBay.PhotoSDK.ViewModel.PhotoStoreViewModel( new DataProviders.FlickrDataProvider() );
            App.ViewModel.SetDispatcher( this.Dispatcher );
         }

         this.DataContext = App.ViewModel;

         this.Loaded += ( o, e ) =>
         {
            App.ViewModel.InitializationCompletedEvent += ViewModel_InitializationCompletedEvent;
            App.ViewModel.InitAsync();
            App.ViewModel.SetDispatcher( this.Dispatcher );
         };
         this.Unloaded += ( o, e ) =>
         {
            App.ViewModel.InitializationCompletedEvent -= ViewModel_InitializationCompletedEvent;
         };
      }

      void ViewModel_InitializationCompletedEvent( object sender, System.EventArgs e )
      {
         var ignore = this.Dispatcher.RunAsync( Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
         {
            App.ViewModel.Load( new EBay.PhotoSDK.Model.PhotoSearchParams { SearchText = "flowers" } );
         } );
      }

      /// <summary>
      /// Invoked when this page is about to be displayed in a Frame.
      /// </summary>
      /// <param name="e">Event data that describes how this page was reached.
      /// This parameter is typically used to configure the page.</param>
      protected override void OnNavigatedTo( NavigationEventArgs e )
      {
         // TODO: Prepare page for display here.

         // TODO: If your application contains multiple pages, ensure that you are
         // handling the hardware Back button by registering for the
         // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
         // If you are using the NavigationHelper provided by some templates,
         // this event is handled for you.
      }

      private void AppBarButtonF_Click( object sender, RoutedEventArgs e )
      {
         App.ViewModel.SetProvider( new DataProviders.FlickrDataProvider(), this.Dispatcher );
      }

      private void AppBarButtonPx_Click( object sender, RoutedEventArgs e )
      {
         App.ViewModel.SetProvider( new DataProviders._500pxDataProvider(), this.Dispatcher );
      }

      private void AppBarButtonL_Click( object sender, RoutedEventArgs e )
      {
         App.ViewModel.SetProvider( new DataProviders._500pxDataProvider(), this.Dispatcher );
      }

      private void ListViewControl_SelectionChanged( object sender, SelectionChangedEventArgs e )
      {
         if( ListViewControl.SelectedIndex >= 0 )
         {
            object item = ListViewControl.Items[ ListViewControl.SelectedIndex ];
            if( item is EBay.PhotoSDK.Model.Photo )
            {
               this.Frame.Navigate( typeof( PhotoDetails ), item );
            }
         }
      }
   }
}
