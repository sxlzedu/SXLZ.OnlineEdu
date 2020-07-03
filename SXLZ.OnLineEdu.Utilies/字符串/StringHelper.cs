using System;
using System.Collections.Generic;
using System.Text;

namespace SXLZ.OnLineEdu.Utilies
{
    public class StringHelper
    {
        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderCn()
        {
            string OrderNo = DateTime.Now.Year.ToString().Substring(2, 2) +
                DateTime.Now.Month.ToString().PadLeft(2, '0') +
                DateTime.Now.Day.ToString().PadLeft(2, '0') +
                DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                DateTime.Now.Second.ToString().PadLeft(2, '0') +
                DateTime.Now.ToString("fff");
            return OrderNo;
        }
    }
}
