using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.Define
{
    /// <summary>
    /// XML属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class XmlPropertyAttribute : Attribute
    {
        /// <summary>
        /// 对应的XML字段名
        /// <para>支持多级解析，如：ParentNode.ChildNode</para>
        /// </summary>
        public string XmlProperty { set; get; }

        /// <summary>
        /// 实体转XML时的字段排序顺序
        /// </summary>
        public int Order { set; get; }

        /// <summary>
        /// 构建XML属性
        /// </summary>
        /// <param name="xmlProperty">对应的XML字段名，支持多级解析，如：ParentNode.ChildNode</param>
        public XmlPropertyAttribute(string xmlProperty)
        {
            this.XmlProperty = xmlProperty; ;
        }

        /// <summary>
        /// 构建XML属性
        /// </summary>
        /// <param name="order">实体转XML时的字段排序顺序</param>
        public XmlPropertyAttribute(int order)
        {
            this.Order = order;
        }

        /// <summary>
        /// 构建XML属性
        /// </summary>
        /// <param name="xmlProperty">对应的XML字段名，支持多级解析，如：ParentNode.ChildNode</param>
        /// <param name="order">实体转XML时的字段排序顺序</param>
        public XmlPropertyAttribute(string xmlProperty, int order)
        {
            this.XmlProperty = xmlProperty;
            this.Order = order;
        }

    }
}
