using System;
using System.Text;
using System.Reflection;
using CommandLine;
using CommandLine.Text;

namespace CLWizardLib
{
    /// <summary>
    /// コマンドラインオプション
    /// </summary>
    public class Options
    {
        /// <summary>
        /// 作成する環境名
        /// </summary>
        [ValueOption(0)]
        public string EnvName { get; set; }

        /// <summary>
        /// 入力Jsonファイルパス
        /// </summary>
        [Option('I', "input", Required = false, HelpText = "Input file")]
        public string Input { get; set; }

        /// <summary>
        /// 実行モード（削除）
        /// </summary>
        [Option('D', "delete", Required = false, HelpText = "Rollback commands environment")]
        public bool IsDeleteMode { get; set; }

        /// <summary>
        /// 実行モード（非対話方式で作成）
        /// </summary>
        [Option('S', "silent", Required = false, HelpText = "Execute commands in silence.")]
        public bool IsSilentMode { get; set; }

        /// <summary>
        /// 使用方法を取得する
        /// </summary>
        [HelpOption]
        public string GetUsage()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            var help = new HelpText
            {
                Heading = new HeadingInfo("CLWizardLib", version.ToString()),
                Copyright = new CopyrightInfo("meganehouser.", 2013),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            help.AddOptions(this);
            return help;
        }

        /// <summary>
        /// 文字列の配列をCLWizardLib.Optionsに変換する
        /// </summary>
        /// <param name="args">コマンドライン引数の配列</param>
        /// <param name="options">(出力)コマンドラインオプションオブジェクト</param>
        /// <returns>成否</returns>
        public static bool ParseArguments(string[] args, out Options options)
        {
            options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                return false;
            }

            if (options.IsDeleteMode && options.IsSilentMode)
            {
                Console.WriteLine(options.GetUsage());
                return false;
            }

            if (options.IsDeleteMode)
            {
                // 実行モード：削除
                if (!string.IsNullOrWhiteSpace(options.Input))
                    return true;
            }
            else if (options.IsSilentMode)
            {
                // 実行モード：作成（非対話方式）
                if (!string.IsNullOrWhiteSpace(options.Input))
                    return true;
            }
            else
            {
                // 実行モード：作成（対話方式）
                if (!string.IsNullOrWhiteSpace(options.EnvName))
                    return true;
            }

            Console.WriteLine(options.GetUsage());
            return false;
        }
    }
}
