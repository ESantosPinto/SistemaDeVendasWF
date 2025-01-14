using SistemaDeVendas.BLL;
using SistemaDeVendas.DAL;
using SistemaDeVendas.Models;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace SistemaDeVendas
{
    public partial class frmCadastroDeCliente : Form
    {
        BLL_Endereco _bllEndereco = new BLL_Endereco();
        BLL_Cliente _bllCliente = new BLL_Cliente();

        public frmCadastroDeCliente()
        {
            InitializeComponent();
        }

        private void frmCadastroDeCliente_Load(object sender, EventArgs e)
        {
            CarregaGridClientes();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente
            {
                Nome = txtNome.Text,
                Cpf = StringUtils.RemoverFormatacao(mtbCpf.Text),
                Email = txtEmail.Text,
                Telefone = StringUtils.RemoverFormatacao(txtTelefone.Text),
                Cep = StringUtils.RemoverFormatacao(mtbCep.Text),
                Bairro = txtBairro.Text,
                Logradouro = txtEndereco.Text,
                Numero = txtNumero.Text,
                Cidade = txtCidade.Text,
                UF = txtEstado.Text
            };

            ResultadoValidacao resultado = _bllCliente.ValidarDadosCliente(cliente);

            if (!string.IsNullOrEmpty(resultado.MensagemErro))
            {
                MessageBox.Show(resultado.MensagemErro, "Erro de Validação");

                // Colocar o foco no campo inválido
                switch (resultado.CampoInvalido)
                {
                    case "Nome":
                        txtNome.Focus();
                        break;

                    case "Cpf":
                        mtbCpf.Focus();
                        break;

                    case "Telefone":
                        txtTelefone.Focus();
                        break;
                    case "Cep":
                        mtbCep.Focus();
                        break;
                    case "Numero":
                        txtNumero.Focus();
                        break;
                }
            }
            else
            {
                string retornoCadastro = _bllCliente.CadastrarCliente(cliente);

                if (!string.IsNullOrEmpty(retornoCadastro))
                {
                    MessageBox.Show(retornoCadastro, "Resultado do Cadastro");
                    CarregaGridClientes();
                }
                else
                {
                    MessageBox.Show("O cadastro foi realizado, mas nenhuma mensagem foi retornada.", "Atenção");
                }
            }
        }


        private void mtbCpf_Leave(object sender, EventArgs e)
        {
            var isValid = ValidadorDeDocumento.ValidarCpf(mtbCpf.Text);
            if (!isValid)
            {
                MessageBox.Show("CPF inválido. Por favor, insira um CPF válido.");
            }
        }

        private async void mtbCep_Leave(object sender, EventArgs e)
        {
            string cep = StringUtils.RemoverFormatacao(mtbCep.Text);

            if (cep.Length == 8 && int.TryParse(cep, out _))
            {
                try
                {
                    var endereco = await _bllEndereco.BuscarEndereco(cep);

                    txtBairro.Text = endereco?.Bairro ?? "";
                    txtCidade.Text = endereco?.Cidade ?? "";
                    txtEstado.Text = endereco?.UF ?? "";
                    txtEndereco.Text = endereco?.Logradouro ?? "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao buscar endereço: {ex.Message}", "Erro");
                }
            }
        }      

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvClientesCadastrados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um cliente para excluir.", "Atenção");
                return;
            }

            // Obter o ID do cliente selecionado
            int clienteId = Convert.ToInt32(dgvClientesCadastrados.SelectedRows[0].Cells["Id"].Value);

            // Confirmar a exclusão
            var confirmResult = MessageBox.Show(
                "Tem certeza de que deseja excluir este cliente?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string resultadoExclusao = _bllCliente.ExcluirCliente(clienteId);

                    if (!string.IsNullOrEmpty(resultadoExclusao))
                    {
                        MessageBox.Show(resultadoExclusao, "Resultado");
                        CarregaGridClientes(); // Atualiza a lista de clientes após a exclusão
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir o cliente: {ex.Message}", "Erro");
                }
            }
        }
        private void CarregaGridClientes()
        {
            try
            {
                object cliente = _bllCliente.ConsultarCliente();
                dgvClientesCadastrados.DataSource = cliente;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar a lista de clientes: {ex.Message}", "Erro");
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            mtbCpf.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            mtbCep.Clear();
            txtBairro.Clear();
            txtEndereco.Clear();
            txtNumero.Clear();
            txtCidade.Clear();
            txtEstado.Clear();


            txtNome.Focus();
        }

       
    }
}  

