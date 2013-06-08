using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using FTIS;
using FTIS.Domain.Impl;
using FTISWeb.Models;
using WuDada.Core.Generic.Mail;

namespace FTISWeb.Service
{
    public class WebMailService
    {
        private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private MailService m_MailService;
        private FTISFactory m_FTISFactory;
        private SystemParamVO m_MailVO;

        public WebMailService()
        {
            m_FTISFactory = new FTISFactory();
            m_MailVO = m_FTISFactory.GetSystemParam();

            bool enableSSL = m_MailVO.EnableSSL;
            int port = 25;

            if (m_MailVO.MailSmtp.IndexOf("gmail") != -1)
            {
                enableSSL = true;
                port = 587;
            }
            else if (!string.IsNullOrEmpty(m_MailVO.MailPort))
            {
                port = int.Parse(m_MailVO.MailPort);
            }

            m_MailService = new MailService(m_MailVO.MailSmtp, port, enableSSL, m_MailVO.Account, m_MailVO.Password);
        }

        /// <summary>
        /// 寄信
        /// </summary>
        /// <param name="mailEntity"></param>
        public void SendMail(SendMailModel mailEntity)
        {
            try
            {
                string sendMailFrom = string.IsNullOrEmpty(mailEntity.SendMailFrom) ? m_MailVO.SendEmail : mailEntity.SendMailFrom;
                m_MailService.SendMail(sendMailFrom, mailEntity.SendMailTo, mailEntity.SendMailTitle, mailEntity.SendMailContent);
            }
            catch (Exception ex)
            {
                m_Log.Error(ex);
            }
        }
    }
}