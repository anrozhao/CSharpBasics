using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTest
{
    /// <summary>
    /// AttributeUsage 允许特性标识的对象
    /// </summary>
    [AttributeUsage( AttributeTargets.Class| AttributeTargets.Delegate),Obsolete()]
   public class MyAttribute : Attribute
    {
        public MyAttribute()
        {

        }

        /// <summary>
        /// 定义有参构造函数
        /// </summary>
        /// <param name="Name"></param>
        public MyAttribute(string Name)
        {

        }
        
    }
}
