#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

using SigaObjects;
using REPORT = SigaObjects.Reports;
#endregion

namespace SigaControls.Report
{
    internal partial class GroupBy : UserControl
    {
        DataTable dados = new DataTable();
        private Table main;

        /// <summary>Retorna um [controle]</summary>
        public Table  MAIN
        {
            get { return main; }
            set { main = value; }
        }
        /// <summary>Retorna a String de ordena��o</summary>
        public String GROUP
        { // TODO <ANY> RETORNO DO GROUPBY (string montada com os campos)
            get
            {
                if (lbCampos.Items.Count > 0)
                    return "";
                else
                    return "";
            }
        }

        #region Construtor
        /// <summary>Chama o m�todo initialize, Inicializa os controles e seus respectivos Members (Display & Value)</summary>
        public GroupBy()
        {
            initialize(null);
        }
        /// <summary>Chama o m�todo initialize, Inicializa os controles e seus respectivos Members (Display & Value)</summary>
        /// <param name="main">Um [controle] mainTable</param>
        public GroupBy(Table main)
        {
            initialize(main);
        }
        #endregion

        #region Aqui � onde ficar� todo o c�digo de inicializa��o do controle groupBy
        /// <summary>Inicializa os controles e seus respectivos Members (Display & Value)</summary>
        /// <param name="main">Um [controle] mainTable</param>
        private void initialize(Table main)
        {
            InitializeComponent();

            cmbTabela.SelectedIndexChanged += new EventHandler(cmbTabela_SelectedIndexChanged);

            cmbCampos.DisplayMember = SXManager.FieldDisplayMember;
            cmbCampos.ValueMember   = SXManager.FieldValueMember;

            cmbTabela.DisplayMember = SXManager.TableDisplayMember;
            cmbTabela.ValueMember   = SXManager.TableValueMember;

            lbCampos.DisplayMember = REPORT.GroupBy.GroupByDao.DisplayMember;
            lbCampos.ValueMember   = REPORT.GroupBy.GroupByDao.ValueMember;

            dados.Columns.Add("id");
            dados.Columns.Add("indice");
            dados.Columns.Add("mainId");
            dados.Columns.Add(REPORT.GroupBy.GroupByDao.DisplayMember);
            dados.Columns.Add(REPORT.GroupBy.GroupByDao.ValueMember  );
            
            this.MAIN = main;
            this.Dock = DockStyle.Fill;
        }
        #endregion

        #region Carrega as Tabelas relacionadas em [cmbTabela]
        /// <summary>Alimenta o combo box cmbTabela com as tabelas relacionadas � tabela principal</summary>
        public void LOAD()
        {
            if (this.MAIN != null)
            {
                /// CARREGAR TABELAS RELACIONADAS.
                cmbTabela.DataSource =
                    new SXManager(sigaSession.EMPRESAS[0].CODIGO)
                    //.getTables("X2_CHAVE IN (" + SXManager.getStringArr(this.MAIN.RELATEDTABLES) + ")")
                    .getTables("X2_CHAVE = '" + this.MAIN.TABLE + "'")
                    .DefaultView;

                /// CARREGAR CONFIGURACAO DE FILDS DO BANCO
                dados = new REPORT.GroupBy.GroupByDao().select(this.MAIN.ID);
                lbCampos.DataSource = dados.DefaultView;
            }
        }
        #endregion

        #region Salvar no Banco de Dados
        /// <summary> Montando a lista para salvar no banco de dados</summary>
        public void SAVE()
        {
            List<REPORT.GroupBy.GroupByVo> groups = new List<REPORT.GroupBy.GroupByVo>();

            for (int i = 0; i < lbCampos.Items.Count; i++)
            {
                REPORT.GroupBy.GroupByVo group = new REPORT.GroupBy.GroupByVo();

                group.MAINID = this.MAIN.ID;
                group.INDICE = i;

                DataRowView valor = (lbCampos.Items[i] as DataRowView);

                group.VALUE   = valor[REPORT.GroupBy.GroupByDao.ValueMember].ToString();
                group.DISPLAY = valor[REPORT.GroupBy.GroupByDao.DisplayMember].ToString();

                groups.Add(group);
            }

            new REPORT.GroupBy.GroupByDao().save(groups);
        }
        #endregion

