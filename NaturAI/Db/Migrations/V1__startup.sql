CREATE SCHEMA IF NOT EXISTS usuario;

CREATE TABLE IF NOT EXISTS usuario.intensidade_treino (
    id SERIAL PRIMARY KEY,
    descricao VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS usuario.permissao (
    id SERIAL PRIMARY KEY,
    descricao VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS usuario.tipo_treino (
    id SERIAL PRIMARY KEY,
    descricao VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS usuario.nivel_treino (
    id SERIAL PRIMARY KEY,
    descricao VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS usuario.foco_muscular (
    id SERIAL PRIMARY KEY,
    descricao VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS usuario.usuario (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    senha_hash VARCHAR(255) NOT NULL,
    data_nascimento DATE NOT NULL,
    sexo CHAR(1) NOT NULL CHECK (sexo IN ('M', 'F')),
    peso FLOAT NOT NULL,
    dias_treino INT NOT NULL,
    intensidade_treino INT,
    nivel_treino INT,
    permissao INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_permissao FOREIGN KEY (permissao) 
        REFERENCES usuario.permissao(id) ON DELETE SET NULL,
    CONSTRAINT fk_intensidade_treino FOREIGN KEY (intensidade_treino) 
        REFERENCES usuario.intensidade_treino(id) ON DELETE SET NULL,
    CONSTRAINT fk_nivel_treino FOREIGN KEY (nivel_treino) 
        REFERENCES usuario.nivel_treino(id) ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS usuario.professor_aluno (
    id SERIAL PRIMARY KEY,
    id_professor INT NOT NULL,
    id_aluno INT NOT NULL,
    CONSTRAINT fk_professor FOREIGN KEY (id_professor) 
        REFERENCES usuario.usuario(id) ON DELETE CASCADE,
    CONSTRAINT fk_aluno FOREIGN KEY (id_aluno) 
        REFERENCES usuario.usuario(id) ON DELETE CASCADE,
    CONSTRAINT uq_professor_aluno UNIQUE (id_professor, id_aluno)
);

CREATE TABLE IF NOT EXISTS usuario.usuario_tipo_treino (
    id SERIAL PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_tipo_treino INT NOT NULL,
    CONSTRAINT fk_usuario FOREIGN KEY (id_usuario) 
        REFERENCES usuario.usuario(id) ON DELETE CASCADE,
    CONSTRAINT fk_tipo_treino FOREIGN KEY (id_tipo_treino) 
        REFERENCES usuario.tipo_treino(id) ON DELETE CASCADE,
    CONSTRAINT uq_usuario_tipo_treino UNIQUE (id_usuario, id_tipo_treino)
);

CREATE TABLE IF NOT EXISTS usuario.usuario_foco_muscular (
    id SERIAL PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_foco_muscular INT NOT NULL,
    CONSTRAINT fk_usuario_foco FOREIGN KEY (id_usuario) 
        REFERENCES usuario.usuario(id) ON DELETE CASCADE,
    CONSTRAINT fk_foco_muscular FOREIGN KEY (id_foco_muscular) 
        REFERENCES usuario.foco_muscular(id) ON DELETE CASCADE,
    CONSTRAINT uq_usuario_foco_muscular UNIQUE (id_usuario, id_foco_muscular)
);

CREATE TABLE IF NOT EXISTS usuario.treinos (
    id SERIAL PRIMARY KEY,
    usuario_id INT NOT NULL,
    descricao TEXT NOT NULL,
    data_geracao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    proximo_treino TIMESTAMP,
    CONSTRAINT fk_usuario FOREIGN KEY (usuario_id)
        REFERENCES usuario.usuario(id) ON DELETE CASCADE
);

