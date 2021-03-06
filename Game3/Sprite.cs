using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3
{
    public class Sprite
    {
        public readonly Texture2D Texture;
        public readonly Vector2 Size;
        public readonly Vector2 TexelSize;
        public Vector2 Origin;

        public Sprite(Texture2D texture, Vector2 origin)
        {
            Texture = texture;
            Size = new Vector2(Texture.Width, Texture.Height);
            TexelSize = Vector2.One / Size;
            Origin = origin;
        }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Size = new Vector2(Texture.Width, Texture.Height);
            TexelSize = Vector2.One / Size;
            Origin = Size / 2f;
        }
    }
}