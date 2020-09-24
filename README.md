# Xrm Data Migrator

## Content
 - [Introduction](#Introduction)
 - [Installation](#Installation)
 - [Import Export Schema Generation](#Import-Export-Schema-Generation)
 - [Data Export](#Data-Export)
 - [Data Import](#Data-Import)
 
----

Current build status: <img src="https://capgeminiuk.visualstudio.com/Capgemini%20Reusable%20IP/_apis/build/status/NUGET%20CI%20Builds/XrmToolBox%20Plugin" alt="CI Build status">

### Introduction
The Xrm Data Migrator plugin provides routines for managing data migration operations within Microsoft Dynamics 365.

### Installation
Before using the Xrm Data Migrator, you will need to install XrmToolBox which can be downloaded from [here](https://www.xrmtoolbox.com/)

Once XrmToolBox is installed, launch it and then select the Tool Library through Configuration menu as shown below:

![Select Tool Library](images/SelectToolLibrary.png "Select Tool Library")

Then search for "Xrm DataMigration" as shown below:
![Search for Xrm Datamigration](images/SearchForXrmDatamigration.png "Search for Xrm Datamigration")

Install the data migrator.

Once the installation has completed successfully, you will see the Xrm Data Migrator listed in the Tools windows as shown below
![Xrm Data Migrator](images/XrmDataMigrator.png "Xrm Data Migrator")

Click the data migrator to launch it. You will be prompted for a connection to Dynamics 365 organization as shown in 
![Connectionstring prompt](images/ConnectionStringPrompt.png "Connectionstring prompt")

Provide valid connection details
![Connecting to Dynamics](images/ConnectingToDynamics.png "Connecting to Dynamics")

The landing page of the data migrator will now be displayed as shown in
![Data migrator landing page](images/DataMigratorLandingPage.png "Data migrator landing page")

### Import Export Schema Generation
The data migrator adhere to a predefined import export schema and the tool can be used to generate the respective schema for import and export. Note that for each of these, both the JSON and CSV formats are supported.
To Generate or modify an export schema, please perforfollow the steps below:
1) Select Generate/Modify Export Schema from the Schema Config tab of the data migrator as shownn in
![Data migrator landing page](images/DataMigratorLandingPage.png "Data migrator landing page")
2) Select the desired entities and attributes combination as shown in
![Export schema generation](images/ExportSchemaGeneration.png "Export schema generation"). Please ensure a schema file path is specified as shown in the image
3) Select Save to generate the schema as shown in ![Generate export schema](images/GenerateExportSchema.png "Generate export schema")
4) Once the export schema is generated, a "Successfully created XML file" dialog will pop up and the export schema XML file will be generated at the specified location. This file will contain all selected entities and their respective selected attributes and relationships 

### Data Export


### Data Import
