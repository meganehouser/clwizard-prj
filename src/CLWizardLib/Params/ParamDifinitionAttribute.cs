using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace OnBaseEnv.Params
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParamDifinitionAttribute : Attribute
    {
        public string Default { get; set; }
        public string GlobalKey { get; set; }
        public string Description { get; set; }

        public ParamDifinitionAttribute()
        {
            this.GlobalKey = "";
            this.Default = "";
            this.Description = "";
        }

        public static ParamDifinitionAttribute Get(MemberInfo info)
        {
            var attr = Attribute.GetCustomAttributes(info, typeof(ParamDifinitionAttribute));
            if (attr == null)
            {
                return null;
            }

            return attr.First() as ParamDifinitionAttribute;
        }
    }
}
