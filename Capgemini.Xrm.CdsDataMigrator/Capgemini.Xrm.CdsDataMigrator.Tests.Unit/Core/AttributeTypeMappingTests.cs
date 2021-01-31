using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class AttributeTypeMappingTests
    {
        private AttributeTypeMapping systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new AttributeTypeMapping();
        }

        [TestMethod]
        public void GetMappingStringType()
        {
            var input = "StringType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("string");
        }

        [TestMethod]
        public void GetMappingUniqueidentifierType()
        {
            var input = "UniqueidentifierType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("guid");
        }

        [TestMethod]
        public void GetMappingPicklistType()
        {
            var input = "PicklistType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("optionsetvalue");
        }

        [TestMethod]
        public void GetMappingMoneyType()
        {
            var input = "MoneyType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("money");
        }

        [TestMethod]
        public void GetMappingBooleanType()
        {
            var input = "BooleanType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("bool");
        }

        [TestMethod]
        public void GetMappingLookupType()
        {
            var input = "LookupType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("entityreference");
        }

        [TestMethod]
        public void GetMappingIntegerType()
        {
            var input = "IntegerType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("integer");
        }

        [TestMethod]
        public void GetMappingDateTimeType()
        {
            var input = "DateTimeType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("datetime");
        }

        [TestMethod]
        public void GetMappingDoubleType()
        {
            var input = "DoubleType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("double");
        }

        [TestMethod]
        public void GetMappingDecimalType()
        {
            var input = "DecimalType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("decimal");
        }

        [TestMethod]
        public void GetMappingMemoType()
        {
            var input = "MemoType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("memo");
        }

        [TestMethod]
        public void GetMappingImageType()
        {
            var input = "ImageType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("image");
        }

        [TestMethod]
        public void GetMappingEntityName()
        {
            var input = "EntityName";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("entityname");
        }

        [TestMethod]
        public void GetMappingStateType()
        {
            var input = "StateType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("state");
        }

        [TestMethod]
        public void GetMappingStatusType()
        {
            var input = "StatusType";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("status");
        }

        [TestMethod]
        public void GetMappingOwner()
        {
            var input = "Owner";

            systemUnderTest.AttributeMetadataType = input;

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("entityreference");
        }
    }
}