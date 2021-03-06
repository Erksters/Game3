//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;
//using tainicom.Aether.Physics2D.Dynamics;
//using tainicom.Aether.Physics2D.Dynamics.Contacts;

//namespace Game3
//{
//    class Protagonist
//    {
//        public Game game;

//        #region Animation
//        /// <summary>
//        /// Helps flip the animation in the Draw()
//        /// </summary>
//        SpriteEffects spriteEffect;

//        /// <summary>
//        /// Determines which frame to draw in the template
//        /// </summary>
//        Rectangle source;

//        /// <summary>
//        /// texture object to display in the Draw()
//        /// </summary>
//        private Texture2D idleTexture;

//        /// <summary>
//        /// texture object to display in the Draw()
//        /// </summary>
//        private Texture2D WalkingTexture;

//        /// <summary>
//        /// texture object to display in the Draw()
//        /// </summary>
//        private Texture2D AttackTexture;

//        /// <summary>
//        /// Helps animate the sprite
//        /// </summary>
//        private double animationTimer;

//        /// <summary>
//        /// Helps animate the sprite
//        /// </summary>
//        private short idleFrame;

//        /// <summary>
//        /// Helps animate the sprite
//        /// </summary>
//        private short walkingFrame;

//        /// <summary>
//        /// Helps animate the sprite
//        /// </summary>
//        private short attackingFrame;

//        /// <summary>
//        /// Helps animate the sprite
//        /// will help loop the animation sprite back to frame 0
//        /// </summary>
//        private int idleTotalFrames = 10;

//        /// <summary>
//        /// Helps animate the sprite
//        /// will help loop the animation sprite back to frame 0
//        /// </summary>
//        private int WalkingTotalFrames = 12;

//        /// <summary>
//        /// Helps animate the sprite
//        /// will help loop the animation sprite back to frame 0
//        /// </summary>
//        private int AttackingTotalFrames = 17;

//        /// <summary>
//        /// Helps animate the sprite
//        /// Determines how quickly the draw() goes through frames
//        /// Smaller is faster
//        /// </summary>
//        private double animationSpeed = 0.1;

//        /// <summary>
//        /// height of the animations sprite
//        /// </summary>
//        private int idleHeight = 32;

//        /// <summary>
//        /// width of the animations sprite
//        /// </summary>
//        private int idleWidth = 24;

//        /// <summary>
//        /// width of the animations sprite
//        /// </summary>
//        private int movingWidth = 22;

//        /// <summary>
//        /// width of the animations sprite
//        /// </summary>
//        private int movingHeight = 33;

//        /// <summary>
//        /// width of the animations sprite
//        /// </summary>
//        private int AttackingHeight = 37;

//        /// <summary>
//        /// width of the animations sprite
//        /// </summary>
//        private int AttackingWidth = 43;
//        #endregion

//        #region State Of Character
//        /// <summary>
//        /// Vector2 Direction for sprite velocity
//        /// </summary>
//        public ProtagonistState HorizontalStatus = ProtagonistState.Idle;

//        /// <summary>
//        /// Which way is the player facing
//        /// When holding the Left Key, it is true.
//        /// When holding the Right Key, it is false.
//        /// </summary>
//        public bool Flipped;

//        /// <summary>
//        /// Helps determine if the player is on a platform
//        /// if True, the use may apply jump force
//        /// if False, you cannot jump in mid-air
//        /// </summary>
//        public bool OnPlatform;

//        /// <summary>
//        /// Determines if we're jumping
//        /// </summary>
//        public JumpState JumpStatus;

//        /// <summary>
//        /// Determines if our player is holding shift
//        /// </summary>
//        public bool Shift;
//        #endregion

//        #region Environmental Bounds

//        /// <summary>
//        /// Attack hit boxes
//        /// </summary>
//        //public BoundingRectangle AttackBounds;
//        #endregion

//        #region Velocity Constants
//        /// <summary>
//        /// Used for constant application of speed onto Position property
//        /// </summary>
//        public Vector2 HorizontalVelocity = new Vector2(3, 0);

//        /// <summary>
//        /// The value of Jumping in this environment
//        /// </summary>
//        public Vector2 Jump = new Vector2(0, -150);

//        /// <summary>
//        /// Determines where to draw the sprite
//        /// </summary>
//        private Vector2 Position;

//        /// <summary>
//        /// Helper attribute for ResetGame()
//        /// </summary>
//        private Vector2 initialPosition;

//        /// <summary>
//        /// Velocity helper and used for speed item upgrades
//        /// </summary>
//        private int SpeedMultiplier = 1;
//        #endregion

//        /// <summary>
//        /// Will prevent the user from jumping infinitely
//        /// </summary>
//        private double jumpingTimer = 0;

