﻿using Bit.iOS;
using Bit.ViewModel.Implementations;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace Bit.CSharpClientSample.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : BitFormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            SQLitePCL.Batteries.Init();

            UseDefaultConfiguration();
            Forms.Init();

            LoadApplication(new App(new SampleAppiOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class SampleAppiOSInitializer : BitPlatformInitializer
    {

    }
}
