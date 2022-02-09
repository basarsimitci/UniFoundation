using JoyfulWorks.UniFoundation.Files;
using Newtonsoft.Json;
using System;

namespace JoyfulWorks.UniFoundation.Config
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class JsonConfig
    {
        public static T LoadConfig<T>(string configFileName)
        {
            string configFileContent;
            try
            {
                configFileContent = PersistentData.ReadText(configFileName);
            }
            catch
            {
                try
                {
                    configFileContent = AppData.ReadText(configFileName);
                    
                    // Copy file to persistent data so that it's contents can be changed without redeploying the application.
                    PersistentData.WriteText(configFileName, configFileContent);
                }
                catch
                {
                    throw new Exception($"{configFileName} cannot be found in persistent data folder or app bundle.");
                }
            }

            return JsonConvert.DeserializeObject<T>(configFileContent);
        }
    }
}