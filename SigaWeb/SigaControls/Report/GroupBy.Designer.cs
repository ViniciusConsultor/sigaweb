namespace SigaControls.Report
{
    partial class GroupBy
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

        #region Visual WebGui UserControl Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPrimeiro = new Gizmox.WebGUI.Forms.Button();
            this.btnUltimo = new Gizmox.WebGUI.Forms.Button();
            this.btnBaixo = new Gizmox.WebGUI.Forms.Button();
            this.btnCima = new Gizmox.WebGUI.Forms.Button();
            this.btnRemover = new Gizmox.WebGUI.Forms.Button();
            this.btnAdicionar = new Gizmox.WebGUI.Forms.Button();
            this.lbCampos = new Gizmox.WebGUI.Forms.ListBox();
            this.label2 = new Gizmox.WebGUI.Forms.Label();
            this.label1 = new Gizmox.WebGUI.Forms.Label();
            this.cmbCampos = new Gizmox.WebGUI.Forms.ComboBox();
            this.cmbTabela = new Gizmox.WebGUI.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnPrimeiro
            // 
            this.btnPrimeiro.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.btnPrimeiro.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrimeiro.Location = new System.Drawing.Point(378, 105);
            this.btnPrimeiro.Name = "btnPrimeiro";
            this.btnPrimeiro.Size = new System.Drawing.Size(40, 23);
            this.btnPrimeiro.TabIndex = 7;
            this.btnPrimeiro.Text = "▲▲";
            this.btnPrimeiro.Click += new System.EventHandler(this.btnPrimeiro_click);
            // 
            // btnUltimo
            // 
            this.btnUltimo.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.btnUltimo.Font = new System.Drawing.Font("Arial", 8F);
            this.btnUltimo.Location = new System.Drawing.Point(378, 192);
            this.btnUltimo.Name = "btnUltimo";
            this.btnUltimo.Size = new System.Drawing.Size(40, 23);
            this.btnUltimo.TabIndex = 8;
            this.btnUltimo.Text = "▼▼";
            this.btnUltimo.Click += new System.EventHandler(this.btnUltimo_Click);
            // 
            // btnBaixo
            // 
            this.btnBaixo.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.btnBaixo.Font = new System.Drawing.Font("Arial", 8F);
            this.btnBaixo.Location = new System.Drawing.Point(378, 163);
            this.btnBaixo.Name = "btnBaixo";
            this.btnBaixo.Size = new System.Drawing.Size(40, 23);
            this.btnBaixo.TabIndex = 8;
            this.btnBaixo.Text = "▼";
            this.btnBaixo.Click += new System.EventHandler(this.btnBaixo_Click);
            // 
            // btnCima
            // 
            this.btnCima.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.btnCima.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCima.Location = new System.Drawing.Point(378, 134);
            this.btnCima.Name = "btnCima";
            this.btnCima.Size = new System.Drawing.Size(40, 23);
            this.btnCima.TabIndex = 7;
            this.btnCima.Text = "▲";
            this.btnCima.Click += new System.EventHandler(this.btnCima_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.btnRemover.Location = new System.Drawing.Point(378, 61);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(75, 23);
            this.btnRemover.TabIndex = 6;
            this.btnRemover.Text = "Remover";
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.btnAdicionar.Location = new System.Drawing.Point(378, 32);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionar.TabIndex = 5;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // lbCampos
            // 
            this.lbCampos.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Bottom)
                        | Gizmox.WebGUI.Forms.AnchorStyles.Left)
                        | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.lbCampos.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.lbCampos.Location = new System.Drawing.Point(3, 61);
            this.lbCampos.Name = "lbCampos";
            this.lbCampos.SelectionMode = Gizmox.WebGUI.Forms.SelectionMode.One;
            this.lbCampos.Size = new System.Drawing.Size(369, 368);
            this.lbCampos.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(139, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Campos :";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tabela :";
            // 
            // cmbCampos
            // 
            this.cmbCampos.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)(((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Left)
                        | Gizmox.WebGUI.Forms.AnchorStyles.Right)));
            this.cmbCampos.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cmbCampos.DropDownWidth = 228;
            this.cmbCampos.Location = new System.Drawing.Point(139, 34);
            this.cmbCampos.Name = "cmbCampos";
            this.cmbCampos.Size = new System.Drawing.Size(233, 21);
            this.cmbCampos.TabIndex = 1;
            // 
            // cmbTabela
            // 
            this.cmbTabela.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Fixed3D;
            this.cmbTabela.DropDownWidth = 121;
            this.cmbTabela.Location = new System.Drawing.Point(3, 34);
            this.cmbTabela.Name = "cmbTabela";
            this.cmbTabela.Size = new System.Drawing.Size(121, 21);
            this.cmbTabela.TabIndex = 0;
            // 
            // GroupBy
            // 
            this.Controls.Add(this.cmbTabela);
            this.Controls.Add(this.cmbCampos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbCampos);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnCima);
            this.Controls.Add(this.btnBaixo);
            this.Controls.Add(this.btnUltimo);
            this.Controls.Add(this.btnPrimeiro);
            this.Location = new System.Drawing.Point(15, 15);
            this.Size = new System.Drawing.Size(473, 432);
            this.Text = "groupBy";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Button btnPrimeiro;
        private Gizmox.WebGUI.Forms.Button btnUltimo;
        private Gizmox.WebGUI.Forms.Button btnBaixo;
        private Gizmox.WebGUI.Forms.Button btnCima;
        private Gizmox.WebGUI.Forms.Button btnRemover;
        private Gizmox.WebGUI.Forms.Button btnAdicionar;
        private Gizmox.WebGUI.Forms.ListBox lbCampos;
        private Gizmox.WebGUI.Forms.Label label2;
        private Gizmox.WebGUI.Forms.Label label1;
        private Gizmox.WebGUI.Forms.ComboBox cmbCampos;
        private Gizmox.WebGUI.Forms.ComboBox cmbTabela;



    }
}