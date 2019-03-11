using Bogus;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._util;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoTeste : IDisposable
    {
        private readonly double _cargaHoraria;
        private readonly string _descricao;
        private readonly string _nome;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        
        //Setup -- Executado antes de cada método de teste.
        public CursoTeste()
        {
            var fake = new Faker();

            _nome = fake.Name.FirstName();
            _descricao = fake.Lorem.Paragraph();
            _cargaHoraria = fake.Random.Double(50, 350);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = fake.Random.Double(500, 3500);
        }

        //Clean Up
        public void Dispose()
        {
            //Todo codigo inserido aqui é executado após cada método de teste
        }

        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                Nome = _nome,
                Descricao = _descricao,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
            };

            //Action
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
               CursoBuilder.Novo().ComNome(nomeInvalido).Build())
               .ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorUm(double cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
               CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
               .ComMensagem("Carga horária inválida.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorMenorQueUm(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
               CursoBuilder.Novo().ComValor(valorInvalido).Build())
               .ComMensagem("Valor inválido");
        }

    }
}
