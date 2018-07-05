// ----------------------------------------------------------------------------
//
// Definition of the Executable class
// Date: May 2018
// Author: X. Carrel
// Modified By: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using System.Windows.Forms;
using Krop.ControlWindow;

namespace Krop.KropExecutionTree.AbstractClass
{
    /// <summary>
    /// Executable objects are either a single command or a list thereof that can be executed
    /// Return true if successful
    /// </summary>
    public abstract class Executable
    {
        public abstract Boolean Execute();

        /// <summary>
        /// Wait ending previous order, during pause or stop execution if clicking on Stop button
        /// </summary>
        /// <returns></returns>
        public bool CanExecute()
        {
            Application.DoEvents();

            while (FormControlWindow.IS_PAUSING == true || FormControlWindow.IS_STOPPING == true || FormControlWindow.PENDING_INSTRUCTION) //Pausing program execution
            {
                Application.DoEvents();
                if (FormControlWindow.IS_STOPPING == true)
                {
                    FormControlWindow.PENDING_INSTRUCTION = false;
                    FormControlWindow.IS_PAUSING = false;
                    FormControlWindow.IS_STOPPING = false;
                    FormControlWindow.IS_RUNNING = false;
                    return false; //Stop execution
                }               
            }

            return true;
        }

    }
}
