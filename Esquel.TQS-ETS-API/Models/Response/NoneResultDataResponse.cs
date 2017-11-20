using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esquel.TQS_ETS_API.Models.TQS.Response
{
    public class NoneResultDataResponse:BaseResponse<bool?>
    {

        public NoneResultDataResponse(Exception ex)
            :base(ex)
        { }

        public NoneResultDataResponse(string message)
            :base(message)
        { }

        public NoneResultDataResponse(Exception ex,string message)
            : base(ex,message)
        { }

        public NoneResultDataResponse(ResultTypeEnum resultType, Exception ex)
            : base(resultType,ex)
        { }

        public NoneResultDataResponse(ResultTypeEnum resultType, Exception ex,string message)
            : base(resultType, null,message,ex)
        { }

        public NoneResultDataResponse(ResultTypeEnum resultType, string message)
            : base(resultType, message)
        { }

    }
}