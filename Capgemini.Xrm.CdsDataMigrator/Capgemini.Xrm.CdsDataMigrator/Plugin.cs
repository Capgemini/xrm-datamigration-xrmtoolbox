using MyXrmToolBoxPlugin3;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.DataMigration.XrmToolBox
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "CDS Data Migrator"),
        ExportMetadata("Description", "CDS Data Migrator tool provides routines for managing data migration operations within Microsoft Power Platform."),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAEnMAABJzAYwiuQcAAALJSURBVEhLY3x1/9WqqlUfnn9goDYQkBQIaw1jnJ4y/faB21AxagMlG2Wmj68/Qnk0AJ/efGKCMmkGRi0gCIafBQZeBvKG8kAGMJvYJ9lDBO0T7QWlBCFsUgG6BeZh5jouOkCGvL68W64bkMHIyOiS5SJnIAeWJxmgW/D20dt3T94BGR9ffXxx6wWQ8f///5d3Xn56+QksTzJg7PDveHX1FZRHIuDi5/r59effP3+hfAwgoiGK7gP3PHcDbwMgQ1pLOqY/BiIY0xcjoy0DYSMDWV3Z3JW5YspiUD42gG6BgrEC0GggQ0ReRNNBE8gAxoG6rbqwvDBImpHBJMjEPNQcgtTt1GX1ZIvWF9nE2TAyMYIUYABmFw2Xr6+/QnlgXz+9+vTNwzf///1nYma6efgmUJBPlO/6wetf338FmpK3Mk/PXU/bWRuIFAwVgLLMrMya9poKRgp3T9798eUH2Bgo4BLhRo8DoHuBsQplMzECrUFjtF9sZ+VgBcujg++fvk+NnPr81nMoH2scxEyIcUx1BDJULVUrdlVABMt3lKtZq0HYuADQf6trVyObDgEsUBoGgEEEREAGJz8njxAPkAH0E7cgNycfJ0j6P8O5LedYWKG6gEGnaqUKZFzZc2VN7ZrPbz5DxJEBehx8evXpwbkHX959ASJg8odkBWCFeufEnV/ffwHZV/devbzrMgQBTdRy1lpbv3Zrz9Zf30CyaAAYB+gW/P/7/9uHb79//v7359/f33+BHocIfnr9CRINaODY0mPAuIVyMADQAvQ4CGkOgRRBWg5a2cuzQUKMDJlLMrUctUBsVADM9u+fvodycAB0C4BO/voB5Opvn759efsFJPSfARhcwBQCYpMO8BYVwKyDJVRIAFiSKQqgzHQIwGsBNcCoBQQB7S1g4cZeNFIF8IjwMN4+f3td2/q/3/9iry8oANzC3MG1wQByRxaavUdbygAAAABJRU5ErkJggg=="),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class Plugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new MyPluginControl();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Plugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to
            // hook into the event that will fire when an Assembly fails to resolve
        }
    }
}
