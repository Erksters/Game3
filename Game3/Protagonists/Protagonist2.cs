using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using GameArchitectureExample.StateManagement;
using Microsoft.Xna.Framework.Input;

namespace Game3
{
    public class Protagonist2 : Game
    {
        public Game game;

        #region Animation
        /// <summary>
        /// Helps flip the animation in the Draw()
        /// </summary>
        SpriteEffects spriteEffect;

        /// <summary>
        /// Determines which frame to draw in the template
        /// </summary>
        Rectangle source;

        /// <summary>
        /// texture object to display in the Draw()
        /// </summary>
        private Texture2D idleTexture;

        /// <summary>
        /// texture object to display in the Draw()
        /// </summary>
        private Texture2D WalkingTexture;

        /// <summary>
        /// texture object to display in the Draw()
        /// </summary>
        private Texture2D AttackTexture;

        /// <summary>
        /// Helps animate the sprite
        /// </summary>
        private double animationTimer;

        /// <summary>
        /// Helps animate the sprite
        /// </summary>
        private short idleFrame;

        /// <summary>
        /// Helps animate the sprite
        /// </summary>
        private short walkingFrame;

        /// <summary>
        /// Helps animate the sprite
        /// </summary>
        private short attackingFrame;

        /// <summary>
        /// Helps animate the sprite
        /// will help loop the animation sprite back to frame 0
        /// </summary>
        private int idleTotalFrames = 10;

        /// <summary>
        /// Helps animate the sprite
        /// will help loop the animation sprite back to frame 0
        /// </summary>
        private int WalkingTotalFrames = 12;

        /// <summary>
        /// Helps animate the sprite
        /// will help loop the animation sprite back to frame 0
        /// </summary>
        private int AttackingTotalFrames = 17;

        /// <summary>
        /// Helps animate the sprite
        /// Determines how quickly the draw() goes through frames
        /// Smaller is faster
        /// </summary>
        private double animationSpeed = 0.1;

        /// <summary>
        /// height of the animations sprite
        /// </summary>
        private int idleHeight = 32;

        /// <summary>
        /// width of the animations sprite
        /// </summary>
        private int idleWidth = 24;

        /// <summary>
        /// width of the animations sprite
        /// </summary>
        private int movingWidth = 22;

        /// <summary>
        /// width of the animations sprite
        /// </summary>
        private int movingHeight = 33;

        /// <summary>
        /// width of the animations sprite
        /// </summary>
        private int AttackingHeight = 37;

        /// <summary>
        /// width of the animations sprite
        /// </summary>
        private int AttackingWidth = 43;
        #endregion

        #region State Of Character
        /// <summary>
        /// Which way is the player facing
        /// When holding the Left Key, it is true.
        /// When holding the Right Key, it is false.
        /// </summary>
        public bool Flipped;

        /// <summary>
        /// Enumeration for which animation should we draw
        /// </summary>
        public enum AnimateStatus { Idle, Walking, Attacking, Falling}

        /// <summary>
        /// Determines which animation we should draw for the protagonist
        /// </summary>
        public AnimateStatus ProtagonistStatus;
        #endregion

        #region Inactive Environmental Bounds

        //        /// <summary>
        //        /// Attack hit boxes
        //        /// </summary>
        //        //public BoundingRectangle AttackBounds;
        #endregion

        #region Velocity Constants
        /// <summary>
        /// Used for constant application of speed onto Position property
        /// </summary>
        public Vector2 HorizontalVelocity = new Vector2(3, 0);

        /// <summary>
        /// The value of Jumping in this environment
        /// </summary>
        public Vector2 VerticalVelocity = new Vector2(0, -3);

        /// <summary>
        /// Determines where to draw the sprite
        /// </summary>
        private Vector2 Position;

        /// <summary>
        /// Helper attribute for ResetGame()
        /// </summary>
        private Vector2 initialPosition;

        /// <summary>
        /// Velocity helper and used for speed item upgrades
        /// </summary>
        private int SpeedMultiplier = 1;
        #endregion

        /// <summary>
        /// Will prevent the user from jumping infinitely
        /// </summary>
        private double jumpingTimer = 0;

        #region Inputs
        InputAction GoLeft = new InputAction(
            new Buttons[] { Buttons.LeftThumbstickLeft },
            new Keys[] { Keys.A , Keys.Left} , false);

        InputAction GoRight = new InputAction(
            new Buttons[] { Buttons.LeftThumbstickRight},
            new Keys[] { Keys.D, Keys.Right }, false);

        InputAction GoUp = new InputAction(
            new Buttons[] { Buttons.LeftThumbstickUp},
            new Keys[] { Keys.W, Keys.Up }, false);

        InputAction GoDown = new InputAction(
            new Buttons[] { Buttons.LeftThumbstickDown},
            new Keys[] { Keys.S, Keys.Down }, false);

        InputAction Attacking = new InputAction(
            new Buttons[] { Buttons.RightTrigger},
            new Keys[] { Keys.Space, Keys.Z }, false);

        #endregion

        /// <summary>
        /// public constructor
        /// </summary>
        public Protagonist2(Vector2 initialPosition)
        {
            Position = initialPosition;
            initialPosition = Position;
        }


