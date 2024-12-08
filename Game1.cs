using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace walkingAnimation;

public class Game1 : Game
{
    private AnimatedTexture brolyWalkLeft;
    private AnimatedTexture brolyWalkRight;
    private AnimatedTexture brolyJump;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public bool _isJumping;
    public bool _isWalkingLeft;
    public bool _isWalkingRight;

    Viewport viewport;
    Vector2 characterPos;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        brolyWalkLeft = new AnimatedTexture(Vector2.Zero, 0f, 0f, 0f);
        brolyWalkRight = new AnimatedTexture(Vector2.Zero, 0f, 0f, 0f);
        brolyJump = new AnimatedTexture(Vector2.Zero, 0f, 0f, 0f);
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        brolyWalkLeft.Load(Content, "brolyWalkLeft", 13, 10);
        brolyWalkRight.Load(Content, "brolyWalkRight", 13, 10);
        brolyJump.Load(Content, "brolyJump", 8, 10);
        viewport = _graphics.GraphicsDevice.Viewport;
        characterPos = new Vector2(viewport.Width / 2, viewport.Height / 2);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        // TODO: Add your update logic here
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState kstate = Keyboard.GetState();
        if(kstate.IsKeyDown(Keys.Left) && !_isJumping)
        {
            _isWalkingRight = false;
            _isWalkingLeft = true;
            characterPos.X -= 3;
            brolyWalkLeft.Play();
            brolyWalkLeft.UpdateFrame(elapsed, false, true, false);
        }
        else if(kstate.IsKeyUp(Keys.Left))
        {
            _isWalkingLeft = false;
            brolyWalkLeft.Pause();
        };

        if(kstate.IsKeyDown(Keys.Right) && !_isJumping)
        {
            _isWalkingLeft = false;
            _isWalkingRight = true;
            characterPos.X += 3;
            brolyWalkRight.Play();
            brolyWalkRight.UpdateFrame(elapsed, false, false, true);
        } 
        else if(kstate.IsKeyUp(Keys.Right))
        {
            _isWalkingRight = false;
            brolyWalkRight.Pause();
        }

        if(kstate.IsKeyDown(Keys.Up) && !_isJumping)
        {
            _isJumping = true;
            if(_isJumping)
            {
                characterPos.Y -= 3;
            }
            brolyJump.Play();
        };

        if(_isJumping)
        {
            brolyJump.UpdateFrame(elapsed, true, false, false);
            if(!brolyJump.IsJumping)
            {
                characterPos.Y += 3;
                _isJumping = false;
                brolyJump.Reset();
            };
        };

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        if(_isJumping)
        {
            brolyJump.DrawFrame(_spriteBatch, characterPos, SpriteEffects.None);
        }
        else if(_isWalkingRight)
        {
            brolyWalkRight.DrawFrame(_spriteBatch, characterPos, SpriteEffects.None);
        }
        else if(_isWalkingLeft)
        {
            brolyWalkLeft.DrawFrame(_spriteBatch, characterPos, SpriteEffects.None);
        }
        else
        {
            brolyWalkLeft.DrawFrame(_spriteBatch, characterPos, SpriteEffects.None);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
