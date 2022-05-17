using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using System;

namespace PermaPlatformer.Engine
{
    class Program
    {
        public static void Main()
        {
            Game game = new Game();

            InitWindow(800, 480, "PermaPlatformer");
            SetTargetFPS(60);

            // load an image for the window icon
            //Image icon = LoadImage("Textures/Logo.png");

            // ensure that the picture is in the correct format
            //ImageFormat(ref icon, PixelFormat.PIXELFORMAT_UNCOMPRESSED_R8G8B8A8);

            //SetWindowIcon(icon);

            //UnloadImage(icon);

            game.LoadGame();

            while (!WindowShouldClose())
            {
                float frameTime = GetFrameTime();

                float offset = 0;
                offset += GetFrameTime();
                while (offset > 0)
                {
                    offset -= 0.1f;
                }
                game.Update(frameTime);
                game.PreDraw();
            }

            CloseWindow();
        }
    }
}
