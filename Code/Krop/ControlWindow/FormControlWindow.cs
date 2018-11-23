// ----------------------------------------------------------------------------
//
// Definition of the FormControlWindow class
// Date: May 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using PerCederberg.Grammatica.Runtime;
using System.Threading;
using Krop.Krohonde;
using Krop.KropGrammaticaParser;
using Krop.KropExecutionTree;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Krop.ControlWindow
{
    public partial class FormControlWindow : Form
    {
        private string KropVersion = "Krop v1.0";

        //Var to control program execution
        public static Boolean IS_PAUSING = false; 
        public static Boolean IS_STOPPING = false;
        public static Boolean IS_RUNNING = false;
        public static Boolean PENDING_INSTRUCTION = false;

        public static TextBox TXT_TERMINAL; //Copy of txtTerminal

        private string CodePath = Directory.GetParent(Application.ExecutablePath).ToString() + @"\Code\"; //Path to directory \Code
        private string GardenPath = Directory.GetParent(Application.ExecutablePath).ToString() + @"\Garden\"; //Path to directory \Garden

        /// <summary>
        /// Initialize Control Window
        /// </summary>
        public FormControlWindow()
        {
            InitializeComponent();
            TXT_TERMINAL = this.txtTerminal;
        }

        /// <summary>
        /// Fill Program and Garden ComboBox at loading of the Control Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormControlWindow_Load(object sender, EventArgs e)
        {
            lblKropVersion.Text = KropVersion;
            lblKropVersion2.Text = KropVersion;

            Console.WriteLine("Loading control window");
            if (!Directory.Exists(CodePath))
            {
                Directory.CreateDirectory(CodePath);
            }
            cbxProgram.SelectedIndex = 0; //Select default value "Nouveau"
            List<string> files = new List<string>(Directory.EnumerateFiles(CodePath));
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension == ".txt")
                {
                    cbxProgram.Items.Add(fileInfo.Name); //Add the program to Program ComboBox
                    Console.WriteLine("Adding " + fileInfo.Name + " to program list");
                }
            }

            Console.WriteLine("Loading control garden");
            if (!Directory.Exists(GardenPath))
            {
                Directory.CreateDirectory(GardenPath);
            }
            cbxGarden.SelectedIndex = 0; //Select default value "Aucun"
            files = new List<string>(Directory.EnumerateFiles(GardenPath));
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension == ".txt")
                {
                    cbxGarden.Items.Add(fileInfo.Name); //Add the garden to Garden ComboBox
                    Console.WriteLine("Adding " + fileInfo.Name + " to garden list");
                }
            }

            this.buttonToolTip.SetToolTip(this.cmdAddIf, "Ajoute une condition if (si)");
            this.buttonToolTip.SetToolTip(this.cmdAddIfElse, "Ajoute une condition if... else (si... sinon)");
            this.buttonToolTip.SetToolTip(this.cmdAddElse, "Ajoute un else (sinon), utilisé après un if");
            this.buttonToolTip.SetToolTip(this.cmdAddWhile, "Ajoute une boucle while (tant que)");
            this.buttonToolTip.SetToolTip(this.cmdAddInt, "Ajoute une déclaration de variable de type int (numérique)");
            this.buttonToolTip.SetToolTip(this.cmdAddString, "Ajoute une déclaration de variable de type string (chaine de caractères)");

            this.buttonToolTip.SetToolTip(this.cmdAddDire, "Ajoute la fonction dire, qui permet d'écrire dans la console.");
            this.buttonToolTip.SetToolTip(this.cmdAddAvancer, "Ajoute la fonction avancer, qui permet de faire avancer la fourmis d'une case.");
            this.buttonToolTip.SetToolTip(this.cmdAddTournerADroite, "Ajoute la fonction TournerADroite, qui permet de faire tourner la fourmis à droite.");
            this.buttonToolTip.SetToolTip(this.cmdAddTournerAGauche, "Ajoute la fonction TournerAGauche, qui permet de faire tourner la fourmis à gauche.");
            this.buttonToolTip.SetToolTip(this.cmdAddPoserPheromone, "Ajoute la fonction PoserPheromone, qui permet de poser une pheromone à la position de la fourmis.");
            this.buttonToolTip.SetToolTip(this.cmdAddPrendrePheromone, "Ajoute la fonction PrendrePheromone, qui permet de prendre une pheromone, s'il y en a une à l'emplacement de la fourmis");
            this.buttonToolTip.SetToolTip(this.cmdAddSurUnePheromone, "Ajoute la fonction SurUnePheromone, qui permet de savoir si la fourmis est actuellement sur une pheromone.");
            this.buttonToolTip.SetToolTip(this.cmdAddObstacleEnFace, "Ajoute la fonction ObstacleEnFace, qui permet de savoir si la fourmis est face à un mur.");
        }

        /// <summary>
        /// Display Krohonde Window after the loading of the Control Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormControlWindow_Shown(object sender, EventArgs e)
        {
            Thread krohonde = new Thread(InitializeKrohondeWindow);
            krohonde.Start();
        }

        /// <summary>
        /// Inialize Krohonde Window
        /// </summary>
        public void InitializeKrohondeWindow()
        {
            Game krohondeWindow = new Game(Game.WIDTHWINDOW, Game.HEIGHTWINDOW);
            krohondeWindow.Run(200);
        }

        /// <summary>
        /// Load the selected garden in Garden ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxGarden_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTerminal.ResetText();

            if (cbxGarden.SelectedIndex == 0)
            {
                Game.ChangeGarden(Game.WIDTHGARDEN, Game.HEIGHTGARDEN);
                Console.WriteLine("Resetting txtCode");
            }
            else
            {
                Game.ChangeGarden(cbxGarden.SelectedItem.ToString());
                Console.WriteLine("Loading file " + GardenPath + cbxGarden.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Load the selected program in Program ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTerminal.ResetText();

            if (cbxProgram.SelectedIndex == 0)
            {
                txtCode.ResetText();
                btnDelete.Visible = false;
                Console.WriteLine("Resetting txtCode");
            }
            else
            {
                btnDelete.Visible = true;
                txtCode.Text = System.IO.File.ReadAllText(CodePath + cbxProgram.SelectedItem.ToString()); //Load the program in txtCode
                Console.WriteLine("Loading file " + CodePath + cbxProgram.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Execute the selected program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Running program " + cbxProgram.SelectedItem.ToString());

            btnPlay.Visible = false;
            btnStop.Visible = true;
            btnPause.Visible = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            cbxProgram.Enabled = false;
            cbxGarden.Enabled = false;
            txtCode.Enabled = false;

            FormControlWindow.IS_PAUSING = false;
            FormControlWindow.IS_STOPPING = false;
            
            this.Refresh();

            if (!IS_RUNNING)
            {
                IS_RUNNING = true;

                if (cbxGarden.SelectedIndex == 0)
                {
                    Game.ChangeGarden(Game.WIDTHGARDEN, Game.HEIGHTGARDEN); //Load default garden
                }
                else
                {
                    Game.ChangeGarden(cbxGarden.SelectedItem.ToString());   //Load selected garden
                }

                try
                {
                    Node program = new KropParser(new StringReader(txtCode.Text.ToLower())).Parse(); //Analyze the selected program and throw an error if there is a parser error
                    Subprogram myProgram = new Subprogram(program); //Create Execution Tree

                    txtTerminal.ResetText();
                    Game.ANT.ResetPlace();

                    myProgram.Execute(); //Execute the program
                }
                catch (PerCederberg.Grammatica.Runtime.ParserLogException msgError) //Stop program if parser error
                {
                    TerminalWriteLine("ParserError : " + msgError.Message);
                }
                finally
                {
                    EndingProgram();
                }  
            }
        }

        /// <summary>
        /// End executed program
        /// </summary>
        public void EndingProgram()
        {
            if (IS_STOPPING)
                Console.WriteLine("Stoping program " + cbxProgram.SelectedItem.ToString());
            else
                Console.WriteLine("Ending program " + cbxProgram.SelectedItem.ToString());

            IS_RUNNING = false;

            btnPlay.Visible = true;
            btnStop.Visible = false;
            btnPause.Visible = false;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            cbxProgram.Enabled = true;
            cbxGarden.Enabled = true;
            txtCode.Enabled = true;
        }

        /// <summary>
        /// Met en pause le programme en exécution lorsque le bouton "Pause" est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPause_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Pausing program " + cbxProgram.SelectedItem.ToString());
            btnPlay.Visible = true;
            btnPause.Visible = false;
            IS_PAUSING = true;
        }

        /// <summary>
        /// Stoppe le programme en exécution lorsque le bouton "Stop" est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStop_Click(object sender, EventArgs e)
        {
            btnPlay.Visible = true;
            btnStop.Visible = false;
            btnPause.Visible = false;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            cbxProgram.Enabled = true;
            cbxGarden.Enabled = true;
            txtCode.Enabled = true;

            IS_STOPPING = true;
            PENDING_INSTRUCTION = false;
        }

        /// <summary>
        /// Delete selected program after confirmation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Voulez-vous vraiment supprimer ce programme?", "Confirmation", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                Console.WriteLine("Deleting program " + cbxProgram.SelectedItem.ToString());

                System.IO.File.Delete(CodePath + cbxProgram.SelectedItem.ToString()); //Delete the program file
                cbxProgram.Items.RemoveAt(cbxProgram.SelectedIndex); //Remove the program of Program ComboBox
                cbxProgram.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Save the selected program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cbxProgram.SelectedIndex == 0)
            {
                gbxControlForm.Visible = false; //Display New Program Form
            }
            else
            {
                System.IO.File.WriteAllText(CodePath + cbxProgram.SelectedItem.ToString(), txtCode.Text);  //Replace the program file with the new one
                Console.WriteLine("Saving program " + CodePath + cbxProgram.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Check if the New Program Name contains only letters and numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtNewProgramName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        /// <summary>
        /// Create the new program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValidateNewProgram_Click(object sender, EventArgs e)
        {
            if(txtNewProgramName.Text != "")
            {
                string fileName = txtNewProgramName.Text + ".txt";

                Console.WriteLine("Creating program " + fileName);

                txtNewProgramName.ResetText();

                System.IO.File.WriteAllText(CodePath + fileName, txtCode.Text); //Create the new program file

                if (cbxProgram.FindStringExact(fileName) == -1)
                {
                    cbxProgram.Items.Add(fileName); //Add the new program to Program ComboBox
                }

                cbxProgram.SelectedIndex = cbxProgram.FindStringExact(fileName); //Select the new program in Program ComboBox
                gbxControlForm.Visible = true; //Display Control Form
            }
        }

        /// <summary>
        /// Cancel saving new program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelNewProgram_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Canceling new program creation");

            gbxControlForm.Visible = true; //Display Control Form
        }

        /// <summary>
        /// Write in txtTerminal
        /// </summary>
        /// <param name="_value"></param>
        public static void TerminalWriteLine(string _value)
        {
            TXT_TERMINAL.AppendText(_value + "\n");
        }

        /// <summary>
        /// Display Grammatica tree in the terminal
        /// </summary>
        /// <param name="node"></param>
        public void GetTree(Node _node)
        {
            Node nodeChild = null;

            Console.WriteLine(_node);

            for (int i = 0; i < _node.GetChildCount(); i++)
            {
                nodeChild = _node.GetChildAt(i);
                GetTree(nodeChild);
            }
        }

        /// <summary>
        /// Close Krop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormControlWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            PENDING_INSTRUCTION = false;
            IS_STOPPING = true;
            
            Game.EXIT_KROHONDE = true;
            Application.Exit();
        }

        /// <summary>
        /// When the text in txtCode has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            int selectStart = this.txtCode.SelectionStart;

            this.txtCode.Select(0, this.txtCode.Text.Length);
            this.txtCode.SelectionColor = Color.Black;
            this.txtCode.Select(selectStart, 0);

            ChangeWordColor("int", Color.Blue);
            ChangeWordColor("string", Color.Blue);
            ChangeWordColor("while", Color.Blue);
            ChangeWordColor("if", Color.Blue);
            ChangeWordColor("else", Color.Blue);

            ChangeWordColor("SurUnePheromone", Color.Green);
            ChangeWordColor("ObstacleEnFace", Color.Green);
            ChangeWordColor("ObstacleADroite", Color.Green);
            ChangeWordColor("ObstacleAGauche", Color.Green);
            ChangeWordColor("PoserPheromone", Color.Green);
            ChangeWordColor("PrendrePheromone", Color.Green);
            ChangeWordColor("Avancer", Color.Green);
            ChangeWordColor("TournerADroite", Color.Green);
            ChangeWordColor("TournerAGauche", Color.Green);
            ChangeWordColor("dire", Color.Green);
        }

        /// <summary>
        /// Check for the string and apply the color to it
        /// </summary>
        /// <param name="word">Word to color</param>
        /// <param name="color">Color</param>
        private void ChangeWordColor(string word, Color color)
        {
            if (this.txtCode.Text.ToLower().Contains(word.ToLower()))
            {
                int index = -1;
                int selectStart = this.txtCode.SelectionStart;

                while ((index = this.txtCode.Text.ToLower().IndexOf(word.ToLower(), (index + 1))) != -1)
                {
                    this.txtCode.Select((index), word.Length);
                    this.txtCode.SelectionColor = color;
                    this.txtCode.Select(selectStart, 0);
                    this.txtCode.SelectionColor = Color.Black;
                }
            }
        }

        #region QuickActionsButtonsClick
        private void addIfButton_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("if(condition){\n    \n}");
        }

        private void cmdAddIfElse_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("if(condition){\n    \n}else{\n    \n}");
        }

        private void cmdAddWhile_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("while(condition){\n    \n}");
        }

        private void cmdAddElse_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("else{\n    \n}");
        }

        private void cmdAddInt_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("int var = 0;\n");
        }

        private void cmdAddString_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("string var = \" \";\n");
        }

        private void cmdAddDire_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("dire \" \";\n");
        }

        private void cmdAddAvancer_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("avancer;\n");
        }

        private void cmdAddTournerADroite_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("TournerADroite;\n");
        }

        private void cmdAddTournerAGauche_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("TournerAGauche;\n");
        }

        private void cmdAddPoserPheromone_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("PoserPheromone;\n");
        }

        private void cmdAddPrendrePheromone_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("PrendrePheromone;\n");
        }

        private void cmdAddSurUnePheromone_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("SurUnePheromone");
        }

        private void cmdAddObstacleEnFace_Click(object sender, EventArgs e)
        {
            AddTextAtCursor("ObstacleEnFace");
        }
        #endregion

        /// <summary>
        /// Add text at the cursor
        /// </summary>
        /// <param name="text">Text to add</param>
        private void AddTextAtCursor(string text)
        {
            this.txtCode.SelectionLength = 0;
            this.txtCode.SelectionColor = Color.Black;
            this.txtCode.SelectedText = text;
        }
    }
}
