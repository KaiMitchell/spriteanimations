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
    private AnimatedTexture brolyJump;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public bool _isJumping;

    Viewport viewport;
    Vector2 characterPos;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        brolyWalkLeft = new AnimatedTexture(Vector2.Zero, 0f, 0f, 0f);
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
        Console.WriteLine(_isJumping);
        if(kstate.IsKeyDown(Keys.Left))
        {
            _isJumping = false;
            brolyWalkLeft.Play();
            brolyWalkLeft.UpdateFrame(elapsed, _isJumping);
        };

        if(kstate.IsKeyDown(Keys.Up))
        {
            _isJumping = true;
        }

        if(_isJumping)
        {
            brolyJump.Play();
            brolyJump.UpdateFrame(elapsed, _isJumping);
        }

        if(kstate.IsKeyUp(Keys.Left))
        {
            brolyWalkLeft.Pause();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        if(!_isJumping)
        {
            brolyWalkLeft.DrawFrame(_spriteBatch, characterPos);
        }
        else 
        {
            brolyJump.DrawFrame(_spriteBatch, characterPos);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
