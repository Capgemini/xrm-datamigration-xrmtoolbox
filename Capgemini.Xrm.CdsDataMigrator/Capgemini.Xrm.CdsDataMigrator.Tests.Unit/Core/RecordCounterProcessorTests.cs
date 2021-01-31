﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FluentAssertions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class RecordCounterProcessorTests
    {
        [TestMethod]
        public void WriteDataToCSVNullItems()
        {
            IEnumerable<string> items = null;
            var path = "TestData/apointmentsSchema.xml";

            FluentActions.Invoking(() => RecordCounterProcessor.WriteDataToCSV(items, path))
                         .Should()
                         .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WriteDataToCSVNullParameters()
        {
            IEnumerable<string> items = null;

            FluentActions.Invoking(() => RecordCounterProcessor.WriteDataToCSV(items, null))
                         .Should()
                         .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WriteDataToCSVNullPath()
        {
            IEnumerable<string> items = new List<string>();

            FluentActions.Invoking(() => RecordCounterProcessor.WriteDataToCSV(items, null))
                         .Should()
                         .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WriteDataToCSV()
        {
            IEnumerable<string> items = new List<string>();
            var path = "TestData/apointmentsSchema.xml";

            FluentActions.Invoking(() => RecordCounterProcessor.WriteDataToCSV(items, path))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void ExecuteRecordsCountNullBackgroundWorker()
        {
            string exportConfigFilePath = "TestData/ExportConfig.json";
            string schemaFilePath = "TestData/apointmentsSchema.xml";
            var mockService = new Mock<IOrganizationService>();

            using (DataGridView gridView = new DataGridView())
            {
                FluentActions.Invoking(() => RecordCounterProcessor.ExecuteRecordsCount(exportConfigFilePath, schemaFilePath, mockService.Object, null, gridView))
                             .Should()
                             .Throw<ArgumentNullException>();
            }
        }

        [TestMethod]
        public void ExecuteRecordsCountNullDataGridView()
        {
            string exportConfigFilePath = "TestData/ExportConfig.json";
            string schemaFilePath = "TestData/apointmentsSchema.xml";
            var mockService = new Mock<IOrganizationService>();

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                FluentActions.Invoking(() => RecordCounterProcessor.ExecuteRecordsCount(exportConfigFilePath, schemaFilePath, mockService.Object, worker, null))
                             .Should()
                             .Throw<ArgumentNullException>();
            }
        }

        [TestMethod]
        public void ExecuteRecordsCount()
        {
            string exportConfigFilePath = "TestData/ExportConfig.json";
            string schemaFilePath = "TestData/apointmentsSchema.xml";
            var mockService = new Mock<IOrganizationService>();

            var response = new FetchXmlToQueryExpressionResponse();

            mockService.Setup(a => a.Execute(It.IsAny<FetchXmlToQueryExpressionRequest>()))
                       .Returns(response);

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                using (DataGridView gridView = new DataGridView())
                {
                    FluentActions.Invoking(() => RecordCounterProcessor.ExecuteRecordsCount(exportConfigFilePath, schemaFilePath, mockService.Object, worker, gridView))
                                 .Should()
                                 .Throw<NullReferenceException>();
                }
            }

            mockService.Verify(a => a.Execute(It.IsAny<FetchXmlToQueryExpressionRequest>()), Times.Once);
        }
    }
}