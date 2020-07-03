using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SXLZ.OnLineEdu.Utilies
{
    public static class UrlHelper
    {
        /// <summary>
        /// 获取绝对地址 http://localhost:5000
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetRequestUrl(this HttpRequest request)
        {
            return new StringBuilder()
             .Append(request.Scheme)
             .Append("://")
             .Append(request.Host)
             .ToString();
        }
        /// <summary>
        /// 获取绝对地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetAbsoluteUri(this HttpRequest request)
        {
            return new StringBuilder()
             .Append(request.Scheme)
             .Append("://")
             .Append(request.Host)
             .Append(request.PathBase)
             .Append(request.Path)
             .Append(request.QueryString)
             .ToString();
        }
        #region 获得当前访问的URL地址
        /// <summary>
        /// 获得当前访问的URL地址/api/declare/savedetail
        /// </summary>
        /// <returns>字符串数组</returns>
        public static string GetUrl(this HttpRequest request)
        {
            return request.Path.ToString();
        }
        #endregion

    }
}
