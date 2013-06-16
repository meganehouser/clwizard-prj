using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CLWizardLib.Persistence
{
    /// <summary>
    /// Json形式で実行情報を永続化・復元する
    /// </summary>
    public class JsonPersister : IPersister
    {
        private const string JSON_EXTENSION = ".json";

        /// <summary>
        /// Json形式で実行情報を永続化する
        /// </summary>
        /// <param name="name">作成する環境名</param>
        /// <param name="objs">永続化する実行情報</param>
        public void Serialize(string name, Dictionary<Type, IParam> objs)
        {
            var str = JsonConvert.SerializeObject(objs, Formatting.Indented);

            var path = Path.Combine(Environment.CurrentDirectory, name + JSON_EXTENSION);

            File.WriteAllText(path, str);
        }

        /// <summary>
        /// 実行情報をJsonファイルから復元する
        /// </summary>
        /// <param name="path">復元するファイルパス</param>
        /// <param name="context">復元した情報を格納する実行情報管理オブジェクト</param>
        public void Deserialize(string path, DataContext context)
        {
            var json = File.ReadAllText(path);
            var strDic = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

            foreach (string key in strDic.Keys)
            {
                var type = Type.GetType(key);
                var instance = Activator.CreateInstance(type);

                foreach (var prop in type.GetProperties())
                {
                    var oneObjectDic = strDic[key];
                    if (!oneObjectDic.ContainsKey(prop.Name))
                        continue;

                    var value = oneObjectDic[prop.Name];
                    prop.SetValue(instance, value, null);
                }

                var param = instance as IParam;
                if (param != null)
                {
                    context.Add(param.GetType(), param);
                }
            }
        }
    }
}
