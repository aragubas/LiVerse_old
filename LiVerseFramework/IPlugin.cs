using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework
{
    public interface IPlugin
    {
        /// <summary>
        /// Unique idenfier for this specific plugin
        /// </summary>
        public string Id { get; }
        
        /// <summary>
        /// Plugin Name shown in UI's
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// First method called when loading the plugin
        /// </summary>
        public void Initialise(IClientInstance clientInstance);

        /// <summary>
        /// Called when the user requested the plugin to be unloaded, after this call the plugin's instance is removed from memory
        /// </summary>
        public void Unload();
    }
}
