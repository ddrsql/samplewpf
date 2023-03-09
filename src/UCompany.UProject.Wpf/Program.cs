using Avalonia;
using Avalonia.ReactiveUI;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace UCompany.UProject.Wpf
{
    internal class Program
    {
        //[STAThread]
        //public static void Main(string[] args) {
        //    Console.WriteLine("执行Main");
        //}

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            if (!args.Contains("--applicationName"))
            {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            }
        }
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
                //.UseReactiveUI();
    }
}