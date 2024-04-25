namespace Sistema_de_Gerenciamento_de_Conferências
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PalestraTempo = new TextBox();
            button2 = new Button();
            PalestraNome = new TextBox();
            button3 = new Button();
            trilha1Grid = new DataGridView();
            trilha2Grid = new DataGridView();
            button4 = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)trilha1Grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trilha2Grid).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(255, 12);
            button1.Name = "button1";
            button1.Size = new Size(113, 36);
            button1.TabIndex = 0;
            button1.Text = "Buscar Lista Json";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ImportarPalestraJSON;
            // 
            // PalestraTempo
            // 
            PalestraTempo.Location = new Point(255, 54);
            PalestraTempo.Name = "PalestraTempo";
            PalestraTempo.Size = new Size(113, 23);
            PalestraTempo.TabIndex = 4;
            PalestraTempo.Tag = "";
            PalestraTempo.Text = "Tempo em minutos";
            PalestraTempo.GotFocus += PalestraTempo_GotFocus;
            PalestraTempo.LostFocus += PalestraTempo_LostFocus;
            // 
            // button2
            // 
            button2.Location = new Point(12, 12);
            button2.Name = "button2";
            button2.Size = new Size(237, 36);
            button2.TabIndex = 1;
            button2.Text = "Adicionar Palestra";
            button2.UseVisualStyleBackColor = true;
            button2.Click += AdiconarPalestra;
            // 
            // PalestraNome
            // 
            PalestraNome.Location = new Point(12, 54);
            PalestraNome.Name = "PalestraNome";
            PalestraNome.Size = new Size(237, 23);
            PalestraNome.TabIndex = 3;
            PalestraNome.Tag = "String";
            PalestraNome.Text = "Nome da palestra";
            PalestraNome.GotFocus += PalestraNome_GotFocus;
            PalestraNome.LostFocus += PalestraNome_LostFocus;
            // 
            // button3
            // 
            button3.Location = new Point(771, 52);
            button3.Name = "button3";
            button3.Size = new Size(117, 24);
            button3.TabIndex = 5;
            button3.Text = "Salvar Json";
            button3.UseVisualStyleBackColor = true;
            button3.Click += SalvarJson;
            // 
            // trilha1Grid
            // 
            trilha1Grid.AllowUserToAddRows = false;
            trilha1Grid.AllowUserToDeleteRows = false;
            trilha1Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            trilha1Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            trilha1Grid.Location = new Point(12, 83);
            trilha1Grid.Name = "trilha1Grid";
            trilha1Grid.ReadOnly = true;
            trilha1Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            trilha1Grid.Size = new Size(438, 552);
            trilha1Grid.TabIndex = 6;
            // 
            // trilha2Grid
            // 
            trilha2Grid.AllowUserToAddRows = false;
            trilha2Grid.AllowUserToDeleteRows = false;
            trilha2Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            trilha2Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            trilha2Grid.Location = new Point(456, 83);
            trilha2Grid.Name = "trilha2Grid";
            trilha2Grid.ReadOnly = true;
            trilha2Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            trilha2Grid.Size = new Size(432, 552);
            trilha2Grid.TabIndex = 8;
            // 
            // button4
            // 
            button4.Location = new Point(648, 53);
            button4.Name = "button4";
            button4.Size = new Size(117, 23);
            button4.TabIndex = 9;
            button4.Text = "Salvar TxT";
            button4.UseVisualStyleBackColor = true;
            button4.Click += SalvarTxT;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 647);
            Controls.Add(button4);
            Controls.Add(trilha2Grid);
            Controls.Add(trilha1Grid);
            Controls.Add(button3);
            Controls.Add(PalestraTempo);
            Controls.Add(PalestraNome);
            Controls.Add(button2);
            Controls.Add(button1);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gerenciamento de Conferências Lucas Albano Jansen";
            ((System.ComponentModel.ISupportInitialize)trilha1Grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)trilha2Grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private TextBox PalestraNome;
        private TextBox PalestraTempo;
        private Button button3;
        private DataGridView trilha1Grid;
        private DataGridView trilha2Grid;
        private Button button4;
    }
}
