// ----------------------------------------------------------------------------
//
// Definition of the IntrustionIf class
// Date: May 2018
// Author: X. Carrel
// Modified By: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using PerCederberg.Grammatica.Runtime;
using Krop.KropGrammaticaParser;
using Krop.KropExecutionTree.AbstractClass;

namespace Krop.KropExecutionTree.Instruction
{
    /// <summary>
    /// If branching instruction
    /// </summary>
    class InstructionIf : Executable
    {
        public Subprogram ParentSubprogram;
        private Measurable<Boolean> Cond; // The boolean expression that decides of the branch
        private Subprogram IfBranch;
        private Subprogram ElseBranch;

        public InstructionIf(Node _nodeIfElseStatement, Subprogram _parentSubprogram)
        {
            ParentSubprogram = _parentSubprogram;
            //Construct the different If branches
            for (int i = 0; i < _nodeIfElseStatement.GetChildCount(); i++)
            {
                switch (_nodeIfElseStatement.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.IF_STATEMENT:
                        for(int y = 0; y < _nodeIfElseStatement.GetChildAt(i).GetChildCount(); y++)
                        {
                            switch (_nodeIfElseStatement.GetChildAt(i).GetChildAt(y).GetId())
                            {
                                case (int)KropConstants.CONDITON_STATEMENT:
                                    this.Cond = Subprogram.SetCond(_nodeIfElseStatement.GetChildAt(i).GetChildAt(y), _parentSubprogram);
                                    break;
                                case (int)KropConstants.PROGRAM:
                                    this.IfBranch = new Subprogram(_nodeIfElseStatement.GetChildAt(i).GetChildAt(y), _parentSubprogram);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case (int)KropConstants.ELSE_STATEMENT:
                        for (int y = 0; y < _nodeIfElseStatement.GetChildAt(i).GetChildCount(); y++)
                        {
                            switch (_nodeIfElseStatement.GetChildAt(i).GetChildAt(y).GetId())
                            {
                                case (int)KropConstants.PROGRAM:
                                    this.ElseBranch = new Subprogram(_nodeIfElseStatement.GetChildAt(i).GetChildAt(y), _parentSubprogram);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public override bool Execute()
        {
            if (Cond.Evaluate())
                return IfBranch.Execute();
            else
            {
                if (ElseBranch != null)
                    return ElseBranch.Execute();
                else
                    return true;
            }
        }
    }
}
