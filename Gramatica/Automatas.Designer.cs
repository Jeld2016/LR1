namespace Gramatica
{
    partial class Automatas
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BotonAFN = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BotonAFD = new System.Windows.Forms.Button();
            this.MinimizarAFD = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(282, 18);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(260, 23);
            this.textBox1.TabIndex = 0;
            // 
            // BotonAFN
            // 
            this.BotonAFN.Location = new System.Drawing.Point(625, 15);
            this.BotonAFN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BotonAFN.Name = "BotonAFN";
            this.BotonAFN.Size = new System.Drawing.Size(170, 28);
            this.BotonAFN.TabIndex = 1;
            this.BotonAFN.Text = "Generar AFN";
            this.BotonAFN.UseVisualStyleBackColor = true;
            this.BotonAFN.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(39, 86);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1111, 354);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // BotonAFD
            // 
            this.BotonAFD.Location = new System.Drawing.Point(625, 50);
            this.BotonAFD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BotonAFD.Name = "BotonAFD";
            this.BotonAFD.Size = new System.Drawing.Size(170, 28);
            this.BotonAFD.TabIndex = 4;
            this.BotonAFD.Text = "Genera AFD";
            this.BotonAFD.UseVisualStyleBackColor = true;
            this.BotonAFD.Visible = false;
            this.BotonAFD.Click += new System.EventHandler(this.BotonAFD_Click);
            // 
            // MinimizarAFD
            // 
            this.MinimizarAFD.Location = new System.Drawing.Point(832, 15);
            this.MinimizarAFD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizarAFD.Name = "MinimizarAFD";
            this.MinimizarAFD.Size = new System.Drawing.Size(170, 28);
            this.MinimizarAFD.TabIndex = 5;
            this.MinimizarAFD.Text = "Minimizar AFD";
            this.MinimizarAFD.UseVisualStyleBackColor = true;
            this.MinimizarAFD.Visible = false;
            this.MinimizarAFD.Click += new System.EventHandler(this.MinimizarAFD_Click);
            // 
            // Automatas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1166, 455);
            this.Controls.Add(this.MinimizarAFD);
            this.Controls.Add(this.BotonAFD);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BotonAFN);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Automatas";
            this.Text = "Automatas";
            this.Load += new System.EventHandler(this.Automatas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BotonAFN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BotonAFD;
        private System.Windows.Forms.Button MinimizarAFD;
    }
}