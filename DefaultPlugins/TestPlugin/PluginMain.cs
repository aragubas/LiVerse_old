using LiVerseFramework;

namespace TestPlugin
{
    public class PluginMain : IPlugin
    {
        public string Id => "aragubas.tests.testplugin";

        public string Title => "Test Plugin";
        IClientInstance? liverseClient;

        public void Initialise(IClientInstance clientInstance)
        {
            liverseClient = clientInstance;

        }

        public void Unload()
        {
            
        }
    }
}