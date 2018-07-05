// ----------------------------------------------------------------------------
//
// Definition of the Level class
// Date: January 2015
// Author: Sil Tutorials
// Modified By: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using System.IO;

namespace Krop.Krohonde
{
    struct Level
    {
        private Block[,] grid;
        private string filePath;

        public int Width
        {
            get
            {
                return grid.GetLength(0);
            }
        }
        public int Height
        {
            get
            {
                return grid.GetLength(1);
            }
        }
        public string FilePath
        {
            get { return filePath; }
        }

        /// <summary>
        /// Load the default garden
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Level(int width, int height)
        {
            filePath = "none";
            this.grid = new Block[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    this.grid[x, y] = new Block(BlockType.Grass, x, y);
                }
            }
            Game.ANT.PlaceAnt(0, 0, Direction.East);
        }

        /// <summary>
        /// Load the selected garden
        /// </summary>
        /// <param name="filePath"></param>
        public Level(string filePath)
        {
            this.filePath = filePath;

            try
            {
                    #region Loading Level
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;

                        int width = Game.WIDTHGARDEN;
                        int height = Game.HEIGHTGARDEN;
 
                        grid = new Block[width, height];
                        line = reader.ReadLine();

                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < width; x++)
                            {
                                char current = line[x];

                                switch (current)
                                {
                                    case '.':
                                        grid[x, y] = new Block(BlockType.Grass, x, y);
                                        break;
                                    case 'R':
                                        grid[x, y] = new Block(BlockType.Stone, x, y);
                                        break;
                                    case 'A':
                                        grid[x, y] = new Block(BlockType.Dirt, x, y);
                                        break;
                                    case 'P':
                                        grid[x, y] = new Block(BlockType.Pheromone, x, y);
                                        break;
                                    case 'N':
                                        grid[x, y] = new Block(BlockType.Grass, x, y);
                                        Game.ANT.PlaceAnt(x, y, Direction.North);
                                        break;
                                    case 'E':
                                        grid[x, y] = new Block(BlockType.Grass, x, y);
                                        Game.ANT.PlaceAnt(x, y, Direction.East);
                                        break;
                                    case 'S':
                                        grid[x, y] = new Block(BlockType.Grass, x, y);
                                        Game.ANT.PlaceAnt(x, y, Direction.South);
                                        break;
                                    case 'W':
                                        grid[x, y] = new Block(BlockType.Grass, x, y);
                                        Game.ANT.PlaceAnt(x, y, Direction.West);
                                        break;
                                    default:
                                        grid[x, y] = new Block(BlockType.Empty, x, y);
                                        break;
                                }
                            }

                            line = reader.ReadLine();
                        }
                    }
                    #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong while loading '{0}'.", filePath);
                Console.WriteLine("Exception was: '{0}'", e);

                #region Create Empty Room
                //We'll create an empty level with size 20,20
                this.grid = new Block[Game.WIDTHGARDEN, Game.HEIGHTGARDEN];

                for (int x = 0; x < Game.WIDTHGARDEN; x++)
                {
                    for (int y = 0; y < Game.HEIGHTGARDEN; y++)
                    {
                        this.grid[x, y] = new Block(BlockType.Grass, x, y);
                    }
                }
                #endregion
            }
        }

        public Block this[int _posX, int _posY]
        {
            get
            {
                if (_posX >= 0 && _posY >= 0 && _posX < this.Width && _posY < this.Height)
                {
                    return grid[_posX, _posY];
                }
                else
                {
                    return new Block(BlockType.Empty, _posX, _posY);
                }
            }
            set
            {
                if (_posX >= 0 && _posY >= 0 && _posX < this.Width && _posY < this.Height)
                {
                    grid[_posX, _posY] = value;
                }
            }
        }
    }

    /// <summary>
    /// Possible type of a garden square
    /// </summary>
    public enum BlockType
    {
        Empty,
        Grass,
        Stone,
        Dirt,
        Pheromone,
    }

    /// <summary>
    /// Square of the garden
    /// </summary>
    struct Block
    {
        public BlockType Type;
        public int PosX, PosY;
        private bool Solid;
        private bool Pheromone;
        

        public bool IsSolid
        {
            get { return Solid; }
        }

        public bool IsPheromone
        {
            get { return Pheromone; }
        }

        public Block(BlockType _type, int _posX, int _posY)
        {
            this.Type = _type;
            this.PosX = _posX;
            this.PosY = _posY;
            this.Solid = false;
            this.Pheromone = false;

            if (Type == BlockType.Empty)
                this.Solid = true;
            if (Type == BlockType.Stone)
                this.Solid = true;
            if (Type == BlockType.Dirt)
                this.Solid = true;
            if (Type == BlockType.Pheromone)
                this.Pheromone = true;
        } 
    }
}
