# CDS Data Migrator Guide

## Content
 - [Introduction](#Introduction)
 - [Use Cases](#Use-Cases)
 - [Installation](#Installation)
 - [Data Import and Data Export Schema Generation](#Data-Import-and-Data-Export-Schema-Generation)
 - [Export Config File](#Export-Config-File)
 - [Import Config File](#Import-Config-File)
 - [Data Export](#Data-Export)
 - [Data Import](#Data-Import) 
----

<img src="https://capgeminiuk.visualstudio.com/Capgemini%20Reusable%20IP/_apis/build/status/NUGET%20CI%20Builds/XrmToolBox%20Plugin" alt="CI Build status"> ![Nuget](https://img.shields.io/nuget/v/Capgemini.DataMigration.XrmToolBoxPlugin)

### Introduction
The CDS Data Migrator tool provides an easy to use interface that enables you to generate an XML schema file that can be used to export data from one CRM environment and import into another. The tool not only supports the ability to add entity attributes and many-to-many relationships to the schema, but beyond that, it supports the creation of filters and GUID mappings which are stored as JSON file formats.

#### Video Intro 

[![CDS Data Migrator - Intro](https://img.youtube.com/vi/-8PHKUIg6MQ/0.jpg)](https://youtu.be/-8PHKUIg6MQ)

### Use Cases
A major benefit of the CDS Data Migrator is that it has data obfuscation built in. For example, sample production data might contain confidential information such as names, email and GUIDs which should not exist in other environments. When import this data into another environment such as test or UAT then the tool can be configured to scrabble select entity fields during the import. Strings are scrabbled by replacing a value with a new value of the same length but with random characters (A-Z a-z 0-9). Numbers are scrabbled to another random number. 
If you need to move reference/configuration data from development into other environments such as test, UAT or production then this tool saves time as the configuration would only need to be done once and then can be replicated in other environments seamlessly using the tool. This includes entities such as Calendars, Business Units, Teams, and Organisation Settings.

#### Feature Comparison
Below is a table comparing the CDS Data Migrator tool with Microsoft's.

|Feature  |MSFT Configuration Migration Tool  |CDS Data Migrator  |
|:---------|:---------:|:---------:|
|Supports attributes in schema     |     ![Yes](images/yes.png "Yes")    |     ![Yes](images/yes.png "Yes")    |
|Supports many to many relationships in schema     |    ![Yes](images/yes.png "Yes")     |    ![Yes](images/yes.png "Yes")     |
|Schema Validation     |    ![Yes](images/yes.png "Yes")     |    ![No](images/no.png "No")     |
|Modification of existing schema     |   ![Yes](images/yes.png "Yes")      |     ![Yes](images/yes.png "Yes")    |
|Exporting of data from a single environment using schema     |     ![Yes](images/yes.png "Yes")    |     ![Yes](images/yes.png "Yes")    |
|Error logging on data export / import process   |     ![Yes](images/yes.png "Yes")    |     ![Yes](images/yes.png "Yes")    |
|Colour coded sorting applied to attributes     |     ![No](images/no.png "No")    |     ![Yes](images/yes.png "Yes")    |
|Apply GUID Mappings included in export / import process     |     ![No](images/no.png "No")    |     ![Yes](images/yes.png "Yes")    |
|Apply Filters included in the export / import process     |     ![No](images/no.png "No")    |     ![Yes](images/yes.png "Yes")    |
|Ability to migrate Teams     |     ![Yes](images/yes.png "Yes")    |     ![Yes](images/yes.png "Yes")    |
|Ability to migrate Calendars     |     ![No](images/no.png "No")    |     ![Yes](images/yes.png "Yes")    |
|Ability to migrate Business Units     |     ![Yes](images/yes.png "Yes")    |     ![Yes](images/yes.png "Yes")    |


### Installation
Before using the CDS Data Migrator, you will need to install XrmToolBox which can be downloaded from [here](https://www.xrmtoolbox.com/)

Once XrmToolBox is installed, launch it and then select the Tool Library through Configuration menu as shown below:
![Select Tool Library](images/SelectToolLibrary.png "Select Tool Library")

Then search for "CDS Data Migrator", select and **Install** it:
![Search for CDS Data Migrator](images/SearchForCDSDatamigration.png "Search for CDS Data Migrator")

Once the installation has completed successfully, you will see the CDS Data Migrator listed in the Tools windows as shown below:
![CDS Data Migrator](images/CDSDataMigrator.png "CDS Data Migrator")

Click the data migrator to launch it. You will be prompted for a connection to Dynamics 365 organization as shown below:

![Connectionstring prompt](images/ConnectionStringPrompt.png "Connectionstring prompt")

Connect to an environment and you will be taken to the Schema Configs page as shown below:
![Schema Configs page](images/InitialSchemaConfig.png "Schema Configs page")

### Data Import and Export Schema Generation
The data migrator adheres to a predefined import export schema and the tool can be used to generate the respective schema for import and export. Note that for each of these, both the JSON and CSV formats are supported. 

**To generate or modify a schema, please follow the steps below:**

1. Select **Schema Config** from the top ribbon if you are not on this page already.

2. If you have an existing schema, you can click on **Load Schema** to edit that file. This will pre-select the entities, attributes and relationships stored in that file.

3. Select/unselect the entities, attributes, and relationships to export/import. In this example, we are creating a schema file for Accounts although you can select multiple entities.
![Select Schema Configs](images/SelectSchemaConfigs.png "Select Schema Configs")

4. To save the file, click on **Save Schema**.
![Save Schema File](images/SaveSchemaConfigs.png "Save Schema File")

Your schema file will be saved at the specified location. This file will contain all selected entities and their respective selected attributes and relationships.

Below shows an example schema file:

![Schema Config Example](images/SchemaConfigExample.png "Schema Config Example")

### Export Config File
The CDS data migrator export user interface exposes only a subset of the available configuration points for the data migration operation. Through the provision of an export configuration file, the user can have a fine grain control of the export process. When the export config is not specified then the default settings are applied. While those values are beyond the scope of this documentation, it suffices to say that the default values are expected to satisfy most data operation scenarios.

For more on the data migration config settings [see](https://github.com/Capgemini/xrm-datamigration#Usage)

1. Select **Data Export** from the top ribbon which will bring you to the below page:
 ![Initial Export Page](images/InitialExportPage.png "Initial Export Page")

2. If you have an exisiting export config file, you can click on 'Load' to edit that file. This will pre-input the configs that you had stored in that file.
![Load Export Config file](images/LoadExportConfig.png "Load Export Config file")

3. Add in (or edit if you have loaded an export config file) Fetch and Write Settings and ensure you add a schema file.
 ![Edited Export Config file](images/EditedExportConfigs.png "Edited Export Config file")

4. To save the new or updated export config file, click on 'Save'.
 ![Save Export Configs](images/SaveExportConfigs.png "Save Export Configs")

Your export configs file will be saved at the specified location.

Below shows an example export config file:

 ![Export Config Example](images/ExportConfigExample.png "Export Config Example")

**NB:** Ensure the JsonFolderPath exists

### Import Config File
The CDS data migrator import user interface exposes only a subset of the available configuration points for the data migration operation. Through the provision of an import configuration file, the user can have a fine grain control of the import process. When the import config is not specified then the default settings are applied. 
For more on the data migration config settings [see](https://github.com/Capgemini/xrm-datamigration#Usage)

1. Select **Data Import** from the top ribbon which will bring you to the below page:
 ![Initial Export Page](images/InitialImportPage.png "Initial Export Page")

2. If you have an exisiting import config file, you can click on 'Load' to edit that file. This will pre-input the configs that you had stored in that file.
![Load Import Config file](images/LoadImportConfig.png "Load Config file")

3. Add in (or edit if you have loaded an import config file) Fetch and Write Settings (including a schema file if importing to CSV) and save.
 ![Edited Import Config file](images/EditedImportConfigs.png "Edited Import Config file")

4. To save the new or updated import config file, click on 'Save'.
 ![Save Import Configs](images/SaveImportConfigs.png "Save Import Configs")

Your import configs file will be saved at the specified location.

Below shows an example import config file:

 ![Import Config Example](images/ImportConfigExample.png "Import Config Example")

**NB:** Ensure the JsonFolderPath exists

### Data Export
Once all the schema and config files are downloaded the next step is to export the data from D365.

1.	Select **Data Export** from the ribbon and select format type of JSON or CSV.

2.	Select the location to save the data.

3.	Load an existing config file by clicking the 'Load' button (Optional Step)

4.	Select the Target Connection String then select the location of the schema file.

The Data Export page should look something like this:
![Run Export](images/EditedExportConfigs.png "Run Export")

5.	Select **Run** and the data will be exported to the specified location. You will also see a success message confirming that data export is complete:

![Data Export Complete](images/ExportDataComplete.png "Data Export Complete")

The Data is now extracted into a JSON file which you can see an example of below: 
![Data Export Output](images/DataExportOutput.png "Data Export Output")

In this example 3 Account records have been extracted with the attributes ‘Account Name’, ‘Email Address’ & ‘Account Number’

### Data Import
To import the exported data into an environment, follow the instructions below

1.	Select **Data Import** from the ribbon and select format type of JSON or CSV.
 
2.	Select the location of the source data.

3.  Load an existing config file by clicking the 'Load' button (Optional Step)

4.	Select the Target Connection String then select the location of the schema file (schema is only required for CSV).

The Data Import page should look something like this:
![Run Import](images/EditedImportConfigs.png "Run Import")

5.	Select **Run** and the data will be imported into D365. You will also see a success message confirming that data import is complete:

![Data Import Complete](images/ImportDataComplete.png "Data Import Complete")

You will then be able to view your data in D365. 
![Imported Accounts](images/D365Accounts.png "Imported Accounts")
