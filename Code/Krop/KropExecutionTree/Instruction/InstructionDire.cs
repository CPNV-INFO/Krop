// ----------------------------------------------------------------------------
//
// Definition of the IntrustionDire class
// Date: June 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using PerCederberg.Grammatica.Runtime;
using Krop.ControlWindow;
using Krop.KropGrammaticaParser;
using Krop.KropExecutionTree.AbstractClass;

namespace Krop.KropExecutionTree.Instruction
{
    /// <summary>
    /// Dire instruction
    /// </summary>
    class InstructionDire : Executable
    {
        Subprogram ParentSubprogram;
        bool IsStringValue = false;
        string VarName;
        string Value;
        string ErrorMsg;

        public InstructionDire(Node _nodeDireStatement, Subprogram _parentSubprogram)
        {
            ParentSubprogram = _parentSubprogram;

            for (int i = 0; i < _nodeDireStatement.GetChildCount(); i++)
            {
                if (_nodeDireStatement.GetChildAt(i).GetId() == (int)KropConstants.DIRE_VALUE)
                {
                    for (int y = 0; y < _nodeDireStatement.GetChildAt(i).GetChildCount(); y++)
                    {
                        switch (_nodeDireStatement.GetChildAt(i).GetChildAt(y).GetId())
                        {
                            case (int)KropConstants.STRING_VALUE:
                                IsStringValue = true;
                                for (int x = 0; x < _nodeDireStatement.GetChildAt(i).GetChildAt(y).GetChildCount(); x++)
                                {
                                    if (_nodeDireStatement.GetChildAt(i).GetChildAt(y).GetChildAt(x).GetId() == (int)KropConstants.SENTENCE)
                                    {
                                        CreateSentence(_nodeDireStatement.GetChildAt(i).GetChildAt(y).GetChildAt(x));
                                    }
                                }
                                break;
                            case (int)KropConstants.ATOM:
                                Token token;
                                token = (Token)_nodeDireStatement.GetChildAt(i).GetChildAt(y).GetChildAt(0);
                                if (_nodeDireStatement.GetChildAt(i).GetChildAt(y).GetChildAt(0).GetId() == (int)KropConstants.NUMBER)
                                {
                                    IsStringValue = true;
                                    Value = token.GetImage();
                                }
                                else
                                {
                                    VarName = token.GetImage();
                                }
                                break;
                        }
                    }
                }
            }
        }

        public override bool Execute()
        {
            if (CanExecute())
            {
                if (Value != null && IsStringValue == true)
                {
                    FormControlWindow.TerminalWriteLine("Fourmi dit : " + Value);
                    return true;
                }
                else
                {
                    if (VarName != null && IsStringValue == false)
                    {
                        if ((Value = Subprogram.VarToString(VarName, ParentSubprogram)) == null)
                        {
                            ErrorMsg = "Variable " + VarName + " n'existe pas.";
                        }
                        else
                        {
                            FormControlWindow.TerminalWriteLine("Fourmi dit : " + Value);
                            return true;
                        }
                    }
                }
            }

            FormControlWindow.TerminalWriteLine("DireError : " + ErrorMsg);
            return false;
        }

        /// <summary>
        /// Set value = sentence
        /// </summary>
        /// <param name="_nodeSentence">Node containing the sentence</param>
        public void CreateSentence(Node _nodeSentence)
        {
            Token token;

            for (int i = 0; i < _nodeSentence.GetChildCount(); i++)
            {
                switch (_nodeSentence.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.ATOM:
                        token = (Token)_nodeSentence.GetChildAt(i).GetChildAt(0);
                        Value += token.GetImage();
                        break;
                    case (int)KropConstants.SPACE:
                        token = (Token)_nodeSentence.GetChildAt(i);
                        Value += token.GetImage();
                        break;
                }
            }
        }
    }
}
