using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Esquel.TQS_ETS_API
{
    public class MSSQLStatment
    {
        public MSSQLStatment()
        {
            this.ParameterList = new List<SqlParameter>();
        }
        public string Statment { get; set; }
        public List<SqlParameter> ParameterList { get; set; }
    }

    public class QueryOrder_Function
    {
        public MSSQLStatment GenerateQueryOrderListSQL(string pi_ZDCode, string pi_CardNo)
        {
            MSSQLStatment result_sql = new MSSQLStatment();

            result_sql.Statment = @"IF OBJECT_ID(N'tempdb.dbo.#temp_order') IS NOT NULL
                                    BEGIN
                                        DROP TABLE #temp_order                                        
                                    END

                                    SELECT 
                                    a.ZDCODE AS ZDCode,
                                    c.CardNo As CardNo,
                                    b.fly_code AS GroupNo,
                                    a.CUSTNAME AS CustName,
                                    a.SampleType AS SampleType,
                                    a.washtype AS WashingType,
                                    a.STYLE_NO AS StyleNo,
                                    a.Section AS SectionGroup,
                                    a.MY_COUNT AS OrderCount,
                                    b.gx_no AS CurrentProcessCode,
                                    d.NAME AS CurrentProcessName,
                                    'Normal' As [Status]
                                    INTO #temp_order
                                    FROM T_SCZZD a(NOLOCK)
                                    INNER JOIN t_gxtime b(NOLOCK) ON a.zdcode=b.zdcode AND b.fly_Code >0 AND ISNULL(a.sendflag,0)=0 AND b.endtime IS NULL
                                    INNER JOIN tbauGroupCard c(NOLOCK) ON b.zdcode=c.zdcode AND b.fly_code=c.GroupNo
                                    LEFT JOIN t_bm d ON b.gx_no=d.code
                                    WHERE (a.ZDcode like '%'+ @ZDCode +'%' OR @ZDCode ='')
                                    AND (c.CardNo=@CardNo OR @CardNo=''); 

                                    --更新状态-----------
                                    UPDATE #temp_order SET  [Status]= Case 
                                    When b.isDelay = 1 Then 'Hold'        
                                    When b.isrework = 1 Then 'ReWork' 
                                    Else a.[Status] END
                                    FROM #temp_order a, tbauGroupCard b(nolock)                
                                    WHERE a.zdcode = b.zdcode         
                                    AND a.GroupNo = b.Groupno;

                                    UPDATE a SET [Status] = '退单'        
                                    FROM #temp_order a, tb_MORetreat_Record b(Nolock)    
                                    WHERE a.zdcode = b.zdcode and a.GroupNo = b.Groupno AND  b.IsReOrder = 0 ;

                                    --更新车缝组别   
                                    UPDATE a set a.SectionGroup=b.workline    
                                    FROM #temp_order a ,T_Gxtime b(NOLOCK)     
                                    WHERE a.zdcode=b.zdcode    
                                    AND a.GroupNo=b.fly_Code    
                                    AND b.gx_no='15' AND ISNULL(b.Workline,'') <>''  ;

 
                                    ----如果车缝有分卡就拿分卡后的section  
                                    update a set a.SectionGroup=c.SewSection    
                                    from #temp_order a inner JOIN T_Gxtime b(NOLOCK)  
                                    ON a.zdcode=b.zdcode AND a.GroupNo=b.fly_Code   
                                    inner join tb_GroupNOChangeRecord c(NOLOCK)  
                                    ON a.zdcode=c.ZdCode  
                                    AND c.ChildGroupNO=a.GroupNo  
                                    where b.workshop in (select WorkshopName from TWorkshop where OrderNo>=11)  
                                    AND ISNULL(c.SewSection,'')<>'' ;

                                    SELECT * FROM #temp_order

                                    IF OBJECT_ID(N'tempdb.dbo.#temp_order') IS NOT NULL
                                    BEGIN
                                        DROP TABLE #temp_order                                        
                                    END";


            result_sql.ParameterList.Add(new SqlParameter("@ZDCode", SqlDbType.VarChar) { Value = string.IsNullOrEmpty(pi_ZDCode) ? "" : pi_ZDCode.Trim() });
            result_sql.ParameterList.Add(new SqlParameter("@CardNo", SqlDbType.VarChar) { Value = string.IsNullOrEmpty(pi_CardNo) ? "" : pi_CardNo.Trim() });

            return result_sql;
        }
    }
}