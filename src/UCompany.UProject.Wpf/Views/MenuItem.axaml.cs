using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UCompany.UProject.Wpf.Views
{
    public partial class MenuItemUC : UserControl
    {
        public string Title { get; set; } = "MenuItemUC";
        public MenuItemUC()
        {
            InitializeComponent();
        }
        //private void InitializeComponent()
        //{
        //    AvaloniaXamlLoader.Load(this);
        //}
    }
}
