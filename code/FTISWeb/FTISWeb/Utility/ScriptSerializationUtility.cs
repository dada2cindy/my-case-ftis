using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace FTISWeb.Utility
{
    /// <summary>
    /// 用來封裝 JavaScriptSerializer
    /// </summary>
    public class ScriptSerializationUtility
    {
        /// <summary>
        /// The json converter.
        /// </summary>
        private static readonly JavaScriptSerializer m_JsonConverter = new JavaScriptSerializer();

        /// <summary>
        /// The get serialized query conditions.
        /// </summary>
        /// <param name="cdts">
        /// The cdts.
        /// </param>
        /// <returns>
        /// The get serialized query conditions.
        /// </returns>
        public static string GetSerializedQueryConditions(object cdts)
        {
            // condition
            PropertyInfo[] pI = cdts.GetType().GetProperties();
            IDictionary<string, string> conditions = new Dictionary<string, string>();

            foreach (PropertyInfo o in pI)
            {
                conditions.Add(o.Name, (o.GetValue(cdts, null) as string) ?? string.Empty);
            }

            return m_JsonConverter.Serialize(conditions);
        }

        /// <summary>
        /// 同GetSerializedQueryConditions，僅參數型態有差別
        /// </summary>
        /// <param name="cdts"></param>
        /// <returns></returns>
        public static string GetSerializedQueryConditionsByDict(IDictionary<string, string> cdts)
        {
            return m_JsonConverter.Serialize(cdts);
        }

        /// <summary>
        /// Deserialize query conditions
        /// </summary>
        /// <param name="condititions">
        /// The serialized string.
        /// </param>
        /// <returns>
        /// The deserialized dictionary object.
        /// </returns>
        public static IDictionary<string, string> DeserializeQueryConditions(string condititions)
        {
            return m_JsonConverter.Deserialize<IDictionary<string, string>>(condititions);
        }

        /// <summary>
        /// 以XmlSerializer將物件序列
        /// </summary>
        public static string SerializeObjectToXmlString(object obj)
        {
            XmlSerializer ser = new XmlSerializer(obj.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ser.Serialize(writer, obj);
            return sb.ToString();
        }

        /// <summary>
        /// 將xml String還原成物件
        /// </summary>
        public static T DeserializeXmlStringToObject<T>(string s)
        {
            XmlDocument xdoc = new XmlDocument();

            try
            {
                xdoc.LoadXml(s);
                XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                object obj = ser.Deserialize(reader);

                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 將物件序列化成Json
        /// </summary>
        public static string ObjectToJson(object obj)
        {
            return m_JsonConverter.Serialize(obj);
        }

        /// <summary>
        /// 將Json序列化成物件
        /// </summary>
        public static T JsonToObject<T>(string json)
        {
            // 純Dictionary只能用JavaScriptSerializer來處理
            if (typeof(T) == typeof(IDictionary<string, string>) || typeof(T) == typeof(Dictionary<string, string>))
            {
                return (T)DeserializeQueryConditions(json);
            }

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        /// <summary>
        /// Base64 解碼處理
        /// </summary>        
        public static T Base64ToObject<T>(string data)
        {
            return JsonToObject<T>(Encoding.UTF8.GetString(Convert.FromBase64String(data)));
        }

        /// <summary>
        /// 將Base64序列化成物件
        /// </summary>
        public static T Base64ToString<T>(string data)
        {
            string result = Encoding.UTF8.GetString(Convert.FromBase64String(data));
            return (T)Activator.CreateInstance(typeof(string), new object[] { result.ToCharArray() });
        }
    }
}