using System;
using System.Collections.Generic;

namespace CLWizardLib.Persistence
{
    /// <summary>
    /// 実行情報を永続化・復元する操作を表す
    /// </summary>
    public interface IPersister
    {
        /// <summary>
        /// 永続化する
        /// </summary>
        /// <param name="name">作成する環境名</param>
        /// <param name="objs">永続化する実行情報</param>
        void Serialize(string name, Dictionary<Type, IParam> objs);

        /// <summary>
        /// 復元する
        /// </summary>
        /// <param name="path">復元するファイルパス</param>
        /// <param name="context">復元した情報を格納する実行情報管理オブジェクト</param>
        void Deserialize(string path, DataContext context);
    }
}
