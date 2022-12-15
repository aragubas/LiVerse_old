using LiVerseFramework;
using LiVerseFramework.AnaBanUI.Controls;
using LiVerseFramework.AnaBanUI.Controls.Containers;
using LiVerseFramework.Graphics;
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

            var ceira = new FontDescriptor("Ubuntu.ttf", 14);
            Label label = new("Label1 OwO", ceira);
            Label label2 = new("Label2 UwU", ceira);
            Label label3 = new("Powered by: AnaBanUI", ceira);
            Button button = new Button();

            label.ID = "label";
            label2.ID = "label2";
            label3.ID = "label3";
            button.ID = "button";

            label.Margin = new Vector2(3, 4);
            label2.Margin = new Vector2(3, 6);
            button.Margin = new Vector2(0, 6);

            label.Padding = new Vector2(4, 3);

            vBox.AddElement(label);
            vBox.AddElement(label2);
            vBox.AddElement(label3);
            vBox.AddElement(button);

            liverseClient.UIRoot.AddElement(vBox);


            //Window window = new Window();
            //window.Rectangle = new Rectangle(20, 20, 250, 150);

            //liverseClient.UIRoot.AddWindow(window);
        }

        public void Unload()
        {
            
        }
    }
}