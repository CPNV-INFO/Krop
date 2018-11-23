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
using System;

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
        Node stringExpression;

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
                            case (int)KropConstants.STRING_EXPRESSION:
                                IsStringValue = true;
                                stringExpression = _nodeDireStatement.GetChildAt(i).GetChildAt(y);
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
                Value = AlgorithmicExpression.CalculStringExpression(stringExpression, ParentSubprogram);

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

                        //Check if this is a backslashed character, like \' for printing '
                        if (Enum.GetName(typeof(KropConstants), token.GetId()).Contains("BACKSLASH_"))
                        {
                            Value += token.GetImage().Replace("\\", "");
                        }
                        else
                        {
                            Value += token.GetImage();
                        }

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
