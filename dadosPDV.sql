DROP TABLE Produtos;
CREATE TABLE Produtos (
    referencia TEXT NOT NULL,
    descricao TEXT NOT NULL,
    unit_compra TEXT NOT NULL,
    unit_venda TEXT NOT NULL,
    preco_atacado REAL,
    preco_varejo REAL,
    estoque INTEGER,
    fornecido INTEGER,
    carteira INTEGER,
    compradas INTEGER,
    vendidas INTEGER
);
