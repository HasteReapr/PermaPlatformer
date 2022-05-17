using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using PermaPlatformer.Engine;

using System;
using System.Numerics;

namespace PermaPlatformer.Engine.Buttons
{
    class PlayButton : Button
    {
        public PlayButton(int x, int y, int width, int height, Texture sprite, string Text) : base(x, y, width, height, sprite, Text)
        {

        }

        public override void MouseOver()
        {
            if (CheckCollisionPointRec(GetMousePosition(), hitbox) && IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                Game.CurrentState = 1;
                Game.reloadNeeded = true;
            }
                
        }
    }
}
