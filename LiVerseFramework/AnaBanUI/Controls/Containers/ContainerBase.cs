using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.AnaBanUI.Controls.Containers
{
    public abstract class ContainerBase : Element
    {
        public List<Element> ChildElements = new();

        public virtual void AddElement(Element element)
        {
            ChildElements.Add(element);
        }

    }
}
