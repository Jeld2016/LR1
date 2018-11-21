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
            this.textBox_Input = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgrid_first_table = new System.Windows.Forms.DataGridView();
            this.No_Terminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Conjunto_Primero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGrid_LR1 = new System.Windows.Forms.DataGridView();
            this.Go_To = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kernel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Num_State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Closure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_build_Parser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Table_LR1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_first_table)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_LR1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table_LR1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Input
            // 
            this.textBox_Input.Location = new System.Drawing.Point(12, 27);
            this.textBox_Input.Multiline = true;
            this.textBox_Input.Name = "textBox_Input";
            this.textBox_Input.Size = new System.Drawing.Size(304, 158);
            this.textBox_Input.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 24);
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
            // dgrid_first_table
            // 
            this.dgrid_first_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrid_first_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No_Terminal,
            this.Conjunto_Primero});
            this.dgrid_first_table.Location = new System.Drawing.Point(22, 236);
            this.dgrid_first_table.Name = "dgrid_first_table";
            this.dgrid_first_table.Size = new System.Drawing.Size(323, 245);
            this.dgrid_first_table.TabIndex = 2;
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
            this.Conjunto_Primero.Width = 200;
            // 
            // dataGrid_LR1
            // 
            this.dataGrid_LR1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_LR1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Go_To,
            this.Kernel,
            this.Num_State,
            this.Closure});
            this.dataGrid_LR1.Location = new System.Drawing.Point(468, 27);
            this.dataGrid_LR1.Name = "dataGrid_LR1";
            this.dataGrid_LR1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid_LR1.Size = new System.Drawing.Size(1444, 548);
            this.dataGrid_LR1.TabIndex = 3;
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
            this.button_build_Parser.Location = new System.Drawing.Point(1112, 605);
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
            // Table_LR1
            // 
            this.Table_LR1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table_LR1.Location = new System.Drawing.Point(22, 504);
            this.Table_LR1.Name = "Table_LR1";
            this.Table_LR1.Size = new System.Drawing.Size(332, 124);
            this.Table_LR1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.Table_LR1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_build_Parser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGrid_LR1);
            this.Controls.Add(this.dgrid_first_table);
            this.Controls.Add(this.textBox_Input);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_first_table)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_LR1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table_LR1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Input;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgrid_first_table;
        private System.Windows.Forms.DataGridView dataGrid_LR1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_build_Parser;
        private System.Windows.Forms.DataGridViewTextBoxColumn Go_To;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kernel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num_State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Closure;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Terminal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Conjunto_Primero;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView Table_LR1;
    }
}

