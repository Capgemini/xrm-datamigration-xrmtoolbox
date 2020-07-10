using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using FluentAssertions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class RecordCounterProcessorTests
    {
        private Mock<IOrganizationService> organizationServiceMock;

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ExecuteRecordsCount()
        {
            organizationServiceMock = new Mock<IOrganizationService>();
            string exportConfigFilePath = "TestData/ExportConfig.json";
            string schemaFilePath = "TestData/TestSchemaFile.xml";

            var response = new FetchXmlToQueryExpressionResponse();
            var query = new QueryExpression();
            response.Results.Add("Query", query);

            organizationServiceMock.Setup(a => a.Execute(It.IsAny<FetchXmlToQueryExpressionRequest>()))
                                   .Returns(response);

            var entityCollection = new EntityCollection();
            organizationServiceMock.Setup(a => a.RetrieveMultiple(It.IsAny<QueryExpression>()))
                .Returns(entityCollection);

            List<RecordCountModel> actual = null;

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.WorkerReportsProgress = true;
                using (DataGridView gridView = new DataGridView())
                {
                    gridView.Columns.Add("Column1", "Column1");
                    gridView.Columns.Add("Column2", "Column2");

                    FluentActions.Invoking(() => actual = RecordCounterProcessor.ExecuteRecordsCount(exportConfigFilePath, schemaFilePath, organizationServiceMock.Object, worker, gridView))
                        .Should()
                        .NotThrow();
                }
            }

            actual.Count.Should().Be(2);
        }

        [TestMethod]
        public void ExecuteRecordsCountNullOrganizationService()
        {
            string exportConfigFilePath = "TestData/ExportConfig.json";
            string schemaFilePath = "TestData/TestSchemaFile.xml";

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                using (DataGridView gridView = new DataGridView())
                {
                    FluentActions.Invoking(() => RecordCounterProcessor.ExecuteRecordsCount(exportConfigFilePath, schemaFilePath, null, worker, gridView))
                        .Should()
                        .Throw<ArgumentNullException>()
                        .Where(e => e.Message.Contains("service"));
                }
            }
        }

        [TestMethod]
        public void ExecuteRecordsCountNullGridView()
        {
            organizationServiceMock = new Mock<IOrganizationService>();
            string exportConfigFilePath = "TestData/ExportConfig.json";
            string schemaFilePath = "TestData/TestSchemaFile.xml";

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                FluentActions.Invoking(() => RecordCounterProcessor.ExecuteRecordsCount(exportConfigFilePath, schemaFilePath, organizationServiceMock.Object, worker, null))
                    .Should()
                    .Throw<ArgumentNullException>()
                    .Where(e => e.Message.Contains("gridView"));
            }
        }

        [TestMethod]
        public void ExecuteRecordsCountNullBackgroundWorker()
        {
            organizationServiceMock = new Mock<IOrganizationService>();
            string exportConfigFilePath = "TestData/ExportConfig.json";
            string schemaFilePath = "TestData/TestSchemaFile.xml";

            using (DataGridView gridView = new DataGridView())
            {
                FluentActions.Invoking(() => RecordCounterProcessor.ExecuteRecordsCount(exportConfigFilePath, schemaFilePath, organizationServiceMock.Object, null, gridView))
                    .Should()
                    .Throw<ArgumentNullException>()
                    .Where(e => e.Message.Contains("worker"));
            }
        }

        [TestMethod]
        public void WriteDataToCsv()
        {
            var items = new List<Item<string, string>>()
            {
                new Item<string, string>("testentity1", Guid.NewGuid().ToString()),
                new Item<string, string>("testentity2", Guid.NewGuid().ToString())
            };
            string path = $"{TestContext.ResultsDirectory}\\test.csv";

            FluentActions.Invoking(() => RecordCounterProcessor.WriteDataToCSV(items, path))
                .Should()
                .NotThrow();

            Assert.IsTrue(File.Exists(path));
        }

        [TestMethod]
        public void WriteDataToCsvNullPath()
        {
            var items = new List<string>() { "Sample text", "Sample text", "Sample text" };
            string path = null;

            FluentActions.Invoking(() => RecordCounterProcessor.WriteDataToCSV(items, path))
                .Should()
                .Throw<ArgumentNullException>()
                .Where(e => e.Message.Contains("path"));
        }

        [TestMethod]
        public void WriteDataToCsvNullItems()
        {
            List<string> items = null;
            string path = "test.csv";

            FluentActions.Invoking(() => RecordCounterProcessor.WriteDataToCSV(items, path))
                .Should()
                .Throw<ArgumentNullException>()
                .Where(e => e.Message.Contains("items"));
        }
    }
}