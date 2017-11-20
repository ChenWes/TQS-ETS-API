using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esquel.TQS_ETS_API.Models.TQS.Response
{
    public enum ResultTypeEnum
    {
        Success = 0,

        //Information = 1,

        //Warning = 2,

        Error = 4,
    }


   // [Serializable] 
    public class BaseResponse<T>
    {

        public virtual string resultType { get; private set; }
        public virtual T results { get; set; }
        public virtual string resultMsg { get; set; }

        public virtual object exceptionDetail { get; private set; }


        public ResultTypeEnum GetResultType()
        {
            ResultTypeEnum result;
            if (Enum.TryParse<ResultTypeEnum>(this.resultType, true, out result))
                return result;
            else
                throw new Exception("GetResultType Enum.TryParse error!");
        }

        public  BaseResponse(ResultTypeEnum resultType, T data, string message, Exception ex)
        {            
            this.resultType = resultType.ToString().ToUpper();

            this.results = data;
            this.resultMsg = message;
            this.exceptionDetail = SimplifyException(ex);
        }

        public  BaseResponse(ResultTypeEnum ResultType, T data, string message)
            : this(ResultType, data, message, null)
        {

        }


        public  BaseResponse(ResultTypeEnum ResultType, string message)
            : this(ResultType, default(T), message, null)
        {

        }

        public  BaseResponse(ResultTypeEnum ResultType, T data)
            : this(ResultType, data, string.Empty, null)
        {

        }

        public  BaseResponse(ResultTypeEnum ResultType, Exception ex)
            : this(ResultType, default(T), GetExceptionInnerMessage(ex), ex)
        {

        }

        public  BaseResponse(string message)
            : this(ResultTypeEnum.Success, default(T), message, null)
        {

        }

        public  BaseResponse(T data, string message)
            : this(ResultTypeEnum.Success, data, message, null)
        {

        }

        public  BaseResponse(T data)
            : this(ResultTypeEnum.Success, data, string.Empty, null)
        {

        }

        public  BaseResponse(Exception ex)
            : this(ResultTypeEnum.Error, default(T), GetExceptionInnerMessage(ex), ex)
        {

        }

        public  BaseResponse(Exception ex, string message)
            : this(ResultTypeEnum.Error, default(T), message, ex)
        {

        }


        public override bool Equals(object obj)
        {

            //if (obj is BaseResponse<T>)
            //{
            //    var that = obj as BaseResponse<T>;
            //    return
            //         this.resultMsg.Equals(that.resultMsg)
            //        && this.resultType.Equals(that.resultType)
            //        && this.results.Equals(that.results)
            //        ;
            //    //return this.AreEquals<BaseResponse<T>>(that);
            //}
            //else
            //{
            //    return false;
            //}

            BaseResponse<T> that = obj as BaseResponse<T>;

            if (that == null)
            {
                return false;
            }

            //if (obj.GetType() != this.GetType())
            //{
            //    return false;
            //}

            if (!(this.resultType == that.resultType
                && this.resultMsg == that.resultMsg))
            {
                return false;
            }

            if (this.results != null && that.results != null)
            {
                if (this.results.GetType().IsArray)
                {
                    var thisList = ((IEnumerable<object>)this.results).ToArray();
                    var thatList = ((IEnumerable<object>)that.results).ToArray();

                    if (thisList.Length != thatList.Length)
                    { return false; }

                    for (int i = 0; i < thisList.Length; i++)
                    {
                        if (!thisList[i].Equals(thatList[i]))
                        { return false; }
                    }                    

                }
                else
                {
                    if (!this.results.Equals(that.results))
                    { return false; }
                }
            }
            else if (!(this.results == null && that.results == null))
            { return false; }

            if (this.exceptionDetail != null && that.exceptionDetail != null)
            {
                // return (!this.exceptionDetail.Equals(compareObj.exceptionDetail));
                return true;//异常内容不作对比，默认一致。
            }
            else if (!(this.exceptionDetail == null && that.exceptionDetail == null))
            { return false; }

            return true;
           // return base.Equals(obj);
        }


        protected static string GetExceptionInnerMessage(Exception ex, bool onlyInnerMessage = true)
        {
            if (ex.InnerException != null)
            {
                if (onlyInnerMessage)
                {
                    return string.Format("{0}", GetExceptionInnerMessage(ex.InnerException));
                }
                else
                {
                    return string.Format("{0}->[{1}]", ex.Message, GetExceptionInnerMessage(ex.InnerException, onlyInnerMessage));
                }
            }
            else
            { return ex.Message; }
        }

        protected static object SimplifyException(Exception ex)
        {
            if (ex != null)
            {
                return new
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Data = ex.Data,
                    InnerException = SimplifyException(ex.InnerException),
                };
            }
            else
            { return null; }
        }
    }
}