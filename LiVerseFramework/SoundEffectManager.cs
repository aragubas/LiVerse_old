using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework
{
    public static class SoundEffectManager
    {
        public static Dictionary<string, SoundEffect> LoadedSounds = new();

        public static void LoadSound(string domainName, string filePath)
        {
            if (Path.GetExtension(filePath) != ".wav")
            {
                throw new Exception("Invalid file type. Only \".wav\" sound effects can be loaded.");
            }

            string keyName = $"{domainName}.{Path.GetFileName(filePath).Replace(".wav", "")}";

            if (LoadedSounds.ContainsKey(keyName))
            {
                throw new ArgumentException($"A sound effect with the name same and domain has been already loaded.\nKeyName: \"{keyName}\"");
            }

            LoadedSounds.Add(keyName, SoundEffectFromFile(filePath));
        }

        /// <summary>
        /// Plays a SoundEffect loaded into SoundEffectManager
        /// </summary>
        /// <param name="domainAndFileName">Domain and FileName (without extension) example: core.startup</param>
        /// <param name="volume">Volume (range: 0.0f to 1.0f)</param>
        /// <param name="pitch">Pitch (range: -1.0f to 1.0f)</param>
        /// <param name="pan">Pan (range: 1.0f center, 0.0f left and 1.0f right)</param> 
        public static void PlaySoundEffect(string domainAndFileName, float volume=0.75f, float pitch=0.0f, float pan=0.0f)
        {
            LoadedSounds[domainAndFileName].Play(volume, pitch, pan);          
        }

        public static SoundEffect SoundEffectFromFile(string filePath)
        {
            SoundEffect soundEffect;

            FileStream fileStream = new(filePath, FileMode.Open);

            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);

            Stream stream = new MemoryStream(data);

            soundEffect = SoundEffect.FromStream(stream);
            fileStream.Dispose();

            return soundEffect;
        }
    }
}
