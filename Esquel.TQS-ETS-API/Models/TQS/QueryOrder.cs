using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esquel.TQS_ETS_API.Models.TQS
{
    public class QueryOrderResult
    {
        /// <summary>
        /// 制单号
        /// </summary>
        public string ZDCode { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustName { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 扎号
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        /// Sample Type
        /// </summary>
        public string SampleType { get; set; }

        /// <summary>
        /// Washing Type
        /// </summary>
        public string WashingType { get; set; }

        /// <summary>
        /// 车缝组别
        /// </summary>
        public string SectionGroup { get; set; }

        /// <summary>
        /// 款号
        /// </summary>
        public string StyleNo { get; set; }

        /// <summary>
        /// 制单数量
        /// </summary>
        public Int32? OrderCount { get; set; }

        /// <summary>
        /// 当前工序编码
        /// </summary>
        public string CurrentProcessCode { get; set; }

        /// <summary>
        /// 当前工序名称
        /// </summary>
        public string CurrentProcessName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }

    public class QueryOrderParameter
    {
        /// <summary>
        /// 制单号
        /// </summary>
        public string ZDCode { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        
    }
}