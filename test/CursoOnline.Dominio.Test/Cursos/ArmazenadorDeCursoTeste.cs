using Bogus;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTeste
    {
        private readonly CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;

        //Mock e Stubs simulam comportamentos iguais ao de produção. Ex.: Acesso ao banco de dados.

        public ArmazenadorDeCursoTeste()
        {
            _cursoRepositorioMock = new Mock<ICursoRepositorio>();

            //Injetando a interface através do construtor
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);

            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = fake.Name.FirstName(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000, 2000)
            };
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            // Validando se o Adicionar da interface foi chamado, nestes casos pode subistituir o Assert.
            // Com o It.Is eu testo se a instancia que estou salvando e realmente a que foi passada.
            _cursoRepositorioMock.Verify(x => x.Adicionar(It.Is<Curso>(c => c.Nome == _cursoDto.Nome && c.Descricao == _cursoDto.Descricao)));  // Mock pois apenas faz uma verificação, não há comportamento.
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Médico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Público Alvo inválido.");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComNomeIgualAOutroJaExistente()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).ComCargaHoraria(1).ComValor(1).Build();
            _cursoRepositorioMock.Setup(c => c.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);// Stub pois simula o comportamento de ir ao banco

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Já existe curso salvo com esse nome.");
        }
    }
}
