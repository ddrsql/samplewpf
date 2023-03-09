using UCompany.Platform.TodoItems.Vtos;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCompany.UProject.Wpf.Infra.Events
{
    public class TodoSentEvent : PubSubEvent<TodoItemVM>
    {
    }
}
