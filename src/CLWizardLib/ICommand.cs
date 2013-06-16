using System.Collections.Generic;
using CLWizardLib.Params;

namespace CLWizardLib
{
    /// <summary>
    /// コマンド管理オブジェクトで実行するコマンドを表す
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// コマンドを実行する
        /// </summary>
        /// <param name="paramPresenter">引数提供オブジェクト</param>
        /// <param name="context">実行情報格納オブジェクト</param>
        void Execute(ParamPresenter paramPresenter, DataContext context);

        /// <summary>
        /// コマンドのロールバック処理を行う
        /// </summary>
        /// <param name="context">実行情報格納オブジェクト</param>
        void Rollback(DataContext context);
    }
}
