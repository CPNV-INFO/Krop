﻿namespace Krop.ControlWindow
{
    partial class FormControlWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated Code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the Code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormControlWindow));
            this.btnPlay = new System.Windows.Forms.Button();
            this.cbxProgram = new System.Windows.Forms.ComboBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.gbxControlForm = new System.Windows.Forms.GroupBox();
            this.lblKropVersion = new System.Windows.Forms.Label();
            this.lblProgram = new System.Windows.Forms.Label();
            this.lblGarden = new System.Windows.Forms.Label();
            this.cbxGarden = new System.Windows.Forms.ComboBox();
            this.txtTerminal = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gbxNewProgramForm = new System.Windows.Forms.GroupBox();
            this.lblKropVersion2 = new System.Windows.Forms.Label();
            this.btnValidateNewProgram = new System.Windows.Forms.Button();
            this.lblTitleNewProgram = new System.Windows.Forms.Label();
            this.btnCancelNewProgram = new System.Windows.Forms.Button();
            this.txtNewProgramName = new System.Windows.Forms.TextBox();
            this.lblRuleNewProgram = new System.Windows.Forms.Label();
            this.lblNewProgramName = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.RichTextBox();
            this.gbxControlForm.SuspendLayout();
            this.gbxNewProgramForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.AccessibleName = "btnPlay";
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(18, 35);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(40, 40);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // cbxProgram
            // 
            this.cbxProgram.AccessibleName = "chkProgram";
            this.cbxProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProgram.FormattingEnabled = true;
            this.cbxProgram.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxProgram.Items.AddRange(new object[] {
            "Nouveau"});
            this.cbxProgram.Location = new System.Drawing.Point(457, 54);
            this.cbxProgram.MaxDropDownItems = 99;
            this.cbxProgram.Name = "cbxProgram";
            this.cbxProgram.Size = new System.Drawing.Size(120, 21);
            this.cbxProgram.TabIndex = 2;
            this.cbxProgram.SelectedIndexChanged += new System.EventHandler(this.CbxProgram_SelectedIndexChanged);
            // 
            // btnStop
            // 
            this.btnStop.AccessibleName = "btnStop";
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(64, 35);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(40, 40);
            this.btnStop.TabIndex = 3;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.AccessibleName = "btnPause";
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(18, 35);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(40, 40);
            this.btnPause.TabIndex = 4;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Visible = false;
            this.btnPause.Click += new System.EventHandler(this.BtnPause_Click);
            // 
            // gbxControlForm
            // 
            this.gbxControlForm.AccessibleName = "gbxControlForm";
            this.gbxControlForm.Controls.Add(this.txtCode);
            this.gbxControlForm.Controls.Add(this.lblKropVersion);
            this.gbxControlForm.Controls.Add(this.lblProgram);
            this.gbxControlForm.Controls.Add(this.lblGarden);
            this.gbxControlForm.Controls.Add(this.cbxGarden);
            this.gbxControlForm.Controls.Add(this.txtTerminal);
            this.gbxControlForm.Controls.Add(this.btnStop);
            this.gbxControlForm.Controls.Add(this.btnSave);
            this.gbxControlForm.Controls.Add(this.btnDelete);
            this.gbxControlForm.Controls.Add(this.cbxProgram);
            this.gbxControlForm.Controls.Add(this.btnPlay);
            this.gbxControlForm.Controls.Add(this.btnPause);
            this.gbxControlForm.Location = new System.Drawing.Point(-5, -29);
            this.gbxControlForm.Name = "gbxControlForm";
            this.gbxControlForm.Size = new System.Drawing.Size(600, 700);
            this.gbxControlForm.TabIndex = 8;
            this.gbxControlForm.TabStop = false;
            // 
            // lblKropVersion
            // 
            this.lblKropVersion.AccessibleName = "lblKropVersion";
            this.lblKropVersion.AutoSize = true;
            this.lblKropVersion.Location = new System.Drawing.Point(524, 675);
            this.lblKropVersion.Name = "lblKropVersion";
            this.lblKropVersion.Size = new System.Drawing.Size(53, 13);
            this.lblKropVersion.TabIndex = 18;
            this.lblKropVersion.Text = "Krop v1.0";
            // 
            // lblProgram
            // 
            this.lblProgram.AccessibleName = "lblProgram";
            this.lblProgram.AutoSize = true;
            this.lblProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgram.Location = new System.Drawing.Point(475, 35);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(88, 16);
            this.lblProgram.TabIndex = 17;
            this.lblProgram.Text = "Programme";
            // 
            // lblGarden
            // 
            this.lblGarden.AccessibleName = "lblGarden";
            this.lblGarden.AutoSize = true;
            this.lblGarden.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGarden.Location = new System.Drawing.Point(212, 35);
            this.lblGarden.Name = "lblGarden";
            this.lblGarden.Size = new System.Drawing.Size(51, 16);
            this.lblGarden.TabIndex = 16;
            this.lblGarden.Text = "Jardin";
            // 
            // cbxGarden
            // 
            this.cbxGarden.AccessibleName = "cbxGarden";
            this.cbxGarden.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGarden.FormattingEnabled = true;
            this.cbxGarden.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxGarden.Items.AddRange(new object[] {
            "Aucun"});
            this.cbxGarden.Location = new System.Drawing.Point(154, 54);
            this.cbxGarden.MaxDropDownItems = 99;
            this.cbxGarden.Name = "cbxGarden";
            this.cbxGarden.Size = new System.Drawing.Size(173, 21);
            this.cbxGarden.TabIndex = 15;
            this.cbxGarden.SelectedIndexChanged += new System.EventHandler(this.CbxGarden_SelectedIndexChanged);
            // 
            // txtTerminal
            // 
            this.txtTerminal.AccessibleName = "txtTerminal";
            this.txtTerminal.Location = new System.Drawing.Point(19, 601);
            this.txtTerminal.Multiline = true;
            this.txtTerminal.Name = "txtTerminal";
            this.txtTerminal.ReadOnly = true;
            this.txtTerminal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTerminal.Size = new System.Drawing.Size(558, 70);
            this.txtTerminal.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleName = "btnSave";
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(401, 35);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 13;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleName = "btnDelete";
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(355, 35);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(40, 40);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // gbxNewProgramForm
            // 
            this.gbxNewProgramForm.AccessibleName = "gbxNewProgramForm";
            this.gbxNewProgramForm.Controls.Add(this.lblKropVersion2);
            this.gbxNewProgramForm.Controls.Add(this.btnValidateNewProgram);
            this.gbxNewProgramForm.Controls.Add(this.lblTitleNewProgram);
            this.gbxNewProgramForm.Controls.Add(this.btnCancelNewProgram);
            this.gbxNewProgramForm.Controls.Add(this.txtNewProgramName);
            this.gbxNewProgramForm.Controls.Add(this.lblRuleNewProgram);
            this.gbxNewProgramForm.Controls.Add(this.lblNewProgramName);
            this.gbxNewProgramForm.Location = new System.Drawing.Point(-4, -26);
            this.gbxNewProgramForm.Name = "gbxNewProgramForm";
            this.gbxNewProgramForm.Size = new System.Drawing.Size(600, 694);
            this.gbxNewProgramForm.TabIndex = 9;
            this.gbxNewProgramForm.TabStop = false;
            // 
            // lblKropVersion2
            // 
            this.lblKropVersion2.AccessibleName = "lblKropVersion";
            this.lblKropVersion2.AutoSize = true;
            this.lblKropVersion2.Location = new System.Drawing.Point(523, 672);
            this.lblKropVersion2.Name = "lblKropVersion2";
            this.lblKropVersion2.Size = new System.Drawing.Size(53, 13);
            this.lblKropVersion2.TabIndex = 19;
            this.lblKropVersion2.Text = "Krop v1.0";
            // 
            // btnValidateNewProgram
            // 
            this.btnValidateNewProgram.AccessibleName = "btnValidateNewProgram";
            this.btnValidateNewProgram.Location = new System.Drawing.Point(502, 197);
            this.btnValidateNewProgram.Name = "btnValidateNewProgram";
            this.btnValidateNewProgram.Size = new System.Drawing.Size(75, 23);
            this.btnValidateNewProgram.TabIndex = 5;
            this.btnValidateNewProgram.Text = "Valider";
            this.btnValidateNewProgram.UseVisualStyleBackColor = true;
            this.btnValidateNewProgram.Click += new System.EventHandler(this.BtnValidateNewProgram_Click);
            // 
            // lblTitleNewProgram
            // 
            this.lblTitleNewProgram.AccessibleName = "lblTitleNewProgram";
            this.lblTitleNewProgram.AutoSize = true;
            this.lblTitleNewProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleNewProgram.Location = new System.Drawing.Point(110, 71);
            this.lblTitleNewProgram.Name = "lblTitleNewProgram";
            this.lblTitleNewProgram.Size = new System.Drawing.Size(336, 37);
            this.lblTitleNewProgram.TabIndex = 2;
            this.lblTitleNewProgram.Text = "Nouveau programme";
            // 
            // btnCancelNewProgram
            // 
            this.btnCancelNewProgram.AccessibleName = "btnCancelNewProgram";
            this.btnCancelNewProgram.Location = new System.Drawing.Point(421, 197);
            this.btnCancelNewProgram.Name = "btnCancelNewProgram";
            this.btnCancelNewProgram.Size = new System.Drawing.Size(75, 23);
            this.btnCancelNewProgram.TabIndex = 4;
            this.btnCancelNewProgram.Text = "Annuler";
            this.btnCancelNewProgram.UseVisualStyleBackColor = true;
            this.btnCancelNewProgram.Click += new System.EventHandler(this.BtnCancelNewProgram_Click);
            // 
            // txtNewProgramName
            // 
            this.txtNewProgramName.AccessibleName = "txtNewProgramName";
            this.txtNewProgramName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewProgramName.Location = new System.Drawing.Point(71, 131);
            this.txtNewProgramName.Name = "txtNewProgramName";
            this.txtNewProgramName.Size = new System.Drawing.Size(506, 29);
            this.txtNewProgramName.TabIndex = 0;
            this.txtNewProgramName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNewProgramName_KeyPress);
            // 
            // lblRuleNewProgram
            // 
            this.lblRuleNewProgram.AccessibleName = "lblRuleNewProgram";
            this.lblRuleNewProgram.AutoSize = true;
            this.lblRuleNewProgram.ForeColor = System.Drawing.Color.Red;
            this.lblRuleNewProgram.Location = new System.Drawing.Point(68, 163);
            this.lblRuleNewProgram.Name = "lblRuleNewProgram";
            this.lblRuleNewProgram.Size = new System.Drawing.Size(173, 13);
            this.lblRuleNewProgram.TabIndex = 3;
            this.lblRuleNewProgram.Text = "(Uniquement des lettres ou chiffres)";
            // 
            // lblNewProgramName
            // 
            this.lblNewProgramName.AccessibleName = "lblNewProgramName";
            this.lblNewProgramName.AutoSize = true;
            this.lblNewProgramName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewProgramName.Location = new System.Drawing.Point(14, 134);
            this.lblNewProgramName.Name = "lblNewProgramName";
            this.lblNewProgramName.Size = new System.Drawing.Size(51, 24);
            this.lblNewProgramName.TabIndex = 1;
            this.lblNewProgramName.Text = "Nom";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(19, 86);
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtCode.Size = new System.Drawing.Size(558, 509);
            this.txtCode.TabIndex = 19;
            this.txtCode.Text = "";
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            // 
            // FormControlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(584, 662);
            this.Controls.Add(this.gbxControlForm);
            this.Controls.Add(this.gbxNewProgramForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(1000, 0);
            this.Name = "FormControlWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Fenêtre de contrôle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormControlWindow_FormClosing);
            this.Load += new System.EventHandler(this.FormControlWindow_Load);
            this.Shown += new System.EventHandler(this.FormControlWindow_Shown);
            this.gbxControlForm.ResumeLayout(false);
            this.gbxControlForm.PerformLayout();
            this.gbxNewProgramForm.ResumeLayout(false);
            this.gbxNewProgramForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ComboBox cbxProgram;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.GroupBox gbxControlForm;
        private System.Windows.Forms.Button btnValidateNewProgram;
        private System.Windows.Forms.Button btnCancelNewProgram;
        private System.Windows.Forms.Label lblRuleNewProgram;
        private System.Windows.Forms.Label lblTitleNewProgram;
        private System.Windows.Forms.Label lblNewProgramName;
        private System.Windows.Forms.TextBox txtNewProgramName;
        private System.Windows.Forms.GroupBox gbxNewProgramForm;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cbxGarden;
        private System.Windows.Forms.Label lblGarden;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.Label lblKropVersion;
        private System.Windows.Forms.Label lblKropVersion2;
        private System.Windows.Forms.TextBox txtTerminal;
        private System.Windows.Forms.RichTextBox txtCode;
    }
}

