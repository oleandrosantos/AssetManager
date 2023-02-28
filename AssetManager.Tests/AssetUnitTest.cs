using AssetManager.Application.DTO.Asset;
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
    public class AssetUnitTest
    {
        [Fact(DisplayName = "Loan Asset With Active Loan Contract")]
        public void LoanAsset_WithActiveLoanContract_ResultTaskFaulted()
        {
            int idAsset = 1;
            var mockAssetRepository = new Mock<IAssetRepository>();
            mockAssetRepository.Setup(m => m.GetById(idAsset).Result).Returns(ObterBaseAssetEmtity()).Verifiable();

            var assetEvent = new Mock<AssetEventsEntity>();

            var mockAssetEventsRepository = new Mock<IAssetEventsRepository>();
            mockAssetEventsRepository.Setup(m => m.LoanAsset(assetEvent.Object)).Returns(Task.CompletedTask).Verifiable();

            var mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }).CreateMapper();


            IAssetEventsService assetEventsService = new AssetEventsService(mockAssetEventsRepository.Object, mapper, mockAssetRepository.Object);
            var result = assetEventsService.LoanAsset(new LoanAssetDTO()
            {
                Description= "Test",
                EventDate= new DateTime(),
                IdAsset = idAsset,
                IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUserRegister = "2d69c663-c484-402a-85d4-002b680eb6f2",
                EventType = (int)Application.Enums.EventsType.Loan,
            });

            Assert.True(assetEventsService != null);
            Assert.True(result.IsFaulted);
        }

        [Fact(DisplayName = "Loan Asset With Active Loan Contract")]
        public void LoanAsset_WithActiveLoanContract_ResultTaskSucess()
        {
            int idAsset = 1;
            var mockAssetRepository = new Mock<IAssetRepository>();
            var terminateContract = new AssetEventsEntity()
            {
                IdAsset = 1,
                Description = "Terminate Contract",
                EventDate = DateTime.Now.AddDays(-3),
                IdEvent = 2,
                EventType = (int)Application.Enums.EventsType.Terminate,
                IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUserRegister = "2d69c663-c484-402a-85d4-002b680eb6f2",
            };

            var assetEntity = ObterBaseAssetEmtity();
            assetEntity.AssetEvents.Add(terminateContract);

            mockAssetRepository.Setup(m => m.GetById(idAsset).Result).Returns(assetEntity).Verifiable();

            var assetEvent = new Mock<AssetEventsEntity>();

            var mockAssetEventsRepository = new Mock<IAssetEventsRepository>();
            mockAssetEventsRepository.Setup(m => m.LoanAsset(assetEvent.Object)).Returns(Task.CompletedTask).Verifiable();

            var mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }).CreateMapper();


            IAssetEventsService assetEventsService = new AssetEventsService(mockAssetEventsRepository.Object, mapper, mockAssetRepository.Object);
            var result = assetEventsService.LoanAsset(new LoanAssetDTO()
            {
                Description = "Test",
                EventDate = new DateTime(),
                IdAsset = idAsset,
                IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUserRegister = "2d69c663-c484-402a-85d4-002b680eb6f2",
                EventType = (int)Application.Enums.EventsType.Loan,
            });

            Assert.True(result.IsCompletedSuccessfully);
        }


        [Fact]
        public void VerificarSeAssetEEmprestavel_DeveRetornarFalse()
        {
            AssetDTO? asset = ObterAssetDTO();

            Assert.False(asset.IsLoanable());
        }

        [Fact]
        public void VerificarSeAssetEEmprestavel_VariosContratos_DeveRetornarFalse()
        {
            AssetDTO? asset = ObterAssetDTO();
            asset.AssetEvents.Add(new AssetEventsDTO()
            {
                IdAsset = 1,
                Description = "Terminate Contract",
                EventDate = DateTime.Now.AddDays(-3),
                IdEvent = 2,
                EventType = (int)Application.Enums.EventsType.Terminate,
                IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUserRegister = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });
            asset.AssetEvents.Add(new AssetEventsDTO()
            {
                IdAsset = 1,
                Description = "Emprestimo Contract",
                EventDate = DateTime.Now,
                IdEvent = 3,
                EventType = (int)Application.Enums.EventsType.Loan,
                IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUserRegister = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });


            Assert.False(asset.IsLoanable());
        }

        [Fact]
        public void VerificarSeAssetEEmprestavel_VariosContratos_DeveRetornarTrue()
        {
            AssetDTO? asset = ObterAssetDTO();
            asset.AssetEvents.Add(new AssetEventsDTO()
            {
                IdAsset = 1,
                Description = "Terminate Contract",
                EventDate = DateTime.Now.AddDays(-3),
                IdEvent = 2,
                EventType = (int)Application.Enums.EventsType.Terminate,
                IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUserRegister = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });
            asset.AssetEvents.Add(new AssetEventsDTO()
            {
                IdAsset = 1,
                Description = "Emprestimo Contract",
                EventDate = DateTime.Now,
                IdEvent = 3,
                EventType = (int)Application.Enums.EventsType.Loan,
                IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUserRegister = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });
            asset.AssetEvents.Add(new AssetEventsDTO()
            {
                IdAsset = 1,
                Description = "Terminate Contract",
                EventDate = DateTime.Now.AddHours(1),
                IdEvent = 2,
                EventType = (int)Application.Enums.EventsType.Terminate,
                IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUserRegister = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });


            Assert.True(asset.IsLoanable());
        }

        [Fact]
        public void VerificarSeAssetEEmprestavel_DeveRetornarTrue()
        {
            AssetDTO? asset = ObterAssetDTO();
            asset.AssetEvents.Add(new AssetEventsDTO()
            {
                IdAsset = 1,
                Description = "Terminate Contract",
                EventDate = DateTime.Now.AddDays(-3),
                IdEvent = 2,
                EventType = (int)Application.Enums.EventsType.Terminate,
                IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                IdUserRegister = "2d69c663-c484-402a-85d4-002b680eb6f2",
            });

            Assert.True(asset.IsLoanable());
        }

        private AssetDTO? ObterAssetDTO()
        {
            var assetDTO = new AssetDTO
            {
                AcquisitionDate = DateTime.Now,
                AssetName = "Produto 01",
                IdAsset = 1,
                AssetPriceInCents = 1000,
                Sku = "4ffd4ed0-e5d1-417b-873e-9be64772b938",
                ExclusionInfos = null,
                IdCompany = 1,
                AssetEvents = new List<AssetEventsDTO>()
                {
                    new AssetEventsDTO {
                        IdAsset = 1,
                        Description = "Loan Product",
                        EventDate = DateTime.Now.AddDays(-7),
                        IdEvent = 1,
                        EventType = (int)Application.Enums.EventsType.Loan,
                        IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                        IdUserRegister ="2d69c663-c484-402a-85d4-002b680eb6f2",
                    }
                }
            };

            return assetDTO;
        }

        private AssetEntity? ObterBaseAssetEmtity()
        {
            var assetDTO = new AssetEntity()
            {
                AcquisitionDate = DateTime.Now,
                AssetName = "Produto 01",
                IdAsset = 1,
                AssetPriceInCents = 1000,
                Sku = "4ffd4ed0-e5d1-417b-873e-9be64772b938",
                ExclusionInfos = null,
                IdCompany = 1,
                AssetEvents = new List<AssetEventsEntity>()
                {
                    new AssetEventsEntity() {
                        IdAsset = 1,
                        Description = "Loan Product",
                        EventDate = DateTime.Now.AddDays(-7),
                        IdEvent = 1,
                        EventType = (int)Application.Enums.EventsType.Loan,
                        IdUser = "2d69c663-c484-402a-85d4-002b680eb6f2",
                        IdUserRegister ="2d69c663-c484-402a-85d4-002b680eb6f2",
                    }
                }
            };

            return assetDTO;
        }

    }
}
