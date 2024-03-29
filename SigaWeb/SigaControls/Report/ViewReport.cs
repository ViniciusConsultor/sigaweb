#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

using REPORT = SigaObjects.Reports;
#endregion

namespace SigaControls.Report
{
    public partial class ViewReport : UserControl
    {
        private bool destroy = false;
        private string rotulo = "Parametros do relat�rio : {0}";

        private REPORT.Report.ReportVo _report = new REPORT.Report.ReportVo();
        public  REPORT.Report.ReportVo RELATORIO
        {
            get { return _report;  }
            set
            {
                _report = value;
                //lblREPORT.Text = string.Format(rotulo, value.NOME);
            }
        }

        private void initializer()
        {
            InitializeComponent();
            
            this.Dock = DockStyle.Fill;
            // TODO Resolver problema do 'WITH' pro sql2000, o qual n�o suporta o metodo.
            // dicas no link: http://www.databasejournal.com/features/mssql/article.php/3415541/Cursors-with-SQL-2000-Part-1.htm
            this.popScreenFromRecursiveTables();
        }
        private void popScreenFromRecursiveTables()
        {
            panelParams.Controls.Clear();

            REPORT.Table.TableVo tabela = new REPORT.Table.TableVo();
            new REPORT.Table.TableDao().load(tabela, this.RELATORIO.ID, 0);

            DataTable tabelas = new REPORT.Params.ParamsDao().getRecursiveTables(tabela, "userParms_"+sigaSession.LoggedUser.ID, tabela.ID);

            if (tabelas.DefaultView.Count == 0)
            { btnExec_Click(null, null); destroy = true; }
            
            foreach (DataRow row in tabelas.Rows)
            {
                string tagFormat    = "@$TAB$@.$CAMPO$ ?? '@?@'";
                string controle     = (string)row["formato"];
                string strNomeCampo = new SigaObjects
                                         .SXManager(sigaSession.EMPRESAS[0].CODIGO)
                                         .getFields((string)row["tabela"], SigaObjects.SXManager.FieldValueMember
                                                                        + " = '"
                                                                        + (string)row["campo"]
                                                                        + "'"
                                                                        , null)
                                         .Rows[0][SigaObjects.SXManager.FieldDisplayMember].ToString();
                //
                // CONTROLE DE
                Control cDE      = FormatScreen.getObjectFromSigaType(controle);
                string lblDE     = strNomeCampo + "\t\t Entre  \t\t";
                cDE.Tag = tagFormat
                          .Replace("$TAB$"  ,(string)row["tabela"])
                          .Replace("$CAMPO$",(string)row["campo" ])
                          .Replace("??"     , ">="                );

                FormatScreen.AddControl(panelParams, new Label(lblDE) ,true, 3, false, false);
                FormatScreen.AddControl(panelParams, cDE              ,true, 3, false, false);
                
                //
                // CONTROLE ATE
                Control cATE     = FormatScreen.getObjectFromSigaType(controle);
                string lblATE    = string.Format("{0,50}","e");
                cATE.Tag = tagFormat
                          .Replace("$TAB$"  ,(string)row["tabela"])
                          .Replace("$CAMPO$",(string)row["campo" ])
                          .Replace("??"     , "<="                );

                //FormatScreen.AddControl(panelParams, new Label(lblATE),true, 4, false, false);
                FormatScreen.AddControl(panelParams, cATE             ,true, 3, false, false);
            }
        }

        public ViewReport()
        {
            initializer();
        }
        public ViewReport(int                    reportId  )
        {
            REPORT.Report.ReportVo relatorio = new REPORT.Report.ReportVo();
            new REPORT.Report.ReportDao().load(relatorio, 0, "id = "+reportId);
            this.RELATORIO = relatorio;
            
            initializer();
        }
        public ViewReport(string                 reportname)
        {
            REPORT.Report.ReportVo relatorio = new REPORT.Report.ReportVo();
            new REPORT.Report.ReportDao().load(relatorio, 0, "nome = '"+reportname+"'");
            this.RELATORIO = relatorio;

            initializer();
        }
        public ViewReport(REPORT.Report.ReportVo report    )
        {
            this.RELATORIO = report;
            initializer();
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            // TODO varrer lista de controles, usar as TAGs pra fazer os filtros.
            StringBuilder filtro = new StringBuilder();
            foreach (Control c in panelParams.Controls)
            {
                string _and = string.IsNullOrEmpty(filtro.ToString()) ? "" : "   AND ";

                if (c.GetType() == typeof(TextBox))
                {
                    TextBox texto = c as TextBox;
                    filtro.AppendLine(_and + texto.Tag.ToString().Replace("@?@", texto.Text));
                }
                if (c.GetType() == typeof(DateTimePicker))
                {
                    DateTimePicker data = c as DateTimePicker;
                    filtro.AppendLine(_and + data.Tag.ToString().Replace("@?@", data.Value.ToString("yyyyMMdd")));
                }
            }

            Report cReport = new Report();
            cReport.LOAD(this.RELATORIO.NOME, false);
            cReport.TABLE.FILTROPARAMETRO = filtro.ToString();

            gridWindow grid = new gridWindow(cReport.TABLE.QUERY.ToString(), null);
            grid.SetGridHeader(cReport.TABLE.FIELDS.TOGRID);
            grid.showWindow();
        }

        private void ViewReport_VisibleChanged(object sender, EventArgs e)
        {
            lblREPORT.Text = string.Format(rotulo, this.RELATORIO.NOME);

            if (this.destroy)
                this.Form.Close();
        }
    }
}