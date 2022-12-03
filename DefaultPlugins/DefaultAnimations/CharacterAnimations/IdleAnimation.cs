using LiVerseFramework.Character;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAnimations.CharacterAnimations
{
    class IdleAnimation : ICharacterAnimation
    {
        public string Name => "default_idle";
        public Vector2 PositionOffset { get; set; }
        public bool IsCharacterSpeaking { get; set; }

        Vector2 _idleTarget = Vector2.Zero;
        Vector2 _idle = Vector2.Zero;
        float _intensity = 5;
        double _time = 0;
         
        void ICharacterAnimation.Update(GameTime gameTime)
        {
            _time += gameTime.ElapsedGameTime.TotalSeconds;

            float f = 0.3f;
            PositionOffset = Vector2.Lerp(PositionOffset, _idle, 1 - MathF.Pow(f, (float)gameTime.ElapsedGameTime.TotalSeconds));

            _idle = new Vector2(MathF.Sin(_idleTarget.X) * _intensity, MathF.Cos(_idleTarget.Y) * _intensity);

            if (_time >= 0.25)
            {
                _idleTarget = new Vector2(Random.Shared.Next((int)-_intensity, (int)_intensity), Random.Shared.Next((int)-_intensity, (int)_intensity));

                _time = 0;
            }
        }

    }

}
