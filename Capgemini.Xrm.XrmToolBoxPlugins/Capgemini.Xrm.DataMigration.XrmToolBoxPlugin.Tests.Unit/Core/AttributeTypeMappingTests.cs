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
            systemUnderTest.AttributeMetadataType = "StringType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("string");
        }

        [TestMethod]
        public void GetMappingUniqueidentifierType()
        {
            systemUnderTest.AttributeMetadataType = "UniqueidentifierType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("guid");
        }

        [TestMethod]
        public void GetMappingPicklistType()
        {
            systemUnderTest.AttributeMetadataType = "PicklistType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("optionsetvalue");
        }

        [TestMethod]
        public void GetMappingMoneyType()
        {
            systemUnderTest.AttributeMetadataType = "MoneyType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("money");
        }

        [TestMethod]
        public void GetMappingBooleanType()
        {
            systemUnderTest.AttributeMetadataType = "BooleanType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("bool");
        }

        [TestMethod]
        public void GetMappingLookupType()
        {
            systemUnderTest.AttributeMetadataType = "LookupType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("entityreference");
        }

        [TestMethod]
        public void GetMappingIntegerType()
        {
            systemUnderTest.AttributeMetadataType = "IntegerType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("integer");
        }

        [TestMethod]
        public void GetMappingDateTimeType()
        {
            systemUnderTest.AttributeMetadataType = "DateTimeType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("datetime");
        }

        [TestMethod]
        public void GetMappingDoubleType()
        {
            systemUnderTest.AttributeMetadataType = "DoubleType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("double");
        }

        [TestMethod]
        public void GetMappingDecimalType()
        {
            systemUnderTest.AttributeMetadataType = "DecimalType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("decimal");
        }

        [TestMethod]
        public void GetMappingMemoType()
        {
            systemUnderTest.AttributeMetadataType = "MemoType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("memo");
        }

        [TestMethod]
        public void GetMappingImageType()
        {
            systemUnderTest.AttributeMetadataType = "ImageType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("image");
        }

        [TestMethod]
        public void GetMappingEntityName()
        {
            systemUnderTest.AttributeMetadataType = "EntityName";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("entityname");
        }

        [TestMethod]
        public void GetMappingStateType()
        {
            systemUnderTest.AttributeMetadataType = "StateType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("state");
        }

        [TestMethod]
        public void GetMappingStatusType()
        {
            systemUnderTest.AttributeMetadataType = "StatusType";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("status");
        }

        [TestMethod]
        public void GetMappingOwner()
        {
            systemUnderTest.AttributeMetadataType = "Owner";

            systemUnderTest.GetMapping();

            systemUnderTest.AttributeMetadataTypeResult.Should().Be("entityreference");
        }
    }
}