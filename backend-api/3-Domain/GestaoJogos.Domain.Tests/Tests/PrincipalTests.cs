using GestaoJogos.CrossCutting.Validation.BaseException;
using GestaoJogos.Domain.Principal.Dto;
using GestaoJogos.Domain.Principal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class PrincipalTests
    {
        [TestMethod]
        [ExpectedException(typeof(ApiException), "O nome do seu amigo é obrigatório.")]
        public void AmigoSemNome()
        {
            var amigoDto = new AmigoDto {
                Nome = ""
            };

           new Amigo(amigoDto);
        }

        [TestMethod]
        [ExpectedException(typeof(ApiException), "O nome do seu jogo é obrigatório.")]
        public void JogoSemNome()
        {
            var jogoDto = new JogoDto
            {
                Nome = ""
            };

            new Jogo(jogoDto);
        }
    }
}
