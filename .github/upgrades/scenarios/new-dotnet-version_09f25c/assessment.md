# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [EnsekMeterReadingAPI\EnsekMeterReadingAPI.csproj](#ensekmeterreadingapiensekmeterreadingapicsproj)
  - [EnsekMEterReadingUnitTests\EnsekMEterReadingUnitTests.csproj](#ensekmeterreadingunittestsensekmeterreadingunittestscsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 2 | All require upgrade |
| Total NuGet Packages | 7 | 3 need upgrade |
| Total Code Files | 17 |  |
| Total Code Files with Incidents | 2 |  |
| Total Lines of Code | 920 |  |
| Total Number of Issues | 7 |  |
| Estimated LOC to modify | 0+ | at least 0.0% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [EnsekMeterReadingAPI\EnsekMeterReadingAPI.csproj](#ensekmeterreadingapiensekmeterreadingapicsproj) | netcoreapp3.1 | 🟢 Low | 5 | 0 |  | AspNetCore, Sdk Style = True |
| [EnsekMEterReadingUnitTests\EnsekMEterReadingUnitTests.csproj](#ensekmeterreadingunittestsensekmeterreadingunittestscsproj) | netcoreapp3.1 | 🟢 Low | 0 | 0 |  | DotNetCoreApp, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 4 | 57.1% |
| ⚠️ Incompatible | 0 | 0.0% |
| 🔄 Upgrade Recommended | 3 | 42.9% |
| ***Total NuGet Packages*** | ***7*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 725 |  |
| ***Total APIs Analyzed*** | ***725*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| Microsoft.EntityFrameworkCore | 5.0.2 | 10.0.3 | [EnsekMeterReadingAPI.csproj](#ensekmeterreadingapiensekmeterreadingapicsproj) | NuGet package upgrade is recommended |
| Microsoft.EntityFrameworkCore.Sqlite | 5.0.2 | 10.0.3 | [EnsekMeterReadingAPI.csproj](#ensekmeterreadingapiensekmeterreadingapicsproj) | NuGet package upgrade is recommended |
| Microsoft.NET.Test.Sdk | 16.5.0 |  | [EnsekMEterReadingUnitTests.csproj](#ensekmeterreadingunittestsensekmeterreadingunittestscsproj) | ✅Compatible |
| Moq | 4.17.2 |  | [EnsekMEterReadingUnitTests.csproj](#ensekmeterreadingunittestsensekmeterreadingunittestscsproj) | ✅Compatible |
| Newtonsoft.Json | 13.0.1 | 13.0.4 | [EnsekMeterReadingAPI.csproj](#ensekmeterreadingapiensekmeterreadingapicsproj) | NuGet package upgrade is recommended |
| NUnit | 3.12.0 |  | [EnsekMEterReadingUnitTests.csproj](#ensekmeterreadingunittestsensekmeterreadingunittestscsproj) | ✅Compatible |
| NUnit3TestAdapter | 3.16.1 |  | [EnsekMEterReadingUnitTests.csproj](#ensekmeterreadingunittestsensekmeterreadingunittestscsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;EnsekMeterReadingAPI.csproj</b><br/><small>netcoreapp3.1</small>"]
    P2["<b>📦&nbsp;EnsekMEterReadingUnitTests.csproj</b><br/><small>netcoreapp3.1</small>"]
    P2 --> P1
    click P1 "#ensekmeterreadingapiensekmeterreadingapicsproj"
    click P2 "#ensekmeterreadingunittestsensekmeterreadingunittestscsproj"

```

## Project Details

<a id="ensekmeterreadingapiensekmeterreadingapicsproj"></a>
### EnsekMeterReadingAPI\EnsekMeterReadingAPI.csproj

#### Project Info

- **Current Target Framework:** netcoreapp3.1
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 0
- **Dependants**: 1
- **Number of Files**: 16
- **Number of Files with Incidents**: 1
- **Lines of Code**: 640
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P2["<b>📦&nbsp;EnsekMEterReadingUnitTests.csproj</b><br/><small>netcoreapp3.1</small>"]
        click P2 "#ensekmeterreadingunittestsensekmeterreadingunittestscsproj"
    end
    subgraph current["EnsekMeterReadingAPI.csproj"]
        MAIN["<b>📦&nbsp;EnsekMeterReadingAPI.csproj</b><br/><small>netcoreapp3.1</small>"]
        click MAIN "#ensekmeterreadingapiensekmeterreadingapicsproj"
    end
    P2 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 591 |  |
| ***Total APIs Analyzed*** | ***591*** |  |

<a id="ensekmeterreadingunittestsensekmeterreadingunittestscsproj"></a>
### EnsekMEterReadingUnitTests\EnsekMEterReadingUnitTests.csproj

#### Project Info

- **Current Target Framework:** netcoreapp3.1
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 12
- **Number of Files with Incidents**: 1
- **Lines of Code**: 280
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["EnsekMEterReadingUnitTests.csproj"]
        MAIN["<b>📦&nbsp;EnsekMEterReadingUnitTests.csproj</b><br/><small>netcoreapp3.1</small>"]
        click MAIN "#ensekmeterreadingunittestsensekmeterreadingunittestscsproj"
    end
    subgraph downstream["Dependencies (1"]
        P1["<b>📦&nbsp;EnsekMeterReadingAPI.csproj</b><br/><small>netcoreapp3.1</small>"]
        click P1 "#ensekmeterreadingapiensekmeterreadingapicsproj"
    end
    MAIN --> P1

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 134 |  |
| ***Total APIs Analyzed*** | ***134*** |  |

