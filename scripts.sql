-- CRIAÇÃO DO BANCO DE DADOS
CREATE DATABASE exercicio_database;
USE exercicio_database;
-- CRIAÇÃO DAS TABELAS
CREATE TABLE produtos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(30),
    preco DECIMAL(9,2),
    estoque INT,
    dataCadastro DATETIME
);


-- CRIAÇÃO DA TABELA PEDIDOS
CREATE TABLE pedidos (
    id INT PRIMARY KEY AUTO_INCREMENT,
    idProduto INT NOT NULL,
    quantidade INT NOT NULL,
    valorTotal DECIMAL(9,2),
    dataPedido DATE,
    
    CONSTRAINT FK_pedidos_produto
        FOREIGN KEY (idProduto)
        REFERENCES produtos(id)
        ON DELETE CASCADE
);

-- INSERÇÃO DE PRODUTOS (necessário para os pedidos serem válidos)
INSERT INTO produtos (nome, preco, estoque, dataCadastro) VALUES
('Bluza Azul', 39.90, 40, NOW()),
('Calça Jeans', 199.90, 32, NOW()),
('Babucha Crocs', 19.90, 4, NOW());

-- INSERÇÃO DE PEDIDOS
INSERT INTO pedidos (idProduto, quantidade, dataPedido, valorTotal)
VALUES 
(1, 2, '2025-07-09', 79.80),    -- 2 x 39.90
(2, 1, '2025-07-09', 199.90),   -- 1 x 199.90
(3, 3, '2025-07-09', 59.70);    -- 3 x 19.90

-- CONSULTAS SQL

-- 1. PRODUTOS COM ESTOQUE ABAIXO DE 10 ITENS
SELECT *
FROM produtos
WHERE estoque < 10;

-- 2. OS 5 PRODUTOS MAIS CAROS
SELECT *
FROM produtos
ORDER BY preco DESC
LIMIT 5;

-- 3. NOME DO PRODUTO + SOMA TOTAL DE PEDIDOS
SELECT 
    p.nome,
    SUM(ped.quantidade) AS total_pedidos
FROM produtos p
JOIN pedidos ped ON p.id = ped.idProduto
GROUP BY p.nome;

-- 4. PREÇO MÉDIO DOS PRODUTOS
SELECT AVG(preco) AS preco_medio
FROM produtos;
