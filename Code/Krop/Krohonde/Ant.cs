// ----------------------------------------------------------------------------
//
// Definition of the Ant class
// Date: May 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Krop.ControlWindow;
using Krop.KropExecutionTree;

namespace Krop.Krohonde
{
    /// <summary>
    /// Hold a Ant
    /// </summary>
    public class Ant
    {
        //Ant orders
        public static bool IS_WALKING = false;
        public static bool IS_TURNING_RIGHT = false;
        public static bool IS_TURNING_LEFT = false;

        //Starting ant position
        private int StartPosX;
        private int StartPosY;
        private Direction StartDirection;

        //Sprite settings
        private Vector2 ScaleSprite = new Vector2(0.75f,0.75f);
        RectangleF sourceRec = new RectangleF(0 * Game.TILESIZE, 6 * Game.TILESIZE, Game.TILESIZE, Game.TILESIZE);
        private int TimerSwapSprite = 0;

        //Ant current position
        private Point Coord;
        private Vector2 Position;
        private Direction Direction;

        //Ant destination position
        private Point NewCoord;
        private Vector2 PositionGoTo;

        //Ant settings
        public bool IsWalking;
        private Color Color;
        private float Speed;

        /// <summary>
        /// Return Ant Direction
        /// </summary>
        /// <returns>Direction</returns>
        public Direction GetAntDirection()
        {
            return Direction;
        }

        /// <summary>
        /// Return Ant Coordinate X
        /// </summary>
        /// <returns>CoordX</returns>
        public int GetAntCoordX()
        {
            return Coord.X;
        }

        /// <summary>
        /// Return Ant Coordinate Y
        /// </summary>
        /// <returns>CoordY</returns>
        public int GetAntCoordY()
        {
            return Coord.Y;
        }
        
        /// <summary>
        ///  Anr Class constructor
        /// </summary>
        public Ant()
        {
            Color = Color.Black;
            NewCoord = Point.Empty;
            PositionGoTo = new Vector2();
            IsWalking = false;

            this.Speed = 1;
        }

        /// <summary>
        /// Drop the ant on a given position
        /// </summary>
        /// <param name="_PosX"></param>
        /// <param name="_PosY"></param>
        /// <param name="_direction"></param>
        public void PlaceAnt(int _PosX, int _PosY, Direction _direction)
        {
            //Reset global variables
            IS_WALKING = false;
            IS_TURNING_RIGHT = false;
            IS_TURNING_LEFT = false;

            //Save starting ant position
            StartPosX = _PosX;
            StartPosY = _PosY;
            StartDirection = _direction;

            //Set ant position
            IsWalking = false;
            Direction = _direction;
            Coord.X = _PosX;
            Coord.Y = _PosY;
            Position = new Vector2(Game.GRIDSIZE * Coord.X, Game.GRIDSIZE * Coord.Y);
            sourceRec = new RectangleF(0 * Game.TILESIZE, 6 * Game.TILESIZE, Game.TILESIZE, Game.TILESIZE);
        }

        /// <summary>
        /// Reset ant position
        /// </summary>
        public void ResetPlace()
        {
            PlaceAnt(StartPosX, StartPosY, StartDirection);
        }

        /// <summary>
        /// Draw ant on garden
        /// </summary>
        public void Draw()
        {
            GL.PushMatrix();

            GL.Translate(Position.X + Game.GRIDSIZE/2, Position.Y + Game.GRIDSIZE / 2, 0);
            GL.Rotate((float)Direction, 0, 0, 1);
            GL.Translate(-(Position.X + Game.GRIDSIZE / 2), -(Position.Y + Game.GRIDSIZE / 2), 0);

            Spritebatch.DrawSprite(Game.TILESET, new RectangleF(this.Position.X, this.Position.Y, Game.GRIDSIZE, Game.GRIDSIZE), this.Color, this.sourceRec);

            GL.PopMatrix();
        }

        /// <summary>
        /// Update ant sprite
        /// </summary>
        private void UpdateSprite()
        {
            if (IsWalking)
            {
                if (TimerSwapSprite < 12)
                    sourceRec = new RectangleF(1 * Game.TILESIZE, 6 * Game.TILESIZE, Game.TILESIZE, Game.TILESIZE);
                else
                    sourceRec = new RectangleF(2 * Game.TILESIZE, 6 * Game.TILESIZE, Game.TILESIZE, Game.TILESIZE);

                TimerSwapSprite++;

                if (TimerSwapSprite > 24)
                    TimerSwapSprite = 0;
            }
            else
            {
                sourceRec = new RectangleF(0 * Game.TILESIZE, 6 * Game.TILESIZE, Game.TILESIZE, Game.TILESIZE);
            }
        }

