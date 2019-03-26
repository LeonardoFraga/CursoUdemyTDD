using System;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

            if(cursoJaSalvo != null)
                throw new ArgumentException("Já existe curso salvo com esse nome.");
            var publicoAlvoParse = Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, false, out var publicoAlvo);

            

            if(!publicoAlvoParse)
                throw new ArgumentException("Público Alvo inválido.");
            

            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, (PublicoAlvo)publicoAlvo, cursoDto.Valor);
            _cursoRepositorio.Adicionar(curso);
        }
    }
}
