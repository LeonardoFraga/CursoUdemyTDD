﻿namespace CursoOnline.Dominio.Test.Cursos
{
    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}
