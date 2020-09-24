# Xrm Data Migrator

## Content
 - [Introduction](#Introduction)
 - [Prerequisites](#Prerequisites)
 - [Installation](#Installation)
 - [Import Export Schema Generation](#Import-Export-Schema-Generation)
 - [Data Export](#Data-Export)
 - [Data Import](#Data-Import)
 
----

Current build status: <img src="https://capgeminiuk.visualstudio.com/Capgemini%20Reusable%20IP/_apis/build/status/NUGET%20CI%20Builds/XrmToolBox%20Plugin" alt="CI Build status">

### Introduction
The Xrm Data Migrator Plugin provides routines for managing data migration operations within Microsoft Dynamics 365.

### Prerequisites

Before using the plugin, we need to install XrmToolBox on your environment

Install the plugin

Launch XrmToolBox, if we haven’t installed the plugin before, then we click on Configuration menu and then on Tool Library

### Installation


### Import Export Schema Generation

### Data Export
Click on Schema Config tab, we can refresh entities by clicking on Refresh Entities button, then it will populate the Available entities list, and we can select the entities that we would to create schemas against.

For each entity selected, we need to select its attributes and we select the Schema File path where we would like to store the schema file, if the file is non-existent then we need to create one, see fig.3.

After fulfilling all the steps on the fig.3, then I click on the Save button and then we’ll be presented with a pop up as shown on screen in Fig.4

After clicking on “Save Schema” button in Fig.4, then the schema XML file will be created and saved in the path provided in the Schema file path field.

### Data Import
After exporting the data, we might need to import it in the target environment, we need to click on “Data Import” tab, then we’ll be presented with two options to select from (JSON or CSV)

After clicking on Next Button on Fig.5, we land on screen Fig.6, below to select the directory that contains the source data.

After clicking the next button on Fig.6 then we land on the screen below Fig.7

Text Boxes we can see the config file is not mandatory, after clicking on the next button n fig.7 then we should land on screen in fif.8

The log after the data are imported in Ms Dynamics environment