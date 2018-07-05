// ----------------------------------------------------------------------------
//
// Definition of the Game class
// Date: May 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using Krop.ControlWindow;
using System.ComponentModel;

namespace Krop.Krohonde
{
    /// <summary>
    /// Enum of the 8 cardinal points
    /// </summary>
    public enum Direction
    {
        North = 0,
        NorthEast = 45,
        East = 90,
        SouthEast = 135,
        South = 180,
        SouthWest = 225,
        West = 270,
        NorthWest = 315,
    }

    /// <summary>
    /// Primary class of the project
    /// </summary>
    public class Game : GameWindow
    {
        public static bool EXIT_KROHONDE = false;   //Exit Krohonde if true   
        public static int WIDTHWINDOW = 960;    //Default window width
        public static int HEIGHTWINDOW = 720;   //Default window height
        public static int GRIDSIZE = 24;        //Width of a grid square
        public static int HEIGHTGARDEN = 30;    //Number of grid rows
        public static int WIDTHGARDEN = 40;     //Number of columns of the grid
        public static int TILESIZE = 32;        //Width of a Tile in the Content/Krohonde_Sheet_32.png
        public static Texture2D TILESET;        //Stock the sprite sheet
        public static Ant ANT = new Ant();      //Ant object
        private static Level GARDEN;            //Garden grid

        /// <summary>
        /// Create the game window
        /// </summary>
        /// <param name="width">Number of grid columns</param>
        /// <param name="height">Number of grid rows</param>
        public Game(int width, int height)
            : base(width, height)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            this.Icon = new Icon("Content/kropIcon.ico");
            this.Title = "Krohonde";                //Set the window title
            this.WindowBorder = WindowBorder.Fixed; //Block the window resize
            this.Location = new Point(0, 0);        //Set the window location at high left of the screen
        }

