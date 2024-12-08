using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using walkingAnimation;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace walkingAnimation
{
    public class AnimatedTexture
    {
        Texture2D texture;
        private int frame;
        private int frameCount;
        private int frameWidth;
        private float timePerFrame;
        private bool _isPaused;
        public bool IsJumping;
        public bool IsWalkingLeft;
        public bool IsWalkingRight;
        public float totalElapsed;
        public float Rotation, Scale, Depth;
        public Vector2 Origin;
        Rectangle rectangle;

        public AnimatedTexture(Vector2 origin, float rotation, float scale, float depth)
        {
            Origin = origin;
            Depth = depth;
            Scale = scale;
            Rotation = rotation;
        }

        public void Load(ContentManager content, string asset, int frameCount, int fps)
        {
            this.frameCount = frameCount;
            texture = content.Load<Texture2D>(asset);
            timePerFrame = (float)1 / fps;
            frame = 0;
            totalElapsed = 0;
            _isPaused = false;
            frameWidth = texture.Width / frameCount + 1;
        }

        public void DrawFrame(SpriteBatch batch, Vector2 screenPos, SpriteEffects effect)
        {
            rectangle = new Rectangle(frameWidth * frame, 0, frameWidth, texture.Height);
            screenPos.X -= rectangle.Width / 2;
            screenPos.Y -= rectangle.Height / 2;
            batch.Draw(texture, screenPos, rectangle, Color.White, Rotation, Origin, Vector2.One, effect, Depth);
        }

        public void UpdateFrame(float elapsed, bool _isJumping, bool _isWalkingLeft, bool _isWalkingRight)
        {
            IsJumping = _isJumping;
            IsWalkingLeft = _isWalkingLeft;
            IsWalkingRight = _isWalkingRight;
            if(_isPaused)
                return;

            totalElapsed += elapsed;

            if(totalElapsed > timePerFrame)
            {
                frame++;
                if(frame >= frameCount)
                {
                    if(IsJumping)
                    {
                        Reset();
                    }
                    else
                    {
                        frame = 1;
                    }
                }
                totalElapsed -= timePerFrame;
            }
        }

        public void Pause() 
        {
            frame = 0;
            _isPaused = true;
        }

        public void Play() 
        {
            _isPaused = false;
        }

        public void Reset() 
        {
            IsJumping = false;
            frame = 0;
            totalElapsed = 0;
            Pause();
        }
    }
}