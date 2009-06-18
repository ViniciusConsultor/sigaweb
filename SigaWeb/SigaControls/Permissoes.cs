#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

using SigaObjects.Session.SysUsers;
using SigaObjects.Session.UsersGroups;
using SigaObjects.Reports.Report;

using SigaObjects.Permissoes;
using SigaObjects.Permissoes.RelGrupo;
using SigaObjects.Permissoes.RelUsu;

#endregion

namespace SigaControls
{
    public partial class Permissoes : UserControl
    {
        #region Construtor
        public Permissoes()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            DataTable tbUsuarios   = new SysUserDao().select();
            DataTable tbGrupos     = new UserGroupDao().select();
            DataTable tbRelatorios = new ReportDao().select();

            addOpcaoTodos(tbUsuarios  , SysUserDao.DisplayMember  , SysUserDao.ValueMember  );
            addOpcaoTodos(tbGrupos    , UserGroupDao.DisplayMember, UserGroupDao.ValueMember);
            addOpcaoTodos(tbRelatorios, ReportDao.DisplayMember   , ReportDao.ValueMember   );

            //Listando todos os usu�rios
            cmbUsuarios.DisplayMember = SysUserDao.DisplayMember;
            cmbUsuarios.ValueMember   = SysUserDao.ValueMember;
            cmbUsuarios.DataSource    = tbUsuarios;

            //Listando todos os Grupos de Usu�rios
            cmbGrupo.DisplayMember = UserGroupDao.DisplayMember;
            cmbGrupo.ValueMember   = UserGroupDao.ValueMember;
            cmbGrupo.DataSource    = tbGrupos;

            //Listando todos os relat�rios
            cmbRelUsu.DisplayMember = ReportDao.DisplayMember;
            cmbRelUsu.ValueMember   = ReportDao.ValueMember;
            cmbRelUsu.DataSource    = tbRelatorios;

            cmbRelGrupo.DisplayMember = ReportDao.DisplayMember;
            cmbRelGrupo.ValueMember   = ReportDao.ValueMember;
            cmbRelGrupo.DataSource    = tbRelatorios;
        }
        #endregion

        #region Filtrar
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            int idReport = (int)(cmbRelUsu.SelectedItem as DataRowView)["id"];
            int idUsuario = (int)(cmbUsuarios.SelectedItem as DataRowView)["id"];

            string filtroReport = idReport == 0 ? "" : "RR.id = " + idReport;
            string filtroUsuario = idUsuario == 0 ? "" : "SU.id = " + idUsuario;
            string filtro = "";

            if (filtroUsuario != "")
            {
                filtro = filtroUsuario;
            }
            if (filtroReport != "")
            {
                filtro += ((filtro.Length > 0) ? "   AND " : "");
                filtro += filtroReport;
            }
            filtro = filtro.Length == 0 ? null : filtro;
            gridRelUsu.DataSource = new RelUsuDao().select(filtro);

            if (gridRelUsu.Columns.Contains("id"))
                gridRelUsu.Columns["id"].Visible = false;
            if (gridRelUsu.Columns.Contains("idUser"))
                gridRelUsu.Columns["idUser"].Visible = false;
            if (gridRelUsu.Columns.Contains("idReport"))
                gridRelUsu.Columns["idReport"].Visible = false;
            if (gridRelUsu.Columns.Contains("N�vel"))
                gridRelUsu.Columns["N�vel"].Visible = false;

