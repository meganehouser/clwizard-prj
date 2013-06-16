using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CLWizardLib.Params
{
    /// <summary>
    /// コマンド実行情報であることを示す
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandParameterAttribute : Attribute
    {
        /// <summary>
        /// デフォルト値
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// 複数コマンド間に連携する場合のキー名
        /// </summary>
        public string GlobalKey { get; set; }
        
        /// <summary>
        /// 実行情報の説明
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// CLWizardLib.Params.CommandParameterAttributeの新しいインスタンスを生成する
        /// </summary>
        public CommandParameterAttribute()
        {
            this.GlobalKey = "";
            this.Default = "";
            this.Description = "";
        }

        /// <summary>
        /// プロパティに付加されているCommandParameterAttributeを取得する
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CommandParameterAttribute Get(MemberInfo info)
        {
            var attr = Attribute.GetCustomAttributes(info, typeof(CommandParameterAttribute));
            if (attr == null)
            {
                return null;
            }

            return attr.First() as CommandParameterAttribute;
        }
    }
}