//        /// <summary>
//        /// public constructor
//        /// </summary>
//        public Protagonist(Game game, Vector2 initialPosition)
//        {
//            this.game = game;
//            Position = initialPosition;
//            initialPosition = Position;
//            RectangleBounds = new BoundingRectangle(initialPosition, idleWidth, idleHeight);
//        }

//        /// <summary>
//        /// The Update method to move, jump, ...etc 
//        /// the protagonist sprite
//        /// </summary>
//        /// <param name="gameTime">the game time</param>
//        /// <param name="movement">enum which direction we need to move</param>
//        /// <param name="moving">to help animate the character sprite</param>
//        /// <param name="flipTexture">to help animate the character sprite</param>
//        /// <param name="jumping">determines if we are actively holding the up key</param>
//        /// <param name="attacking">determines if we are pressing the attack key</param>
//        /// <param name="pressingShift">determines if we are pressing the shift key</param>
//        public void Update(GameTime gameTime, ProtagonistState horizontalStatus, JumpState jumpStatus, bool flipTexture, bool pressingShift, bool onPlatform, bool stopLeftMovement, bool stopRightMovement, bool stopJump, bool stopFall, bool stopMoving)
//        {
//            #region Load in Updates
//            //Update state of protagonist
//            HorizontalStatus = horizontalStatus;
//            Flipped = flipTexture;
//            JumpStatus = jumpStatus;
//            Shift = pressingShift;
//            OnPlatform = onPlatform;
//            #endregion

//            if (stopMoving)
//            {
//                return;
//            }

//            //Horizontal Movements
//            if (HorizontalStatus == ProtagonistState.Walking)
//            {
//                switch (Flipped)
//                {
//                    case true:
//                        Position += -HorizontalVelocity * SpeedMultiplier;
//                        updateBounds();
//                        break;

//                    case false:
//                        Position += HorizontalVelocity * SpeedMultiplier;
//                        updateBounds();
//                        break;
//                }
//            }

//            //Vertical Movements
//            jumpingTimer += gameTime.ElapsedGameTime.TotalSeconds;

//            //HandleJump
//            //if (JumpStatus == JumpState.Jumping && OnPlatform && !stopJump) 
//            //{
//            //    jump(gameTime);

//            //    //If we need to stop jumping
//            //    if(jumpingTimer > 3) 
//            //    { 
//            //        jumpingTimer = 0;
//            //        Jump = new Vector2(0, -150);
//            //    }
//            //    OnPlatform = false;
//            //}


//            //TODO: Determine if we want to always apply gravity
//            //I don't think we should
//            if (OnPlatform == false && JumpStatus == JumpState.Falling)
//            {
//                applyGravity(gameTime);
//            }

//            //We are falling, We landed, so keep the horizontal 
//            //Velocity, but reset gravity and jumping forces
//            if (stopFall)
//            {
//                Velocity = new Vector2(Velocity.X, 0);
//            }
//        }

//        /// <summary>
//        /// Helper method to place the force of a jump on our protagonists position
//        /// </summary>
//        /// <param name="gameTime"></param>
//        private void jump(GameTime gameTime)
//        {
//            if (Jump.Y > 0) { return; }
//            Position += Jump * (float)gameTime.ElapsedGameTime.TotalSeconds;
//            updateBounds();
//            //Decrement the Jump power 
//            Jump += new Vector2(0, 1);
//        }

//        /// <summary>
//        /// Helper function for applying gravity onto our position attribute
//        /// Used in Update()
//        /// </summary>
//        /// <param name="gameTime"></param>
//        private void applyGravity(GameTime gameTime)
//        {
//            Velocity += Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
//            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
//            updateBounds();
//        }

//        /// <summary>
//        /// Helper method to update the bounds after updating the Position
//        /// </summary>
//        private void updateBounds()
//        {
//            RectangleBounds.X += Position.X;
//            RectangleBounds.Y += Position.Y;
//        }

//        /// <summary>
//        /// The drawing method to display the protagonist
//        /// </summary>
//        /// <param name="gameTime"></param>
//        /// <param name="spriteBatch"></param>
//        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
//        {
//            //Update animation timer
//            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
//            handleSpriteEffect();

//            if (HorizontalStatus == ProtagonistState.Attacking)
//            {
//                DrawAttack(spriteBatch);
//            }
//            else if (HorizontalStatus == ProtagonistState.Walking)
//            {
//                DrawWalk(spriteBatch);
//            }
//            else
//            {
//                DrawIdle(spriteBatch);
//            }
//        }

//        /// <summary>
//        /// Helper method to help condense Draw()
//        /// Will draw the Attack animation for the protagonist
//        /// </summary>
//        /// <param name="spriteBatch"></param>
//        private void DrawAttack(SpriteBatch spriteBatch)
//        {
//            //Reset the other frames for cleanliness
//            walkingFrame = 0;
//            idleFrame = 0;

