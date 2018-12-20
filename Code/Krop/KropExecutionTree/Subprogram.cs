// ----------------------------------------------------------------------------
//
// Definition of the Subprogram class
// Date: May 2018
// Author: X. Carrel
// Modified By: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using Krop.KropGrammaticaParser;
using Krop.KropExecutionTree.Interface;
using Krop.KropExecutionTree.AbstractClass;
using Krop.KropExecutionTree.Variable;
using Krop.KropExecutionTree.Condition;
using Krop.KropExecutionTree.Instruction;
using Krop.ControlWindow;
using System.Windows.Forms;

namespace Krop.KropExecutionTree
{
    /// <summary>
    /// A Subprogram is a sequence of executable object (which can be subprograms!)
    /// </summary>
    public class Subprogram : Executable
    {
        public Subprogram ParentSubprogram; // Parent Subprogram

        public List<IVariable> ListVar; // List of variables

        private List<Executable> Prg; // List of objects

        public Subprogram(Node _nodeProgram)
        {
            Prg = new List<Executable>();
            ListVar = new List<IVariable>();
            ParentSubprogram = null;

            BuildExecutionTree(_nodeProgram);
        }

        public Subprogram(Node _nodeProgram, Subprogram _subprogramSubprogram)
        {
            Prg = new List<Executable>();
            ListVar = new List<IVariable>();
            ParentSubprogram = _subprogramSubprogram;

            BuildExecutionTree(_nodeProgram);
        }

