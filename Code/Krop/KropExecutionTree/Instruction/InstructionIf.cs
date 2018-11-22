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
using System.Collections.Generic;

namespace Krop.KropExecutionTree.Instruction
{
    /// <summary>
    /// If branching instruction
    /// </summary>
    class InstructionIf : Executable
    {
        public Subprogram ParentSubprogram;
        private Dictionary<Measurable<Boolean>, string> Conds; // The boolean expression that decides of the branch
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
                                    this.Conds = Subprogram.SetCond(_nodeIfElseStatement.GetChildAt(i).GetChildAt(y), _parentSubprogram);
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

            Console.WriteLine(this.Conds.Count);
        }

        public override bool Execute()
        {
            bool result = true;

            foreach (Measurable<Boolean> cond in this.Conds.Keys)
            {
                bool evaluation = cond.Evaluate();

                if (this.Conds[cond] == "and")
                {
                    if (!result || !evaluation)
                        result = false;
                }else if (this.Conds[cond] == "or")
                {
                    if (!result && !evaluation)
                        result = false;
                    else
                        result = true;
                }
                else
                {
                    if (!evaluation)
                        result = false;
                }
                
            }

            if (result)
            {
                return IfBranch.Execute();
            }
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
