using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

using PermaPlatformer.Engine;

using System.Numerics;
using System;

namespace PermaPlatformer
{
    class Player
    {
        private Vector2 position;
        private Vector2 oldPos;
        private Vector2 velocity;
        private Rectangle hitbox;

        public static bool Colliding;
        public static bool Intersecting;
        public static int IntersectingDir; //0 for top, 1 for right, 2 for bottom, 3 for left

        private Texture playerSprite;
        private int SpeedY;
        private int SpeedX;
        public static int HP;
        public static int MaxHP;

        public Player()
        {
            HP = 15;
            MaxHP = HP;
            position = new Vector2(250, 250);
            hitbox = new Rectangle(position.X, position.Y, 32, 32);
        }

        public void Load()
        {
            playerSprite = LoadTexture("Sprites/Player_Idle_Side big.png");
        }

        public void Draw()
        {
            //if (SpeedX != 0)
            //Sprite.Animate(player run sprite);
            //if (SpeedY > 0)
            //Sprite.Animate(player fall sprite);
            //else if(SpeedY < 0)
            //Sprite.Animate(player jump sprite);
            //Sprite.Draw(playerSprite, position, WHITE);
            Sprite.Draw(playerSprite, new Rectangle(0, 0, 32 * SpeedX, 32), position, WHITE);
            //DrawRectangle((int)position.X, (int)position.Y, (int)hitbox.width, (int)hitbox.height, RED);
        }

        public void PreUpdate()
        {
            oldPos = position;
            Update();
        }

        public void Update()
        {
            hitbox.X = position.X;
            hitbox.Y = position.Y;

            SpeedY = (int)(position.Y - oldPos.Y);
            if ((int)(position.X - oldPos.Y) < 0) SpeedX = -1; else SpeedX = 1;
            float offset = 0;
            offset += GetFrameTime();
            while(offset > 0)
            {
                offset -= 0.1f;
            }
            if(offset <= 0) Move();

            if (!Colliding)
            {
                if (velocity.Y < 8)
                    velocity.Y += 0.75f;
                else if (velocity.Y > 8)
                    velocity.Y += 1;
                else if (velocity.Y > 24)
                    velocity.Y = 24;
            }
            else if (Colliding && velocity.Y > 0)
                velocity.Y = 0;

            if(velocity != Vector2.Zero)
            {
                //add collision X
                velocity.X = Game.Lerp(velocity.X, 0, 0.1f);
                //add collision Y
                velocity.Y = Game.Lerp(velocity.Y, 0, velocity.Y > 8 ? 0.5f : 0.1f);
                position += velocity;
            }

            //snap back up if inside a hitbox
            if (Intersecting && IntersectingDir == 0)
            {
                position.Y -= 0.06f;
                Colliding = true;
            }
        }

        public void Move()
        {
            if (IsKeyDown(KeyboardKey.KEY_A))
                velocity.X = Game.Lerp(velocity.X, -6, 0.9f);

            if (IsKeyDown(KeyboardKey.KEY_D))
                velocity.X = Game.Lerp(velocity.X, 6, 0.9f);

            if (IsKeyPressed(KeyboardKey.KEY_SPACE) )//&& Colliding)
            {
                velocity.Y = -16;
            }            
        }

        public void SetColliding(bool input)
        {
            Colliding = input;
        }

        public bool Collision(Rectangle hitbox)
        {
            
            return CheckCollisionRecs(this.hitbox, hitbox);
        }

        public bool IntersectingTop(Rectangle hitbox, Vector2 position)
        {
            Rectangle newHitBox = new Rectangle((int)position.X, (int)position.Y, (int)hitbox.width, 8);
            DrawRectangle((int)position.X, (int)position.Y, (int)hitbox.width, 8, BLUE);
            return CheckCollisionRecs(this.hitbox, newHitBox);
            //return CheckCollisionLines(position, position + new Vector2(hitbox.width, 8), this.position + new Vector2(this.hitbox.width, this.hitbox.height), this.position + new Vector2(0, this.hitbox.height), &output);
            //return CheckCollisionPointRec(position + new Vector2(hitbox.width/2, 0), this.hitbox);
        }

        public bool IntersectingSide(Rectangle hitbox, Vector2 position, int side)
        {
            //side is positive for left side, negative for right side
            Vector2 pointSide = side == 1 ? new Vector2(position.X, position.Y + hitbox.height / 2) : new Vector2(position.X + hitbox.width, position.Y + hitbox.height / 2);
            return CheckCollisionPointRec(pointSide, this.hitbox);
        }
    }
}