        /// <summary>
        /// Build execution tree
        /// </summary>
        /// <param name="_nodeProgram">Node containing the program</param>
        private void BuildExecutionTree(Node _nodeProgram)
        {
            for (int i = 0; i < _nodeProgram.GetChildCount(); i++)
            {
                if (_nodeProgram.GetChildAt(i).GetId() == (int)KropConstants.STATEMENT)
                {
                    Node nodeStatement = _nodeProgram.GetChildAt(i);

                    for (int y = 0; y < nodeStatement.GetChildCount(); y++)
                    {
                        switch (nodeStatement.GetChildAt(y).GetId())
                        {
                            case (int)KropConstants.DECLARATION_STATEMENT:
                                Node nodeDeclaration = nodeStatement.GetChildAt(y);

                                switch (nodeDeclaration.GetChildAt(0).GetId())
                                {
                                    case (int)KropConstants.INT_STATEMENT:
                                        this.AddInstruction(new InstructionInt(nodeDeclaration.GetChildAt(0), this));
                                        break;
                                    case (int)KropConstants.STRING_STATEMENT:
                                        this.AddInstruction(new InstructionString(nodeDeclaration.GetChildAt(0), this));
                                        break;
                                }

                                break;
                            case (int)KropConstants.INSTRUCTION_STATEMENT:
                                this.AddInstruction(new Command((Token)nodeStatement.GetChildAt(y).GetChildAt(0)));
                                break;
                            case (int)KropConstants.IF_ELSE_STATEMENT:
                                this.AddInstruction(new InstructionIf(nodeStatement.GetChildAt(y), this));
                                break;
                            case (int)KropConstants.WHILE_STATEMENT:
                                this.AddInstruction(new InstructionWhile(nodeStatement.GetChildAt(y), this));
                                break;
                            case (int)KropConstants.DIRE_STATEMENT:
                                this.AddInstruction(new InstructionDire(nodeStatement.GetChildAt(y), this));
                                break;                    
                            case (int)KropConstants.SET_VAR_STATEMENT:
                                this.AddInstruction(new InstructionSetVar(nodeStatement.GetChildAt(y), this));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add an executable object at the end of the subprogram
        /// </summary>
        public void AddInstruction(Executable _instruction)
        {
            Prg.Add(_instruction);
        }

        public override bool Execute()
        {
            foreach (Executable ins in Prg)
                if (!ins.Execute())
                    return false; // in case of any error: stop and report false to the caller
            return true; // Everything went fine if we made it this far
        }

        /// <summary>
        /// Determine the condition type and create it
        /// </summary>
        /// <param name="_nodeConditionStatement">Node containing the Condition</param>
        /// <returns></returns>
        public static Dictionary<Measurable<Boolean>, string> SetCond(Node _nodeConditionStatement, Subprogram _parentSubprogram)
        {
            bool isNot = false;
            Token token;

            Dictionary<Measurable<Boolean>, string> condList = new Dictionary<Measurable<bool>, string>();

            for (int i = 0; i < _nodeConditionStatement.GetChildCount(); i++)
            {
                if (_nodeConditionStatement.GetChildAt(i).GetId() == (int)KropConstants.CONDITION_EXPR)
                {
                    if (_nodeConditionStatement.GetChildAt(i).GetChildAt(0).GetId() == (int)KropConstants.NOT)
                        isNot = true;

                    for (int y = 0; y < _nodeConditionStatement.GetChildAt(i).GetChildCount(); y++)
                    {
                        switch (_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetId())
                        {
                            case (int)KropConstants.CONDITION_PARAMETER:
                                switch (_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildAt(0).GetId())
                                {
                                    case (int)KropConstants.CONDITION:
                                        token = (Token)_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildAt(0);
                                        condList.Add(new BooleanFunction(token.GetImage(), isNot), "");
                                        break;
                                    case (int)KropConstants.TRUE:
                                        condList.Add(new BooleanVar(true, isNot), "");
                                        break;
                                    case (int)KropConstants.FALSE:
                                        condList.Add(new BooleanVar(false, isNot), "");
                                        break;
                                    case (int)KropConstants.BOOLEAN_EXPRESSION:
                                        condList.Add(new BooleanExpression(_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildAt(0), _parentSubprogram, isNot), "");        
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }else if (_nodeConditionStatement.GetChildAt(i).GetId() == (int)KropConstants.CONDITION_REST)
                {
                    string logicalOperator = "";

                    for (int y = 0; y < _nodeConditionStatement.GetChildAt(i).GetChildCount(); y++)
                    {
                        switch (_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetId())
                        {
                            case (int)KropConstants.LOGICAL_OPERATOR:
                                token = (Token)_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildAt(0);
                                logicalOperator = token.GetImage();
                                break;
                            case (int)KropConstants.CONDITION_EXPR:

                                if (_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildAt(0).GetId() == (int)KropConstants.NOT)
                                    isNot = true;

                                for (int j = 0; j < _nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildCount(); j++)
                                {
                                    switch (_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildAt(j).GetId())
                                    {
                                        case (int)KropConstants.CONDITION_PARAMETER:
                                            switch (_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildAt(j).GetChildAt(0).GetId())
                                            {
                                                case (int)KropConstants.CONDITION:
                                                    token = (Token)_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildAt(j).GetChildAt(0);
                                                    condList.Add(new BooleanFunction(token.GetImage(), isNot), logicalOperator);
                                                    break;
                                                case (int)KropConstants.TRUE:
                                                    condList.Add(new BooleanVar(true, isNot), logicalOperator);
                                                    break;
                                                case (int)KropConstants.FALSE:
                                                    condList.Add(new BooleanVar(false, isNot), logicalOperator);
                                                    break;
                                                case (int)KropConstants.BOOLEAN_EXPRESSION:
                                                    condList.Add(new BooleanExpression(_nodeConditionStatement.GetChildAt(i).GetChildAt(y).GetChildAt(j).GetChildAt(0), _parentSubprogram, isNot), logicalOperator);
                                                    break;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            return condList;
        }

        /// <summary>
        /// Check if the variable already exists
        /// </summary>
        /// <param name="_varName">Variable Name</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Result</returns>
        public static bool VarExists(string _varName, Subprogram _parentSubprogram)
        {
            foreach (IVariable var in _parentSubprogram.ListVar)
            {
                if (var.GetName() == _varName)
                {
                    return true;
                }

            }

            return false;
        }

        /// <summary>
        /// Return the variable string
        /// </summary>
        /// <param name="_varName">Variable Name</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Variable string</returns>
        public static string VarToString(string _varName, Subprogram _parentSubprogram)
        {
            foreach (IVariable var in _parentSubprogram.ListVar)
            {
                if (var.GetName() == _varName)
                {
                    return var.ToString();
                }
            }

            if(_parentSubprogram.ParentSubprogram != null)
            {
                return VarToString(_varName, _parentSubprogram.ParentSubprogram);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Return the value of an Int variable
        /// </summary>
        /// <param name="_varName">Variable Name</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Int variable value</returns>
        public static int? GetIntVarValue(string _varName, Subprogram _parentSubprogram)
        {
            foreach (IVariable var in _parentSubprogram.ListVar)
            {
                if(var is IntVar intVar)
                {
                    if(intVar.GetName() == _varName)
                    {
                        return intVar.GetValue();
                    }
                }
            }

            if (_parentSubprogram.ParentSubprogram != null)
            {
                return GetIntVarValue(_varName, _parentSubprogram.ParentSubprogram);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Return the value of a String variable
        /// </summary>
        /// <param name="_varName">Variable Name</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Int variable value</returns>
        public static string GetStringVarValue(string _varName, Subprogram _parentSubprogram)
        {
            foreach (IVariable var in _parentSubprogram.ListVar)
            {
                if (var is StringVar stringVar)
                {
                    if (stringVar.GetName() == _varName)
                    {
                        return stringVar.GetValue();
                    }
                }
            }

            if (_parentSubprogram.ParentSubprogram != null)
            {
                return GetStringVarValue(_varName, _parentSubprogram.ParentSubprogram);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Return the variable
        /// </summary>
        /// <param name="_varName">Variable Name</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Variable</returns>
        public IVariable GetVar(string _varName, Subprogram _parentSubprogram)
        {
            foreach (IVariable var in _parentSubprogram.ListVar)
            {
                if (var.GetName() == _varName)
                {
                    return var;
                }
            }

            if (_parentSubprogram.ParentSubprogram != null)
            {
                return GetVar(_varName, _parentSubprogram.ParentSubprogram);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Prompt a value with the InputBox Form
        /// </summary>
        /// <typeparam name="T">Type of the prompted value</typeparam>
        /// <returns></returns>
        public static T PromptInputValue<T>()
        {
            bool keepPrompt = true;
            do
            {
                Input inputDialog = new Input();
                inputDialog.lblInput.Text = "Indiquez une valeur de type " + typeof(T).Name;

                if (inputDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        return (T)Convert.ChangeType(inputDialog.txtInput.Text, typeof(T));
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("Valeur incorrecte, il faut un " + typeof(T).Name, "Erreur de valeur");
                    }
                }
                else
                {
                    keepPrompt = false;
                    return (T)Convert.ChangeType(0, typeof(T));
                }

            } while (keepPrompt);

            return (T)Convert.ChangeType(0, typeof(T));
        }

    }
}
