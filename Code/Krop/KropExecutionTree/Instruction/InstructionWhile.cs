// ----------------------------------------------------------------------------
//
// Definition of the InstructionWhile class
// Date: May 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using PerCederberg.Grammatica.Runtime;
using Krop.KropGrammaticaParser;
using Krop.KropExecutionTree.AbstractClass;

namespace Krop.KropExecutionTree.Instruction
{
    /// <summary>
    /// While branching instruction
    /// </summary>
    class InstructionWhile : Executable
    {
        public Subprogram ParentSubprogram;
        private Measurable<Boolean> Cond; // The boolean expression that decides of the branch
        private Subprogram WhileBranch;

        public InstructionWhile(Node _nodeWhileStatement, Subprogram _parentSubprogram)
        {
            ParentSubprogram = _parentSubprogram;

            //Construct the While branch
            for (int i = 0; i < _nodeWhileStatement.GetChildCount(); i++)
            {
                switch (_nodeWhileStatement.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.CONDITON_STATEMENT:
                        this.Cond = Subprogram.SetCond(_nodeWhileStatement.GetChildAt(i), _parentSubprogram);
                        break;
                    case (int)KropConstants.PROGRAM:
                        this.WhileBranch = new Subprogram(_nodeWhileStatement.GetChildAt(i), _parentSubprogram);
                        break;
                    default:
                        break;
                }
            }
        }

        public override bool Execute()
        {
            while (Cond.Evaluate())
            {
                if (!WhileBranch.Execute()) return false;
            }

            return true;
        }
    }
}
