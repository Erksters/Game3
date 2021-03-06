using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using tainicom.Aether.Physics2D.Common;

namespace Game3
{
    public class Terrains
    {
        Vertices terrain = new Vertices();

        /// <summary>
        /// Get the terrains for the first stage
        /// </summary>
        /// <returns>A list container of Vertices</returns>
        public Vertices StageOneTerrain()
        {
            Vertices terrain = new Vertices();

            terrain.Add(new Vector2(-20f, 5f));
            terrain.Add(new Vector2(-20f, 0f));
            terrain.Add(new Vector2(20f, 0f));
            terrain.Add(new Vector2(25f, 0.25f));
            terrain.Add(new Vector2(30f, 1f));
            terrain.Add(new Vector2(35f, 4f));
            terrain.Add(new Vector2(40f, 0f));
            terrain.Add(new Vector2(45f, 0f));
            terrain.Add(new Vector2(50f, -1f));
            terrain.Add(new Vector2(55f, -2f));
            terrain.Add(new Vector2(60f, -2f));
            terrain.Add(new Vector2(65f, -1.25f));
            terrain.Add(new Vector2(70f, 0f));
            terrain.Add(new Vector2(75f, 0.3f));
            terrain.Add(new Vector2(80f, 1.5f));
            terrain.Add(new Vector2(85f, 3.5f));
            terrain.Add(new Vector2(90f, 0f));
            terrain.Add(new Vector2(95f, -0.5f));
            terrain.Add(new Vector2(100f, -1f));
            terrain.Add(new Vector2(105f, -2f));
            terrain.Add(new Vector2(110f, -2.5f));
            terrain.Add(new Vector2(115f, -1.3f));
            terrain.Add(new Vector2(120f, 0f));
            terrain.Add(new Vector2(160f, 0f));
            terrain.Add(new Vector2(159f, -10f));
            terrain.Add(new Vector2(201f, -10f));
            terrain.Add(new Vector2(200f, 0f));
            terrain.Add(new Vector2(240f, 0f));
            terrain.Add(new Vector2(250f, 5f));
            terrain.Add(new Vector2(250f, -10f));
            terrain.Add(new Vector2(270f, -10f));
            terrain.Add(new Vector2(270f, 0));
            terrain.Add(new Vector2(310f, 0));
            terrain.Add(new Vector2(310f, 5));

            return terrain;
        }
        
    }
}
