// ----------------------------------------------------------------------------
//
// Definition of the InstructionString class
// Date: 03.11.2018
// Author: Dorian Niclass
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
    class InstructionString : Executable
    {
        string VarValue;
        string VarName;
        bool Error = false;
        string ErrorMsg;

        public InstructionString(Node _nodeIntStatement, Subprogram _parentSubprogram)
        {
            for (int i = 0; i < _nodeIntStatement.GetChildCount(); i++)
            {
                switch (_nodeIntStatement.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.WORD:
                        Token token = (Token)_nodeIntStatement.GetChildAt(i);
                        VarName = token.GetImage();
                        break;
                    case (int)KropConstants.STRING_EXPRESSION:
                        VarValue = AlgorithmicExpression.CalculStringExpression(_nodeIntStatement.GetChildAt(i), _parentSubprogram);
                        break;

                }
            }

            if (!Subprogram.VarExists(VarName, _parentSubprogram))
            {
                if (VarValue != null)
                    _parentSubprogram.ListVar.Add(new StringVar(VarName, VarValue));
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
