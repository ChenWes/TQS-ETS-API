using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esquel.TQS_ETS_API.Models.TQS.Response
{
    public class PagingData<T>
    {
        public int totalPage { get; set; }
        public IEnumerable<T> data { get; set; }
    }
}