        /// <summary>
        /// Update ant position if the ant is walking
        /// </summary>
        private void UpdatePosition()
        {
            if (IsWalking)
            {
                if (Position != PositionGoTo)
                {
                    float moveDistance = this.Speed;

                    switch (Direction)
                    {
                        case Direction.North:
                            if (Position.Y - moveDistance < PositionGoTo.Y) Position.Y = PositionGoTo.Y;
                            else Position.Y -= moveDistance;

                            break;
                        case Direction.NorthEast:
                            if (Position.Y - moveDistance < PositionGoTo.Y) Position.Y = PositionGoTo.Y;
                            else Position.Y -= moveDistance;

                            if (Position.X + moveDistance > PositionGoTo.X) Position.X = PositionGoTo.X;
                            else Position.X += moveDistance;

                            break;
                        case Direction.East:
                            if (Position.X + moveDistance > PositionGoTo.X) Position.X = PositionGoTo.X;
                            else Position.X += moveDistance;

                            break;
                        case Direction.SouthEast:
                            if (Position.Y + moveDistance > PositionGoTo.Y) Position.Y = PositionGoTo.Y;
                            else Position.Y += moveDistance;

                            if (Position.X + moveDistance > PositionGoTo.X) Position.X = PositionGoTo.X;
                            else Position.X += moveDistance;

                            break;
                        case Direction.South:
                            if (Position.Y + moveDistance > PositionGoTo.Y) Position.Y = PositionGoTo.Y;
                            else Position.Y += moveDistance;

                            break;
                        case Direction.SouthWest:
                            if (Position.Y + moveDistance > PositionGoTo.Y) Position.Y = PositionGoTo.Y;
                            else Position.Y += moveDistance;

                            if (Position.X - moveDistance < PositionGoTo.X) Position.X = PositionGoTo.X;
                            else Position.X -= moveDistance;

                            break;
                        case Direction.West:
                            if (Position.X - moveDistance < PositionGoTo.X) Position.X = PositionGoTo.X;
                            else Position.X -= moveDistance;

                            break;
                        case Direction.NorthWest:
                            if (Position.Y - moveDistance < PositionGoTo.Y) Position.Y = PositionGoTo.Y;
                            else Position.Y -= moveDistance;

                            if (Position.X - moveDistance < PositionGoTo.X) Position.X = PositionGoTo.X;
                            else Position.X -= moveDistance;

                            break;
                    }
                }
                else
                {
                    switch (Direction)
                    {
                        case Direction.North:
                            Coord.Y--;
                            break;
                        case Direction.NorthEast:
                            Coord.Y--;
                            Coord.X++;
                            break;
                        case Direction.East:
                            Coord.X++;
                            break;
                        case Direction.SouthEast:
                            Coord.Y++;
                            Coord.X++;
                            break;
                        case Direction.South:
                            Coord.Y++;
                            break;
                        case Direction.SouthWest:
                            Coord.Y++;
                            Coord.X--;
                            break;
                        case Direction.West:
                            Coord.X--;
                            break;
                        case Direction.NorthWest:
                            Coord.Y--;
                            Coord.X--;
                            break;
                    }

                    this.NewCoord = Point.Empty;
                    IsWalking = false;
                    FormControlWindow.PENDING_INSTRUCTION = false;
                }
            }
        }

        /// <summary>
        /// Ant starts to walk
        /// </summary>
        private void Walk()
        {
            Point newCoord = Coord;
            IS_WALKING = false;

            switch (Direction)
            {
                case Direction.North:
                    newCoord.Y--;
                    break;
                case Direction.NorthEast:
                    newCoord.Y--;
                    newCoord.X++;
                    break;
                case Direction.East:
                    newCoord.X++;
                    break;
                case Direction.SouthEast:
                    newCoord.Y++;
                    newCoord.X++;
                    break;
                case Direction.South:
                    newCoord.Y++;
                    break;
                case Direction.SouthWest:
                    newCoord.Y++;
                    newCoord.X--;
                    break;
                case Direction.West:
                    newCoord.X--;
                    break;
                case Direction.NorthWest:
                    newCoord.Y--;
                    newCoord.X--;
                    break;
            }

            //Check if destination is free
            if (Game.PlaceIsFree(newCoord.X, newCoord.Y))
            {
                IsWalking = true;
                NewCoord = newCoord;
                PositionGoTo.X = Game.GRIDSIZE * NewCoord.X;
                PositionGoTo.Y = Game.GRIDSIZE * NewCoord.Y;
            }
            else
            {
                FormControlWindow.PENDING_INSTRUCTION = false;
            }
        }

        /// <summary>
        /// Ant turns right
        /// </summary>
        private void TurningRight()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.East;
                    break;
                case Direction.East:
                    Direction = Direction.South;
                    break;
                case Direction.South:
                    Direction = Direction.West;
                    break;
                case Direction.West:
                    Direction = Direction.North;
                    break;
                default:
                    break;
            }

            IS_TURNING_RIGHT = false;
            FormControlWindow.PENDING_INSTRUCTION = false;
        }

        /// <summary>
        /// Ant turns left
        /// </summary>
        private void TurningLeft()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.West;
                    break;
                case Direction.West:
                    Direction = Direction.South;
                    break;
                case Direction.South:
                    Direction = Direction.East;
                    break;
                case Direction.East:
                    Direction = Direction.North;
                    break;
                default:
                    break;
            }

            IS_TURNING_LEFT = false;
            FormControlWindow.PENDING_INSTRUCTION = false;
        }

        /// <summary>
        /// Update on each update frame
        /// </summary>
        public void Update()
        {
            if (IS_WALKING) Walk();
            if (IS_TURNING_RIGHT) TurningRight();
            if (IS_TURNING_LEFT) TurningLeft();
            UpdatePosition();
            UpdateSprite();
        }

        public void SetSpeed(float speed)
        {
            this.Speed = speed;
        }
    }
}
