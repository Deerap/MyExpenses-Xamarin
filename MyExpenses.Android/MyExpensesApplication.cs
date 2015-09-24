using System;
using Android.App;
using MyExpenses.Helpers;
using Microsoft.WindowsAzure.MobileServices;

namespace MyExpenses.Android
{
    [Application(Theme = "@android:style/Theme.Holo.Light")]
    public class MyExpensesApplication : Application
    {
        public static Activity CurrentActivity { get; set; }
        public MyExpensesApplication(IntPtr handle, global::Android.Runtime.JniHandleOwnership transer)
            : base(handle, transer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            CurrentPlatform.Init();
            ServiceRegistrar.Startup();

        }
    }
}
