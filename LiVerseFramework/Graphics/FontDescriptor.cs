using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVerseFramework.Graphics
{
    /// <summary>
    /// Represents a font loaded in <seealso cref="Fonts.Cache"/>
    /// </summary>
    public struct FontDescriptor
    {
        public string Path { get; }
        public int FontSize { get; }

        public FontDescriptor(string path, int fontSize)
        {
            Path = path;
            FontSize = fontSize;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is FontDescriptor fontDescriptor)
            {
                return fontDescriptor.Path == Path && fontDescriptor.FontSize == FontSize;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return FontSize.GetHashCode() + Path.GetHashCode();
        }
    }
}
