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

namespace UCompany.UProject.TodoList.ViewModels
{
    public class AddItemViewModel : ViewModelBase, INavigationAware
    {
        private readonly ITodoItemAppService _todoItemAppService = IocManager.Instance.Resolve<ITodoItemAppService>();
        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal? _journal;

        public AddItemViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #region Properties
        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        #endregion

        #region Commands
        
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));

        private DelegateCommand _okCommand;
        public DelegateCommand OkCommand =>
            _okCommand ?? (_okCommand = new DelegateCommand(ExecuteOkCommand));
        #endregion

        #region  Excutes
        async void ExecuteOkCommand()
        {
            await _todoItemAppService.CreateOrUpdateTodoItemAsync(new CreateOrUpdateTodoItemInput() { Description = _description });
            var param = new NavigationParameters();
            param.Add("Page", "AddItemViewModel");
            param.Add("Description", _description);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(TodoListView), NavigationCompelted, param);
        }

        void ExecuteCancelCommand()
        {
            if (_journal != null && _journal.CanGoBack)
                _journal.GoBack();
        }

        /// <summary>
        /// 导航后目的页面触发，一般用于初始化或者接受上页面的传递参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            var para = navigationContext.Parameters;
        }

        private void NavigationCompelted(NavigationResult result)
        {
            if (result.Result == true)
            {

            }
            else
            {

            }
        }
        #endregion
    }
}
