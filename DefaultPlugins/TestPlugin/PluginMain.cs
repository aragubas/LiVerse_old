using LiVerseFramework;
using LiVerseFramework.AnaBanUI.Controls;
using Microsoft.Xna.Framework;

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

            VBox vBox = new VBox();
            vBox.Padding = new Vector2(14, 14);
            vBox.ID = "MainVBox";

            liverseClient.UIRoot.AddElement(vBox);
        }

        public void Unload()
        {
            
        }
    }
}