            //Inserindo campo Permiss�o
            updatePermissoesRows(gridRelUsu.Rows,gridRelUsu);
        }
        private void btnFiltrarGrupo_Click(object sender, EventArgs e)
        {
            int idReport = (int)(cmbRelGrupo.SelectedItem as DataRowView)["id"];
            int idGrupo = (int)(cmbGrupo.SelectedItem as DataRowView)["id"];

            string filtroReport = idReport == 0 ? "" : "RR.id = " + idReport;
            string filtroGrupo = idGrupo == 0 ? "" : "UG.id = " + idGrupo;
            string filtro = "";

            if (filtroGrupo != "")
            {
                filtro = filtroGrupo;
            }
            if (filtroReport != "")
            {
                filtro += ((filtro.Length > 0) ? " AND " : "");
                filtro += filtroReport;
            }
            filtro = filtro.Length == 0 ? null : filtro;
            gridRelGrupo.DataSource = new RelGrupoDao().select(filtro);

            if (gridRelGrupo.Columns.Contains("id"))
                gridRelGrupo.Columns["id"].Visible = false;
            if (gridRelGrupo.Columns.Contains("idUser"))
                gridRelGrupo.Columns["idUserGroup"].Visible = false;
            if (gridRelGrupo.Columns.Contains("idReport"))
                gridRelGrupo.Columns["idReport"].Visible = false;
            if (gridRelGrupo.Columns.Contains("N�vel"))
                gridRelGrupo.Columns["N�vel"].Visible = false;

            //Inserindo campo Permiss�o
            updatePermissoesRows(gridRelGrupo.Rows, gridRelGrupo);
        }
        #endregion

        #region Adiciona uma coluna a um n�mero qualquer de [DataGridView]
        private void addColuna(string coluna, params DataGridView[] grids)
        {
            foreach (DataGridView grid in grids)
                if (!grid.Columns.Contains(coluna))
                    grid.Columns.Add(coluna, coluna);
        }
        #endregion

        #region Esconde a Coluna com o valor Inteiro da Permiss�o e Coloca um valor String
        private void updatePermissoesRows(DataGridViewRowCollection rows, DataGridView grid)
        {
            //Este m�todo deve ser utilizado sen�o n�o � poss�vel
            //Saber se esta coluna estar� nos grids escolhidos
            addColuna("Permiss�o", grid);

            foreach (DataGridViewRow row in rows)
                switch ((int)row.Cells["N�vel"].Value)
                {
                    case 0: row.Cells["Permiss�o"].Value = Nivel.Nenhuma; break;
                    case 1: row.Cells["Permiss�o"].Value = Nivel.Visualizar; break;
                    case 2: row.Cells["Permiss�o"].Value = Nivel.Adicionar; break;
                    case 3: row.Cells["Permiss�o"].Value = Nivel.Editar; break;
                    case 4: row.Cells["Permiss�o"].Value = Nivel.Deletar; break;
                    default: row.Cells["Permiss�o"].Value = Nivel.NaoDefinido; break;
                }
        }
        public void updatePermissoesSelectedRows(DataGridViewSelectedRowCollection selectedRows)
        {
            //Este m�todo deve ser utilizado sen�o n�o � poss�vel
            //Saber se esta coluna estar� nos grids escolhidos
            addColuna("Permiss�o", gridRelGrupo, gridRelUsu);

            foreach (DataGridViewRow row in selectedRows)
                switch ((int)row.Cells["N�vel"].Value)
                {
                    case 0: row.Cells["Permiss�o"].Value = Nivel.Nenhuma; break;
                    case 1: row.Cells["Permiss�o"].Value = Nivel.Visualizar; break;
                    case 2: row.Cells["Permiss�o"].Value = Nivel.Adicionar; break;
                    case 3: row.Cells["Permiss�o"].Value = Nivel.Editar; break;
                    case 4: row.Cells["Permiss�o"].Value = Nivel.Deletar; break;
                    default: row.Cells["Permiss�o"].Value = Nivel.NaoDefinido; break;
                }
        }
        #endregion

        #region Definir
        private void btnDefinir_Click(object sender, EventArgs e)
        {
            ControlsConfig.formShow(new DefinirPermissoes(gridRelUsu), this.Form, ControlsConfig.showType.Dialog);
        }
        private void btnDefinirGrupo_Click(object sender, EventArgs e)
        {
            ControlsConfig.formShow(new DefinirPermissoes(gridRelGrupo), this.Form, ControlsConfig.showType.Dialog);
        }
        #endregion

        #region Salvar
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            new RelUsuDao().commit(getRelUsuModificados());
            new RelGrupoDao().commit(getRelGrupoModificados());

            btnFiltrar_Click(sender, e);
            btnFiltrarGrupo_Click(sender, e);
        }
        #endregion

        #region Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
        #endregion

        #region Outros
        private void addOpcaoTodos(DataTable tb, string DisplayMember, string ValueMember)
        {
            //Adicionando nova linha
            tb.DefaultView.AddNew();
            //Fazer esta nova linha receber os valores da primeira linha
            tb.DefaultView[tb.DefaultView.Count - 1][DisplayMember] = tb.DefaultView[0][DisplayMember];
            tb.DefaultView[tb.DefaultView.Count - 1][ValueMember] = tb.DefaultView[0][ValueMember];
            //Fazer a coluna username receber a opcao todos e o id receber ""(vazio) da �ltima linha
            tb.DefaultView[0][DisplayMember] = "[TODOS]";
            tb.DefaultView[0][ValueMember] = 0;
        }
        private List<RelUsuVo> getRelUsuModificados()
        {
            List<RelUsuVo> lista = new List<RelUsuVo>();

            if (gridRelUsu.DataSource != null)
                foreach (DataRow row in (gridRelUsu.DataSource as DataTable).Select(null, null, DataViewRowState.ModifiedCurrent))
                    //Adiciona um novo RelUsu com os dados atuais do conjunto de linhas modificadas
                    lista.Add(
                        new RelUsuVo((int)row["idUser"], (int)row["idReport"], (int)row["N�vel"]));

            return lista;
        }
        private List<RelGrupoVo> getRelGrupoModificados()
        {
            List<RelGrupoVo> lista = new List<RelGrupoVo>();

            if (gridRelGrupo.DataSource != null)
                foreach (DataRow row in (gridRelGrupo.DataSource as DataTable).Select(null, null, DataViewRowState.ModifiedCurrent))
                    //Adiciona um novo RelGrupo com os dados atuais do conjunto de linhas modificadas
                    lista.Add(
                        new RelGrupoVo((int)row["idUserGroup"], (int)row["idReport"], (int)row["N�vel"]));

            return lista;
        }
        #endregion
    }
}