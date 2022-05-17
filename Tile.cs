using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using PermaPlatformer.Engine;

using System.Numerics;

namespace PermaPlatformer
{
    class Tile
    {
        private Vector2 position;
        private Rectangle hitbox;
        private Texture tileSprite;

        public Tile(int x, int y)
        {
            position = new Vector2(x, y);
            hitbox = new Rectangle(x, y, 32, 32);
        }

        public Tile(int x, int y, int width, int height)
        {
            position = new Vector2(x, y);
            hitbox = new Rectangle(x, y, width, height);
        }

        public void Load()
        {
            tileSprite = LoadTexture("Sprites/Stone_Tile.png");
        }

        public void Draw()
        {
            Sprite.Draw(tileSprite, position, WHITE);
        }

        public void DrawTiled(Vector2 position, int width, int height, int xscale, int yscale, Color color)
        {
            Sprite.DrawTiled(tileSprite, position, width, height, xscale, yscale, color);
        }

        public Vector2 getPosition()
        {
            return position;
        }
    
        public Rectangle getHitbox()
        {
            return hitbox;
        }
    }
}
