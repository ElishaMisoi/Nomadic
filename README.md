# Nomadic

## About

[Microsoft News](https://play.google.com/store/apps/details?id=com.microsoft.amp.apps.bingnews&hl=en) is built with Xamarin. I wondered… can I build the same 🤔? So I set out on this journey, I called it Nomadic.

### Screenshots

![](https://miro.medium.com/max/774/1*Uhsmgy1_iVakYzQwuF6NhQ.gif)

![Nomadic Dark](https://cdn-images-1.medium.com/max/800/1*DxXWGIXy2LA4tXgw8n7kfQ.png)

![MS News Dark](https://cdn-images-1.medium.com/max/800/1*sJQuOpx_sPNfm5GPKtzPmw.png)

![Nomadic Light](https://cdn-images-1.medium.com/max/800/1*C6wtGlfVS_09SCsR7JUxug.png)

![MS News Light](https://cdn-images-1.medium.com/max/800/1*zvpBF4oq59NUl0iEA02dOg.png)

## Getting Started

To be able to run the app you will need to do a little setup. No worries, there's a whole blog about it 🙃. But if you want a quick start, check the instructions below:

### Create a NewsAPI Key

Head over to [newsapi.org](https://newsapi.org/) and register. Once you have an account, go to your account dashboard and retrieve your API key.

![NewsAPI Key](https://cdn-images-1.medium.com/max/800/1*fH4zsOFTXvuP1qsZaZa6cQ.png)

Add your NewsAPI Key in the Constants.cs file at the root of the shared project.

![Constants.cs file](https://cdn-images-1.medium.com/max/800/1*0CgPiNV4MN7QXjk0qfI7sQ.png)

At this point you can build and deploy the Application and you'll have almost all the functionality working 🙃... login will not work though.

If you're interested in login as well, follow the blog post below to create your Google Client Ids:

[Google Authentication Blog Post](https://link.medium.com/UsQv6t9O15)

## The making of...

I have been writing some blog posts on how to create this app, which can be found below:

- [Part 1:](https://link.medium.com/RF1k3lBN15) Setting Up
- [Part 2:](https://link.medium.com/Hfp1tLHN15) Using Shell
- [Part 3:](https://link.medium.com/82PjSwTN15) Using Custom Fonts
- [Part 4:](https://link.medium.com/bpzxOmZN15) Using Themes
- [Part 5:](https://link.medium.com/TbZ9nodO15) NewsAPI
- [Part 6:](https://link.medium.com/R4RPUTkO15) Using MVVM
- [Part 7:](https://link.medium.com/RRWaHIwO15) Using CarouselView
- [Part 8:](https://link.medium.com/UYqcSsFO15) Using ListView with DataTemplateSelector
- [Part 9:](https://link.medium.com/1loOkgLO15) Using WebView
- [Part 10:](https://link.medium.com/E5mdpESO15) Using CollectionView
- [Part 11:](https://link.medium.com/pYQD37XO15) Using Geo Location
- [Part 12:](https://link.medium.com/Z7A83o3O15) Creating a Firebase Project
- [Part 13:](https://link.medium.com/UsQv6t9O15) Google Authentication with Firebase
- [Part 14:](https://link.medium.com/hCSlA6dP15) Using Xamarin.Essentials Preferences and SecureStorage .....aand Wrapping Up 🙃

## The awesome Libraries used 😎:

- [Xamarin.Essentials](https://github.com/xamarin/Essentials) by Microsoft
- [NewsAPI C# Client](https://newsapi.org/docs/client-libraries/csharp) by [newsapi.org](https://newsapi.org/)
- [Xamarin.Auth](https://github.com/xamarin/Xamarin.Auth)
- [Plugin.FirebaseAuth](https://github.com/f-miyu/Plugin.FirebaseAuth) by [f-miyu](https://github.com/f-miyu)
- [Plugin.CloudFirestore](https://github.com/f-miyu/Plugin.CloudFirestore) by [f-miyu](https://github.com/f-miyu)
- [UserDialogs](https://github.com/aritchie/userdialogs) by [Allan Ritchie](https://allancritchie.net/)
- [PancakeView](https://github.com/sthewissen/Xamarin.Forms.PancakeView) by [Steven Thewissen](https://thewissen.io)
- [CardsView](https://github.com/AndreiMisiukevich/CardView) by [Andrei Misiukevich](https://github.com/AndreiMisiukevich)
- [FFImageLoading](https://github.com/luberda-molinet/FFImageLoading) by [Daniel Luberda](https://github.com/daniel-luberda)
- [Rg.Plugins.Popup](https://github.com/rotorgames/Rg.Plugins.Popup) by [Martijn van Dijk](https://github.com/martijn00)
- [CurrentActivityPlugin](https://github.com/jamesmontemagno/CurrentActivityPlugin) by [James Montemagno](https://github.com/jamesmontemagno)

## Disclaimer ☠!!

As much as consideration was taken for iOS platform-specific code, this application has not been tested on iOS. In theory it should work, but whatever bugs that you may encounter or on the event that the application does not successfully build or for whatever reason your device bursts into flames, I might not be of help. Sorry 😐.

On a different note, please contribute and/or raise issues 😁.
