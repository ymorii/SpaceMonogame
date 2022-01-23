﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Core;
using SpaceInvaders.Extensions;

namespace SpaceInvaders.Entity
{
    public abstract class Bullet : Entity
    {
        protected int _baseDamage = 1;

        public BulletStatisticsBase BulletStatistics { get; set; }

        public Bullet()
        {
        }
        public Bullet(Texture2D image, Vector2 position, Vector2 velocity)
        {
            _image = image;
            Position = position;
            Velocity = velocity;
            Orientation = Velocity.ToAngle();
            Radius = 8; //depends
        }

        public abstract int GetDamage();

        public override void Update()
        {
            if (Velocity.LengthSquared() > 0)
            {
                Orientation = Velocity.ToAngle();
            }
            
            Position += Velocity;

            //delete bullet that go off-screen
            if (!Game1.Viewport.Bounds.Contains(Position.ToPoint()))
            {
                IsExpired = true;
            }
        }
    }

    /* concrete classes */
    public class Rocket : Bullet
    {
        private const int rocketDamage = 1;
        public Rocket(Vector2 position, Vector2 velocity) : base(Art.Bullet, position, velocity)
        {
            BulletStatistics = new RocketStatistics();
        }

        public override int GetDamage()
        {
            return BulletStatistics.GetDamage();
        }
    }

    public class Bomb : Bullet
    {
        private const int bombDamage = 2;
        private int _framesToExplode = 50;
        public Bomb(Vector2 position, Vector2 velocity) : base(Art.Bomb, position, velocity * 0.5f)
        {
            BulletStatistics = new BombStatistics();
            Radius = 15;
        }

        public override void Update()
        {
            if (_framesToExplode <= 0)
            {
                //explode
                IsExpired = true;
            }
            else
            {
                --_framesToExplode;
            }

            base.Update();
        }

        public override int GetDamage()
        {
            return BulletStatistics.GetDamage();
        }
    }

    public class Laser : Bullet
    {
        private const int laserDamage = 1;
        public Laser(Vector2 position, Vector2 velocity) : base(Art.Bullet, position, velocity)
        {
        }

        public override int GetDamage()
        {
            return BulletStatistics.GetDamage();
        }
    }
}
