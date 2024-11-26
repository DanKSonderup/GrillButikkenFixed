using System;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using WebApp.Models;

namespace SpecFlowTests.StepDefinitions
{
    [Binding]
    public class ProductStepDefinitions
    {
        private Product _product;

        [Given(@"a product named ""([^""]*)"" with an estimated production time of ""([^""]*)""")]
        public void GivenAProductNamedWithAnEstimatedProductionTimeOf(string name, string productionTime)
        {
            TimeSpan estimatedProductionTime = TimeSpan.Parse(productionTime);
            var createdAt = new DateTime(2024, 11, 26, 9, 15, 0);
            var updatedAt = createdAt.AddDays(1);


            _product = new Product(name, estimatedProductionTime, createdAt, updatedAt, 0);

        }

        [When(@"I add a raw material ""([^""]*)"" with amount ""([^""]*)""")]
        public void WhenIAddARawMaterialWithAmount(string rawMaterialName, string s1)
        {
            double amount = double.Parse(s1);


            RawMaterial rawMaterial = new RawMaterial
            {
                Name = rawMaterialName
            };
            _product.AddMaterial(rawMaterial, amount);

        }

        [Then(@"the product should have (.*) raw material needed")]
        public void ThenTheProductShouldHaveRawMaterialNeeded(int count)
        {
            Assert.Equal(count, _product.ProductRawMaterialNeeded.Count);

        }

        [Then(@"the first raw material should be ""([^""]*)"" with amount ""([^""]*)""")]
        public void ThenTheFirstRawMaterialShouldBeWithAmount(string steel, string p1)
        {
            double amount = double.Parse(p1);

            var material = _product.ProductRawMaterialNeeded.First();
            Assert.Equal(steel, material.RawMaterial.Name);
            Assert.Equal(amount, material.Quantity);

        }

        [Given(@"the product has a raw material ""([^""]*)"" with amount ""([^""]*)""")]
        public void GivenTheProductHasARawMaterialWithAmount(string rawMaterialName, string p1)
        {
            double amount = double.Parse(p1);

            RawMaterial rawMaterial = new RawMaterial
            {
                Name = rawMaterialName
            };
            _product.AddMaterial(rawMaterial, amount);

        }

        [When(@"I remove the raw material ""([^""]*)""")]
        public void WhenIRemoveTheRawMaterial(string steel)
        {
            var materialToRemove = _product.ProductRawMaterialNeeded.FirstOrDefault(m => m.RawMaterial.Name == steel);
            Assert.NotNull(materialToRemove);
            _product.RemoveMaterial(materialToRemove);

        }

        [Then(@"the product should have (.*) raw materials needed")]
        public void ThenTheProductShouldHaveRawMaterialsNeeded(int p0)
        {
            Assert.Empty(_product.ProductRawMaterialNeeded);

        }

        private string _toStringResult;

        [When(@"I call the ToString method")]
        public void WhenICallTheToStringMethod()
        {
            _toStringResult = _product.ToString();
        }

        [Then(@"the result should be ""(.*)""")]
        public void ThenTheResultShouldBe(string exptectedString)
        {
            Assert.StartsWith(exptectedString, _toStringResult);
        }
    }
}
