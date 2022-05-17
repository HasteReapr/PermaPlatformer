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

        private Texture playerSprite;
        private int SpeedY;
        private int SpeedX;
        private Rectangle hitbox;
        private bool Colliding;
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
        }

        public void PreUpdate()
        {
            oldPos = position;
            Update();
        }

        public void Update()
        {
            if (!Colliding)
                velocity.Y = 2;

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

            if(velocity != Vector2.Zero)
            {
                velocity.X = Game.Lerp(velocity.X, 0, 0.1f);
                velocity.Y = Game.Lerp(velocity.Y, 0, 0.1f);
                position += velocity;
            }
        }

        public void Move()
        {
            if (IsKeyDown(KeyboardKey.KEY_A))
                velocity.X = -4;

            if (IsKeyDown(KeyboardKey.KEY_D))
                velocity.X = 4;

            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                velocity.Y = -24;
            }            
        }

        public void SetColliding(bool input)
        {
            Colliding = input;
        }

        public bool colliding(Rectangle hitbox)
        {
            return CheckCollisionRecs(this.hitbox, hitbox);
        }
    }
}
