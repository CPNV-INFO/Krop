// ----------------------------------------------------------------------------
//
// Definition of the BooleanFunction class
// Date: May 2018
// Author: S. Gueissaz
// Modified By: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerCederberg.Grammatica.Runtime;
using PerCederberg.Grammatica.Runtime.RE;
using Krop.Krohonde;
using Krop.KropExecutionTree.AbstractClass;

namespace Krop.KropExecutionTree.Condition
{
    /// <summary>
    /// Holds a Boolean variable
    /// </summary>
    class BooleanFunction : Measurable<Boolean>
    {
        private string NameFunction;
        private Boolean IsNot;

        public BooleanFunction(string _nameFunction, Boolean _isNot)
        {
            NameFunction = _nameFunction;
            IsNot = _isNot;
        }

        public override bool Evaluate()
        {
            if (CanEvaluate())
            {
                Boolean result = false;

                switch (NameFunction)
                {
                    case "obstacleenface":
                        result = Game.ObstacleInFront();
                        break;
                    case "obstacleadroite":
                        result = Game.ObstacleOnRight();
                        break;
                    case "obstacleagauche":
                        result = Game.ObstacleOnLeft();
                        break;
                    case "surunepheromone":
                        result = Game.OnPheromone();
                        break;
                    default:
                        return false;
                }

                if (IsNot)
                    return !result;
                else
                    return result;
            }
            else
            {
                return false;
            }
        }
    }
}

