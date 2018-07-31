//
//文件名：    GetAppList.aspx.cs
//功能描述：  获取所有应用信息
//创建时间：  2015/09/18
//作者：      
//修改时间：  暂无
//修改描述：  暂无
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Leo;
using ServiceInterface.Common;

namespace M_Gwt.Service.App
{
    public partial class GetAppList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //设备类型
            var deviceType = Request.Params["DeviceType"];
            //应用类型
            var appType = Request.Params["AppType"];

            try
            {
                if (deviceType == null || appType == null)
                {
                    string warning = string.Format("参数CodeCompany，AppType不能为nul！举例：http://218.92.115.55/M_Gwt/Service/App/GetAppList.aspx?DeviceType=iOS&AppType=0");
                    Json = JsonConvert.SerializeObject(new DicPackage(false, null, warning).DicInfo());
                    return;

                }



                //ServiceSCYWZH.AuthHeader authHeader = new ServiceSCYWZH.AuthHeader();
                //authHeader.UserName = "scywzh";
                //authHeader.PassWord = "MV4F*DeCY/c0EXh9k8Mg=";

                //ServiceSCYWZH.ServiceSCYWZHSoapClient ss = new ServiceSCYWZH.ServiceSCYWZHSoapClient();
                //bool isSuccess = ss.SubmitPlanTask(authHeader, "227", "0", "LHGL", "", "富隆/铁矿石/5000.000/009场/黄河1", "Type=0&&Pmno=20151010000162&&Cgno=5a2d437315ac417fbc74e1f3c8a1e268").Success;


                //ServiceMobile.AuthHeader authHeader = new ServiceMobile.AuthHeader();
                //authHeader.UserName = "mobile";
                //authHeader.PassWord = "MW4F*DeC#8M/c0h5kEXg=";

                
                //ServiceMobile.ServiceMobileSoapClient sm = new ServiceMobile.ServiceMobileSoapClient();
                //bool isSuccess = sm.Login(authHeader, "18036600293", "123456", "SPH").Success;
                //string message = sm.Login(authHeader, "18036600293", "123456", "SPH").Value;


                //bool isSuccess = sm.Register(authHeader, "18036600293", "123456", "123456", "SPH").Success;
                //string message = sm.Register(authHeader, "18036600293", "123456", "123456", "SPH").Value;


                //ServiceECommerce.AuthHeader authHeader = new ServiceECommerce.AuthHeader();
                //authHeader.UserName = "dzsw";
                //authHeader.PassWord = "MR5A*#90MDeC/c0IklEXg=";

                //ServiceECommerce.ServiceECommerceSoapClient serviceECommerce = new ServiceECommerce.ServiceECommerceSoapClient();
                ////更新采购订单明细
                //bool isSuccess = serviceECommerce.UpdatePurchasingOrderDetail(authHeader, "11111", "222222", "3333333", "正在发货", 100).IsSuccess;
                //if (!isSuccess)
                //{
                //    "报错"
                //    错误信息为：serviceECommerce.UpdatePurchasingOrderDetail(authHeader, "11111", "222222", "3333333", "正在发货", 100).Exception;
                //}

                ////更新物资采购
                //isSuccess = serviceECommerce.UpdateMaterialsPurchasing(authHeader, "11111", "222222", 100).IsSuccess;
                //if (!isSuccess)
                //{
                //    "报错"
                //    错误信息为：serviceECommerce.UpdateMaterialsPurchasing(authHeader, "11111", "222222",  100).Exception;
                //}
        

                string strSql =
                    string.Format("select url,appname,name from TB_APP_UPDATE where devicetype='{0}' and apptype='{1}'", deviceType, appType);
                var dt = new Leo.Oracle.DataAccess(RegistryKey.KeyPathMa).ExecuteTable(strSql);
                if (dt.Rows.Count <= 0)
                {
                    Json = JsonConvert.SerializeObject(new DicPackage(false, null, "暂无数据！").DicInfo());
                    return;
                }

                string[,] strArray = new string[dt.Rows.Count, 3];
                for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    strArray[iRow, 0] = Convert.ToString(dt.Rows[iRow]["url"]);
                    strArray[iRow, 1] = Convert.ToString(dt.Rows[iRow]["appname"]);
                    strArray[iRow, 2] = Convert.ToString(dt.Rows[iRow]["name"]);
                }

                Json = JsonConvert.SerializeObject(new DicPackage(true, strArray, null).DicInfo());
            }
            catch (Exception ex)
            {
                Json = JsonConvert.SerializeObject(new DicPackage(false, null, string.Format("{0}：获取数据发生异常。{1}", ex.Source, ex.Message)).DicInfo());
            }
        }
        protected string Json;
    }
}