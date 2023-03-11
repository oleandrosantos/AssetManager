using AssetManager.Application.DTO.Ativo;
using AssetManager.Application.DTO.AssetEvents;
using AssetManager.Application.Enums;
using AssetManager.Application.Interfaces;
using AssetManager.Application.Profiles;
using AssetManager.Application.Service;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Xunit;

namespace AssetManager.Tests
{
    public class AtivosUniTest
    {
        [Fact(DisplayName = "Emprestar ativo com emprestimo vigente")]
        public void EmprestrarAtivo_AtivoComUmEmprestimoVigente_ResultadoEsperadoTaskFalha()
        {
            int idAtivo = 1;
            var mockAssetRepository = new Mock<IAtivosRepository>();
            mockAssetRepository.Setup(m => m.ObterAtivoPorId(idAtivo).Result).Returns(ObterBaseAssetEmtity()).Verifiable();

            var assetEvent = new Mock<EventosAtivoEntity>();

            var mockAssetEventsRepository = new Mock<IEventosAtivosRepository>();
            mockAssetEventsRepository.Setup(m => m.EmprestarAtivo(assetEvent.Object)).Returns(Task.CompletedTask).Verifiable();

            var mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }).CreateMapper();


            IEventosAtivosService assetEventsService = new EventosAtivoService(mockAssetEventsRepository.Object, mapper, mockAssetRepository.Object);
            var resultado = assetEventsService.EmprestarAtivo(new EmpretimoAtivoDTO()
            {
                Descricao= "Test",
                DataEvento= new DateTime(),
                IdAtivo = idAtivo,
                IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUsuarioRegistro = "2d69c663-c484-402a-85d4-002b680eb6f2",
                TipoEvento = (int)Application.Enums.TiposEventos.Emprestimo,
            });

