using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using System.Numerics;
using System;

namespace PermaPlatformer.Engine.Buttons
{
    class Button
    {
        public static Vector2 position;
        public static Rectangle hitbox;
        private Texture buttonSprite;
        private string Text;
        public Button(int x, int y, int width, int height, Texture sprite, string Text)
        {
            position = new Vector2(x, y);
            hitbox = new Rectangle(x, y, width, height);
            buttonSprite = sprite;
            this.Text = Text;
        }

        public virtual void MouseOver()
        {
            if (CheckCollisionPointRec(GetMousePosition(), hitbox) && IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
                Console.WriteLine("Button Pressed!");
        }

        public void Draw()
        {
            DrawRectangle((int)position.X, (int)position.Y, (int)hitbox.width, (int)hitbox.height, GRAY);
            if (CheckCollisionPointRec(GetMousePosition(), hitbox))
                Sprite.Draw(buttonSprite, position, GRAY);
            else
                Sprite.Draw(buttonSprite, position, WHITE);
            DrawText(Text, (int)position.X + hitbox.width/2, (int)position.Y + hitbox.height/2, 12, BLACK);
        }
    }
}
