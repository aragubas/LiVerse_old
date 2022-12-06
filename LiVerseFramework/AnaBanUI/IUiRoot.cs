using LiVerseFramework.AnaBanUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LiVerseFramework.AnaBanUI
{
    public interface IUiRoot
    {
        #region Add Methods
        public void AddElement(Element element);
        public void AddWindow(WindowBase window);
        #endregion

        /// <summary>
        /// Called when the Main window is resized
        /// </summary> 
        public void WindowResized();

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
