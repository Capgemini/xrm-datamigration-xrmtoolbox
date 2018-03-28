using System.ComponentModel.Composition;
using Capgemini.Xrm.XrmToolBoxPluginBase.Models;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.ContactTelephoneUpdate
{
    [Export(typeof(IXrmToolBoxPlugin)),
    ExportMetadata("BackgroundColor", "Gray"),
    ExportMetadata("PrimaryFontColor", "White"),
    ExportMetadata("SecondaryFontColor", "LightGray"),
    ExportMetadata("SmallImageBase64", PluginConstants.SmallImageBase64),
    ExportMetadata("BigImageBase64", PluginConstants.BigImageBase64),
    ExportMetadata("Name", "Contact Telephone Update"),
    ExportMetadata("Description", "Capgemini Update Entity Attributes - updates telephone numbers for all active donors")]
    public class Plugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new PluginControl();
        }
    }

}
