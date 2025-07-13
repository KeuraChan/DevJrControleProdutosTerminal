using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Teste_Dev_Junior.Data;

namespace Teste_Dev_Junior.Models
{
    public class Produto
    {
        [JsonInclude]
        public int Id { get; private set; }

        [JsonInclude]
        public string Nome { get; private set; }

        [JsonInclude]
        public decimal Preco { get; private set; }

        [JsonInclude]
        public int Estoque { get; private set; }

        [JsonInclude]
        public DateTime DataCadastro { get; private set; }

        public Produto() { }

        public Produto(int id, string nome, decimal preco, int estoque, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
            DataCadastro = dataCadastro;
        }

        // Construtor para novo produto (sem id)
        public Produto(string nome, decimal preco, int estoque)
        {
            Id = GerarNovoId();
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
            DataCadastro = DateTime.Now;
        }

        private int GerarNovoId()
        {
            var produtos = new ProdutoDAO().CarregarTodosProdutos();
            return produtos.Count > 0 ? produtos.Max(p => p.Id) + 1 : 1;
        }

        public bool AtualizarEstoqueProduto(int id, int novoEstoque)
        {
            var produto = new ProdutoDAO().CarregarProdutoId(id);

            if(this.Id == id)
            {
               this.Estoque = novoEstoque;
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
