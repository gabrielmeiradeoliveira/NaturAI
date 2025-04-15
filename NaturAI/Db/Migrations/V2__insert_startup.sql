DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM usuario.intensidade_treino) THEN
        INSERT INTO usuario.intensidade_treino (descricao) VALUES
        ('Baixa'),
        ('Moderada'),
        ('Alta');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM usuario.permissao) THEN
        INSERT INTO usuario.permissao (descricao) VALUES
        ('Admin'),
        ('Aluno'),
        ('Professor');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM usuario.tipo_treino) THEN
        INSERT INTO usuario.tipo_treino (descricao) VALUES
        ('Musculação'),
        ('Força'),
        ('Resistência'),
        ('Funcional'),
        ('Hipertrofia'),
        ('Potência'),
        ('Cardio'),
        ('CrossFit'),
        ('Kettlebell'),
        ('Flexibilidade'),
        ('Agilidade'),
        ('Endurance');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM usuario.nivel_treino) THEN
        INSERT INTO usuario.nivel_treino (descricao) VALUES
        ('Iniciante'),
        ('Intermediário'),
        ('Avançado');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM usuario.foco_muscular) THEN
        INSERT INTO usuario.foco_muscular (descricao) VALUES
        ('Peito'),
        ('Costas'),
        ('Ombros'),
        ('Braços'),
        ('Pernas'),
        ('Glúteos'),
        ('Panturrilhas'),
        ('Abdômen'),
        ('Trapézio');
    END IF;
END $$;
