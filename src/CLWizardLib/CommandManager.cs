using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLWizardLib.Logging;
using CLWizardLib.Params;

namespace CLWizardLib
{
    /// <summary>
    /// 複数コマンドの実行・ロールバックを実行するクラス
    /// </summary>
    public class CommandManager : List<ICommand>
    {
        /// <summary>
        /// 現在処理インデックス
        /// </summary>
        private int currnetIndex;

        /// <summary>
        /// 実行情報保持オブジェクト
        /// </summary>
        private DataContext context;

        /// <summary>
        /// 作成する環境名
        /// </summary>
        private string name;

        /// <summary>
        /// コマンドの実行
        /// </summary>
        /// <param name="envName">作成する環境名</param>
        /// <param name="input">実行情報を格納したファイルのパス</param>
        public void Execute(ParamPresenter paramPresenter, string envName="", string input="")
        {
            this.name = envName;
            this.context = new DataContext();
            this.context.Global.Add("ENV_NAME", this.name);

            if (!string.IsNullOrEmpty(input))
            {
                this.context.Load(input);
            }

            for(this.currnetIndex = 0; this.currnetIndex < this.Count; this.currnetIndex++)
            {
                this[this.currnetIndex].Execute(paramPresenter, context);
                Console.WriteLine("--------------------------------");
            }
        }

        /// <summary>
        /// コマンド実行情報の保存を行う
        /// </summary>
        public void Save()
        {
            // 実行情報を永続化する
            this.context.Serialize(this.name);
        }

        /// <summary>
        /// ロールバック処理を行う
        /// </summary>
        public void Rollback()
        {
            foreach (var i in Enumerable.Range(0, this.currnetIndex).Reverse())
            {
                try
                {
                    this[i].Rollback(context);
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), "Rollback Error", ex);
                }
            }

            this.currnetIndex = 0;
        }

        /// <summary>
        /// すべてのコマンドのロールバック処理を行う
        /// </summary>
        public void RollbackAll(string input)
        {
            this.context = new DataContext();
            this.context.Load(input);

            this.currnetIndex = this.Count;
            Rollback();
            this.currnetIndex = 0;
        }
    }
}
