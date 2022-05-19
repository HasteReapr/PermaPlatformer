using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using PermaPlatformer.Engine;

using System.Numerics;

namespace PermaPlatformer
{
    class Tile
    {
        public Vector2 position;
        public Texture tileSprite;
        private string name;

        public Tile(int x, int y)
        {
            position = new Vector2(x, y);
        }

        public Tile(int x, int y, string name)
        {
            position = new Vector2(x, y);
            this.name = name;
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

        public override string ToString()
        {
            return name + " position " + position + " hitbox <" + position.X + 32 + " " + position.Y + 32 + ">";
        }
    }
}
