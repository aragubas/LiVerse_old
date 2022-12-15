using LiVerseFramework.AnaBanUI.Window;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        /// Called after Draw, draw all windows, windows are drawn on top of everything 
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void DrawWindows(SpriteBatch spriteBatch);

        /// <summary>
        /// Called every frame before Update
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Update(GameTime gameTime);
    }
}
