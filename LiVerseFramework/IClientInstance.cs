using LiVerseFramework.AnaBanUI;
using LiVerseFramework.Character;
using Microsoft.Xna.Framework;
using NAudio.CoreAudioApi;

namespace LiVerseFramework
{
    public interface IClientInstance
    {
        public MMDevice Microphone { get; set; }
        
        /// <summary>
        /// The current chracter on screen
        /// </summary>
        public ICharacter Character { get; set; }

        /// <summary>
        /// When enabled all UI elements will be hidden and the background will change to the specified background mode
        /// </summary>
        public bool TransparentMode { get; set; }

        public Game? GameInstance { get; }

        /// <summary>
        /// Main UIRoot used by all plugins
        /// </summary>
        public IUiRoot UIRoot { get; }

        /// <summary>
        /// Method called by Plugins for registering animations
        /// </summary>
        /// <param name="animation">An <seealso cref="ICharacterAnimation"/> to be added</param>
        public void AddCharacterAnimation(ICharacterAnimation animation);


    }
}
