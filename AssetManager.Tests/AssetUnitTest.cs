using AssetManager.Application.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AssetManager.Tests
{
    public class AssetUnitTest
    {
        [Fact(DisplayName = "Loan Asset With Active Loan Contract")]
        public void LoanAsset_WithActiveLoanContract_ResultException()
        {
            var mockRepository = new Mock<IAssetService>();
        }

    }
}