        /// <summary>
        /// Handles the movement of the protagonist
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="input"></param>
        public void Update(GameTime gameTime, InputState input)
        {
            //Check inputs
            ProtagonistStatus = AnimateStatus.Idle;
            PlayerIndex player;
            if (Attacking.Occurred(input, null, out player) )
            {
                ProtagonistStatus = AnimateStatus.Attacking;
                HandleMovementsWithAttacks(input, player, true);
            }
            else
            {
                HandleMovementsWithAttacks(input, player, false);
            }
        }

        private void HandleMovementsWithAttacks(InputState input, PlayerIndex player, bool prioritizeAttack )
        {
            if (GoLeft.Occurred(input, null, out player))
            {
                if (!prioritizeAttack) { ProtagonistStatus = AnimateStatus.Walking; }
                Flipped = true;    
                Position += -HorizontalVelocity;
            }
            if (GoRight.Occurred(input, null, out player))
            {
                Flipped = false;
                if (!prioritizeAttack) { ProtagonistStatus = AnimateStatus.Walking; }
                Position += HorizontalVelocity;
            }
            if (GoDown.Occurred(input, null, out player))
            {
                if (!prioritizeAttack) { ProtagonistStatus = AnimateStatus.Walking; }
                Position += -VerticalVelocity;
            }
            if (GoUp.Occurred(input, null, out player))
            {
                if (!prioritizeAttack) { ProtagonistStatus = AnimateStatus.Walking; }
                Position += VerticalVelocity;
            }
        }

        /// <summary>
        /// Load the texture sprite for the animation
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            idleTexture = content.Load<Texture2D>("SkeletonIdle");
            WalkingTexture = content.Load<Texture2D>("Skeleton Walk");
            AttackTexture = content.Load<Texture2D>("Skeleton Attack");
        }

        /// <summary>
        /// The drawing method to display the protagonist
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            handleSpriteEffect();

            if (ProtagonistStatus == AnimateStatus.Attacking)
            {
                DrawAttack(spriteBatch);
            }
            else if (ProtagonistStatus == AnimateStatus.Walking)
            {
                DrawWalk(spriteBatch);
            }
            else
            {
                DrawIdle(spriteBatch);
            }

        }

        /// <summary>
        /// Helper function to determine if we need to flip the direction of the animation
        /// Used in Draw()
        /// </summary>
        /// <param name="flipped"></param>
        private void handleSpriteEffect()
        {
            if (Flipped)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffect = SpriteEffects.None;
            }
        }

        /// <summary>
        /// Helper method to help condense Draw()
        /// Will draw the Attack animation for the protagonist
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawAttack(SpriteBatch spriteBatch)
        {
            //Reset the other frames for cleanliness
            walkingFrame = 0;
            idleFrame = 0;

            //Update the frame
            if (animationTimer > animationSpeed)
            {
                animationTimer -= animationSpeed;
                attackingFrame++;
            }

            //Loop the frame back to the first image in the texture template
            if (attackingFrame > AttackingTotalFrames) { attackingFrame = 0; }

            //Redefine the source rectangle because we are using a different template texture
            source = new Rectangle(attackingFrame * AttackingWidth, 0, AttackingWidth, AttackingHeight);

            //Draw onto screen :)
            spriteBatch.Draw(
                AttackTexture,
                new Rectangle((int)Position.X, (int)Position.Y, AttackingWidth, AttackingHeight),
                source,
                Color.White,
                0f,
                new Vector2(0, 0),
                spriteEffect, 0);
        }

        /// <summary>
        /// Helper method to help condense Draw()
        /// Will draw the Idle animation for the protagonist
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawIdle(SpriteBatch spriteBatch)
        {
            //reset the other frames for animations cleanliness
            walkingFrame = 0;
            attackingFrame = 0;

            //Update the frame
            if (animationTimer > animationSpeed)
            {
                animationTimer -= animationSpeed;
                idleFrame++;
            }

            //loop back down to the first frame in the template
            if (idleFrame > idleTotalFrames) { idleFrame = 0; }

            //Redefine the source rectangle because we are using a different template texture
            source = new Rectangle(idleFrame * idleWidth, 0, idleWidth, idleHeight);

            //Draw onto screen
            spriteBatch.Draw(
                idleTexture,
                new Rectangle((int)Position.X, (int)Position.Y, idleWidth, idleHeight),
                source,
                Color.White,
                0f,
                new Vector2(0, 0),
                spriteEffect, 0);
        }

        /// <summary>
        /// Helper method to help condense Draw()
        /// Will draw the Walking animation for the protagonist
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawWalk(SpriteBatch spriteBatch)
        {
            //reset the other frames for animations cleanliness
            attackingFrame = 0;
            idleFrame = 0;

            //Update the frame
            if (animationTimer > animationSpeed)
            {
                animationTimer -= animationSpeed;
                walkingFrame++;
            }

            //loop back down to the first frame in the template
            if (walkingFrame > WalkingTotalFrames) { walkingFrame = 0; }

            //redefine the source rectangle because we are using a different template texture
            source = new Rectangle(walkingFrame * movingWidth, 0, movingWidth, movingHeight);

            //Draw onto the screen
            spriteBatch.Draw(
                WalkingTexture,
                new Rectangle((int)Position.X, (int)Position.Y, movingWidth, movingHeight),
                source,
                Color.White,
                0f,
                new Vector2(0, 0),
                spriteEffect, 0);
        }
    }
}
