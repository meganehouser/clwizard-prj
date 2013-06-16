using System;
using System.Collections.Generic;
using CommandLine;
using CLWizardLib.Params;
using CLWizardLib.Commands;
using CLWizardLib.Logging;

namespace CLWizardLib
{
    public class Program
    {
        /// <summary>
        /// テスト環境作成を実行する
        /// </summary>
        /// <param name="commands">コマンドのリスト</param>
        /// <param name="options">コマンドラインオプション</param>
        public void Execute(IEnumerable<ICommand> commands, Options options)
        {
            Logger.Info(this.GetType(), "Execute Commands Start");

            var manager = new CommandManager();
            manager.AddRange(commands);

            if (options.IsDeleteMode)
            {
                // 実行モード：削除
                manager.RollbackAll(options.Input);
                return;
            }

            try
            {
                if (options.IsSilentMode)
                {
                    // 実行モード：作成（非対話方式）
                    manager.Execute(new BackgroundParamPresenter(), input: options.Input);
                }
                else
                {
                    // 実行モード：作成（対話方式）
                    manager.Execute(new ConsoleParamPresenter(), envName: options.EnvName, input: options.Input);
                    manager.Save();
                }

                Logger.Info(this.GetType(), "Complete to execute commands.");
            }
            catch (Exception ex)
            {
                Logger.Error(this.GetType(), "throw..", ex);
                manager.Rollback();
            }
        }

        /// <summary>
        /// エントリーポイント
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        static void Main(string[] args)
        {
            Options options;
            if (!Options.ParseArguments(args, out options))
            {
                return;
            }

            var main = new Program();
            var commands = new List<ICommand> 
            {
                new SampleCommand()
            };

            main.Execute(commands, options);
        }
    }
}
