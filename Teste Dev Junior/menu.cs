using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Teste_Dev_Junior.Controllers;
using System.Text.RegularExpressions;
using Teste_Dev_Junior.Models;
using System.Globalization;
namespace Teste_Dev_Junior
{
    public static class menu
    {
        public static void EscreverMenu()
        {
            Console.WriteLine("|===================== MENU ===================|");
            Console.WriteLine("| 1. Adicionar Novo Produto                    |");
            Console.WriteLine("| 2. Mostrar Todos os Produtos                 |");
            Console.WriteLine("| 3. Mostrar Produto por ID                    |");
            Console.WriteLine("| 4. Atualizar Estoque de Produto              |");
            Console.WriteLine("| 5. Remover Produto por ID                    |");
            Console.WriteLine("| 6. Mostrar Produtos por Faixa de Preço       |");
            Console.WriteLine("| 7. Calcular Valor Total em Estoque           |");
            Console.WriteLine("| 8. Encerrar Processo                         |");
            Console.WriteLine("|==============================================|");
        }

        public static void PressAnyKey()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(); // Espera o usuário pressionar uma tecla
            Console.Clear();   // Limpa o console
        }
        public static bool OpcaoMenuSelecionada(string opcao)
        {
            bool emOperacao = true;

            opcao = opcao.Trim();
            opcao = new string(opcao.Where(char.IsDigit).ToArray());

            switch (opcao)
            {
                case "1": //ADICIONAR PRODUTO
                    AdicionarProduto();
                    break;
                case "2": // MOSTRAR TODOS OS PRODUTOS
                    MostrarTodosProdutos();
                    break;
                case "3": // BUSCAR PRODUTO POR ID
                    EncontrarProdutoPorId();
                    break;
                case "4": // ATUALIZAR ESTOQUE
                    AtualizarEstoque();
                    break;
                case "5": // REMOVER PRODUTO
                    RemoverProduto();
                    break;
                case "6": // Filtrar Produtos por Preço
                    FiltrarProdutoPreco();
                    break;
                case "7": //  Calcular Valor Total em Estoque 
                    SomaTotalPrecoEmEstoque();
                    break;
                case "8":
                    emOperacao = false;
                    break;

                default:
                    Console.WriteLine($"A opção '{opcao}' não é válida");
                    break;
            }

            PressAnyKey();
            return emOperacao;
        }
        private static void AdicionarProduto()
        {
            string nomeProduto = "";
            decimal precoProduto = (decimal) 0.00;
            int estoqueProduto = 0;

            bool validarEntrada = false;

            do {
                Console.WriteLine("Insira o NOME do produto");
                nomeProduto = Console.ReadLine();
                if (!string.IsNullOrEmpty(nomeProduto) || nomeProduto.Length >= 50)
                {
                    validarEntrada = true;
                }
                else
                {
                    Console.WriteLine("Nome INVÁLIDO, insira um nome com menos de 30 caracteres");
                    PressAnyKey();
                }
            } while (!validarEntrada);

            validarEntrada = false;

            do
            {
                Console.WriteLine("Insira o PRECO do produto (00,00)");
                Console.Write("R$ ");
                string strPreco = Console.ReadLine().Replace(",", ".").Replace("R$", "");
                if (decimal.TryParse(strPreco, out decimal preco) && preco >= 0)
                {
                    Console.WriteLine($"Preço formatado: R$ {preco:N2}");
                    precoProduto = preco;
                    validarEntrada = true;
                }
                else
                {
                    Console.WriteLine($"O valor {strPreco} não é válido");
                    PressAnyKey();
                }

            } while (!validarEntrada);
            validarEntrada = false;
            do
            {
                Console.WriteLine("Insira o ESTOQUE do produto");
                string strEstoque = Console.ReadLine().Trim();
                strEstoque = Regex.Replace(strEstoque, "[^0-9]", "");
                if(int.TryParse(strEstoque, out int estoque) && estoque >= 0)
                {
                    estoqueProduto = estoque;
                    validarEntrada = true;
                }
                else
                {
                    Console.WriteLine($"O valor {strEstoque} não é válido");
                    PressAnyKey();
                }

            } while (!validarEntrada);

            var novoProduto = new ProdutoController().AdicionarProduto(nomeProduto, precoProduto, estoqueProduto);

            Console.WriteLine($"|==============================================");
            Console.WriteLine($"| ID: {novoProduto.Id} ");
            Console.WriteLine($"| Nome: {novoProduto.Nome}");
            Console.WriteLine($"| Preço: {novoProduto.Preco}");
            Console.WriteLine($"| Estoque: {novoProduto.Estoque} ");
            Console.WriteLine($"| Data de Cadastro: {novoProduto.DataCadastro}");
            Console.WriteLine($"|==============================================");

        }
        private static void MostrarTodosProdutos()
        {
            var ListaProdutos = new ProdutoController().ListarProdutos();

            foreach (var produto in ListaProdutos)
            {
                Console.WriteLine($"|==============================================");
                Console.WriteLine($"| ID: { produto.Id} ");    
                Console.WriteLine($"| Nome: { produto.Nome}");
                Console.WriteLine($"| Preço: { produto.Preco }");    
                Console.WriteLine($"| Estoque: { produto.Estoque } ");    
                Console.WriteLine($"| Data de Cadastro: { produto.DataCadastro}");
                Console.WriteLine($"|==============================================");
            }
        }
        private static Produto? EncontrarProdutoPorId()
        {
            Console.Write("Digite o ID do produto a verificar: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int IdProduto))
            {
                var produto = new ProdutoController().ListarProdutoPorId(IdProduto);

                if (produto != null && produto.Id != 0)
                {
                    Console.WriteLine($"|==============================================");
                    Console.WriteLine($"| ID: {produto.Id} ");
                    Console.WriteLine($"| Nome: {produto.Nome}");
                    Console.WriteLine($"| Preço: {produto.Preco}");
                    Console.WriteLine($"| Estoque: {produto.Estoque} ");
                    Console.WriteLine($"| Data de Cadastro: {produto.DataCadastro}");
                    Console.WriteLine($"|==============================================");

                    return produto;
                }
                else
                {
                    Console.WriteLine("Produto não encontrado com o ID fornecido.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Digite um número inteiro.");
            }
            return null;

        }
        private static void AtualizarEstoque()
        {
            Produto? produtoAtualizarEstoque = EncontrarProdutoPorId();

            if (produtoAtualizarEstoque != null)
            {

                int EstoqueAtual = produtoAtualizarEstoque.Estoque;
                bool validarEntrada = false;
                do
                {
                    Console.WriteLine($"O estoque atual do produto ({produtoAtualizarEstoque.Nome}) é: " + EstoqueAtual);

                    Console.WriteLine("Deseja atualizar o valor para: ");
                    string strEstoque = Console.ReadLine().Trim();
                    strEstoque = Regex.Replace(strEstoque, "[^0-9]", "");

                    if (int.TryParse(strEstoque, out int estoque) && estoque >= 0)
                    {
                        int novoEstoque = estoque;
                        if(new ProdutoController().AtualizarEstoqueProduto(produtoAtualizarEstoque.Id, novoEstoque))
                        {
                            Console.WriteLine("Estoque atualizado com sucesso!!");
                            validarEntrada = true;
                        }
                        else
                        {
                            Console.WriteLine("Não foi possível atualizar o estoque selecionado!!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"O valor {strEstoque} não é válido");
                        PressAnyKey();
                    }
                } while (!validarEntrada);

            }
            else
            {
                Console.WriteLine("ID inválido. Digite um número inteiro.");
            }
        }

        private static bool RemoverProduto()
        {
            Produto? produtoRemover = EncontrarProdutoPorId();

            if (produtoRemover != null)
            {
                Console.WriteLine($"Deseja remover o produto {produtoRemover.Nome} (Y/N)");

                char resposta = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine(); // pula linha

                while (resposta != 'Y' && resposta != 'N')
                {
                    Console.Write("Entrada inválida. Digite apenas 'y' ou 'n': ");
                    resposta = char.ToUpper(Console.ReadKey().KeyChar);
                    Console.WriteLine(); // pula linha
                }

                if(resposta == 'Y')
                {
                    new ProdutoController().RemoverProduto(produtoRemover.Id);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        private  static void FiltrarProdutoPreco()
        {
            var validarEntrada = false;
            decimal precoInicial = (decimal) 0.00;
            decimal precoFinal = (decimal) 0.00;
            do
            {
                Console.WriteLine("Insira o PREÇO INICIAL");
                Console.Write("R$ ");
                string strPreco = Console.ReadLine().Replace(",", ".").Replace("R$", "");
                if (decimal.TryParse(strPreco, out decimal preco) && preco >= 0)
                {
                    Console.WriteLine($"Preço formatado: R$ {preco:N2}");
                    precoInicial = preco;
                    validarEntrada = true;
                }
                else
                {
                    Console.WriteLine($"O valor {strPreco} não é válido");
                    PressAnyKey();
                }

            } while (!validarEntrada);
            validarEntrada = false;
            do
            {
                Console.WriteLine("Insira o PREÇO FINAL");
                Console.Write("R$ ");
                string strPreco = Console.ReadLine().Replace(",", ".").Replace("R$", "");
                if (decimal.TryParse(strPreco, out decimal preco) && preco >= 0)
                {
                    Console.WriteLine($"Preço formatado: R$ {preco:N2}");
                    precoFinal = preco;
                    validarEntrada = true;
                }
                else
                {
                    Console.WriteLine($"O valor {strPreco} não é válido");
                    PressAnyKey();
                }

            } while (!validarEntrada);

            List<Produto>? produtosFaixaPreco = new ProdutoController().BuscarProdutoRangePreco(precoInicial, precoFinal);

            if(produtosFaixaPreco.Count > 0)
            {
                Console.WriteLine($"PRODUTOS NA FAIXA DE PREÇO DE {precoInicial} À {precoFinal}");
                foreach(var produto in produtosFaixaPreco)
                {
                    Console.WriteLine($"|==============================================");
                    Console.WriteLine($"| ID: {produto.Id} ");
                    Console.WriteLine($"| Nome: {produto.Nome}");
                    Console.WriteLine($"| Preço: {produto.Preco}");
                    Console.WriteLine($"| Estoque: {produto.Estoque} ");
                    Console.WriteLine($"| Data de Cadastro: {produto.DataCadastro}");
                    Console.WriteLine($"|==============================================");
                }
            }
        }

        private static void SomaTotalPrecoEmEstoque()
        {
            var total =  new ProdutoController().SomaValorTotalEmEstoque();
            decimal somaTotal = (decimal)0;
            foreach (var produto in total)
            {
                Console.WriteLine($"|==============================================");
                Console.WriteLine($"| Nome: {produto.Nome}");
                Console.WriteLine($"| Total em Estoque: R$ {produto.Total.ToString().Replace(".", ",")}");
                Console.WriteLine($"|==============================================");
                somaTotal += produto.Total;
            }
                Console.WriteLine($"|==============================================|");
                Console.WriteLine($" VALOR TOTAL EM ESTOQUE R$ {somaTotal.ToString().Replace(".", ",")}");
                Console.WriteLine($"|==============================================|");


        }

    }
}
