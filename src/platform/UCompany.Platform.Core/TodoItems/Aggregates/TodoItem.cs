using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UCompany.Platform.TodoItems.Aggregates
{
    public class TodoItem : FullAuditedAggregateRoot<int>
    {
        public string Description { get; set; }
        public bool IsChecked { get; set; }

        #region 构造
        public TodoItem(string description, bool isChecked)
        {
            Description = description;
            IsChecked = isChecked;
        }
        #endregion


        #region 方法
        public void SetProperties(string description, bool isChecked)
        {
            Description = description;
            IsChecked = isChecked;
        }
        #endregion
    }
}
