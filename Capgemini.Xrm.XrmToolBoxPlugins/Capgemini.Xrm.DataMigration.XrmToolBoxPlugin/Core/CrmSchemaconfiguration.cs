using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core.EntitySchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    class CrmSchemaconfiguration
    {
        public String exceptionData { get; set; }
        public void SaveToFile(string filePath, CrmEntity entity)
        {
            entity = new CrmEntity();
            XmlSerializer ser = new XmlSerializer(typeof(CrmEntity));
            var path = filePath + "//Schema.xml";
            try
            {
                System.IO.FileStream file = System.IO.File.Create(path);
                ser.Serialize(file, entity);
                file.Close();
            }
            catch (Exception e)
            {
                exceptionData = e.Message;
            }
        }
    }
}
