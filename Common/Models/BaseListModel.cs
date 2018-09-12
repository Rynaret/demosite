using System.Collections.Generic;

namespace Common.Models
{
    public class BaseListModel<T>
    {
        public List<T> Data { get; set; }
    }
}
