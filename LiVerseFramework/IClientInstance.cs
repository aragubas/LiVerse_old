using LiVerseFramework.Character;
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

        /// <summary>
        /// Method called by Plugins for registering animations
        /// </summary>
        /// <param name="animation">An <seealso cref="ICharacterAnimation"/> to be added</param>
        public void AddCharacterAnimation(ICharacterAnimation animation);

    }
}
