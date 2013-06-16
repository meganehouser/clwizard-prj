using System;
using System.Reflection;

namespace CLWizardLib.Params
{
    /// <summary>
    /// 実行情報を取得する
    /// </summary>
    public abstract class ParamPresenter
    {
        /// <summary>
        /// 実行情報を取得する
        /// </summary>
        /// <typeparam name="T">実行情報の型</typeparam>
        /// <param name="context">実行情報管理オブジェクト</param>
        public T Get<T>(DataContext context) where T : IParam, new()
        {
            var targetObject = new T();

            var type = targetObject.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                // パラメータ定義属性を取得する
                var attr = CommandParameterAttribute.Get(property);
                if (attr == null)
                {
                    continue;
                }

                // グローバル値取得
                var globalValue = context.GetGlobalValue(attr.GlobalKey);
                var paramValue = "";
                if (string.IsNullOrEmpty(globalValue))
                {
                    // コンテキスト値取得
                    var contextValue = context.GetContextValue(type, property.Name);

                    // パラメータを取得
                    paramValue = GetParam(attr, contextValue, globalValue);
                }
                else
                {
                    paramValue = globalValue;
                }

                // グローバルに値をセット
                if (!string.IsNullOrEmpty(attr.GlobalKey))
                {
                    context.Global[attr.GlobalKey] = paramValue;
                }

                // プロパティに値をセット
                property.SetValue(targetObject, paramValue, null);
            }

                // コンテキストに値をセット
                context[targetObject.GetType()] = targetObject;


            return targetObject;
        }

        /// <summary>
        /// 実行情報を1個取得する
        /// </summary>
        /// <param name="attr">実行情報に対応するCommandParameterAttributeオブジェクト</param>
        /// <param name="contextValue">実行情報のコンテキスト値</param>
        /// <param name="globalValue">実行情報の他コマンドからの連携値</param>
        public abstract string GetParam(CommandParameterAttribute attr, string contextValue, string globalValue);
    }
}
