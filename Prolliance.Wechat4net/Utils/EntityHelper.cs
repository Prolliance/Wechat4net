using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Wechat4net.Define;

namespace Wechat4net.Utils
{
    public static class EntityHelper
    {
        /// <summary>
        /// 根据XML信息填充实实体
        /// </summary>
        /// <typeparam name="T">ReceiveMessageBase为基类的类型</typeparam>
        /// <param name="entity">对象实体</param>
        /// <param name="xml">XML</param>
        public static void FillReceiveMessageFromXml<T>(this T entity, XmlDocument xml) where T : class, new()
        {
            entity = entity ?? new T();
            var props = entity.GetType().GetProperties();
            XmlNode root = xml.FirstChild;
            foreach (var prop in props)
            {
                if (!prop.CanWrite)
                {
                    continue;
                }
                var propName = prop.Name;
                object[] Attributes = prop.GetCustomAttributes(true);
                foreach (var Attribute in Attributes)
                {
                    if (Attribute.GetType() == typeof(XmlPropertyAttribute))
                    {
                        if (!string.IsNullOrEmpty(((XmlPropertyAttribute)Attribute).XmlProperty))
                        {
                            propName = ((XmlPropertyAttribute)Attribute).XmlProperty;
                        }
                    }
                }
                var list = propName.Split('.');
                XmlNode temp = root;
                for (int i = 0; i < list.Length; i++)
                {
                    temp = temp[list[i]];
                    if (temp == null)
                    {
                        break;
                    }
                }


                //if (root[propName] != null)
                if (temp != null)
                {
                    switch (prop.PropertyType.Name)
                    {
                        //case "String":
                        //    prop.SetValue(t, temp.InnerText, null);
                        //    break;
                        case "DateTime":
                            prop.SetValue(entity, DateTimeConverter.GetDateTimeFromXml(temp.InnerText), null);
                            break;
                        case "Boolean":
                            if (propName == "FuncFlag")
                            {
                                prop.SetValue(entity, temp.InnerText == "1", null);
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        case "Int32":
                            prop.SetValue(entity, int.Parse(temp.InnerText), null);
                            break;
                        case "Int64":
                            prop.SetValue(entity, long.Parse(temp.InnerText), null);
                            break;
                        case "Double":
                            prop.SetValue(entity, double.Parse(temp.InnerText), null);
                            break;
                        default:
                            prop.SetValue(entity, temp.InnerText, null);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 将对象转换为XML
        /// </summary>
        /// <typeparam name="T">ReplyMessageBase为基类的类型</typeparam>
        /// <param name="entity">对象实体</param>
        /// <returns>XML</returns>
        public static XDocument BuidReplyMessageXml<T>(this T entity) where T : class , new()
        {
            var doc = new XDocument();
            doc.Add(new XElement("xml"));
            var root = doc.Root;

            /* 注意！
             * 经过测试，微信对字段排序有严格要求，这里对排序进行强制约束
             */
            //Func<string, int> orderByPropName = propNameOrder.IndexOf;
            Func<PropertyInfo, int> orderByPropName = delegate(PropertyInfo p)
            {
                object[] Attributes = p.GetCustomAttributes(true);
                foreach (var Attribute in Attributes)
                {
                    if (Attribute.GetType() == typeof(XmlPropertyAttribute))
                    {
                        return ((XmlPropertyAttribute)Attribute).Order;
                    }
                }
                return 0;
            };

            //var props = entity.GetType().GetProperties().OrderBy(p => orderByPropName(p.Name)).ToList();
            var props = entity.GetType().GetProperties().OrderBy(p => orderByPropName(p)).ToList();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                object[] Attributes = prop.GetCustomAttributes(true);
                foreach (var Attribute in Attributes)
                {
                    if (Attribute.GetType() == typeof(XmlPropertyAttribute))
                    {
                        if (!string.IsNullOrEmpty(((XmlPropertyAttribute)Attribute).XmlProperty))
                        {
                            propName = ((XmlPropertyAttribute)Attribute).XmlProperty;
                        }
                    }
                }
                var XmlPropertyList = propName.Split('.');
                XElement tempElement = root;
                for (int i = 0; i < XmlPropertyList.Length - 1; i++)
                {
                    if (!tempElement.HasElements || tempElement.Element(XmlPropertyList[i]) == null)
                    {
                        tempElement.Add(new XElement(XmlPropertyList[i]));
                    }
                    tempElement = tempElement.Element(XmlPropertyList[i]);
                }

                switch (prop.PropertyType.Name)
                {
                    case "String":
                        tempElement.Add(new XElement(XmlPropertyList[XmlPropertyList.Length - 1], new XCData(prop.GetValue(entity, null) as string ?? "")));
                        break;
                    case "DateTime":
                        tempElement.Add(new XElement(
                            XmlPropertyList[XmlPropertyList.Length - 1],
                            DateTimeConverter.GetWeixinDateTime(
                                                  (DateTime)prop.GetValue(entity, null))));
                        break;
                    case "Boolean":
                        if (propName == "FuncFlag")
                        {
                            tempElement.Add(new XElement(
                                XmlPropertyList[XmlPropertyList.Length - 1],
                                (bool)prop.GetValue(entity, null) ? "1" : "0"));
                        }
                        else
                        {
                            goto default;
                        }
                        break;
                    case "List`1":
                        var subEntitys = prop.GetValue(entity, null) as IEnumerable<object>;
                        var atriclesElement = new XElement(XmlPropertyList[XmlPropertyList.Length - 1]);
                        foreach (var subEntity in subEntitys)
                        {
                            var subNodes = BuidReplyMessageXml(subEntity).Root.Elements();
                            atriclesElement.Add(new XElement("item", subNodes));
                        }
                        tempElement.Add(atriclesElement);
                        break;
                    //case "ResponseMsgType":
                    //    root.Add(new XElement(propName, new XCData(prop.GetValue(entity, null).ToString().ToLower())));
                    //    break;
                    //case "Article":
                    //    root.Add(new XElement(propName, prop.GetValue(entity, null).ToString().ToLower()));
                    //    break;
                    //case "TransInfo":
                    //    root.Add(new XElement(propName, prop.GetValue(entity, null).ToString().ToLower()));
                    //    break;
                    default:
                        if (prop.PropertyType.IsClass && prop.PropertyType.IsPublic)
                        {
                            //自动处理其他实体属性
                            var subEntity = prop.GetValue(entity, null);
                            var subNodes = BuidReplyMessageXml(subEntity).Root.Elements();
                            tempElement.Add(new XElement(XmlPropertyList[XmlPropertyList.Length - 1], subNodes));
                        }
                        else
                        {
                            tempElement.Add(new XElement(XmlPropertyList[XmlPropertyList.Length - 1], prop.GetValue(entity, null)));

                        }
                        break;
                }

            }


            return doc;
        }

        /// <summary>
        /// 将对象转换为XML字符串
        /// </summary>
        /// <typeparam name="T">ReplyMessageBase为基类的类型</typeparam>
        /// <param name="entity">对象实体</param>
        /// <returns>XML字符串</returns>
        public static string BuidReplyMessageXmlString<T>(this T entity) where T : class , new()
        {
            return entity.BuidReplyMessageXml().ToString();
        }
    }
}
