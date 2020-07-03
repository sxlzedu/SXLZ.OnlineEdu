using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SXLZ.OnLineEdu.Utilies
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="FromMail">发件人邮箱</param>
        /// <param name="ToMail">收件人邮箱(多个收件人地址用";"号隔开)</param>
        /// <param name="AuthorizationCode">发件人授权码</param>
        /// <param name="ReplyTo">对方回复邮件时默认的接收地址（不设置也是可以的）</param>
        /// <param name="CCMial">//邮件的抄送者(多个抄送人用";"号隔开)</param>
        /// <param name="File_Path">附件的地址</param>
        public bool SendMail(string subject, string body, string FromMail,
            string ToMail,
            string AuthorizationCode, string ReplyTo, string CCMial,
            List<string> attachUrls,
            bool isHtmlBody = false)
        {
            try
            {
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage();

                //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
                mailMessage.Priority = MailPriority.Normal;

                //发件人邮箱地址。
                mailMessage.From = new MailAddress(FromMail);

                //收件人邮箱地址。需要群发就写多个
                //拆分邮箱地址
                List<string> ToMiallist = ToMail.Split(';').ToList();
                for (int i = 0; i < ToMiallist.Count; i++)
                {
                    mailMessage.To.Add(new MailAddress(ToMiallist[i]));  //收件人邮箱地址。
                }

                if (ReplyTo == "" || ReplyTo == null)
                {
                    ReplyTo = FromMail;
                }

                //对方回复邮件时默认的接收地址(不设置也是可以的哟)
                mailMessage.ReplyTo = new MailAddress(ReplyTo);

                if (!string.IsNullOrEmpty(CCMial))
                {
                    List<string> CCMiallist = CCMial.Split(';').ToList();
                    for (int i = 0; i < CCMiallist.Count; i++)
                    {
                        //邮件的抄送者，支持群发
                        mailMessage.CC.Add(new MailAddress(CCMiallist[i]));
                    }
                }
                //如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
                mailMessage.SubjectEncoding = Encoding.UTF8;

                //邮件正文是否是HTML格式
                mailMessage.IsBodyHtml = isHtmlBody;

                //邮件标题。
                mailMessage.Subject = subject;
                //邮件内容。
                mailMessage.Body = body;

                //设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中  
                if (attachUrls != null && attachUrls.Count > 0)
                {
                    foreach (var File_Path in attachUrls)
                    {
                        if (!string.IsNullOrEmpty(File_Path))
                        {
                            //将附件添加到邮件
                            mailMessage.Attachments.Add(new System.Net.Mail.Attachment(File_Path));
                            //获取或设置此电子邮件的发送通知。
                            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                        }
                    }
                }

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();

                #region 设置邮件服务器地址

                //在这里我使用的是163邮箱，所以是smtp.163.com，如果你使用的是qq邮箱，那么就是smtp.qq.com。
                // client.Host = "smtp.163.com";
                if (FromMail.Length != 0)
                {
                    //根据发件人的邮件地址判断发件服务器地址   默认端口一般是25
                    string[] addressor = FromMail.Trim().Split(new Char[] { '@', '.' });
                    switch (addressor[1])
                    {
                        case "163":
                            client.Host = "smtp.163.com";
                            break;
                        case "126":
                            client.Host = "smtp.126.com";
                            break;
                        case "qq":
                            client.Host = "smtp.qq.com";
                            client.Port = 587;
                            break;
                        case "gmail":
                            client.Host = "smtp.gmail.com";
                            break;
                        case "hotmail":
                            client.Host = "smtp.live.com";//outlook邮箱
                                                          //client.Port = 587;
                            break;
                        case "foxmail":
                            client.Host = "smtp.foxmail.com";
                            break;
                        case "sina":
                            client.Host = "smtp.sina.com.cn";
                            break;
                        default:
                            client.Host = "smtp.exmail.qq.com";//qq企业邮箱
                            break;
                    }
                }
                #endregion

                //使用安全加密连接。
                client.EnableSsl = true;
                //不和请求一块发送。
                client.UseDefaultCredentials = false;

                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential(FromMail, AuthorizationCode);

                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //发送
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                NLogger.Default.Error("", ex);
                return false;
            }
        }
    }
}
