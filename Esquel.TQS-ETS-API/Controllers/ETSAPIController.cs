using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Esquel.TQS_ETS_API.Models.ETS;
using Esquel.TQS_ETS_API.Models.TQS;
using Esquel.TQS_ETS_API.Models.TQS.Response;

using System.Data.SqlClient;

namespace Esquel.TQS_ETS_API.Controllers
{
    public class ETSAPIController : ApiController
    {

        [HttpGet]
        [Route("api/v1/orderlist")]
        public Models.TQS.Response.BaseResponse<QueryOrderResult[]> GetOrderList([FromUri] QueryOrderParameter pi_parameter)
        {
            try
            {
                using (ETSContext mycontext = new ETSContext())
                {
                    QueryOrder_Function l_fun = new QueryOrder_Function();
                    MSSQLStatment getSQL = l_fun.GenerateQueryOrderListSQL(pi_parameter.ZDCode, pi_parameter.CardNo);

                    var l_queryOrderList = mycontext.Database.SqlQuery<QueryOrderResult>(getSQL.Statment, getSQL.ParameterList.ToArray()).ToArray();
                    return new BaseResponse<QueryOrderResult[]>(l_queryOrderList.ToArray());
                }
            }
            catch (Exception ex)
            {
                var result = new Models.TQS.Response.BaseResponse<QueryOrderResult[]>(ex);
                result.resultMsg = ex.Message;
                return result;
            }
        }
    }
}