        /// <summary>
        /// Start when the program loads
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            TILESET = ContentPipe.LoadTexture("Krohonde_Sheet_32.png"); //Load the sprite sheet
        }

        /// <summary>
        /// Close Krop
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Start on each update frame
        /// This function starts all the calculation functions of the differents garden elements like the new position, the sprite 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (EXIT_KROHONDE) this.Exit();
            if(!FormControlWindow.IS_PAUSING && !FormControlWindow.IS_STOPPING) ANT.Update();
        }

        /// <summary>
        /// Manage the elements display on the window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.CornflowerBlue);

            Matrix4 world = Matrix4.CreateOrthographicOffCenter(0, this.Width, this.Height, 0, 0, 1);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref world);

            //Display the garden
            for (int x = 0; x < GARDEN.Width; x++)
            {
                for (int y = 0; y < GARDEN.Height; y++)
                {
                    RectangleF sourceRec = new RectangleF(0, 0, 0, 0);

                    switch (GARDEN[x, y].Type)
                    {
                        case BlockType.Grass:
                            sourceRec = new RectangleF(1 * TILESIZE, 1 * TILESIZE, TILESIZE, TILESIZE);
                            break;
                        case BlockType.Stone:
                            sourceRec = new RectangleF(6 * TILESIZE, 4 * TILESIZE, TILESIZE, TILESIZE);
                            break;
                        case BlockType.Dirt:
                            sourceRec = new RectangleF(6 * TILESIZE, 1 * TILESIZE, TILESIZE, TILESIZE);
                            break;
                        case BlockType.Pheromone:
                            sourceRec = new RectangleF(3 * TILESIZE, 5 * TILESIZE, TILESIZE, TILESIZE);
                            break;
                    }

                    Spritebatch.DrawSprite(TILESET, new RectangleF(x * GRIDSIZE, y * GRIDSIZE, GRIDSIZE, GRIDSIZE), Color.Transparent, sourceRec);  //Display a grid square
                }
            }

            ANT.Draw();

            this.SwapBuffers();
        }

        /// <summary>
        /// Check if the grid square is of type grass
        /// </summary>
        /// <param name="_posX"></param>
        /// <param name="_posY"></param>
        /// <returns>!IsSolid of the block</returns>
        public static bool PlaceIsFree(int _posX, int _posY)
        {
            return !GARDEN[_posX, _posY].IsSolid;
        }

        /// <summary>
        /// Check if the ant is on a pheromone
        /// </summary>
        /// <returns>IsPheromone of the block</returns>
        public static bool OnPheromone()
        {
            int coordX = ANT.GetAntCoordX();
            int coordY = ANT.GetAntCoordY();

            return GARDEN[coordX, coordY].IsPheromone;
        }

        /// <summary>
        /// Drop a pheromone below the ant
        /// </summary>
        public static void DropPheromone()
        {
            int coordX = ANT.GetAntCoordX();
            int coordY = ANT.GetAntCoordY();

            GARDEN[coordX, coordY] = new Block(BlockType.Pheromone, coordX, coordY);

            FormControlWindow.PENDING_INSTRUCTION = false;
        }

        /// <summary>
        /// Take a pheromone below the ant
        /// </summary>
        public static void TakePheromone()
        {
            int coordX = ANT.GetAntCoordX();
            int coordY = ANT.GetAntCoordY();

            GARDEN[coordX, coordY] = new Block(BlockType.Grass, coordX, coordY);

            FormControlWindow.PENDING_INSTRUCTION = false;
        }

        /// <summary>
        /// Check if there is an obstacle in front of the ant
        /// </summary>
        /// <returns>IsSolid of the block</returns>
        public static bool ObstacleInFront()
        {
            int coordX = ANT.GetAntCoordX();
            int coordY = ANT.GetAntCoordY();

            switch (ANT.GetAntDirection())
            {
                case Direction.North:
                    coordY--;
                    break;
                case Direction.East:
                    coordX++;
                    break;
                case Direction.South:
                    coordY++;
                    break;
                case Direction.West:
                    coordX--;
                    break;
            }

            return GARDEN[coordX, coordY].IsSolid;
        }

        /// <summary>
        /// Check if there is an obstacle on the right of the ant
        /// </summary>
        /// <returns>IsSolid of the block</returns>
        public static bool ObstacleOnRight()
        {
            int coordX = ANT.GetAntCoordX();
            int coordY = ANT.GetAntCoordY();


            switch (ANT.GetAntDirection())
            {
                case Direction.North:
                    coordX++;
                    break;
                case Direction.East:
                    coordY++;
                    break;
                case Direction.South:
                    coordX--;
                    break;
                case Direction.West:
                    coordY--;
                    break;
            }

            return GARDEN[coordX, coordY].IsSolid;
        }

        /// <summary>
        /// Check if there is an obstacle on the left of the ant
        /// </summary>
        /// <returns>IsSolid of the block</returns>
        public static bool ObstacleOnLeft()
        {
            int coordX = ANT.GetAntCoordX();
            int coordY = ANT.GetAntCoordY();


            switch (ANT.GetAntDirection())
            {
                case Direction.North:
                    coordX--;
                    break;
                case Direction.East:
                    coordY--;
                    break;
                case Direction.South:
                    coordX++;
                    break;
                case Direction.West:
                    coordY++;
                    break;
            }

            return GARDEN[coordX, coordY].IsSolid;
        }

        /// <summary>
        /// Load a garden
        /// </summary>
        /// <param name="_path">Name of the file containing garden </param>
        public static void ChangeGarden(string _path)
        {
            GARDEN = new Level(Directory.GetParent(Application.ExecutablePath).ToString() + @"\Garden\" + _path);
        }

        /// <summary>
        /// Load default garden
        /// </summary>
        /// <param name="_x">Width of the garden</param>
        /// <param name="_y">Height of the garden</param>
        public static void ChangeGarden(int _x, int _y)
        {
            GARDEN = new Level(_x, _y);
        }
    }
}
