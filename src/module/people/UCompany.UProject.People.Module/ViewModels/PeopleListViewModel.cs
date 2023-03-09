using System;
using System.Collections.Generic;
using Abp.Dependency;
using Material.Icons.Avalonia;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ReactiveUI;
using UCompany.UProject.People.Application.Contracts.Accounts;

namespace UCompany.UProject.People.Module.ViewModels
{
	public class PeopleListViewModel : BindableBase
    {
        private readonly IPeopleAppService _peopleAppService = IocManager.Instance.Resolve<IPeopleAppService>();

        #region Properties
        private int _count;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
        #endregion

        #region Commands
        private DelegateCommand _testCommand;
        public DelegateCommand TestCommand =>
            _testCommand ?? (_testCommand = new DelegateCommand(ExecuteTestCommand));
        #endregion

        #region  Excutes
        void ExecuteTestCommand()
        {
            Count = _peopleAppService.UpdateCount();
        }
        #endregion
    }
}