using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste_Dev_Junior.Data;
using Teste_Dev_Junior.Models;
using Teste_Dev_Junior.Controllers;
using Teste_Dev_Junior;
using System.Globalization;

// See https://aka.ms/new-console-template for more information
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

var produtoDAO = new ProdutoDAO();
bool emExecucao;

do { 
    menu.EscreverMenu();

    Console.Write("Selecione uma Opção: ");
    emExecucao = menu.OpcaoMenuSelecionada(Console.ReadLine());


} while(emExecucao);