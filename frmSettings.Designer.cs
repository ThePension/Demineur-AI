
namespace Demineur
{
    partial class frmSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.rb8 = new System.Windows.Forms.RadioButton();
            this.rb16 = new System.Windows.Forms.RadioButton();
            this.rb20 = new System.Windows.Forms.RadioButton();
            this.btnCommencer = new System.Windows.Forms.Button();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.rbAutre = new System.Windows.Forms.RadioButton();
            this.cbAI = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Taille de la grille : ";
            // 
            // rb8
            // 
            this.rb8.AutoSize = true;
            this.rb8.Checked = true;
            this.rb8.Location = new System.Drawing.Point(116, 17);
            this.rb8.Margin = new System.Windows.Forms.Padding(1);
            this.rb8.Name = "rb8";
            this.rb8.Size = new System.Drawing.Size(93, 17);
            this.rb8.TabIndex = 1;
            this.rb8.TabStop = true;
            this.rb8.Text = "8x8 (10 mines)";
            this.rb8.UseVisualStyleBackColor = true;
            // 
            // rb16
            // 
            this.rb16.AutoSize = true;
            this.rb16.Location = new System.Drawing.Point(116, 46);
            this.rb16.Margin = new System.Windows.Forms.Padding(1);
            this.rb16.Name = "rb16";
            this.rb16.Size = new System.Drawing.Size(105, 17);
            this.rb16.TabIndex = 2;
            this.rb16.Text = "16x16 (40 mines)";
            this.rb16.UseVisualStyleBackColor = true;
            // 
            // rb20
            // 
            this.rb20.AutoSize = true;
            this.rb20.Location = new System.Drawing.Point(116, 76);
            this.rb20.Margin = new System.Windows.Forms.Padding(1);
            this.rb20.Name = "rb20";
            this.rb20.Size = new System.Drawing.Size(105, 17);
            this.rb20.TabIndex = 3;
            this.rb20.Text = "20x20 (99 mines)";
            this.rb20.UseVisualStyleBackColor = true;
            // 
            // btnCommencer
            // 
            this.btnCommencer.Location = new System.Drawing.Point(20, 142);
            this.btnCommencer.Margin = new System.Windows.Forms.Padding(1);
            this.btnCommencer.Name = "btnCommencer";
            this.btnCommencer.Size = new System.Drawing.Size(201, 39);
            this.btnCommencer.TabIndex = 4;
            this.btnCommencer.Text = "Commencer";
            this.btnCommencer.UseVisualStyleBackColor = true;
            this.btnCommencer.Click += new System.EventHandler(this.btnCommencer_Click);
            // 
            // nudSize
            // 
            this.nudSize.Location = new System.Drawing.Point(176, 107);
            this.nudSize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(45, 20);
            this.nudSize.TabIndex = 6;
            this.nudSize.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // rbAutre
            // 
            this.rbAutre.AutoSize = true;
            this.rbAutre.Location = new System.Drawing.Point(116, 107);
            this.rbAutre.Margin = new System.Windows.Forms.Padding(1);
            this.rbAutre.Name = "rbAutre";
            this.rbAutre.Size = new System.Drawing.Size(56, 17);
            this.rbAutre.TabIndex = 7;
            this.rbAutre.Text = "Autre :";
            this.rbAutre.UseVisualStyleBackColor = true;
            // 
            // cbAI
            // 
            this.cbAI.AutoSize = true;
            this.cbAI.Location = new System.Drawing.Point(20, 46);
            this.cbAI.Name = "cbAI";
            this.cbAI.Size = new System.Drawing.Size(42, 17);
            this.cbAI.TabIndex = 8;
            this.cbAI.Text = "AI?";
            this.cbAI.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 203);
            this.Controls.Add(this.cbAI);
            this.Controls.Add(this.rbAutre);
            this.Controls.Add(this.nudSize);
            this.Controls.Add(this.btnCommencer);
            this.Controls.Add(this.rb20);
            this.Controls.Add(this.rb16);
            this.Controls.Add(this.rb8);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "frmSettings";
            this.Text = "Démineur paramètres";
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb8;
        private System.Windows.Forms.RadioButton rb16;
        private System.Windows.Forms.RadioButton rb20;
        private System.Windows.Forms.Button btnCommencer;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.RadioButton rbAutre;
        private System.Windows.Forms.CheckBox cbAI;
    }
}