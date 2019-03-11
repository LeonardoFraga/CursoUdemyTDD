using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTeste
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDto = new CursoDto
            {
                Nome = "Nome",
                Descricao = "Descricao",
                CargaHoraria = 80,
                PublicoAlvoId = 1,
                Valor = 850
            };

            var armazenadorDeCurso = new ArmazenadorDeCurso();

            armazenadorDeCurso.Armazenar(cursoDto);
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {
        public void Armazenar(CursoDto cursoDto)
        {
            cursoDto.Descricao = "a";
        }
    }

    internal class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public int PublicoAlvoId { get; set; }
        public double Valor { get; set; }
    }
}
