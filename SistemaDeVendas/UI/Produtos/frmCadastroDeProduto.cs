using SistemaDeVendas.BLL;
using SistemaDeVendas.Models;
using SistemaDeVendas.Models.Produto;
using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaDeVendas.UI.Produtos
{
    public partial class frmCadastroDeProduto : Form
    {
        BLL_Produto _bllProduto = new BLL_Produto();
        int IdProduto;
        public frmCadastroDeProduto()
        {
            InitializeComponent();
        }

        private void frmCadastroDeProduto_Load(object sender, EventArgs e)
        {
            CarregarProdutosCadastrados();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {                
                Produto produto = new Produto
                {
                    Codigo = txtCodigo.Text,
                    Nome = txtNome.Text,
                    Preco = decimal.Parse(txtPreco.Text),
                    Descricao = txtDescricao.Text,
                    Quantidade = int.Parse(txtQuantidade.Text)
                };

               
               ResultadoValidacao resultadoValidacao = _bllProduto.ValidarProduto(produto);

                if (!string.IsNullOrEmpty(resultadoValidacao.MensagemErro))
                {
                    MessageBox.Show(resultadoValidacao.MensagemErro, "Erro de Validação.");

                    switch (resultadoValidacao.CampoInvalido)
                    {
                        case "Codigo":
                            txtCodigo.Focus();
                            break;

                        case "Nome":
                            txtNome.Focus();
                            break;

                        case "Preco":
                            txtPreco.Focus();
                            break;

                        case "Descricao":
                            txtDescricao.Focus();
                            break;

                        case "Quantidade":
                            txtQuantidade.Focus();
                            break;
                    }
                }

                else
                {
                    string retornoCadastro =  _bllProduto.CadastrarProduto(produto);

                    MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }               
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Erro de validação: {ex.Message}", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarProdutosCadastrados()
        {
            try
            {
                DataTable produtos = _bllProduto.BuscarProdutos(); 
                dgvProdutosCadastrados.DataSource = produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar produtos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtPreco.Clear();
            txtDescricao.Clear();
            txtQuantidade.Clear();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Por favor, selecione um cliente para editar.", "Atenção");
                return;
            }


            // Criar o objeto cliente com os dados editados
            Produto produtoEditado = new Produto
            {
                Id = IdProduto,
                Nome = txtNome.Text,
                Codigo = txtCodigo.Text,
                Descricao = txtDescricao.Text,
                Preco = decimal.Parse(txtPreco.Text),
                Quantidade = int.Parse(txtQuantidade.Text)
            };

            // Validar os dados do cliente
            ResultadoValidacao resultado = _bllProduto.ValidarProduto(produtoEditado);

            if (!string.IsNullOrEmpty(resultado.MensagemErro))
            {
                MessageBox.Show(resultado.MensagemErro, "Erro de Validação");
                return;
            }

            // Atualizar os dados do cliente no banco
            string retornoEdicao = _bllProduto.AtualizarProduto(produtoEditado);

            if (!string.IsNullOrEmpty(retornoEdicao))
            {
                MessageBox.Show(retornoEdicao, "Resultado da Edição");
                CarregarProdutosCadastrados(); // Atualizar a lista de clientes
            }

            LimparCampos();
        }       

        private void dgvProdutosCadastrados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                IdProduto = Convert.ToInt32(dgvProdutosCadastrados.Rows[e.RowIndex].Cells["IdProduto"].Value);

                txtNome.Text = dgvProdutosCadastrados.Rows[e.RowIndex].Cells["Nome"].Value.ToString();
                txtCodigo.Text = dgvProdutosCadastrados.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                txtDescricao.Text = dgvProdutosCadastrados.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();
                txtPreco.Text = dgvProdutosCadastrados.Rows[e.RowIndex].Cells["Preco"].Value.ToString();
                txtQuantidade.Text = dgvProdutosCadastrados.Rows[e.RowIndex].Cells["Estoque"].Value.ToString();

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvProdutosCadastrados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um produto para excluir.", "Atenção");
                return;
            }

            // Obter o ID do produto selecionado
            int produtoId = Convert.ToInt32(dgvProdutosCadastrados.SelectedRows[0].Cells["IdProduto"].Value);

            // Confirmar a exclusão
            var confirmResult = MessageBox.Show(
                "Tem certeza de que deseja excluir este produto?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string resultadoExclusao = _bllProduto.ExcluirProduto(produtoId);

                    if (!string.IsNullOrEmpty(resultadoExclusao))
                    {
                        MessageBox.Show(resultadoExclusao, "Resultado");
                        CarregarProdutosCadastrados(); // Atualiza a lista de produtos após a exclusão
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir o produto: {ex.Message}", "Erro");
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtCodigo.Clear();
            txtDescricao.Clear();
            txtPreco.Clear();
            txtQuantidade.Clear();
        }
    }
}