Take Home Exercise 
This solution will build Apps for Windows Phone 8.1 along with the one for Windows 8.1. This app has three different photo data sources (flickr, 500px and local camera roll folder). You can switch between these three sources by clicking on the buttons on the App bar.
This app will load photos from the specified data source, and will give you the option to load more. In this implementation Flickr and Camera Roll (only when they have more than 20 images) support loading more images. 500px Api DLL does not support loading more pages, but if you can find better implementation then things will automatically start to work.
1.	EBay.PhotoSDK (Portable DLL)
Entire logic of the SDK resides in EBay.PhotoSDK. This will allow other apps to use this dll and keep the app clean.
2.	Universal App
Universal App will allow us to write the app logic once and get Apps for both platforms.
3.	UnitTest Project
Unit test will test the logic of PhotoSDK.
4.	Flickr Authentication
Currently Flickr API is authorized, but you can remove the “#if true” block from FlickrDataProvider.cs. Once you do that you will see the Authentication flow working.
5.	ViewModel
PhotoStoreViewModel contains the ViewModel. App only needs to bind to “Photos” property and data will be loaded automatically.
6.	PhotoTemplateSelector
This template selector will allow correct DataTemplate to be used in GridView.
7.	ValueConverters
These 3 valueconverters will convert data to images, or data to text.


