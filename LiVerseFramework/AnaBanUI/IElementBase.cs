using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.AnaBanUI.Controls
{
    public interface IElementBase
    {
        /// <summary>
        /// Called every frame after Update.<br></br>You don't need to begin/end sprite batch because the parent container will already start and end the sprite batch. But you can also End/Begin End if starting another Batch is suitable
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Called every frame before Update
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Update(GameTime gameTime);
    }
}