            Assert.True(assetEventsService != null);
            Assert.True(resultado.IsFaulted);
        }

        [Fact(DisplayName = "Emprestar ativo sem emprestimo vigente")]
        public void EmprestrarAtivo_SemEmprestimoEmAberto_ResultadoEsperadoSucesso()
        {
            int idAtivo = 1;
            var mockAssetRepository = new Mock<IAtivosRepository>();
            var terminateContract = new EventosAtivoEntity()
            {
                IdAtivo = 1,
                Descricao = "Terminate Contract",
                DataEvento = DateTime.Now.AddDays(-3),
                IdEventosAtivo = 2,
                TipoEvento = (int)Application.Enums.TiposEventos.FimEmprestimo,
                IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUsuarioRegistro = "2d69c663-c484-402a-85d4-002b680eb6f2",
            };

            var assetEntity = ObterBaseAssetEmtity();
            assetEntity.EventosAtivo.Add(terminateContract);

            mockAssetRepository.Setup(m => m.ObterAtivoPorId(idAtivo).Result).Returns(assetEntity).Verifiable();

            var assetEvent = new Mock<EventosAtivoEntity>();

            var mockAssetEventsRepository = new Mock<IEventosAtivosRepository>();
            mockAssetEventsRepository.Setup(m => m.EmprestarAtivo(assetEvent.Object)).Returns(Task.CompletedTask).Verifiable();

            var mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }).CreateMapper();


            IEventosAtivosService assetEventsService = new EventosAtivoService(mockAssetEventsRepository.Object, mapper, mockAssetRepository.Object);
            var resultado = assetEventsService.EmprestarAtivo(new EmpretimoAtivoDTO()
            {
                Descricao = "Test",
                DataEvento = new DateTime(),
                IdAtivo = idAtivo,
                IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUsuarioRegistro = "2d69c663-c484-402a-85d4-002b680eb6f2",
                TipoEvento = (int)Application.Enums.TiposEventos.Emprestimo,
            });

            Assert.True(resultado.IsCompletedSuccessfully);
        }


        [Fact(DisplayName = "Verificar se Ativo com emprestimo em aberto pode ser emprestado novamente")]
        public void VerificarSeAtivoEEmprestavel_AtivoComUmEmprestimoEmAberto_DeveRetornarFalse()
        {
            AtivoDTO? ativo = ObterAssetDTO();

            Assert.False(ativo.ELocavel());
        }

        [Fact(DisplayName = "Verificar se Ativo com emprestimo encerrado pode ser emprestado novamente")]
        public void VerificarSeAtivoEEmprestavel_AtivoComVariosContratosDeEmprestimoAberto_DeveRetornarFalse()
        {
            AtivoDTO? ativo = ObterAssetDTO();
            ativo.EventosAtivo.Add(new EventosAtivoDTO()
            {
                IdAtivo = 1,
                Descricao = "Terminate Contract",
                DataEvento = DateTime.Now.AddDays(-3),
                IdEventosAtivo = 2,
                TipoEvento = (int)Application.Enums.TiposEventos.FimEmprestimo,
                IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUsuarioRegistro = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });
            ativo.EventosAtivo.Add(new EventosAtivoDTO()
            {
                IdAtivo = 1,
                Descricao = "Emprestimo Contract",
                DataEvento = DateTime.Now,
                IdEventosAtivo = 3,
                TipoEvento = (int)Application.Enums.TiposEventos.Emprestimo,
                IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUsuarioRegistro = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });


            Assert.False(ativo.ELocavel());
        }

        [Fact(DisplayName = "Verificar se Ativo com emprestimo varios emprestimos em aberto, porem o ultimo encerrado pode ser emprestado novamente")]
        public void VerificarSeAssetEEmprestavel_VariosContratosDeEmprestimoAbertoPoremUltimoEncerrado_DeveRetornarTrue()
        {
            AtivoDTO? ativo = ObterAssetDTO();
            ativo.EventosAtivo.Add(new EventosAtivoDTO()
            {
                IdAtivo = 1,
                Descricao = "Terminate Contract",
                DataEvento = DateTime.Now.AddDays(-3),
                IdEventosAtivo = 2,
                TipoEvento = (int)Application.Enums.TiposEventos.FimEmprestimo,
                IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUsuarioRegistro = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });
            ativo.EventosAtivo.Add(new EventosAtivoDTO()
            {
                IdAtivo = 1,
                Descricao = "Emprestimo Contract",
                DataEvento = DateTime.Now,
                IdEventosAtivo = 3,
                TipoEvento = (int)Application.Enums.TiposEventos.Emprestimo,
                IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUsuarioRegistro = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });
            ativo.EventosAtivo.Add(new EventosAtivoDTO()
            {
                IdAtivo = 1,
                Descricao = "Terminate Contract",
                DataEvento = DateTime.Now.AddHours(1),
                IdEventosAtivo = 2,
                TipoEvento = (int)Application.Enums.TiposEventos.FimEmprestimo,
                IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUsuarioRegistro = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });


            Assert.True(ativo.ELocavel());
        }

        [Fact(DisplayName = "Verificar se Ativo com somente 1 emprestimo aberto e encerradop ode ser emprestado novamente")]
        public void VerificarSeAssetEEmprestavel_AssetComApenas1ContratoEEncerrado_DeveRetornarTrue()
        {
            AtivoDTO? ativo = ObterAssetDTO();
            ativo.EventosAtivo.Add(new EventosAtivoDTO()
            {
                IdAtivo = 1,
                Descricao = "Terminate Contract",
                DataEvento = DateTime.Now.AddDays(-3),
                IdEventosAtivo = 2,
                TipoEvento = (int)Application.Enums.TiposEventos.FimEmprestimo,
                IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUsuarioRegistro = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });

            Assert.True(ativo.ELocavel());
        }

        private AtivoDTO? ObterAssetDTO()
        {
            var assetDTO = new AtivoDTO
            {
                DataAquisicao = DateTime.Now,
                NomeAtivo = "Produto 01",
                IdAtivo = 1,
                PrecoEmCentavos = 1000,
                Sku = "4ffd4ed0-e5d1-417b-873e-9be64772b938",
                InformacoesExclusao = null,
                IdCompanhia = 1,
                EventosAtivo = new List<EventosAtivoDTO>()
                {
                    new EventosAtivoDTO {
                        IdAtivo = 1,
                        Descricao = "Loan Product",
                        DataEvento = DateTime.Now.AddDays(-7),
                        IdEventosAtivo = 1,
                        TipoEvento = (int)Application.Enums.TiposEventos.Emprestimo,
                        IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                        IdUsuarioRegistro ="2d69c663-c484-402a-85d4-002b680eb6f2",
                    }
                }
            };

            return assetDTO;
        }

        private AtivoEntity? ObterBaseAssetEmtity()
        {
            var assetDTO = new AtivoEntity()
            {
                DataAquisicao = DateTime.Now,
                NomeAtivo = "Produto 01",
                IdAtivo = 1,
                PrecoEmCentavos = 1000,
                Sku = "4ffd4ed0-e5d1-417b-873e-9be64772b938",
                InformacoesExclusao = null,
                IdCompanhia = 1,
                EventosAtivo = new List<EventosAtivoEntity>()
                {
                    new EventosAtivoEntity() {
                        IdAtivo = 1,
                        Descricao = "Loan Product",
                        DataEvento = DateTime.Now.AddDays(-7),
                        IdEventosAtivo = 1,
                        TipoEvento = (int)Application.Enums.TiposEventos.Emprestimo,
                        IdUsuario = "2d69c663-c484-402a-85d4-002b680eb6f2",
                        IdUsuarioRegistro ="2d69c663-c484-402a-85d4-002b680eb6f2",
                    }
                }
            };

            return assetDTO;
        }

    }
}
