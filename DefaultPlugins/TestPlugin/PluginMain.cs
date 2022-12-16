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

            HBox hBox = new HBox();
            hBox.Padding = new Vector2(14, 14);
            hBox.ID = "MainVBox";

            var ceira = new FontDescriptor("Ubuntu.ttf", 14);
            Label label = new("Label1 OwO", ceira);
            Label label2 = new("Label2 UwU", ceira);
            Label label3 = new("Powered by: AnaBanUI", ceira);
            Button button = new Button();

            VBox vBox = new VBox();

            Button button2 = new Button("Sinas");
            Label label1 = new Label("Ceira", new FontDescriptor("Ubuntu.ttf", 14));

            vBox.AddElement(button2);
            vBox.AddElement(label1);

            hBox.AddElement(vBox);

            hBox.AddElement(label);
            hBox.AddElement(label2);
            hBox.AddElement(label3);
            hBox.AddElement(button);

            liverseClient.UIRoot.SetRootContainer(hBox);

            button.Clicked += () =>
            {
                GlobalEventManager.InvokeEvent("core.test");

            };

            //Window window = new Window();
            //window.Rectangle = new Rectangle(20, 20, 250, 150);

            //liverseClient.UIRoot.AddWindow(window);

            SoundEffectManager.PlaySoundEffect("core.startup_short", 0.4f);
        }

        public void Unload()
        {
            
        }
    }
}