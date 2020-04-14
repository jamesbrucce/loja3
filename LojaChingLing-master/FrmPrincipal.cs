using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LojaCL
{
    
    
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }
        
        public void CarregaDgvPripedido()
        {
            // esta conectando com o banco atraves da classe que foi feita a string de conexao
            SqlConnection con = Class1.obterConexao();
            String query = "select * from cartaovenda";
            SqlCommand cmd = new SqlCommand(query, con);
            Class1.obterConexao();
            cmd.CommandType = CommandType.Text;
            //SQLDataAdapter, usado para preencher o DataTable
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Adiciona um DataTable carregado da memoria
            DataTable cartao = new DataTable();
            //aqui esta fazendo o preenchemento do da para o cartao atraves do comando Fill
            da.Fill(cartao);
            //Onde ira capturar toda a fonte de dados
            dgvPriPedido.DataSource = cartao;
            //Quando for criar um controle em tempo de execução, e importante atribuir um nome para ele, e as principais propriedades do controle
            DataGridViewButtonColumn fechar = new DataGridViewButtonColumn();
            fechar.Name = "FecharConta";
            fechar.HeaderText = "Fechar Conta";
            fechar.Text = "Fechar conta";
            fechar.UseColumnTextForButtonValue = true;
            int columIndex = 4;
            dgvPriPedido.Columns.Insert(columIndex, fechar);
            Class1.fecharConexao();
            dgvPriPedido.CellClick += DgvPriPedido_CellClick;
            //Criando uma variavel coluna e contando as colunas do datagrid especificado
            int colunas = dgvPriPedido.ColumnCount;
            if(colunas > 5)
            {
                dgvPriPedido.Columns.Remove("FecharConta");
            }



        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudCliente cli = new FrmCrudCliente();
            cli.Show();
        }

        private void testarBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Class1.obterConexao();
                String query = "select * from cliente";
                SqlCommand cmd = new SqlCommand(query, con);
                Class1.obterConexao();
                DataSet ds = new DataSet();
                MessageBox.Show("Conectado ao Banco de Dados com Sucesso!", "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Class1.fecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudProduto pro = new FrmCrudProduto();
            pro.Show();
        }

        private void vendasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmVenda ven = new FrmVenda();
            ven.Show();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrudUsuario usu = new FrmCrudUsuario();
            usu.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CarregaDgvPripedido();
        }

        private void CadastrosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void DgvPriPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.ColumnIndex == dgvPriPedido.Columns["FecharConta"].Index)
                {
                    if(Application.OpenForms["FrmVenda"] == null)
                    {
                        FrmVenda ven = new FrmVenda();
                        ven.Show();
                    }
                }
            }
            catch
            {
                
            }
        }

        private void CartaoDeVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCartaoVenda carven = new FrmCartaoVenda();
            carven.Show();
        }
    }
}
