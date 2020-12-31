using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Extensions;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Extensions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    public static class RecordCounterProcessor
    {
        private const string FetchXmlTemplate = @"<fetch mapping='logical'><entity name='{entity}'><attribute name='{entity}id'/>{filter}</entity></fetch> ";

        public static void WriteDataToCSV<T>(IEnumerable<T> items, string path)
        {
            items.ThrowArgumentNullExceptionIfNull(nameof(items));
            path.ThrowArgumentNullExceptionIfNull(nameof(path));

            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .OrderBy(p => p.Name);

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                foreach (var item in items)
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                }
            }
        }

        public static List<RecordCountModel> ExecuteRecordsCount(string exportConfigFilePath, string schemaFilePath, IOrganizationService service, BackgroundWorker worker, DataGridView gridView)
        {
            worker.ThrowArgumentNullExceptionIfNull(nameof(worker));
            gridView.ThrowArgumentNullExceptionIfNull(nameof(gridView));
            service.ThrowArgumentNullExceptionIfNull(nameof(service));

            CrmExporterConfig crmExporterConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            CrmSchemaConfiguration crmSchemaConfiguration = CrmSchemaConfiguration.ReadFromFile(schemaFilePath);
            List<RecordCountModel> entityWrapperList = new List<RecordCountModel>();
            foreach (var item in crmSchemaConfiguration.Entities)
            {
                string fetchXml = FetchXmlTemplate;
                string filters = (crmExporterConfig.CrmMigrationToolSchemaFilters != null && crmExporterConfig.CrmMigrationToolSchemaFilters.ContainsKey(item.Name)) ? crmExporterConfig.CrmMigrationToolSchemaFilters[item.Name] : string.Empty;
                fetchXml = (!string.IsNullOrEmpty(filters)) ? fetchXml.Replace("{filter}", filters) : fetchXml.Replace("{filter}", string.Empty);
                fetchXml = fetchXml.Replace("{entity}", item.Name);

                // Convert the FetchXML into a query expression.
                var conversionRequest = new FetchXmlToQueryExpressionRequest
                {
                    FetchXml = fetchXml
                };

                var conversionResponse =
                    (FetchXmlToQueryExpressionResponse)service.Execute(conversionRequest);

                QueryExpression queryExpression = conversionResponse.Query;
                queryExpression.ColumnSet = new ColumnSet(false);
                worker.ReportProgress(0, $"Counting... {item.Name}");
                var results = service.GetDataByQuery(queryExpression, 5000, false).TotalRecordCount;
                worker.ReportProgress(0, $"{item.Name} record count: {results}");
                entityWrapperList.Add(new RecordCountModel { EntityName = item.Name, RecordCount = results });
                gridView.DataSource = null;
                gridView.Refresh();
                gridView.DataSource = entityWrapperList;
                gridView.Columns[0].Width = 250;
                gridView.Columns[1].Width = 250;
            }

            return entityWrapperList;
        }
    }
}