using Avalonia.Controls;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;

namespace UCompany.UProject.Wpf
{
    class TabControlAdapter : RegionAdapterBase<TabControl>
    {
        public TabControlAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, TabControl regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    foreach (UserControl item in e.NewItems)
                    {
                        //regionTarget.Items.ToDynamicList().Add(new TabItem { Header = item.Name, Content = item });
                        var items = regionTarget.Items as List<TabItem>;
                        if (items == null)
                            items = new List<TabItem>();
                        items?.Add(new TabItem { Header = item.Name, Content = item });
                        regionTarget.Items = items;
                        //regionTarget.Items.Add(new TabItem { Header = item.Name, Content = item });
                    }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                    foreach (UserControl item in e.OldItems)
                    {
                        var tabTodelete = regionTarget.Items.OfType<TabItem>().FirstOrDefault(n => n.Content == item);
                        regionTarget.Items.ToDynamicList().Remove(tabTodelete);
                        //regionTarget.Items.Remove(tabTodelete);
                    }

            };
        }


        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
