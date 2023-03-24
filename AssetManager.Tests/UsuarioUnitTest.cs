using AssetManager.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AssetManager.Tests
{
    public class UsuarioUnitTest
    {
        [Fact(DisplayName = "Validar se Refresh Token esta expirado")]
        public void DataExpiracaoRefreshTokenMenorQueAgora_VerificarSeORefreshTokenEstaExpirado_ResultadoSucesso()
        {
            UsuarioEntity usuario = new UsuarioEntity()
            {
                Ativo = true,
                Companhia = new CompanhiaEntity(1, "Brasil Co", "51192264000125", true),
                DataExpiracaoRefreshToken = DateTime.Now.AddMinutes(-1),
                Email = "contato@email.com.br",
                Password = "123456789",
                Nome = "Joao da Silva Santos",
                IdUsuario = Guid.NewGuid().ToString(),
                RefreshToken = Guid.NewGuid().ToString(),
                Role = "Suporte",
                IdCompanhia = 1            
            };


            Assert.True(usuario.TokenExpirado());        
        }
    }
}
