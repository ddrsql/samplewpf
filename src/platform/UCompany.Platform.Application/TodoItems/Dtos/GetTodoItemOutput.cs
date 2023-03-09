using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCompany.Platform.TodoItems.Dtos
{
    public class GetTodoItemOutput
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
    }
}
