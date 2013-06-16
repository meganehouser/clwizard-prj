using System;
using System.Collections.Generic;
using CLWizardLib.Persistence;

namespace CLWizardLib
{
    /// <summary>
    /// コマンドの実行情報を管理するクラス
    /// </summary>
    public class DataContext : Dictionary<Type, IParam>
    {
        /// <summary>
        /// 複数コマンド間の連携情報を管理する
        /// </summary>
        public Dictionary<string,string> Global { get; set; }
        
        /// <summary>
        /// 実行情報の永続化を行うオブジェクト
        /// </summary>
        public IPersister Persister { get; set; }

        /// <summary>
        /// DataContextの新しいインスタンスを生成する
        /// </summary>
        public DataContext()
        {
            this.Global = new Dictionary<string, string>();
            this.Persister = new JsonPersister();
        }

        /// <summary>
        /// 指定したパスのファイルから実行情報を読み込む
        /// </summary>
        /// <param name="path">ファイルパス</param>
        public void Load(string path)
        {
            this.Persister.Deserialize(path, this);
        }

        /// <summary>
        /// 複数コマンド間の連携情報を取得する
        /// </summary>
        /// <param name="key">キー項目</param>
        public string GetGlobalValue(string key)
        {
            string defaultValue;
            if (this.Global.TryGetValue(key, out defaultValue))
            {
                return defaultValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// 実行情報を取得する
        /// </summary>
        /// <param name="type">実行情報の型</param>
        /// <param name="key">キー項目</param>
        public string GetContextValue(Type type, string key)
        {
            IParam param;
            if (!this.TryGetValue(type, out param))
            {
                return string.Empty;
            }

            var prop = type.GetProperty(key);
            if (prop == null)
            {
                return string.Empty;
            }

            var obj = prop.GetValue(param, null);

            if (obj == null)
            {
                return string.Empty;
            }

            return (string)obj;
        }

        /// <summary>
        /// 実行情報を永続化する
        /// </summary>
        /// <param name="name">テスト環境名</param>
        public void Serialize(string name)
        {
            this.Persister.Serialize(name, this);
        }
    }
}
