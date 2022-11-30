using LiVerseFramework.Character;
using NAudio.CoreAudioApi;

namespace LiVerseFramework
{
    public interface IClientInstance
    {
        public MMDevice Microphone { get; set; }
        public ICharacter Character { get; set; }
        public bool TransparentMode { get; set; }


    }
}
