﻿using LiVerseFramework;

namespace DefaultAnimations
{
    public class PluginMain : IPlugin
    {
        public string Id => "aragubas.liverseCore.defaultAnimations";

        public string Title => "Default Animations";
        IClientInstance? clientInstance;

        public void Initialise(IClientInstance clientInstance)
        {
            this.clientInstance = clientInstance;
        }

        public void Unload()
        {
            
        }
    }
}