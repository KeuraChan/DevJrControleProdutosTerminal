# DevJrControleProdutosTerminal
Sistema de Controle de Produtos Terminal

# 3 - Autoavaliação

## 3.1 – Como me senti realizando o teste?

Durante o teste, me senti animado com o desafio — a sensação de fazer algo do zero, descobrir a tecnologia, entender como eu gostaria de resolver e pesquisar formas da linguagem para isso. Planejei um projeto simples de banco de dados, buscando entender melhor o que seria o "banco de dados" que deveria ser gerado. Tentar cumprir os desafios me trouxe uma sensação de dever e completude.

---

## 3.2 – Maiores desafios

Adicionar as verificações e realizar testes ao longo da produção para garantir que tudo estivesse funcionando corretamente. Tentei, durante a execução, realizar testes com entradas incorretas para garantir que apenas informações válidas fossem aceitas. Mesmo assim, sempre há algo que não prevemos.

---

## 3.3 – O que eu faria diferente se tivesse mais tempo?

Ao criar o modelo lógico do banco de dados, assumi a tabela `PRODUTOS` como a mesma da solução anterior. Com isso em mente, criei os campos da tabela `PEDIDOS` como descrito, embora a tabela criada não suporte múltiplos produtos por pedido. Em uma aplicação real, seria ideal criar uma **tabela intermediária** (ex: `PedidoProduto`) para armazenar a quantidade e permitir múltiplas inserções de produtos em pedidos.

Além disso, adicionaria mais campos à tabela `PRODUTOS`, como data da última modificação e quem a realizou.

Com mais tempo, teria separado melhor os métodos e camadas. Adotei:

- `Model` como a classe de `Produto` (representação dos dados)
- `DAO` como a conexão com o "banco de dados" (JSON)
- `Controller` como o intermediador entre o console e os dados

Tentei deixar os métodos mais concisos e específicos, mas reconheço que a nomeação e organização de pastas ainda podem melhorar para garantir clareza e manutenção do projeto.

---

## 3.4 – O que considero que poderia ter feito melhor?

Como citado anteriormente, eu analisaria melhor o projeto e as tecnologias para entregar a aplicação em um estado mais refinado, buscando me aproximar da melhor versão possível.

---

## 3.5 – Extras que implementei (caso aplicável)

- [x] Persistência em JSON  
- [x] Filtro de produtos por faixa de preço  
- [x] Cálculo do valor total em estoque  
- [ ] Testes automatizados  
