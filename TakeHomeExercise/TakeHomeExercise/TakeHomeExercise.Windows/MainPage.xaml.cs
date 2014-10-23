﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TakeHomeExercise
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = ( ( App ) App.Current ).ViewModel;

            this.Loaded += ( o, e ) =>
            {
               ( ( App ) App.Current ).ViewModel.SetDispatcher( this.Dispatcher );
               ( ( App ) App.Current ).ViewModel.InitAsync();
               ( ( App ) App.Current ).ViewModel.Load( new EBay.PhotoSDK.Model.PhotoSearchParams { SearchText = "clouds" } );
            };
        }
    }
}
