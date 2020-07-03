using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SXLZ.OnLineEdu.Utilies
{
    public class XmlHelper
    {
        public static List<string> XmlCommentsFilePath
        {
            get
            {
                //var basePath = Path.GetDirectoryName(Directory.GetCurrentDirectory());//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var basePath = Path.GetDirectoryName(typeof(XmlHelper).Assembly.Location);
                DirectoryInfo d = new DirectoryInfo(basePath);
                FileInfo[] files = d.GetFiles("*.xml");
                var xmls = files.Select(a => Path.Combine(basePath, a.FullName)).ToList();
                return xmls;
            }
        }
    }
}
