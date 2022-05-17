using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using System.Numerics;

namespace PermaPlatformer.Engine
{
    static class Sprite
    {
        private static int frame = 0;
        private static float offset;
        public static void Draw(Texture texture, Vector2 position, Color color)
        {
            DrawTexture(texture, (int)position.X, (int)position.Y, color);
        }

        public static void DrawTiled(Texture texture, Vector2 position, int width, int height, int xscale, int yscale, Color color)
        {
            DrawTexturePro(texture, new Rectangle(0, 0, xscale * width, yscale * height), new Rectangle(position.X, position.Y, width * xscale, height * yscale), new Vector2(0, 0), 0, color);
        }

        public static void Animate(Texture texture, Vector2 position, int width, int height, Color color, int framecount, float frametime)
        {
            //add x and y for the position on the sprite sheet
            offset += GetFrameTime();
            while (offset > frametime)
            {   
                offset -= frametime;
                frame++;
            }

            frame %= framecount; //does the same thing as below
            //if (frame >= framecount)
            //    frame = 0;

            DrawTextureRec(texture, new Rectangle(0, height * frame, width, height), position, color);
        }

        public static void Draw(Texture texture, Rectangle textureLoc, Vector2 position, Color color)
        {
            DrawTextureRec(texture, textureLoc, position, color);
        }
    }
}