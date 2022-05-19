using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using PermaPlatformer.Engine.Buttons;

namespace PermaPlatformer.Engine
{
    class Game
    {
        public static int CurrentState = (int)GameState.MAIN_MENU;
        public static bool reloadNeeded = false;

        Player player = new Player();
        Tile tile = new Tile(192, 320, "Big Tile");
        Tile smallTile = new Tile(320, 256, "Small Tile");

        static Texture playTexture = LoadTexture("Stone_Tile.png");
        PlayButton play = new PlayButton(350, 250, 50, 25, playTexture, "Play");

        public static List<Tile> tileList = new List<Tile>();

        public enum GameState
        {
            MAIN_MENU,
            LEVEL
        }

        enum CurrentLevel
        {
            Level1,
            Level2,
            Level3,
            Level4,
            Level5
        }

        public static void CreateTile(Vector2 position)
        {
            Tile output = new Tile((int)position.X, (int)position.Y, "PlacedTile");
            output.Load();
            tileList.Add(output);
        }

        public void AddTile(Tile tile)
        {
            tileList.Add(tile);
        }

        public void LoadGame()
        {
            //load assets here
            if(CurrentState == (int)GameState.MAIN_MENU)
            {

            }

            if(CurrentState == (int)GameState.LEVEL)
            {
                player.Load();
                tile.Load();
                smallTile.Load();

                tileList.Add(smallTile);
                tileList.Add(tile);
            }
        }

        public void Update(float DeltaTime)
        {
            if (IsKeyPressed(KeyboardKey.KEY_Q))
            {
                CurrentState = (int)GameState.MAIN_MENU;
                reloadNeeded = true;
            }
            // have logic handeled here
            if (reloadNeeded)
            {
                reloadNeeded = false;
                LoadGame();
            }

            if(CurrentState == (int)GameState.MAIN_MENU)
            {
                play.MouseOver();
            }

            if(CurrentState == (int)GameState.LEVEL)
            {
                player.PreUpdate();

                /*if(player.getPos().X < smallTile.getPosition().X)
                    player.LeftCollision(smallTile.getHitbox());
                else
                    player.RightCollision(smallTile.getHitbox());

                player.VerticalCollision(smallTile.getHitbox());*/

                for (int x = 0; x < tileList.Count; x++)
                {
                    //Rectangle drawZone = new Rectangle((int)player.position.X - 16, (int)player.position.Y - 16, 32, 32);
                    Rectangle hitZone = new Rectangle(player.position.X - 16, player.position.Y - 16, 64, 64);
                    Rectangle hit = new Rectangle(tileList[x].position.X, tileList[x].position.Y, 32, 32);
                    if (CheckCollisionRecs(hit, hitZone))
                    {
                        player.HorizontalCollision(hit);
                        player.VerticalCollision(hit);
                    }

                    //Console.WriteLine(tileList[x]);
                }
            }
        }

        public void PreDraw()
        {
            //this happens before draw
            BeginDrawing();
            ClearBackground(BLACK);
            //code to happen before draw

            if(CurrentState == (int)GameState.MAIN_MENU)
            {
                //main menu pre draw
            }

            if(CurrentState == (int)GameState.LEVEL)
            {

            }

            Draw();
        }

        public void Draw()
        {
            //this happens after PreDraw but before PostDraw, used for drawing most things

            if(CurrentState == (int)GameState.MAIN_MENU)
            {
                play.Draw();
            }

            if(CurrentState == (int)GameState.LEVEL)
            {
                player.Draw();
                for (int x = 0; x < tileList.Count; x++)
                {
                    Rectangle hit = new Rectangle(tileList[x].position.X, tileList[x].position.Y, 32, 32);
                    if (CheckCollisionRecs(hit, player.drawZone))
                        tileList[x].Draw();
                    else
                        DrawRectangle((int)tileList[x].position.X, (int)tileList[x].position.Y, 32, 32, BLACK);
                }
                //tileList[x].Draw();

                //tile.DrawTiled(new Vector2(320, 256), 32, 32, 8, 4, WHITE);
                //tile.Draw();
                //smallTile.Draw();
                DrawRectangle((int)player.position.X - 6, (int)player.position.Y - 8, 40, 40, ColorAlpha(RED, 0.5f));
            }

            PostDraw();
        }

        public void PostDraw()
        {
            //do some thing before the drawing is done.

            if(CurrentState == (int)GameState.MAIN_MENU)
            {

            }

            if(CurrentState == (int)GameState.LEVEL)
            {

            }

            EndDrawing();
        }

        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
    }
}