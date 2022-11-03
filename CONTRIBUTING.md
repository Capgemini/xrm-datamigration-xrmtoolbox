# Contributing

Please first discuss the change you wish to make via an issue before making a change. 

## Debugging

To debug the application within Visual Studio, follow these steps:

1. Right click "Capgemini.Xrm.CdsDataMigrator" project and select "Properties"
2. Select the "Debug" tab in the left
3. Set "Launch" to "Executable"
4. Set "Executable" to the location of XrmToolBox.exe you have installed (do not use the file under `bin/`)
5. Set "Application arguments" to `/overridepath:$(TargetDir)` 
6. The run project (green play button in the toolbar)

## Pull request process

1. Ensure that there are automated tests that cover any changes 
2. Update the README.md with details of any significant changes to functionality
3. Ensure that your commit messages increment the version using [GitVersion syntax](https://gitversion.readthedocs.io/en/latest/input/docs/more-info/version-increments/). If no message is found then the patch version will be incremented by default.
4. You may merge the pull request once it meets all of the required checks. If you do not have permision, a reviewer will do it for you
