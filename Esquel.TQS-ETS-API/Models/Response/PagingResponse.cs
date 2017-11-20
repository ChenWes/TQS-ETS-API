using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esquel.TQS_ETS_API.Models.TQS.Response
{
    public class PagingResponse<T> : BaseResponse<PagingData<T>>
    {
        public PagingResponse(ResultTypeEnum resultType, PagingData<T> data, string message, Exception ex)
            :base(resultType,data,message,ex)
        {           
        }

        public PagingResponse(ResultTypeEnum ResultType, PagingData<T> data, string message)
            : base(ResultType, data, message)
        {

        }


        public  PagingResponse(ResultTypeEnum ResultType, string message)
            : base(ResultType, message)
        {

        }

        public PagingResponse(ResultTypeEnum ResultType, PagingData<T> data)
            : base(ResultType, data)
        {

        }

        public  PagingResponse(ResultTypeEnum ResultType, Exception ex)
            : base(ResultType, ex)
        {

        }

        public  PagingResponse(string message)
            : base( message)
        {

        }

        public PagingResponse(PagingData<T> data, string message)
            : base( data, message)
        {

        }

        public PagingResponse(PagingData<T> data)
            : base(data)
        {

        }

        public  PagingResponse(Exception ex)
            : base( ex)
        {

        }

        public PagingResponse(Exception ex, string message)
            : base( ex,message)
        {

        }
    }
}