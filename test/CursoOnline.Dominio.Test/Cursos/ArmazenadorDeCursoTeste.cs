using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTeste
    {
        // Simula comportamentos Ex.: Acesso ao banco de dados.
        // Mock
        // Stubs

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

            var cursoRepositorioMock = new Mock<ICursoRepositorio>();

            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);

            armazenadorDeCurso.Armazenar(cursoDto);

            //Validando se o Adicionar da interface foi chamado, nestes casos pode subistituir o Assert.
            cursoRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Curso>()));
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, PublicoAlvo.Estudante, cursoDto.Valor);
            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public int PublicoAlvoId { get; set; }
        public double Valor { get; set; }
    }
}
