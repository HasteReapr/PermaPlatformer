using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using PermaPlatformer.Engine;

using System.Numerics;
using System;

namespace PermaPlatformer
{
    class Player
    {
        public Vector2 position;
        public Vector2 oldPos;
        public Vector2 velocity;
        public Rectangle hitBoxY;
        public Rectangle hitBoxX;
        public Rectangle drawZone;

        private Texture playerSprite;
        public static int HP;
        public static int MaxHP;
        public static int Lives;

        private int SpeedY;
        private int SpeedX;
        private int jumpCount;
        private int overlapX;
        private int overlapY;
        private int glideTime;
        private float gravity;
        
        public Player()
        {
            HP = 15;
            MaxHP = HP;
            position = new Vector2(250, 250);
            hitBoxX = new Rectangle(position.X, position.Y + 8, 32, 16);

            hitBoxY = new Rectangle(position.X + 8, position.Y, 16, 32);

            drawZone = new Rectangle(position.X - 128, position.Y - 128, 256, 256);

            SpeedX = 1;
            jumpCount = 0;
            glideTime = 120;
            Lives = 5;
        }
        Texture block;
        public void Load()
        {
            playerSprite = LoadTexture("Sprites/Player_Idle_Side big.png");
            block = LoadTexture("Sprites/Stone_Tile.png");
        }

        
        public void Draw()
        {
            //if (SpeedX != 0)
            //Sprite.Animate(player run sprite);
            //if (SpeedY == 1)
            //Sprite.Animate(player fall sprite);
            //else if(SpeedY == -1)
            //Sprite.Animate(player jump sprite);
            //Sprite.Draw(playerSprite, position, WHITE);
            //DrawRectangle((int)drawZone.X, (int)drawZone.Y, (int)drawZone.width, (int)drawZone.height, GRAY);
            //DrawRectangle((int)drawZone.X - 32, (int)drawZone.Y - 32, (int)drawZone.width + 64, (int)drawZone.height + 64, ColorAlpha(GRAY, 0.5f));
            DrawCircle((int)position.X + 16, (int)position.Y + 16, 160, GRAY);
            DrawCircle((int)position.X + 16, (int)position.Y + 16, 192, ColorAlpha(GRAY, 0.5f));
            DrawCircle((int)position.X + 16, (int)position.Y + 16, 216, ColorAlpha(GRAY, 0.25f));

            Sprite.Draw(playerSprite, new Rectangle(0, 0, 32 * SpeedX, 32), position, WHITE);
            DrawRectangle((int)hitBoxX.X, (int)hitBoxX.Y, (int)hitBoxX.width, (int)hitBoxX.height, ColorAlpha(RED, 0.25f));
            
            DrawRectangle((int)hitBoxY.X, (int)hitBoxY.Y, (int)hitBoxY.width, (int)hitBoxY.height, ColorAlpha(BLUE, 0.25f));

            DrawText($"Jumps Left {2-jumpCount}", 10, 10, 20, WHITE);
            DrawText($"Glide Time {glideTime}", 10, 30, 20, WHITE);
            DrawText($"Lives {Lives}", 10, 50, 20, WHITE);

            bool drawBlock = IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL);
            
            int positionX = (int)Math.Round((decimal)GetMouseX() / 32) * 32;
            int positionY = (int)Math.Round((decimal)GetMouseY() / 32) * 32;

            Vector2 drawPosition = new Vector2(positionX, positionY);
            if (drawBlock)
                Sprite.DrawAlpha(block, drawPosition, WHITE, 0.5f);

            if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) && drawBlock)
                Game.CreateTile(drawPosition);
        }

        public void PreUpdate()
        {
            oldPos = position;
            hitBoxX.X = position.X;
            hitBoxX.Y = position.Y + 8;

            hitBoxY.X = position.X + 8;
            hitBoxY.Y = position.Y;

            drawZone.X = position.X - 128 + 16;
            drawZone.Y = position.Y - 128 + 16;

            SpeedX = Math.Sign(velocity.X);
            SpeedY = Math.Sign(velocity.Y);
            Update();
        }

        public void Update()
        {
            velocity.Y += gravity;
            
            if (velocity.X != 0)
                velocity.X = Game.Lerp(velocity.X, 0, 0.25f);

            if (velocity.Y != 0)
                velocity.Y = Game.Lerp(velocity.Y, 0, 0.25f);

            position += velocity;

            if (overlapX != 0)
               position.X += overlapX;

            if (overlapY != 0)
            {
                position.Y += overlapY;
                jumpCount = 0;
                if (glideTime < 120)
                    glideTime += 4;
                else if (glideTime > 120)
                    glideTime = 120;
            }

            if (position.Y > 480)
                Kill();

            if (position.X < -32 || position.X > 832)
                Kill();

            Move();
        }

        public void Move()
        {
            if (IsKeyDown(KeyboardKey.KEY_A))
                velocity.X = -6;
            //move left

            if (IsKeyDown(KeyboardKey.KEY_D))
                velocity.X = 6;
            //move right

            if (IsKeyDown(KeyboardKey.KEY_W) && glideTime > 0)
            {
                glideTime--;
                gravity = Game.Lerp(gravity, 0.25f, 0.1f);
            }
            else
                gravity = Game.Lerp(gravity, 1.75f, 0.1f);

            if (IsKeyPressed(KeyboardKey.KEY_SPACE) && jumpCount < 2)
            {
                velocity.Y = -36;
                jumpCount++;
            } 
        }

        public void Kill()
        {
            position = new Vector2(400 - 32, 0);
            Lives--;
            glideTime = 120;
            jumpCount = 0;
        }

        public void HorizontalCollision(Rectangle colHitBox)
        {
            int Dir = Math.Sign(velocity.X);
            Rectangle newHitbox = new Rectangle((int)position.X + velocity.X, (int)position.Y + 8, hitBoxX.width, hitBoxX.height);
            if (CheckCollisionRecs(colHitBox, newHitbox))
                switch (Dir)
                {
                    case -1:
                        overlapX = (int)(colHitBox.X + colHitBox.width - newHitbox.x);
                        break;

                    case 1:
                        overlapX = (int)(colHitBox.X - (newHitbox.x + newHitbox.width));
                        break;
                }
            else
                overlapX = 0;
        }

        public void VerticalCollision(Rectangle colHitBox)
        {
            int Dir = Math.Sign(velocity.Y);
            Rectangle newHitBox = new Rectangle((int)position.X + 8, (int)position.Y + velocity.Y, hitBoxY.width, hitBoxY.height);
            if (CheckCollisionRecs(colHitBox, newHitBox))
                switch (Dir)
                {
                    case 1:
                        overlapY = (int)(colHitBox.Y - (newHitBox.Y + newHitBox.height));
                        break;

                    case -1:
                        overlapY = (int)(colHitBox.Y + colHitBox.height - newHitBox.Y);
                        break;
                }
            else
                overlapY = 0;
        }
    }
}
