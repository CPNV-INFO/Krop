// ----------------------------------------------------------------------------
//
// Definition of the InstructionInt class
// Date: June 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using PerCederberg.Grammatica.Runtime;
using Krop.ControlWindow;
using Krop.KropGrammaticaParser;
using Krop.KropExecutionTree.AbstractClass;
using Krop.KropExecutionTree.Variable;

namespace Krop.KropExecutionTree.Instruction
{
    /// <summary>
    /// Declaration of an Int variable instruction
    /// </summary>
    class InstructionInt : Executable
    {
        int? VarValue;
        string VarName;
        bool Error = false;
        string ErrorMsg;

        public InstructionInt(Node _nodeIntStatement, Subprogram _parentSubprogram)
        {
            for (int i = 0; i < _nodeIntStatement.GetChildCount(); i++)
            {
                switch (_nodeIntStatement.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.WORD:
                        Token token = (Token)_nodeIntStatement.GetChildAt(i);
                        VarName = token.GetImage();
                        break;
                    case (int)KropConstants.EXPRESSION:
                        VarValue = AlgorithmicExpression.CalculExpression(_nodeIntStatement.GetChildAt(i), _parentSubprogram);
                        break;

                }
            }

            if (!Subprogram.VarExists(VarName, _parentSubprogram))
            {
                if (VarValue != null)
                    _parentSubprogram.ListVar.Add(new IntVar(VarName, VarValue));
                else
                {
                    Error = true;
                    ErrorMsg = "Variable " + VarName + " ne peut pas être égal à NULL.";
                }
            }
            else
            {
                Error = true;
                ErrorMsg = "Variable " + VarName + " existe déjà.";
            }
        }

        public override bool Execute()
        {
            if (!Error)
                return true;
            else
            {
                FormControlWindow.TerminalWriteLine("VarError : " + ErrorMsg);
                return false;
            }
        }
    }
}
