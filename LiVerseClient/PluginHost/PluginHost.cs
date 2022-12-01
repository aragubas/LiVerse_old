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
        /// <param name="pluginPathName">Plugin folder name (usually the plugin id) inside the Plugins directory</param>
        /// <returns>PluginMetadata object with values loaded from plugin's metadata file</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static PluginMetadata LoadPluginMetadata(string pluginPathName)
        {
            string pluginMetadataPath = Path.Combine(Environment.CurrentDirectory, "Plugins", pluginPathName, "plugin_metadata.json");

            if (!File.Exists(pluginMetadataPath))
            {
                throw new FileNotFoundException($"Could not find plugin metadata file. Path '{pluginPathName}'");
            }

            return JsonConvert.DeserializeObject<PluginMetadata>(File.ReadAllText(pluginMetadataPath));
        }

        /// <summary>
        /// Loads plugin assembly and add plugin into the Plugins instance list
        /// </summary>
        /// <param name="clientInstance">An instance of IClient</param>
        /// <param name="pluginPathName">Plugin folder name (usually the plugin id) inside the Plugins directory</param>
        /// <param name="pluginMetadata">Metadata information about the plugin</param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="FileLoadException"></exception>
        /// <exception cref="Exception"></exception>
        public static void LoadPluginAssembly(IClientInstance clientInstance, string pluginPathName, PluginMetadata pluginMetadata)
        {
            string fullAssemblyPath = Path.Combine(Environment.CurrentDirectory, "Plugins", pluginPathName, pluginMetadata.AssemblyPath);

            if (!File.Exists(fullAssemblyPath))
            {
                throw new FileNotFoundException($"Could not find plugin's assembly. Assembly path: '{fullAssemblyPath}'");
            }

            Assembly assembly = Assembly.LoadFile(fullAssemblyPath);
            TypeInfo type = assembly.DefinedTypes.Where(ceira => ceira.Name == "PluginMain").FirstOrDefault();

            if (type == null)
            {
                throw new FileLoadException($"Could not find Plugin main class. for a plugin to be valid it needs to have a class implementing the interface '{nameof(IPlugin)}' and be named exactly 'PluginMain'");
            }

            // Check if type is valid
            if (!type.GetInterfaces().Contains(typeof(IPlugin)))
            {
                throw new Exception($"Invalid Plugin, plugin doesn't implement '{nameof(IPlugin)}'");
            }


            object instance = Activator.CreateInstance(type);

            if (instance == null)
            {
                throw new Exception($"Could not activate plugin. Activator returned null object");
            }

            ((IPlugin)instance).Initialise(clientInstance);

            // Adds plugin instance to Plugins list
            Plugins.Add((IPlugin)instance);
        }

        /// <summary>
        /// Loads a plugin inside the plugins directory<br></br>This methods calls <seealso cref="LoadPluginMetadata(string)"/>, <seealso cref="LoadPluginAssembly"/> and adds the plugin instance to <seealso cref="Plugins"/>
        /// </summary>
        /// <param name="PluginPathName">Plugin folder name (usually the plugin id) inside the Plugins directory</param>
        /// <exception cref="FileNotFoundException"></exception>
        public static void LoadPlugin(IClientInstance clientInstance, string pluginPathName)
        {
            PluginMetadata pluginMetadata = LoadPluginMetadata(pluginPathName);

            LoadPluginAssembly(clientInstance, pluginPathName, pluginMetadata);

            Console.WriteLine("Done!");
        }

    }
}
