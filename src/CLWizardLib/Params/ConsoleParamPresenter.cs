using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLWizardLib.Params
{
    /// <summary>
    /// コンソールで対話式に実行情報を入力する実行情報提供クラス
    /// </summary>
    public class ConsoleParamPresenter : ParamPresenter
    {
        /// <summary>
        /// 実行情報を1個取得する
        /// </summary>
        /// <param name="attr">実行情報に対応するCommandParameterAttributeオブジェクト</param>
        /// <param name="contextValue">実行情報のコンテキスト値</param>
        /// <param name="globalValue">実行情報の他コマンドからの連携値</param>
        public override string GetParam(CommandParameterAttribute attr, string contextValue, string globalValue)
        {
            var defaultValue = GetDefalutValue(contextValue, globalValue, attr);
            var inputRequest = string.Format("{0} [{1}]: ", attr.Description, defaultValue);
            
            // コンソールから入力値を取得
            Console.Write(inputRequest);
            var inputValue = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputValue))
            {
                return defaultValue;
            }

            return inputValue;
        }

        /// <summary>
        /// 実行情報のデフォルト値を取得する
        /// </summary>
        /// <param name="contextValue">コンテキスト値</param>
        /// <param name="globalValue">他コマンドからの連携値</param>
        /// <param name="attr">実行情報を示す属性</param>
        private static string GetDefalutValue(string contextValue, string globalValue, CommandParameterAttribute attr)
        {
            if (!string.IsNullOrEmpty(globalValue))
            {
                return globalValue;
            }

            if (!string.IsNullOrEmpty(contextValue))
            {
                return contextValue;
            }

            return attr.Default;
        }
    }
}
