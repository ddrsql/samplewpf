using UCompany.Platform.TodoItems;
using UCompany.Platform.TodoItems.Dtos;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using UCompany.UProject.Wpf.Infra.Common;
using UCompany.UProject.TodoList.Views;
using UCompany.UProject.Wpf.Infra.Constants;
using UCompany.Platform.TodoItems.Vtos;
using Abp.ObjectMapping;

namespace UCompany.UProject.TodoList.ViewModels
{
    public class TodoListViewModel : ViewModelBase, INavigationAware, IJournalAware
    {
        private readonly ITodoItemAppService _todoItemAppService = IocManager.Instance.Resolve<ITodoItemAppService>();
        private readonly IObjectMapper _objectMapper = IocManager.Instance.Resolve<IObjectMapper>();
        private readonly IRegionManager _regionManager;

        public TodoListViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Items = new ObservableCollection<TodoItemVM>();
        }

        #region Properties
        private ObservableCollection<TodoItemVM> _items = new ObservableCollection<TodoItemVM>();
        public ObservableCollection<TodoItemVM> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        #endregion
        
        #region Commands
        private DelegateCommand _addItemCommand;
        public DelegateCommand AddItemCommand =>
            _addItemCommand ?? (_addItemCommand = new DelegateCommand(ExecuteAddItemCommand));
        private DelegateCommand _TestCommand;
        public DelegateCommand TestCommand =>
            _TestCommand ?? (_TestCommand = new DelegateCommand(ExecuteTestCommand));

        private void ExecuteTestCommand()
        {
            _items[0].Description = $"变更{DateTime.Now}";
        }
        #endregion

        #region  Excutes
        async void ExecuteAddItemCommand()
        {
            var todoItems = await _todoItemAppService.GetTodoItemListAsync();
            var param = new NavigationParameters();
            param.Add("Page", "MainWindowViewModel");
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(AddItemView));
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            Task.Run(async () =>
            {
                var page = navigationContext.Parameters["Page"] as string;
                if (page != "AddItemViewModel" && _items.Count == 0)
                {
                    var todoItems = await _todoItemAppService.GetTodoItemListAsync();
                    var list = _objectMapper.Map<ObservableCollection<TodoItemVM>>(todoItems);
                    _items.AddRange(list);
                }
            });

            var description = navigationContext.Parameters["Description"] as string;
            if (description != null && _items.Where(p => p.Description == description).Count() == 0)
            {
                _items.Add(new TodoItemVM() { Description = description });
            }
        }
        
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// false 不将页面在导航过程中不加入导航日志
        /// </summary>
        /// <returns></returns>
        public bool PersistInHistory()
        {
            return true;
        }
        #endregion

    }
}
