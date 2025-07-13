using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Teste_Dev_Junior.Models;

namespace Teste_Dev_Junior.Data
{
    class ProdutoDAO
    {
        private readonly string caminho = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Data\produtos.json");

        public Produto produto { get; set; }

        public bool SalvarLista(string listProdutoJson)
        {
            try
            {
                File.WriteAllText(caminho, listProdutoJson);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar a lista de produtos: " + ex.Message);
                return false;
            }
        }

        public List<Produto> CarregarTodosProdutos()
        {
            List<Produto> listaProdutos = new List<Produto>();

            try
            {
                if (!File.Exists(caminho))
                {
                    return new List<Produto>();
                }
                string json = File.ReadAllText(caminho);
                var resultado = JsonSerializer.Deserialize<List<Produto>>(json);
                return resultado;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar produtos: " + ex.Message);
                return new List<Produto>();
            }

        }

        public Produto CarregarProdutoId(int id)
        {
            List<Produto> listaProdutos = new List<Produto>();

            try
            {
                if (!File.Exists(caminho))
                {
                    return new Produto();
                }
                string json = File.ReadAllText(caminho);
                listaProdutos = JsonSerializer.Deserialize<List<Produto>>(json);

                foreach (Produto produto in listaProdutos)
                {
                    if (produto.Id == id)
                    {
                        return produto; // Retorna o produto encontrado
                    }
                }
                return new Produto();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar produtos: " + ex.Message);
                return new Produto();
            }

        }
    }
}
