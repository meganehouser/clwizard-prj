using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLWizardLib.Params
{
    /// <summary>
    /// バックグラウンド処理用の実行情報提供クラス
    /// </summary>
    public class BackgroundParamPresenter : ParamPresenter
    {
        /// <summary>
        /// 実行情報を1個取得する
        /// </summary>
        /// <param name="attr">実行情報に対応するCommandParameterAttributeオブジェクト</param>
        /// <param name="contextValue">実行情報のコンテキスト値</param>
        /// <param name="globalValue">実行情報の他コマンドからの連携値</param>
        public override string GetParam(CommandParameterAttribute attr, string contextValue, string globalValue)
        {
            Console.WriteLine(string.Format("{0}: {1}", attr.Description, contextValue));

            // コンテキスト値優先
            return contextValue;
        }
    }
}
