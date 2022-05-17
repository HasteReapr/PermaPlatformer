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
        Tile tile = new Tile(0, 300, 800, 192);

        static Texture playTexture = LoadTexture("Stone_Tile.png");
        PlayButton play = new PlayButton(350, 250, 50, 25, playTexture, "Play");

        public List<Tile> tileList = new List<Tile>();

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


                player.SetColliding(player.colliding(tile.getHitbox()));
            }
        }

        public void PreDraw()
        {
            //this happens before draw
            BeginDrawing();
            ClearBackground(WHITE);
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
                tile.DrawTiled(new Vector2(0, 300), 800, 32, 25, 6, WHITE);
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