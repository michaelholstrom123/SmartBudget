using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;

namespace PrismIntro.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            new I18N.West.CP1250();
            new I18N.CJK.CP50220();
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
        public class iOSInitializer : IPlatformInitializer // Implement the IPlatformInitializer.
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                
            }
        }
    }
}
