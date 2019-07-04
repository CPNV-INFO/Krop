// ----------------------------------------------------------------------------
//
// Definition of the Command class
// Date: May 2018
// Author: X. Carrel
// Modified By: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using PerCederberg.Grammatica.Runtime;
using Krop.ControlWindow;
using Krop.Krohonde;
using Krop.KropExecutionTree.AbstractClass;

namespace Krop.KropExecutionTree.Instruction
{
    /// <summary>
    /// Holds a single known command
    /// </summary>
    class Command : Executable
    {
        private string Name;

        public Command(Token _tokenInstruction)
        {
            Name = _tokenInstruction.GetImage();
        }
        public override bool Execute()
        {
            if (CanExecute())
            {
                Console.WriteLine(string.Format("Command: {0}", Name));

                FormControlWindow.PENDING_INSTRUCTION = true;

                switch (Name)
                {
                    case "avancer":
                        Ant.IS_WALKING = true;
                        return true;
                    case "tourneradroite":
                        Ant.IS_TURNING_RIGHT = true;
                        return true;
                    case "tourneragauche":
                        Ant.IS_TURNING_LEFT = true;
                        return true;
                    case "poserpheromone":
                        Game.DropPheromone();
                        return true;
                    case "prendrepheromone":
                        Game.TakePheromone();
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }

            
        }
    }
}
