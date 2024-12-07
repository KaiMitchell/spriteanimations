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
        private int frameHeight;
        private float timePerFrame;
        private bool _isPaused;
        public float totalElapsed;
        public float Rotation, Scale, Depth;
        public bool State;
        public Vector2 Origin;
        Rectangle rectangle;

        public AnimatedTexture(Vector2 origin, float rotation, float scale, float depth)
        {
            this.Origin = origin;
            this.Depth = depth;
            this.Scale = scale;
            this.Rotation = rotation;
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

        public void DrawFrame(SpriteBatch batch, Vector2 screenPos)
        {
            rectangle = new Rectangle(frameWidth * frame, 0, frameWidth, texture.Height);
            batch.Draw(texture, screenPos, rectangle, Color.White, Rotation, Origin, Vector2.One, SpriteEffects.None, Depth);
        }

        public void UpdateFrame(float elapsed, bool state)
        {
            this.State = state;
            //State = _isJumping
            if(_isPaused)
                return;

            totalElapsed += elapsed;

            if(totalElapsed > timePerFrame)
            {
                if(frame < frameCount - 1)
                {
                    frame++;
                    frame %= frameCount;
                }
                else
                {
                    if(!State)
                    {
                        frame = 1;
                    }
                    else
                    {
                        State = false;
                        Reset();
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
            frame = 0;
            totalElapsed = 0;
            Pause();
        }
    }
}