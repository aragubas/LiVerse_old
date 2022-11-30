using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.Character
{
    public interface ICharacterAnimation
    {
        /// <summary>
        /// Descriptive name for your animation, usually separated by _ or written in CamelCase
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The final position offset for the current frame, set by Animation's Update method every frame
        /// </summary> 
        public Vector2 PositionOffset { get; set; }

        /// <summary>
        /// True if the character is speaking (set by the current <seealso cref="ICharacter"/> instance)
        /// </summary>
        public bool IsCharacterSpeaking { get; set; }

        /// <summary>
        /// Update method called every frame
        /// </summary>
        /// <param name="gameTime">Monogame's GameTime time step class</param>
        void Update(GameTime gameTime) { }
    }
}
