using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using System.Net.Mail;

namespace WuDada.Core.Generic.Mail
{
    public class MailService
    {
        private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string m_SmtpHost;
        private int m_Port = 25;
        private bool m_EnableSSL = true;
        private string m_CredentialsID;
        private string m_CredentialsPW;

        /// <summary>
        /// 用於多個mail分開
        /// </summary>
        private static readonly char m_Split = ';';


        public MailService(string smtpHost)
        {
            this.m_SmtpHost = smtpHost;
        }

        public MailService(string smtpHost, int port)
        {
            this.m_SmtpHost = smtpHost;
            this.m_Port = port;
        }

        public MailService(string smtpHost, int port, bool enableSsl, string credentialsID, string credentialsPW)
        {
            this.m_SmtpHost = smtpHost;
            this.m_Port = port;
            this.m_EnableSSL = enableSsl;
            this.m_CredentialsID = credentialsID;
            this.m_CredentialsPW = credentialsPW;
        }

        public void SendMail(MailAddress from, MailAddress to, string subject, string body)
        {
            List<MailAddress> mailAddressList = new List<MailAddress>();
            mailAddressList.Add(to);
            SendMail(from, to, subject, body);
        }


        public void SendMail(string from, string to, string subject, string body)
        {
            SendMail(from, to, subject, body, null, null);
        }

        public void SendMail(string from, string to, string subject, string body, string bcc)
        {
            MailAddress mailfrom = new MailAddress(from);

            List<MailAddress> mailAddressList = PutMailAddress(to);
            List<MailAddress> mailBccAddressList = PutMailAddress(bcc);

            SendMail(mailfrom, mailAddressList, subject, body, mailBccAddressList, null);
        }

        public void SendMail(string from, string to, string subject, string body, string bcc, List<Attachment> fileList)
        {
            MailAddress mailfrom = new MailAddress(from);

            List<MailAddress> mailAddressList = PutMailAddress(to);
            List<MailAddress> mailBccAddressList = PutMailAddress(bcc);

            SendMail(mailfrom, mailAddressList, subject, body, mailBccAddressList, fileList);
        }

        public void SendMail(MailAddress from, List<MailAddress> to, string subject, string body)
        {
            SendMail(from, to, subject, body, null, null);
        }

        public void SendMail(MailAddress from, IList<MailAddress> to, string subject, string body, IList<MailAddress> bcc, List<Attachment> fileList)
        {

            MailMessage em = new MailMessage();
            em.From = from;

            if (to != null && to.Count > 0)
            {
                foreach (MailAddress mail in to)
                {
                    em.To.Add(mail);
                }
            }

            if (bcc != null && bcc.Count > 0)
            {
                foreach (MailAddress mail in bcc)
                {
                    em.Bcc.Add(mail);
                }
            }

            //附檔
            if (fileList != null && fileList.Count > 0)
            {
                foreach (Attachment file in fileList)
                {
                    em.Attachments.Add(file);
                }
            }

            em.SubjectEncoding = System.Text.Encoding.UTF8;
            em.BodyEncoding = System.Text.Encoding.UTF8;

            //信件主題 
            em.Subject = subject;
            //內容 
            em.Body = body;
            em.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            //登入帳號認證  
            if (!string.IsNullOrEmpty(m_CredentialsID) && !string.IsNullOrEmpty(m_CredentialsPW))
            {
                client.Credentials = new System.Net.NetworkCredential(m_CredentialsID, m_CredentialsPW);
            }

            //使用587 Port 
            client.Port = m_Port;
            client.Host = m_SmtpHost;
            //啟動SSL 
            client.EnableSsl = m_EnableSSL;
            //寄出 
            client.Send(em);
        }


        private List<MailAddress> PutMailAddress(string mailStr)
        {
            List<MailAddress> MailList = new List<MailAddress>();

            if (!string.IsNullOrEmpty(mailStr))
            {
                mailStr = mailStr.Trim();

                if (mailStr.IndexOf(m_Split) != -1)
                {
                    foreach (string mail in mailStr.Split(m_Split))
                    {
                        if (!string.IsNullOrEmpty(mail))
                        {
                            MailList.Add(new MailAddress(mail));
                        }
                    }
                }
                else
                {
                    m_Log.Debug("mail:" + mailStr);
                    MailList.Add(new MailAddress(mailStr));
                }

                return MailList;
            }
            else
            {
                return null;
            }
        }
    }
}
