# Greg.Xrm

[![Build Status](https://neronotte.visualstudio.com/Greg.Xrm/_apis/build/status/neronotte.Greg.Xrm?branchName=master)](https://neronotte.visualstudio.com/Greg.Xrm/_build/latest?definitionId=2&branchName=master)

A suite of [XrmToolBox](https://www.xrmtoolbox.com/) tools to overcome D365 feature gaps.

## _n.EnvironmentComparer

**Environment comparer** is a tool that simplifies the comparison between different environments.

MORE INFO SOON

## _n.SolutionManager

**Solution manager** is a tool that provides an intuitive, easy to use, UI on the solution import process.
When connected to an environment, it shows the **last importjob** with the following info:

- Solution uniquename
- Solution friendlyname
- Solution version
- Solution publisher
- Solution description
- Import percentage of completeness (with a progressbar)
- Import output logs (with warnings and errors)

It provides the following capabilities

- autorefresh
- search by CTRL+F / F3
- enriched output log view via treeview

## _n.DataModelWikiEditor

**Data Model Wiki Editor** mai purpose is to generate markdown documentation
for a set of entities in a given DataVerse environment.

Inputs:
- A target folder, that can contain
   - a json configuration file (if none, it will be created)
   - an excel configuration file (if none, it will be created)


### _n.RoleEditor

This is a tool designed to streamline security role editing in dataverse.

It provides a rich set of features:

- Browsing roles from different environments in a single panel
- Environment > Business Unit > Role hierarchy
- Possibility to search for roles or filter not customizable or managed roles
- Simultaneous editing of multiple roles (even from different environments)
- Docking/undocking role editors to simplify role comparison
- Advanced privilege grouping and filtering
- Massive privilege editing "by table"
- Massive privilege editing "by column" (that applies only to visible privileges)
- Copy-Paste privilege configuration
- Export in Excel and Markdown
- Possibility to configure several aspects of the UI (such as icons, default filters, and privilege grouping)
- Contextual detailed help in all screens



## Releases

You can download the tool using the [Release page](https://github.com/neronotte/Greg.Xrm/releases) or NuGet:

- [_n.EnvironmentComparer](https://www.nuget.org/packages/Greg.Xrm.EnvironmentComparer)
- [_n.SolutionManager](https://www.nuget.org/packages/Greg.Xrm.SolutionManager)




# Instructions

In any project you should add a file called `Properties\AssemblyInfo.Partial.cs`
that will be re-generated on each build. Copy the file contents from one of the existing projects.

You should also update the .csproj files to add the following build steps:

```xml
  <PropertyGroup>
    <MSBuildCommunityTasksPath>$(SolutionDir)\.build</MSBuildCommunityTasksPath>
  </PropertyGroup>
  <PropertyGroup>
    <VersionNumber>1.0.0.0</VersionNumber>
  </PropertyGroup>
  <Import Project="$(MSBuildCommunityTasksPath)\MSBuild.Community.Tasks.Targets" />
  <Target Name="BeforeBuild">
    <Message Text="Updating AssemblyInfo to Version $(VersionNumber)" />
    <Message Text="Writing to AssemblyInfo files in $(SolutionRoot)" />
    <AssemblyInfo CodeLanguage="CS" OutputFile="Properties\AssemblyInfo.Partial.cs" AssemblyVersion="$(VersionNumber)" AssemblyFileVersion="$(VersionNumber)" />
  </Target>
```

## ILRepack Installation

ILRepack can now be installed as a dotnet tool:

```Powershell
PS C:\> dotnet tool install -g dotnet-ilrepack
```

See [official website](https://github.com/gluck/il-repack)
