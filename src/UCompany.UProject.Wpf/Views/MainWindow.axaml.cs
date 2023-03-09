using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Material.Styles.Controls;
using Material.Styles.Themes.Base;
using Material.Styles.Themes;
using DryIoc;

namespace UCompany.UProject.Wpf.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        //private void InitializeComponent()
        //{
        //    AvaloniaXamlLoader.Load(this);
        //}
        private void TemplatedControl_OnTemplateApplied(object? sender, TemplateAppliedEventArgs e)
        {
            SnackbarHost.Post("Welcome to demo of Material.Avalonia!", null, DispatcherPriority.Normal);
        }

        private void MaterialIcon_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            var materialTheme = Application.Current.LocateMaterialTheme<MaterialTheme>();
            materialTheme.BaseTheme = materialTheme.BaseTheme == BaseThemeMode.Light ? BaseThemeMode.Dark : BaseThemeMode.Light;
        }
    }
}