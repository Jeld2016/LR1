namespace LR1_Final
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_build_Parser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox_Input = new System.Windows.Forms.TextBox();
            this.dgrid_first_table = new System.Windows.Forms.DataGridView();
            this.dataGrid_LR1 = new System.Windows.Forms.DataGridView();
            this.Go_To = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kernel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Num_State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Closure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Table_LR1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.No_Terminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Conjunto_Primero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_first_table)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_LR1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table_LR1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1917, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tabla de Primero";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1041, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tabla de Cerradura";
            // 
            // button_build_Parser
            // 
            this.button_build_Parser.Location = new System.Drawing.Point(944, 714);
            this.button_build_Parser.Name = "button_build_Parser";
            this.button_build_Parser.Size = new System.Drawing.Size(132, 23);
            this.button_build_Parser.TabIndex = 5;
            this.button_build_Parser.Text = "Generar";
            this.button_build_Parser.UseVisualStyleBackColor = true;
            this.button_build_Parser.Click += new System.EventHandler(this.button_build_Parser_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 484);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tabla LR1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1905, 681);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dataGrid_LR1);
            this.tabPage1.Controls.Add(this.dgrid_first_table);
            this.tabPage1.Controls.Add(this.textBox_Input);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1897, 655);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "afd LR1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Table_LR1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1827, 597);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tabla de Acciones LR1";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox_Input
            // 
            this.textBox_Input.Location = new System.Drawing.Point(6, 22);
            this.textBox_Input.Multiline = true;
            this.textBox_Input.Name = "textBox_Input";
            this.textBox_Input.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Input.Size = new System.Drawing.Size(498, 314);
            this.textBox_Input.TabIndex = 1;
            this.textBox_Input.WordWrap = false;
            this.textBox_Input.TextChanged += new System.EventHandler(this.textBox_Input_TextChanged);
            // 
            // dgrid_first_table
            // 
            this.dgrid_first_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrid_first_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No_Terminal,
            this.Conjunto_Primero});
            this.dgrid_first_table.Location = new System.Drawing.Point(3, 358);
            this.dgrid_first_table.Name = "dgrid_first_table";
            this.dgrid_first_table.Size = new System.Drawing.Size(501, 291);
            this.dgrid_first_table.TabIndex = 3;
            // 
            // dataGrid_LR1
            // 
            this.dataGrid_LR1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_LR1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Go_To,
            this.Kernel,
            this.Num_State,
            this.Closure});
            this.dataGrid_LR1.Location = new System.Drawing.Point(510, 22);
            this.dataGrid_LR1.Name = "dataGrid_LR1";
            this.dataGrid_LR1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_LR1.Size = new System.Drawing.Size(1391, 627);
            this.dataGrid_LR1.TabIndex = 4;
            // 
            // Go_To
            // 
            this.Go_To.HeaderText = "Go_To";
            this.Go_To.Name = "Go_To";
            // 
            // Kernel
            // 
            this.Kernel.HeaderText = "Nucleo";
            this.Kernel.Name = "Kernel";
            this.Kernel.Width = 500;
            // 
            // Num_State
            // 
            this.Num_State.HeaderText = "Num_State";
            this.Num_State.Name = "Num_State";
            // 
            // Closure
            // 
            this.Closure.HeaderText = "Cerradura";
            this.Closure.Name = "Closure";
            this.Closure.Width = 1060;
            // 
            // Table_LR1
            // 
            this.Table_LR1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table_LR1.Location = new System.Drawing.Point(3, 6);
            this.Table_LR1.Name = "Table_LR1";
            this.Table_LR1.Size = new System.Drawing.Size(1824, 588);
            this.Table_LR1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(957, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Tabla de Cerrradura";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(122, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Gramatica";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(166, 339);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tabla de Primeros";
            // 
            // No_Terminal
            // 
            this.No_Terminal.HeaderText = "No_Terminal";
            this.No_Terminal.Name = "No_Terminal";
            // 
            // Conjunto_Primero
            // 
            this.Conjunto_Primero.HeaderText = "Conjunto_Primero";
            this.Conjunto_Primero.Name = "Conjunto_Primero";
            this.Conjunto_Primero.Width = 500;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1917, 749);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_build_Parser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_first_table)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_LR1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table_LR1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_build_Parser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox_Input;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGrid_LR1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Go_To;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kernel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num_State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Closure;
        private System.Windows.Forms.DataGridView dgrid_first_table;
        private System.Windows.Forms.DataGridView Table_LR1;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Terminal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Conjunto_Primero;
    }
}