//            //Update the frame
//            if (animationTimer > animationSpeed)
//            {
//                animationTimer -= animationSpeed;
//                attackingFrame++;
//            }

//            //Loop the frame back to the first image in the texture template
//            if (attackingFrame > AttackingTotalFrames) { attackingFrame = 0; }

//            //Redefine the source rectangle because we are using a different template texture
//            source = new Rectangle(attackingFrame * AttackingWidth, 0, AttackingWidth, AttackingHeight);

//            //Draw onto screen :)
//            spriteBatch.Draw(
//                AttackTexture,
//                new Rectangle((int)Position.X, (int)Position.Y, AttackingWidth, AttackingHeight),
//                source,
//                Color.White,
//                0f,
//                new Vector2(0, 0),
//                spriteEffect, 0);
//        }

//        /// <summary>
//        /// Helper method to help condense Draw()
//        /// Will draw the Idle animation for the protagonist
//        /// </summary>
//        /// <param name="spriteBatch"></param>
//        private void DrawIdle(SpriteBatch spriteBatch)
//        {
//            //reset the other frames for animations cleanliness
//            walkingFrame = 0;
//            attackingFrame = 0;

//            //Update the frame
//            if (animationTimer > animationSpeed)
//            {
//                animationTimer -= animationSpeed;
//                idleFrame++;
//            }

//            //loop back down to the first frame in the template
//            if (idleFrame > idleTotalFrames) { idleFrame = 0; }

//            //Redefine the source rectangle because we are using a different template texture
//            source = new Rectangle(idleFrame * idleWidth, 0, idleWidth, idleHeight);

//            //Draw onto screen
//            spriteBatch.Draw(
//                idleTexture,
//                new Rectangle((int)Position.X, (int)Position.Y, idleWidth, idleHeight),
//                source,
//                Color.White,
//                0f,
//                new Vector2(0, 0),
//                spriteEffect, 0);
//        }

//        /// <summary>
//        /// Helper method to help condense Draw()
//        /// Will draw the Walking animation for the protagonist
//        /// </summary>
//        /// <param name="spriteBatch"></param>
//        private void DrawWalk(SpriteBatch spriteBatch)
//        {
//            //reset the other frames for animations cleanliness
//            attackingFrame = 0;
//            idleFrame = 0;

//            //Update the frame
//            if (animationTimer > animationSpeed)
//            {
//                animationTimer -= animationSpeed;
//                walkingFrame++;
//            }

//            //loop back down to the first frame in the template
//            if (walkingFrame > WalkingTotalFrames) { walkingFrame = 0; }

//            //redefine the source rectangle because we are using a different template texture
//            source = new Rectangle(walkingFrame * movingWidth, 0, movingWidth, movingHeight);

//            //Draw onto the screen
//            spriteBatch.Draw(
//                WalkingTexture,
//                new Rectangle((int)Position.X, (int)Position.Y, movingWidth, movingHeight),
//                source,
//                Color.White,
//                0f,
//                new Vector2(0, 0),
//                spriteEffect, 0);
//        }

//        /// <summary>
//        /// Resets the state of the protagonist
//        /// </summary>
//        public void ResetGame()
//        {
//            Position = initialPosition;
//            RectangleBounds = new BoundingRectangle(initialPosition, idleWidth, idleHeight);
//            Shift = false;
//            SpeedMultiplier = 1;
//            HorizontalStatus = ProtagonistState.Idle;
//        }

//        /// <summary>
//        /// Load the texture sprite for the animation
//        /// </summary>
//        /// <param name="content"></param>
//        public void LoadContent(ContentManager content)
//        {
//            idleTexture = content.Load<Texture2D>("SkeletonIdle");
//            WalkingTexture = content.Load<Texture2D>("Skeleton Walk");
//            AttackTexture = content.Load<Texture2D>("Skeleton Attack");
//        }

//        /// <summary>
//        /// Helper function to determine if we need to flip the direction of the animation
//        /// Used in Draw()
//        /// </summary>
//        /// <param name="flipped"></param>
//        private void handleSpriteEffect()
//        {
//            if (Flipped)
//            {
//                spriteEffect = SpriteEffects.FlipHorizontally;
//            }
//            else
//            {
//                spriteEffect = SpriteEffects.None;
//            }
//        }

//        /// <summary>
//        /// Helper function to determine if the texture objects loaded correctly
//        /// Used in LoadContent()
//        /// </summary>
//        private void checkTextures()
//        {
//            if (idleTexture is null) throw new InvalidOperationException("Texture must be loaded to render");
//            if (WalkingTexture is null) throw new InvalidOperationException("Texture must be loaded to render");
//            if (AttackTexture is null) throw new InvalidOperationException("Texture must be loaded to render");
//        }
//    }
//}
