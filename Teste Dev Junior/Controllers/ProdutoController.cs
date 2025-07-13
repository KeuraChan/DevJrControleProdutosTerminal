using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Teste_Dev_Junior.Data;
using Teste_Dev_Junior.Models;

namespace Teste_Dev_Junior.Controllers
{
    public class ProdutoController
    {
        private List<Produto> produtos;
        
        public List<Produto> ListarProdutos()
        {
            ProdutoDAO conexao = new ProdutoDAO();

            var list = conexao.CarregarTodosProdutos();

            return list;
        }

        public Produto ListarProdutoPorId(int id)
        {
            ProdutoDAO conexao = new ProdutoDAO();

            var produto = conexao.CarregarProdutoId(id);

            return produto;
        }

        public Produto AdicionarProduto(string nome, decimal valor, int estoque)
        {
            var novoProduto = new Produto(nome, valor, estoque);

            ProdutoDAO conexao = new ProdutoDAO();
            var list = conexao.CarregarTodosProdutos();

            list.Add(novoProduto);

            string jsonProdutos = JsonSerializer.Serialize(list);

            if (conexao.SalvarLista(jsonProdutos))
                return novoProduto;
            else
                return new Produto();
        }

        public bool AtualizarEstoqueProduto(int id, int novoEstoque)
        {
            ProdutoDAO conexao = new ProdutoDAO();
            var list = conexao.CarregarTodosProdutos();
            Produto produtoParaAtualizar = list.FirstOrDefault(p => p.Id == id);
            foreach(var produto in list)
            {
                if(produto.Id == id)
                {
                    produto.AtualizarEstoqueProduto(id, novoEstoque);
                    conexao.SalvarLista(JsonSerializer.Serialize(list));
                    return true;
                }
            }
            return false;
        }

        public bool RemoverProduto(int id)
        {
            ProdutoDAO conexao = new ProdutoDAO();
            var list = conexao.CarregarTodosProdutos();

            Produto produtoParaRemover = list.FirstOrDefault(p => p.Id == id);

            if (produtoParaRemover != null)
            {
                list.Remove(produtoParaRemover); 

                conexao.SalvarLista(JsonSerializer.Serialize(list));        
                Console.WriteLine("Produto removido com sucesso.");
                return true;
            }
            else
            {
                Console.WriteLine($"Produto com ID {id} não encontrado.");
                return false;
            }
        }

        public List<Produto> BuscarProdutoRangePreco(decimal precoInicial, decimal precoFinal)
        {
            decimal variavelAuxiliar = (decimal)0.0;

            if(precoInicial > precoFinal)
            {
                variavelAuxiliar = precoFinal;
                precoFinal = precoInicial;
                precoInicial = variavelAuxiliar;
            }

            ProdutoDAO conexao = new ProdutoDAO();
            List<Produto> produtosRange = new List<Produto>();

            foreach(var produto in conexao.CarregarTodosProdutos())
            {
                if(produto.Preco >= precoInicial && produto.Preco <= precoFinal)
                {
                    produtosRange.Add(produto);
                }
            }

            return produtosRange;

        }

        public (string Nome, decimal Total)[] SomaValorTotalEmEstoque()
        {
            ProdutoDAO conexao = new ProdutoDAO();
            var list = conexao.CarregarTodosProdutos();

            var totais = list.Select(p => (p.Nome, Total: p.Preco * p.Estoque)).ToArray();

            return totais;
        }
    }
}