        #region Remover Item
        /// <summary>Remove um item da lista [lbCampos] e remove tamb�m do seu respectivo DataTable</summary>
        /// <param name="sender">Objeto que mandou</param>
        /// <param name="e">Argumentos de eventos</param>
        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (lbCampos.SelectedIndex != -1)
            {
                dados.Rows[lbCampos.SelectedIndex].Delete();
                lbCampos.DataSource = dados.DefaultView;
            }
        }
        #endregion

        #region Adicionar Item
        /// <summary>Adiciona um item da lista [lbCampos] e remove tamb�m do seu respectivo DataTable</summary>
        /// <param name="sender">Objeto que mandou</param>
        /// <param name="e">Argumentos de eventos</param>
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            DataRowView tabela = (cmbTabela.SelectedItem as DataRowView);
            DataRowView campos = (cmbCampos.SelectedItem as DataRowView);

            string display =
                tabela["X2_NOME"].ToString()   + " ( " +
                campos["X3_TITULO"].ToString() + " ) ";
            string value =
                tabela["X2_CHAVE"].ToString() + "." +
                campos["X3_CAMPO"].ToString();

            if (this.Parent.GetType() == typeof(mainTable))
            {
                DataTable orderByCampos =
                    //ComboBox combo =
                ((
                    (this.Parent as mainTable)
                    .Controls[new OrderBy().Name]
                    .Controls["lbCampos"]
                 ) as ComboBox
                ).DataSource as DataTable;

                if (orderByCampos.DefaultView.Count > 0)
                {
                    if (canAdd(orderByCampos, value))
                    {
                        dados.Rows.Add(new Object[]
                        { 
                            0,//Id
                            (lbCampos.Items.Count - 1),//Indice, o mesmo do controle
                            this.MAIN.ID,//Id do controle pai
                            display,//O valor que � mostrado para o cliente
                            value //O valor que para a forma��o da cl�usula groupBy
                        });
                        lbCampos.DataSource = dados.DefaultView;
                        lbCampos.SelectedIndex = lbCampos.Items.Count - 1;
                    }
                }
            }
        }

        #region Pode Adicionar ?
        private bool canAdd(DataTable orderByCampos, string valueToAdd)
        {
            //Pega os valores do DataSource do controle lbCampos

            DataRow visao;
            //Vis�o de uma linha[Row] do orderByCampos
            //recebe um [Row] qualquer e o manipula

            bool can = false;//pode inserir ?
            for (int i = 0;
                //O momento em que acabar as linhas 
                //do lbCampos do controle orderBy
                //Ou um daqueles valores for igual
                //ao valor que se quer inserir
                //ele p�ra o loop
                i < orderByCampos.DefaultView.Count && !can;
                i++)
            {
                //A vis�o recebe o registro atual
                //da lista bCampos
                visao = orderByCampos.Rows[i];

                //Comparando o valor a ser inserido
                //com o valor atual do orderbyCampos
                can = valueToAdd == visao[4].ToString();
            }

            //pode inserir ?
            return can;
        }
        #endregion

        #region Preenche o combobox cmbCampos
        void cmbTabela_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCampos.DataSource =
                new SXManager(sigaSession.EMPRESAS[0].CODIGO)
                .getFields(cmbTabela.SelectedValue.ToString())
                .DefaultView;
        }
        #endregion

        #endregion

        #region Move Controles
        #region Aux�lio
        private void preencheSwapComDados(DataTable swap)
        {
            swap.Columns.Add("id");
            swap.Columns.Add("indice");
            swap.Columns.Add("mainId");
            swap.Columns.Add(REPORT.GroupBy.GroupByDao.DisplayMember);
            swap.Columns.Add(REPORT.GroupBy.GroupByDao.ValueMember);

            //Passa todos os registros de [dados] para o [swap]
            for (int i = 0; i < dados.DefaultView.Count; i++)
            {
                swap.Rows.Add(
                    new object[]
                    {
                        dados.DefaultView[i]["id"],
                        dados.DefaultView[i]["indice"],
                        dados.DefaultView[i]["mainId"],
                        dados.DefaultView[i][REPORT.GroupBy.GroupByDao.DisplayMember],
                        dados.DefaultView[i][REPORT.GroupBy.GroupByDao.ValueMember]
                    }
                );
            }
        }
        private void adicionaIndiceEmDados(int indice, DataTable swap)
        {
            dados.Rows.Add(
                        new object[]
                    {
                        swap.DefaultView[indice]["id"],
                        swap.DefaultView[indice]["indice"],
                        swap.DefaultView[indice]["mainId"],
                        swap.DefaultView[indice][REPORT.GroupBy.GroupByDao.DisplayMember],
                        swap.DefaultView[indice][REPORT.GroupBy.GroupByDao.ValueMember]
                    }
                    );
        }
        private void limpaEmDados()
        {
            dados.Rows.Clear();
        }
        private void preencheDadosComSwap(DataTable swap)
        {
            for (int i = 0; i < swap.DefaultView.Count; i++)
            {
                dados.Rows.Add(
                    new object[]
                    {
                        swap.DefaultView[i]["id"],
                        swap.DefaultView[i]["indice"],
                        swap.DefaultView[i]["mainId"],
                        swap.DefaultView[i][REPORT.GroupBy.GroupByDao.DisplayMember],
                        swap.DefaultView[i][REPORT.GroupBy.GroupByDao.ValueMember]
                    }
                );
            }
        }
        #endregion

        #region Passa a sele��o do lbCampos para a primeira posi��o
        private void btnPrimeiro_click(object sender, EventArgs e)
        {
            if (lbCampos.SelectedIndex != -1)
            {
                DataTable swap = new DataTable();
                int indice = lbCampos.SelectedIndex;

                //Preenche todo o [swap]
                preencheSwapComDados(swap);

                //Limpa todas as linhas de [dados] (Por�m o Backup est� em [swap])
                limpaEmDados();
                //Adiciona na primeira linha o valor do indice do [swap] (Passa a ser o primeiro)
                adicionaIndiceEmDados(indice, swap);

                //Deleta aquele indice do [swap], mas [dados] cont�m aquele registro na primeira linha
                swap.Rows[indice].Delete();
                //Preenche [dados] com o restante dos dados de [swap]
                preencheDadosComSwap(swap);

                //Passa [dados] como refer�ncia
                lbCampos.DataSource = dados.DefaultView;

                //Mant�m o cursor no primeiro �ndice
                lbCampos.SelectedIndex = 0;
            }
        }
        #endregion

        #region Passa a sele��o do lbCampos para a posi��o de cima
        private void btnCima_Click(object sender, EventArgs e)
        {
            if (lbCampos.SelectedIndex > 0)
            {
                DataTable swap = new DataTable();
                int indice = lbCampos.SelectedIndex;

                preencheSwapComDados(swap);

                //Limpa a tabela de [dados]
                limpaEmDados();

                object[] anterior =
                    new object[]
                    {
                        swap.Rows[indice - 1]["id"],
                        swap.Rows[indice - 1]["indice"],
                        swap.Rows[indice - 1]["mainId"],
                        swap.Rows[indice - 1][REPORT.GroupBy.GroupByDao.DisplayMember],
                        swap.Rows[indice - 1][REPORT.GroupBy.GroupByDao.ValueMember]
                    };
                object[] eu =
                    new object[]
                    {
                        swap.Rows[indice]["id"],
                        swap.Rows[indice]["indice"],
                        swap.Rows[indice]["mainId"],
                        swap.Rows[indice][REPORT.GroupBy.GroupByDao.DisplayMember],
                        swap.Rows[indice][REPORT.GroupBy.GroupByDao.ValueMember]
                    };
                for (int i = 0; i < swap.DefaultView.Count; i++)
                {
                    dados.Rows.Add(
                        i == (indice - 1)
                    ? eu
                    : (i == (indice)
                        ? anterior
                        : new object[]
                        {
                            swap.Rows[i]["id"],
                            swap.Rows[i]["indice"],
                            swap.Rows[i]["mainId"],
                            swap.Rows[i][REPORT.GroupBy.GroupByDao.DisplayMember],
                            swap.Rows[i][REPORT.GroupBy.GroupByDao.ValueMember]
                        }
                       )
                    );
                }

                //Mant�m o cursor 1 �ndice acima
                lbCampos.SelectedIndex = indice - 1;
            }
        }
        #endregion

        #region Passa a sele��o do lbCampos para a posi��o de baixo
        private void btnBaixo_Click(object sender, EventArgs e)
        {
            if (lbCampos.SelectedIndex < lbCampos.Items.Count - 1)
            {
                DataTable swap = new DataTable();
                int indice = lbCampos.SelectedIndex;

                preencheSwapComDados(swap);

                //Limpa a tabela de [dados]
                limpaEmDados();

                object[] anterior =
                    new object[]
                    {
                        swap.Rows[indice]["id"],
                        swap.Rows[indice]["indice"],
                        swap.Rows[indice]["mainId"],
                        swap.Rows[indice][REPORT.GroupBy.GroupByDao.DisplayMember],
                        swap.Rows[indice][REPORT.GroupBy.GroupByDao.ValueMember]
                    };
                object[] proximo =
                    new object[]
                    {
                        swap.Rows[indice + 1]["id"],
                        swap.Rows[indice + 1]["indice"],
                        swap.Rows[indice + 1]["mainId"],
                        swap.Rows[indice + 1][REPORT.GroupBy.GroupByDao.DisplayMember],
                        swap.Rows[indice + 1][REPORT.GroupBy.GroupByDao.ValueMember]
                    };
                for (int i = 0; i < swap.DefaultView.Count; i++)
                {
                    dados.Rows.Add(
                        i == indice
                    ? proximo
                    : (i == (indice + 1)
                        ? anterior
                        : new object[]
                        {
                            swap.Rows[i]["id"],
                            swap.Rows[i]["indice"],
                            swap.Rows[i]["mainId"],
                            swap.Rows[i][REPORT.GroupBy.GroupByDao.DisplayMember],
                            swap.Rows[i][REPORT.GroupBy.GroupByDao.ValueMember]
                        }
                       )
                    );
                }

                //Mant�m o cursor 1 �ndice abaixo
                lbCampos.SelectedIndex = indice + 1;
            }
        }
        #endregion

        #region Passa a sele��o do lbCampos para a �ltima posi��o
        private void btnUltimo_Click(object sender, EventArgs e)
        {
            DataTable swap = new DataTable();
            int indice = lbCampos.SelectedIndex;

            //Passa todos os registros de [dados] para o [swap]
            preencheSwapComDados(swap);

            //Limpa todas as linhas de [dados] (Por�m o Backup est� em [swap])
            limpaEmDados();

            //Guarda o que ser� o �ltimo �ndice
            object[] ultimo =
                new object[]
                    {
                        swap.DefaultView[indice]["id"],
                        swap.DefaultView[indice]["indice"],
                        swap.DefaultView[indice]["mainId"],
                        swap.DefaultView[indice][REPORT.GroupBy.GroupByDao.DisplayMember],
                        swap.DefaultView[indice][REPORT.GroupBy.GroupByDao.ValueMember]
                    };

            //Deleta aquele indice do [swap], mas dados cont�m aquele registro na primeira linha
            swap.Rows[indice].Delete();

            //Preenche [dados] com o restante dos dados de [swap]
            preencheDadosComSwap(swap);

            //Adiciona o �ltimo registro
            dados.Rows.Add(ultimo);

            //Passa dados como refer�ncia para o conjunto de dados
            //de lbCampos, Recebendo os registros de [dados],
            //lbCampos poder� mostrar esses registros para o usu�rio
            //sendo assim o usu�rio poder� manipular o DataSource
            //atrav�s de um controle visual
            lbCampos.DataSource = dados.DefaultView;

            //Mant�m o cursor no �ltimo �ndice
            lbCampos.SelectedIndex = lbCampos.Items.Count - 1;
        }
        #endregion
        #endregion
    }
}