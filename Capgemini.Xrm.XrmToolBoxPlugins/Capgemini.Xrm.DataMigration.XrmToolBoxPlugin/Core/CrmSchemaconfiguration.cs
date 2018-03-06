using Capgemini.Xrm.DataMigration.Model;
using System;
using System.Xml.Serialization;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    internal class CrmSchemaconfiguration
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