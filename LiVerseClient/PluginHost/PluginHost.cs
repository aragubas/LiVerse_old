using LiVerseFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseClient.PluginHost
{
    public static class InstanceManager
    {
        /// <summary>
        /// List of all loaded plugins instances
        /// </summary>
        public static List<IPlugin> Plugins = new List<IPlugin>();

        /// <summary>
        /// Loads metadata about a plugin
        /// </summary>
        /// <param name="PluginPathName">Plugin folder name (usually the plugin id) inside the Plugins directory</param>
        /// <returns>PluginMetadata object with values loaded from plugin's metadata file</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static PluginMetadata LoadPluginMetadata(string PluginPathName)
        {
            string pluginMetadataPath = Path.Combine(Environment.CurrentDirectory, "Plugins", PluginPathName, "plugin_metadata.json");

            if (!File.Exists(pluginMetadataPath))
            {
                throw new FileNotFoundException($"Could not find plugin metadata file. Path '{PluginPathName}'");
            }

            return JsonConvert.DeserializeObject<PluginMetadata>(File.ReadAllText(pluginMetadataPath));
        }

        public static void LoadPluginAssembly()
        {

        }

        /// <summary>
        /// Loads a plugin inside the plugins directory<br></br>This methods calls <seealso cref="LoadPluginMetadata(string)"/>, <seealso cref="LoadPluginAssembly"/> and adds the plugin instance to <seealso cref="Plugins"/>
        /// </summary>
        /// <param name="PluginPathName">Plugin folder name (usually the plugin id) inside the Plugins directory</param>
        /// <exception cref="FileNotFoundException"></exception>
        public static void LoadPlugin(string PluginPathName)
        {
            PluginMetadata pluginMetadata = LoadPluginMetadata(PluginPathName);
            string fullAssemblyPath = Path.Combine(Environment.CurrentDirectory, "Plugins", PluginPathName, pluginMetadata.AssemblyPath);

            if (!File.Exists(fullAssemblyPath))
            {
                throw new FileNotFoundException($"Could not find plugin's assembly. Assembly path: '{fullAssemblyPath}'");
            }

            Assembly assembly = Assembly.LoadFrom(fullAssemblyPath);
            var type = assembly.DefinedTypes.Where(ceira => ceira.Name == "PluginMain").FirstOrDefault();
            
            var instance = Activator.CreateInstance(type);

            Console.WriteLine("Done!");
        }

    }
}
