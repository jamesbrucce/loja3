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
    public partial class FrmCartaoVenda : Form
    {
        public FrmCartaoVenda()
        {
            InitializeComponent();
            CarregaDgvCartaoVenda();
        }
        public void CarregacbxUsuario()
        {
            SqlConnection con = Class1.obterConexao();
            string usu = "select Id,nome from usuario";
            SqlCommand cmd = new SqlCommand(usu, con);
            Class1.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(usu, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "nome");
            cbxUsuario.ValueMember = "Id";
            cbxUsuario.DisplayMember = "nome";
            cbxUsuario.DataSource = ds.Tables["nome"];
            Class1.fecharConexao();
        }

        public void CarregaDgvCartaoVenda()
        {
            SqlConnection con = Class1.obterConexao();
            String query = "select * from cartaovenda";
            SqlCommand cmd = new SqlCommand(query, con);
            Class1.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable cartaovenda = new DataTable();
            da.Fill(cartaovenda);
            DgvCartaoVenda.DataSource = cartaovenda;
            Class1.fecharConexao();
        }
        private void BtnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Class1.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Inserir_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numero", txtNumero.Text);
                cmd.Parameters.AddWithValue("@usuario", cbxUsuario.Text);
                Class1.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPripedido();
                MessageBox.Show("Registro inserido com sucesso!", "Cadastro", MessageBoxButtons.OK);
                Class1.fecharConexao();
                txtId.Text = "";
                txtNumero.Text = "";
                cbxUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Class1.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Excluir_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Class1.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPripedido();
                MessageBox.Show("Registro apagado com sucesso!", "Excluir Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Class1.fecharConexao();
                txtId.Text = "";
                txtNumero.Text = "";
                cbxUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Class1.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Atualizar_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                cmd.Parameters.AddWithValue("@numero", this.txtNumero.Text);
                cmd.Parameters.AddWithValue("@usuario", this.cbxUsuario.Text);
                Class1.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvCartaoVenda();
                FrmPrincipal obj = (FrmPrincipal)Application.OpenForms["FrmPrincipal"];
                obj.CarregaDgvPripedido();
                MessageBox.Show("Registro atualizado com sucesso!", "Atualizar Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Class1.fecharConexao();
                txtNumero.Text = "";
                cbxUsuario.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void BtnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Class1.obterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Localizar_CartaoVenda";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Class1.obterConexao();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtId.Text = rd["Id"].ToString();
                    txtNumero.Text = rd["numero"].ToString();
                    cbxUsuario.Text = rd["usuario"].ToString();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!", "Sem registro!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
            }
        }

        private void CbxUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = Class1.obterConexao();
            SqlCommand cmd = new SqlCommand("LocalizarUsuario", con);
            cmd.Parameters.AddWithValue("@Id", cbxUsuario.SelectedValue);
            cmd.CommandType = CommandType.StoredProcedure;
            Class1.obterConexao();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                //txt.Text = rd["valor"].ToString();
                txtId.Text = rd["Id"].ToString();
                Class1.fecharConexao();
            }
            else
            {
                MessageBox.Show("Nenhum registro encontrado!", "Erro de Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgvCartaoVenda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmCartaoVenda_Load(object sender, EventArgs e)
        {
            CarregacbxUsuario();
        }

        private void DgvCartaoVenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DgvCartaoVenda.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtNumero.Text = row.Cells[1].Value.ToString();
                cbxUsuario.Text = row.Cells[2].Value.ToString();
            }
        }
    }
}
