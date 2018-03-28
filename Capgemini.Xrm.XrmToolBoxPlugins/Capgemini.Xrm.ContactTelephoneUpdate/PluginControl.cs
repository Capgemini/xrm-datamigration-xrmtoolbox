using System;
using System.Threading;
using System.Windows.Forms;
using Capgemini.Xrm.ContactTelephoneUpdate.Models;
using Capgemini.Xrm.XrmToolBoxPluginBase.DataMigration;
using Capgemini.Xrm.XrmToolBoxPluginBase.Models;

namespace Capgemini.Xrm.ContactTelephoneUpdate
{
    public partial class PluginControl : NhsbtPluginControlBase
    {
        public PluginControl()
        {
            InitializeComponent();

            _logger = new MessageLogger(richTextBoxLog, SynchronizationContext.Current);
            MessageLogger.LogLevel = LogLevel.Info;
        }

        private void PluginControl_Load(object sender, EventArgs e)
        {

        }

        private async void buttonUpdateCRM_Click(object sender, EventArgs e)
        {
            buttonUpdateCRM.Enabled = false;
            buttonCancel.Enabled = true;

            var migrationParameters = ReadConfigEntries();

            string validationMessage;
            if (migrationParameters.Validate(out validationMessage))
            {
                TokenSource = new CancellationTokenSource();
                var dataMigrationRunner = new ContactTelephoneUpdateDataMigrationRunner(_logger);

                try
                {
                    await dataMigrationRunner.ExecuteMigrationAsync(migrationParameters, TokenSource.Token);
                }
                catch (Exception exception)
                {
                   _logger.Error(exception.Message);
                }
            }
            else
            {
                MessageBox.Show(validationMessage);
            }

            buttonUpdateCRM.Enabled = true;
            buttonCancel.Enabled = false;
        }

        private MigrationParameters ReadConfigEntries()
        {
            var migrationParameters = new MigrationParameters(textBoxCrmConnString.Text);
            return migrationParameters;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            buttonCancel.Enabled = false;
            TokenSource?.Cancel();
        }
        
    }
}
