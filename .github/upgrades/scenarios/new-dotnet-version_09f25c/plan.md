# .NET 10.0 Upgrade Plan

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
  - [EnsekMeterReadingAPI.csproj](#ensekmeterreadingapicsproj)
  - [EnsekMEterReadingUnitTests.csproj](#ensekmeterreadingunittestscsproj)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Risk Management](#risk-management)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Description
Upgrade the EnsekMeterReadingAPI solution from .NET Core 3.1 to .NET 10.0 (Long Term Support). This solution consists of a single ASP.NET Core API project and its associated unit test project.

### Scope
**Projects Affected:** 2 projects
- `EnsekMeterReadingAPI.csproj` - Main ASP.NET Core API (640 LOC)
- `EnsekMEterReadingUnitTests.csproj` - NUnit test project (280 LOC)

**Current State:** All projects targeting `netcoreapp3.1`

**Target State:** All projects targeting `net10.0`

### Selected Strategy
**All-At-Once Strategy** - All projects upgraded simultaneously in a single atomic operation.

**Rationale:**
- Small solution (only 2 projects, 920 total LOC)
- Simple linear dependency structure (Test → API)
- No circular dependencies
- Both projects currently on .NET Core 3.1
- All required packages have clear upgrade paths to .NET 10.0-compatible versions
- No security vulnerabilities detected
- Low complexity rating for both projects

### Discovered Metrics
| Metric | Value |
|--------|-------|
| Total Projects | 2 |
| Total LOC | 920 |
| Dependency Depth | 1 (linear) |
| High-Risk Projects | 0 |
| Security Vulnerabilities | 0 |
| Packages Requiring Updates | 3 of 7 |
| Circular Dependencies | None |

### Critical Issues
- **No blocking issues identified**
- 3 packages require version updates (Entity Framework Core 5.0.2 → 10.0.3, Newtonsoft.Json 13.0.1 → 13.0.4)
- No deprecated APIs in use
- No source or binary incompatibilities detected

### Recommended Approach
Execute all-at-once atomic upgrade:
1. Update both project files to `net10.0` simultaneously
2. Update all package references in single operation
3. Build solution and address any compilation errors
4. Run all unit tests to validate functionality

### Iteration Strategy
**Fast Batch Approach** (Simple Solution):
- Phase 1: Discovery & Classification (Complete)
- Phase 2: Foundation (Iterations 2.1-2.3)
- Phase 3: Consolidated Detail Generation (1-2 iterations for all projects)
- Expected total iterations: 5-6

---

## Migration Strategy

### Approach Selection

**Selected Approach: All-At-Once Migration**

This solution is an ideal candidate for the All-At-Once strategy based on the following factors:

| Criteria | Assessment | Supports All-At-Once? |
|----------|------------|----------------------|
| Solution Size | 2 projects, 920 LOC | ✅ Yes - Well under 30 projects |
| Current Framework | Both on netcoreapp3.1 | ✅ Yes - Homogeneous starting point |
| Dependency Complexity | Linear (depth 1) | ✅ Yes - Simple structure |
| Package Compatibility | All have .NET 10.0 versions | ✅ Yes - Clear upgrade path |
| Security Issues | None detected | ✅ Yes - No urgent concerns |
| Test Coverage | NUnit test project present | ✅ Yes - Can validate changes |
| Risk Level | Low for both projects | ✅ Yes - Minimal risk |

### All-At-Once Strategy Rationale

**Advantages for This Solution:**
1. **Fastest completion time** - Single coordinated update completes upgrade in one operation
2. **No multi-targeting complexity** - Avoid intermediate SDK/package version management
3. **Unified testing** - Test entire solution at target framework in one pass
4. **Clean dependency resolution** - All projects on same framework eliminates compatibility issues
5. **Simplified coordination** - No need to track which projects are on which framework version

**Why Not Incremental:**
- Only 2 projects (incremental approach adds unnecessary overhead)
- No complex cross-project dependencies requiring staged migration
- Low total LOC (920) means risk is manageable in single operation
- No mission-critical constraints requiring zero-downtime phased approach

### Dependency-Based Ordering

While All-At-Once updates all projects simultaneously, the logical dependency order informs our approach:

**Dependency Flow:**
```
API (Foundation) → Tests (Consumer)
```

**Implementation Approach:**
- Update both project files to `net10.0` in single commit
- Update all package references across both projects together
- Build solution (tests will fail if API breaking changes exist)
- Fix any compilation errors in both projects
- Run tests to validate functionality

This ordering ensures that if compilation errors occur, they surface immediately during the atomic build, making them easier to identify and fix.

### Parallel vs Sequential Execution

**Sequential Execution Within Atomic Operation:**

Even though this is an all-at-once upgrade, individual steps occur sequentially:

1. **Project file updates** (both projects)
2. **Package reference updates** (both projects)
3. **Dependency restore** (solution-level)
4. **Build** (solution-level, exposes errors)
5. **Fix compilation errors** (as needed)
6. **Rebuild verification** (solution-level)
7. **Test execution** (test project)

This sequence ensures each step completes successfully before proceeding, while maintaining the atomic nature of the upgrade (no intermediate states pushed to source control).

### Phase Definitions

**Phase 0: Prerequisites (Not Applicable)**
- No global.json detected in assessment
- .NET 10.0 SDK availability will be verified at execution time

**Phase 1: Atomic Upgrade**

**Operations** (performed as single coordinated batch):
- Update `EnsekMeterReadingAPI.csproj` TargetFramework to `net10.0`
- Update `EnsekMEterReadingUnitTests.csproj` TargetFramework to `net10.0`
- Update Microsoft.EntityFrameworkCore packages (5.0.2 → 10.0.3)
- Update Microsoft.EntityFrameworkCore.Sqlite (5.0.2 → 10.0.3)
- Update Newtonsoft.Json (13.0.1 → 13.0.4)
- Restore dependencies
- Build solution
- Fix any compilation errors (reference Breaking Changes Catalog)
- Rebuild to verify fixes

**Deliverables:** Solution builds with 0 errors

**Phase 2: Test Validation**

**Operations:**
- Execute all NUnit tests in `EnsekMEterReadingUnitTests.csproj`
- Address any test failures
- Verify test coverage remains consistent

**Deliverables:** All tests pass

### Source Control Approach

**Single Commit Strategy (Recommended):**

Given the small scope and atomic nature, prefer a single commit containing:
- Both project file updates
- All package reference updates
- Any required code fixes for compilation

This approach:
- Maintains atomic upgrade semantics
- Simplifies rollback (single commit to revert)
- Clearly delineates "before upgrade" vs "after upgrade" state
- Avoids partially-upgraded intermediate commits

**Commit Message Example:**
```
Upgrade solution to .NET 10.0

- Update both projects from netcoreapp3.1 to net10.0
- Upgrade Entity Framework Core 5.0.2 → 10.0.3
- Upgrade EF Core SQLite 5.0.2 → 10.0.3
- Upgrade Newtonsoft.Json 13.0.1 → 13.0.4
- Fix compilation errors (if any)

All tests passing.
```

---

## Detailed Dependency Analysis

### Dependency Graph Summary

The solution has a simple, linear dependency structure with no circular dependencies:

```
EnsekMEterReadingUnitTests.csproj (netcoreapp3.1)
    └─→ EnsekMeterReadingAPI.csproj (netcoreapp3.1)
```

**Dependency Depth:** 1 level (very shallow)

**Critical Path:** API project → Test project

### Project Groupings by Migration Phase

Given the All-At-Once strategy, all projects will be upgraded simultaneously in a single atomic operation.

**Single Atomic Phase:**
- `EnsekMeterReadingAPI.csproj` (API - foundation)
- `EnsekMEterReadingUnitTests.csproj` (Tests - depends on API)

Both projects will have their target frameworks and packages updated together, ensuring consistency and avoiding any temporary multi-targeting scenarios.

### Dependency Relationships

| Project | Type | Dependencies | Dependants | Migration Priority |
|---------|------|--------------|------------|-------------------|
| EnsekMeterReadingAPI.csproj | AspNetCore | 0 projects | 1 project (Tests) | Phase 1 (Atomic) |
| EnsekMEterReadingUnitTests.csproj | DotNetCoreApp | 1 project (API) | 0 projects | Phase 1 (Atomic) |

### Circular Dependencies
**None detected** - Clean dependency structure enables straightforward migration.

### Migration Ordering Rationale

While the dependency graph suggests migrating the API before the test project, the All-At-Once strategy dictates updating both simultaneously. This approach is safe because:

1. **Small scope** - Only 2 projects, 920 total LOC
2. **Single dependency** - Test project only depends on API project
3. **No shared libraries** - No complex multi-project package coordination needed
4. **SDK-style projects** - Both use modern project format, simplifying updates

The atomic update ensures that both projects remain compatible throughout the upgrade process.

---

## Project-by-Project Plans

### EnsekMeterReadingAPI.csproj

#### Current State
- **Target Framework:** `netcoreapp3.1`
- **Project Type:** AspNetCore (ASP.NET Core API)
- **SDK-Style:** Yes
- **Lines of Code:** 640
- **Dependencies:** 0 project references
- **Dependants:** 1 project (EnsekMEterReadingUnitTests)
- **Packages Requiring Updates:** 3
  - Microsoft.EntityFrameworkCore: 5.0.2
  - Microsoft.EntityFrameworkCore.Sqlite: 5.0.2
  - Newtonsoft.Json: 13.0.1
- **Risk Level:** 🟢 Low

#### Target State
- **Target Framework:** `net10.0`
- **Packages Updated:** 3
  - Microsoft.EntityFrameworkCore: 10.0.3
  - Microsoft.EntityFrameworkCore.Sqlite: 10.0.3
  - Newtonsoft.Json: 13.0.4

#### Migration Steps

##### 1. Prerequisites
- ✅ .NET 10.0 SDK installed on development machine
- ✅ No project dependencies to migrate first (leaf node in dependency graph)
- ✅ Verify current build succeeds before starting upgrade

##### 2. Update Project File (EnsekMeterReadingAPI.csproj)

**Change TargetFramework:**
```xml
<!-- Before -->
<TargetFramework>netcoreapp3.1</TargetFramework>

<!-- After -->
<TargetFramework>net10.0</TargetFramework>
```

##### 3. Update Package References

| Package | Current Version | Target Version | Update Reason |
|---------|----------------|----------------|---------------|
| Microsoft.EntityFrameworkCore | 5.0.2 | 10.0.3 | Compatibility with .NET 10.0 |
| Microsoft.EntityFrameworkCore.Sqlite | 5.0.2 | 10.0.3 | Compatibility with .NET 10.0 |
| Newtonsoft.Json | 13.0.1 | 13.0.4 | Recommended update (bug fixes) |

**PackageReference Updates:**
```xml
<!-- Update in EnsekMeterReadingAPI.csproj -->
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.3" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.3" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.4" />
```

##### 4. Expected Breaking Changes

**Entity Framework Core 5.0 → 10.0:**

Potential breaking changes across major versions:

- **EF Core 6.0 Changes:**
  - `DbContext` configuration changes
  - Query behavior changes (null semantics)
  - SQLite provider improvements

- **EF Core 7.0 Changes:**
  - JSON column support improvements
  - Bulk update/delete operations
  - ExecuteUpdate/ExecuteDelete methods

- **EF Core 8.0 Changes:**
  - Complex types support
  - Primitive collections
  - HierarchyId support

- **EF Core 9.0 Changes:**
  - LINQ query improvements
  - Performance enhancements
  - AOT compilation support

- **EF Core 10.0 Changes:**
  - Check official [EF Core 10.0 breaking changes documentation](https://learn.microsoft.com/ef/core/what-is-new/ef-core-10.0/breaking-changes)

**ASP.NET Core 3.1 → 10.0:**

- **Startup Pattern:** .NET 6+ uses minimal hosting model (Program.cs only)
  - If project uses `Startup.cs`, may continue working but consider migrating to minimal API pattern

- **Middleware Registration:** Order and availability may have changed

- **Authentication/Authorization:** Some configuration methods may have changed

- **Endpoint Routing:** Further improvements and potential changes

**Newtonsoft.Json 13.0.1 → 13.0.4:**
- Minor version update, no breaking changes expected
- Bug fixes and performance improvements

##### 5. Code Modifications

**Areas Requiring Review:**

1. **DbContext Configuration**
   - Review `OnConfiguring` and `OnModelCreating` methods
   - Check connection string configuration
   - Verify SQLite-specific settings

2. **Program.cs / Startup.cs**
   - Review middleware registration order
   - Verify service registration patterns
   - Check authentication/authorization setup
   - Consider migrating to minimal hosting model if using Startup.cs

3. **Controllers**
   - Review attribute routing patterns
   - Check model binding behavior
   - Verify action result types

4. **Configuration**
   - Review `appsettings.json` if present
   - Verify logging configuration
   - Check CORS configuration if used

5. **Database Queries**
   - Review LINQ queries for behavior changes
   - Test null handling in queries
   - Verify Include/ThenInclude patterns

**Obsolete API Replacements:**
- Assessment shows no obsolete APIs detected
- Monitor compiler warnings during build for any deprecations

##### 6. Testing Strategy

**Unit Testing:**
- Will be covered by EnsekMEterReadingUnitTests project (see next section)

**API Testing:**
- Test all API endpoints after upgrade
- Verify request/response serialization (JSON)
- Test error handling and validation

**Database Testing:**
- Verify database connection and context initialization
- Test CRUD operations
- Verify any migrations still work (if present)
- Test SQLite-specific functionality

**Integration Testing:**
- Test full request pipeline
- Verify middleware chain execution
- Test authentication/authorization if present

##### 7. Validation Checklist

- [ ] Project file updated to `net10.0`
- [ ] All 3 packages updated to target versions
- [ ] Dependencies restored successfully (`dotnet restore`)
- [ ] Project builds without errors
- [ ] Project builds without warnings (or warnings reviewed and acceptable)
- [ ] All API endpoints respond correctly
- [ ] Database operations function correctly
- [ ] No Entity Framework query errors
- [ ] JSON serialization/deserialization works
- [ ] Configuration loads correctly
- [ ] Ready for test project execution

---

### EnsekMEterReadingUnitTests.csproj

#### Current State
- **Target Framework:** `netcoreapp3.1`
- **Project Type:** DotNetCoreApp (Test Project)
- **SDK-Style:** Yes
- **Lines of Code:** 280
- **Dependencies:** 1 project reference (EnsekMeterReadingAPI.csproj)
- **Dependants:** 0 projects
- **Packages Requiring Updates:** 0 (all compatible)
  - Microsoft.NET.Test.Sdk: 16.5.0 (Compatible)
  - Moq: 4.17.2 (Compatible)
  - NUnit: 3.12.0 (Compatible)
  - NUnit3TestAdapter: 3.16.1 (Compatible)
- **Risk Level:** 🟢 Low

#### Target State
- **Target Framework:** `net10.0`
- **Packages:** All existing packages remain compatible

#### Migration Steps

##### 1. Prerequisites
- ✅ EnsekMeterReadingAPI.csproj successfully upgraded to net10.0
- ✅ API project builds without errors
- ✅ .NET 10.0 SDK installed

##### 2. Update Project File (EnsekMEterReadingUnitTests.csproj)

**Change TargetFramework:**
```xml
<!-- Before -->
<TargetFramework>netcoreapp3.1</TargetFramework>

<!-- After -->
<TargetFramework>net10.0</TargetFramework>
```

##### 3. Update Package References

**No package updates required.** All test packages are marked as compatible with .NET 10.0:

| Package | Current Version | Status | Notes |
|---------|----------------|--------|-------|
| Microsoft.NET.Test.Sdk | 16.5.0 | ✅ Compatible | May consider updating to latest (17.x) for improvements |
| Moq | 4.17.2 | ✅ Compatible | Recent version, works with .NET 10.0 |
| NUnit | 3.12.0 | ✅ Compatible | May consider updating to 3.14+ for .NET 10.0 optimizations |
| NUnit3TestAdapter | 3.16.1 | ✅ Compatible | May consider updating to 4.x for better performance |

**Optional Package Updates** (not required, but recommended for optimal .NET 10.0 experience):
```xml
<!-- Optional: Update to latest versions for better .NET 10.0 support -->
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.0" />
<PackageReference Include="NUnit" Version="4.2.2" />
<PackageReference Include="NUnit3TestAdapter" Version="4.6.0" />
<PackageReference Include="Moq" Version="4.20.72" />
```

⚠️ **Note:** If updating NUnit from 3.x to 4.x, review [NUnit 4.0 breaking changes](https://docs.nunit.org/articles/nunit/release-notes/breaking-changes.html).

##### 4. Expected Breaking Changes

**Minimal Breaking Changes Expected:**

- **Test Framework:** NUnit 3.12.0 is compatible with .NET 10.0, no code changes required
- **Mocking Framework:** Moq 4.17.2 works with .NET 10.0
- **Project Reference:** Now references net10.0 version of API project (automatic)

**Potential Issues:**

1. **Test Behavior Changes:** If API project has behavioral changes due to framework upgrade, tests may fail (not due to test framework, but due to changed behavior)

2. **Async Test Patterns:** .NET 10.0 may have improved async behavior; verify async tests still pass

3. **Exception Messages:** Framework exception messages may have changed; tests asserting on exact messages may fail

##### 5. Code Modifications

**Areas Requiring Review:**

1. **Test Fixtures**
   - Review setup/teardown methods
   - Verify test initialization patterns
   - Check for any framework-specific test attributes

2. **Mocks and Stubs**
   - Verify Moq setup patterns still work
   - Check mock verification syntax
   - Test any mocked API interfaces

3. **Assertions**
   - Review NUnit assertions for behavior changes
   - Verify custom assertions if any
   - Check exception assertion patterns

4. **Test Data**
   - Verify test data setup
   - Check any test database initialization
   - Review in-memory database usage (SQLite)

**Obsolete API Replacements:**
- Assessment shows no obsolete APIs detected in test project

##### 6. Testing Strategy

**Test Execution:**
- Run all tests after framework upgrade
- Verify test discovery works correctly
- Check test execution times (should be similar or faster)

**Test Validation:**
- Verify all tests that passed before still pass
- Investigate any new failures (likely due to API behavior changes, not test framework)
- Check test coverage metrics remain consistent

**Integration with API:**
- Verify project reference to API project works
- Test API mocking still functions
- Validate integration tests with upgraded API

##### 7. Validation Checklist

- [ ] Project file updated to `net10.0`
- [ ] Dependencies restored successfully (`dotnet restore`)
- [ ] Test project builds without errors
- [ ] Test project builds without warnings
- [ ] Test discovery succeeds (all tests found)
- [ ] All tests execute successfully
- [ ] All tests that passed before upgrade still pass
- [ ] Test execution time acceptable
- [ ] No Moq-related errors
- [ ] Integration with API project verified
- [ ] Test coverage consistent with pre-upgrade baseline

---

## Package Update Reference

### Overview

| Total Packages | Compatible | Requiring Updates | Deprecated |
|----------------|------------|-------------------|------------|
| 7 | 4 (57.1%) | 3 (42.9%) | 0 |

### Required Package Updates

| Package | Current | Target | Affected Projects | Update Reason | Priority |
|---------|---------|--------|-------------------|---------------|----------|
| Microsoft.EntityFrameworkCore | 5.0.2 | 10.0.3 | EnsekMeterReadingAPI | Framework compatibility | High |
| Microsoft.EntityFrameworkCore.Sqlite | 5.0.2 | 10.0.3 | EnsekMeterReadingAPI | Framework compatibility | High |
| Newtonsoft.Json | 13.0.1 | 13.0.4 | EnsekMeterReadingAPI | Bug fixes, recommended update | Medium |

### Compatible Packages (No Update Required)

| Package | Current Version | Affected Projects | Status |
|---------|----------------|-------------------|--------|
| Microsoft.NET.Test.Sdk | 16.5.0 | EnsekMEterReadingUnitTests | ✅ Compatible with .NET 10.0 |
| Moq | 4.17.2 | EnsekMEterReadingUnitTests | ✅ Compatible with .NET 10.0 |
| NUnit | 3.12.0 | EnsekMEterReadingUnitTests | ✅ Compatible with .NET 10.0 |
| NUnit3TestAdapter | 3.16.1 | EnsekMEterReadingUnitTests | ✅ Compatible with .NET 10.0 |

### Optional Package Updates

While not required, consider updating test packages for improved .NET 10.0 support:

| Package | Current | Latest Compatible | Benefit |
|---------|---------|-------------------|---------|
| Microsoft.NET.Test.Sdk | 16.5.0 | 17.11.0 | Improved test execution performance |
| NUnit | 3.12.0 | 4.2.2 | Better .NET 10.0 integration, new features |
| NUnit3TestAdapter | 3.16.1 | 4.6.0 | Improved test discovery and execution |
| Moq | 4.17.2 | 4.20.72 | Latest features and bug fixes |

⚠️ **Note:** Updating NUnit 3.x → 4.x includes breaking changes. Review [NUnit 4.0 migration guide](https://docs.nunit.org/articles/nunit/release-notes/breaking-changes.html) before updating.

### Package Update Strategy

**All-At-Once Approach:**
- All 3 required package updates applied simultaneously
- API project packages updated in single operation
- Test project requires no mandatory updates (framework update only)

**Verification Steps:**
1. Update package references in project file
2. Run `dotnet restore` to download new packages
3. Build solution to identify any breaking changes
4. Address compilation errors
5. Run tests to verify functionality

### Package-Specific Considerations

#### Microsoft.EntityFrameworkCore (5.0.2 → 10.0.3)

**Major Version Jumps:** 5 → 6 → 7 → 8 → 9 → 10

**Impact:** HIGH - Multiple major versions

**Breaking Changes to Review:**
- EF Core 6.0: Nullable reference types, query behavior changes
- EF Core 7.0: Bulk operations, JSON columns
- EF Core 8.0: Complex types, primitive collections
- EF Core 9.0: LINQ improvements, AOT support
- EF Core 10.0: Latest features and optimizations

**Migration Strategy:**
- Review all breaking changes documentation
- Test database queries thoroughly
- Verify SQLite provider compatibility
- Check context configuration patterns

#### Microsoft.EntityFrameworkCore.Sqlite (5.0.2 → 10.0.3)

**Major Version Jumps:** 5 → 6 → 7 → 8 → 9 → 10

**Impact:** HIGH - Mirrors EF Core updates

**Breaking Changes to Review:**
- SQLite-specific provider changes
- Connection string format changes
- Feature support additions

**Migration Strategy:**
- Test all SQLite-specific features
- Verify connection initialization
- Check any SQLite pragmas or settings

#### Newtonsoft.Json (13.0.1 → 13.0.4)

**Version Jump:** Minor patch update

**Impact:** LOW - Patch version update only

**Breaking Changes:** None expected (patch version)

**Migration Strategy:**
- Minimal testing required
- Verify JSON serialization/deserialization still works
- Check any custom converters if present

### Dependency Resolution

**No Conflicts Expected:**

The dependency graph is simple and all packages are compatible:

```
EnsekMEterReadingUnitTests (net10.0)
  ├─→ EnsekMeterReadingAPI (net10.0)
  │     ├─→ Microsoft.EntityFrameworkCore (10.0.3)
  │     ├─→ Microsoft.EntityFrameworkCore.Sqlite (10.0.3)
  │     └─→ Newtonsoft.Json (13.0.4)
  ├─→ Microsoft.NET.Test.Sdk (16.5.0)
  ├─→ Moq (4.17.2)
  ├─→ NUnit (3.12.0)
  └─→ NUnit3TestAdapter (3.16.1)
```

**Verification:** Run `dotnet restore` and check for any package conflict warnings.

---

## Breaking Changes Catalog

### Overview

The upgrade from .NET Core 3.1 to .NET 10.0 spans multiple major framework versions (3.1 → 5.0 → 6.0 → 7.0 → 8.0 → 9.0 → 10.0). Each version introduced breaking changes that may affect this solution.

**Assessment Status:** No binary or source incompatibilities detected in current codebase, but breaking changes should still be reviewed.

### .NET Framework Breaking Changes

#### .NET 5.0 Breaking Changes
[Reference: https://docs.microsoft.com/dotnet/core/compatibility/5.0]

**Potential Impact Areas:**
- **Globalization:** ICU libraries on Windows
- **Serialization:** BinaryFormatter restrictions
- **Windows Forms:** (Not applicable - no WinForms in solution)

**Action Required:** Low - Most changes not applicable to ASP.NET Core APIs

#### .NET 6.0 Breaking Changes
[Reference: https://docs.microsoft.com/dotnet/core/compatibility/6.0]

**High Impact:**
- **Nullable Reference Types:** C# 10 enables nullable by default
- **Minimal Hosting Model:** New Program.cs pattern (Startup.cs optional)
- **Parameter Binding:** Changes in model binding behavior

**Action Required:** Medium - Review nullable annotations, consider hosting model

#### .NET 7.0 Breaking Changes
[Reference: https://docs.microsoft.com/dotnet/core/compatibility/7.0]

**Potential Impact Areas:**
- **Regular Expressions:** Performance and behavior changes
- **JSON Serialization:** System.Text.Json improvements

**Action Required:** Low - Limited impact on typical API scenarios

#### .NET 8.0 Breaking Changes
[Reference: https://docs.microsoft.com/dotnet/core/compatibility/8.0]

**Potential Impact Areas:**
- **Authentication:** Identity changes
- **Dependency Injection:** Service lifetime validation
- **HTTPS:** Certificate validation changes

**Action Required:** Medium - Review DI and authentication if used

#### .NET 9.0 Breaking Changes
[Reference: https://learn.microsoft.com/dotnet/core/compatibility/9.0]

**Potential Impact Areas:**
- **Performance:** LINQ and collection optimizations
- **Security:** Enhanced security defaults

**Action Required:** Low - Mostly improvements, minimal breaking

#### .NET 10.0 Breaking Changes
[Reference: https://learn.microsoft.com/dotnet/core/compatibility/10.0]

**Potential Impact Areas:**
- Check official documentation as it becomes available
- Focus on new features and optimizations

**Action Required:** To be determined - review upon release

### ASP.NET Core Breaking Changes

#### ASP.NET Core 3.1 → 5.0
- Endpoint routing is default
- Authorization middleware order matters
- Kestrel configuration changes

#### ASP.NET Core 5.0 → 6.0
- Minimal API introduction
- Startup.cs optional (Program.cs only)
- WebApplicationBuilder pattern
- Middleware registration changes

**Recommended Actions:**
- ✅ Review middleware registration order in Program.cs/Startup.cs
- ✅ Verify authentication/authorization configuration
- ✅ Test endpoint routing behavior
- ⚠️ Consider migrating to minimal hosting model (optional)

#### ASP.NET Core 6.0 → 10.0
- Improved minimal APIs
- Enhanced dependency injection
- Performance improvements
- Security enhancements

**Recommended Actions:**
- ✅ Review any custom middleware
- ✅ Verify service registration patterns
- ✅ Test request pipeline behavior

### Entity Framework Core Breaking Changes

#### EF Core 5.0 → 6.0
[Reference: https://docs.microsoft.com/ef/core/what-is-new/ef-core-6.0/breaking-changes]

**High Impact:**
- **Nullable Reference Types:** Better null handling in queries
- **Provider Changes:** SQLite provider improvements
- **Query Behavior:** Null semantics changes
- **ToQueryString:** Renamed from ToQueryString

**Recommended Actions:**
- ✅ Review LINQ queries for null handling
- ✅ Test queries that check for null values
- ✅ Verify Include/ThenInclude patterns
- ✅ Check any raw SQL queries

#### EF Core 6.0 → 7.0
[Reference: https://docs.microsoft.com/ef/core/what-is-new/ef-core-7.0/breaking-changes]

**Medium Impact:**
- **Bulk Operations:** ExecuteUpdate/ExecuteDelete methods added
- **JSON Columns:** Native JSON support
- **Temporal Tables:** SQL Server feature support

**Recommended Actions:**
- ⚠️ Consider using new bulk operations for efficiency
- ✅ Review any custom bulk update logic

#### EF Core 7.0 → 8.0
[Reference: https://docs.microsoft.com/ef/core/what-is-new/ef-core-8.0/breaking-changes]

**Medium Impact:**
- **Complex Types:** Value objects support
- **Primitive Collections:** Native array/list mapping
- **HierarchyId:** SQL Server hierarchy support

**Recommended Actions:**
- ✅ Review value object patterns
- ✅ Test collection properties in entities

#### EF Core 8.0 → 9.0
[Reference: https://learn.microsoft.com/ef/core/what-is-new/ef-core-9.0/breaking-changes]

**Medium Impact:**
- **LINQ Improvements:** Better query translation
- **Performance:** Optimizations in query pipeline
- **AOT Support:** Ahead-of-time compilation support

**Recommended Actions:**
- ✅ Test complex LINQ queries
- ✅ Verify query performance

#### EF Core 9.0 → 10.0
[Reference: https://learn.microsoft.com/ef/core/what-is-new/ef-core-10.0/breaking-changes]

**Impact:** To be determined

**Recommended Actions:**
- ⚠️ Review official documentation when available
- ✅ Test all database operations thoroughly

### Package-Specific Breaking Changes

#### Newtonsoft.Json 13.0.1 → 13.0.4

**Impact:** None expected (patch version)

**Type:** Patch release (bug fixes only)

**Recommended Actions:**
- ✅ Verify JSON serialization/deserialization works
- ✅ Test any custom JsonConverter implementations

### C# Language Version Changes

#### C# 9.0 (.NET 5.0)
- Records
- Init-only properties
- Pattern matching improvements

#### C# 10.0 (.NET 6.0)
- **Nullable Reference Types enabled by default**
- Global usings
- File-scoped namespaces
- Record structs

**Impact:** Medium - Nullable warnings may appear

**Recommended Actions:**
- ⚠️ Review compiler warnings for null reference issues
- ⚠️ Consider enabling nullable annotations: `<Nullable>enable</Nullable>`
- ⚠️ Address nullable reference warnings

#### C# 11.0 (.NET 7.0)
- Raw string literals
- Required members
- UTF-8 string literals

#### C# 12.0 (.NET 8.0)
- Primary constructors
- Collection expressions
- Ref readonly parameters

#### C# 13.0 (.NET 9.0)
- Enhanced pattern matching
- Collection improvements

#### C# 14.0 (.NET 10.0)
- Check official documentation for new features

### Testing Framework Considerations

#### NUnit 3.12.0 with .NET 10.0

**Compatibility:** ✅ Compatible, no breaking changes expected

**Optional Upgrade Path:**
- NUnit 3.x → 4.x brings performance improvements
- Review [NUnit 4.0 breaking changes](https://docs.nunit.org/articles/nunit/release-notes/breaking-changes.html) if upgrading

**Recommended Actions:**
- ✅ Verify all tests execute successfully
- ✅ Check test discovery works
- ✅ Monitor test execution times

### SQLite-Specific Considerations

#### Microsoft.EntityFrameworkCore.Sqlite 5.0.2 → 10.0.3

**Potential Issues:**
- Connection string format changes
- SQLite version requirements
- Feature support changes (JSON, full-text search)

**Recommended Actions:**
- ✅ Verify SQLite connection initialization
- ✅ Test database file creation/access
- ✅ Check any SQLite-specific queries
- ✅ Verify transaction handling

### Summary of Action Items

#### High Priority (Must Review)
- [ ] Test all Entity Framework queries (null handling, behavior changes)
- [ ] Verify middleware registration and order in Program.cs/Startup.cs
- [ ] Review authentication/authorization configuration
- [ ] Test database context initialization and migrations
- [ ] Run complete test suite and verify results

#### Medium Priority (Should Review)
- [ ] Review nullable reference type warnings if any
- [ ] Consider enabling nullable annotations in project
- [ ] Review dependency injection service registrations
- [ ] Test JSON serialization/deserialization patterns
- [ ] Verify CORS configuration if applicable

#### Low Priority (Optional)
- [ ] Consider migrating to minimal hosting model (Program.cs only)
- [ ] Consider upgrading test packages to latest versions
- [ ] Review new C# language features for code improvements
- [ ] Consider using new EF Core features (bulk operations, JSON columns)

### Breaking Change Detection Strategy

**During Build:**
1. Compiler errors → Breaking changes requiring code fixes
2. Compiler warnings → Potential issues or deprecations
3. Clean build → No syntactic breaking changes detected

**During Testing:**
1. Test failures → Behavioral changes requiring investigation
2. Exception messages → Runtime breaking changes
3. All tests pass → No functional regressions detected

**During Runtime:**
1. Application startup → Configuration or middleware issues
2. API endpoints → Request/response pipeline changes
3. Database operations → EF Core query or connection issues

---

## Risk Management

### Overall Risk Assessment

**Solution Risk Level: 🟢 LOW**

This upgrade presents minimal risk due to:
- Small codebase (920 LOC total)
- Simple dependency structure (no circular dependencies)
- Modern starting point (.NET Core 3.1)
- All packages have compatible versions
- No security vulnerabilities
- Existing test coverage
- No deprecated APIs detected

### High-Risk Changes

**None identified** - All changes are standard framework/package version updates.

### Medium-Risk Considerations

| Area | Risk Description | Mitigation Strategy |
|------|------------------|---------------------|
| Entity Framework Core | Major version jump (5.0 → 10.0) | Review EF Core 6.0-10.0 breaking changes; test database operations thoroughly |
| ASP.NET Core | Framework jump (3.1 → 10.0) | Review middleware/configuration patterns; test all API endpoints |
| Testing Framework | Compatibility with .NET 10.0 | Verify NUnit 3.12.0 and adapters work with .NET 10.0 |

### Security Vulnerabilities

**Status: ✅ None Detected**

No packages with known security vulnerabilities were identified in the assessment. All package updates are for compatibility, not security remediation.

### Contingency Plans

#### Scenario: Entity Framework Core Breaking Changes

**If:** Database operations fail after EF Core upgrade

**Then:**
1. Review [EF Core 6.0](https://docs.microsoft.com/ef/core/what-is-new/ef-core-6.0/breaking-changes), [7.0](https://docs.microsoft.com/ef/core/what-is-new/ef-core-7.0/breaking-changes), [8.0](https://docs.microsoft.com/ef/core/what-is-new/ef-core-8.0/breaking-changes), [9.0](https://learn.microsoft.com/ef/core/what-is-new/ef-core-9.0/breaking-changes), and [10.0](https://learn.microsoft.com/ef/core/what-is-new/ef-core-10.0/breaking-changes) documentation
2. Check for changes in SQLite provider behavior
3. Verify connection strings and configuration patterns
4. Update database context initialization if needed

**Rollback:** Revert to .NET Core 3.1 and EF Core 5.0.2

#### Scenario: Test Failures After Upgrade

**If:** NUnit tests fail after framework upgrade

**Then:**
1. Verify test framework compatibility (NUnit 3.12.0 may need update for .NET 10.0)
2. Check for behavioral changes in tested APIs
3. Review test setup/teardown patterns for .NET 10.0 compatibility
4. Update test assertions if framework behavior changed

**Rollback:** Revert target framework changes

#### Scenario: ASP.NET Core Configuration Issues

**If:** API fails to start or middleware errors occur

**Then:**
1. Review Program.cs/Startup.cs for deprecated patterns
2. Check middleware registration order
3. Verify dependency injection service lifetimes
4. Review authentication/authorization configuration

**Rollback:** Revert to netcoreapp3.1

### Rollback Strategy

**Simple Rollback Process:**

Given the single-commit approach, rollback is straightforward:

```bash
git revert <upgrade-commit-hash>
```

Or if changes haven't been committed:
```bash
git checkout -- .
```

**Rollback Validation:**
1. Solution builds successfully
2. All tests pass
3. API starts and responds to requests
4. Database operations function correctly

### Risk Mitigation Checklist

- [ ] .NET 10.0 SDK installed and verified
- [ ] All package versions confirmed compatible with net10.0
- [ ] Test project runs successfully before upgrade (baseline)
- [ ] Database backup created (if using persistent SQLite database)
- [ ] API endpoint smoke tests identified
- [ ] Rollback procedure documented and understood

---

## Testing & Validation Strategy

### Multi-Level Testing Approach

This upgrade requires validation at three levels: atomic operation validation, comprehensive solution validation, and regression testing.

---

### Phase 1: Atomic Upgrade Validation

#### Build Validation

**Objective:** Ensure solution compiles without errors after framework and package updates.

**Steps:**
1. Execute `dotnet restore` on solution
   - ✅ All packages download successfully
   - ✅ No package conflict warnings
   - ✅ Dependency resolution succeeds

2. Execute `dotnet build` on solution
   - ✅ Both projects build without errors
   - ⚠️ Review any warnings (nullable references, deprecations)
   - ✅ Output assemblies target net10.0

3. Inspect build output
   - ✅ Verify target framework in assembly metadata
   - ✅ Check for unexpected warnings
   - ✅ Confirm no package downgrade warnings

**Success Criteria:**
- [ ] `dotnet restore` exits with code 0
- [ ] `dotnet build` exits with code 0
- [ ] Zero errors reported
- [ ] All warnings reviewed and explained
- [ ] Both projects output net10.0 assemblies

#### Compilation Error Resolution

**If Compilation Errors Occur:**

1. **Categorize Errors:**
   - Framework API changes (reference Breaking Changes Catalog)
   - Package API changes (review package release notes)
   - Nullable reference type issues (C# 10 default)
   - Syntax errors (unlikely with SDK-style project)

2. **Resolution Strategy:**
   - Consult Breaking Changes Catalog for documented fixes
   - Review official migration guides for specific APIs
   - Update code to use new APIs or patterns
   - Add nullable annotations if required

3. **Verification:**
   - Rebuild after each fix
   - Verify fix doesn't introduce new errors
   - Document changes made for future reference

**Expected Errors:** Low probability - assessment found no incompatibilities

---

### Phase 2: Test Execution Validation

#### Unit Test Execution

**Objective:** Verify all existing tests pass after upgrade.

**Baseline Capture** (if possible):
- Record test count before upgrade
- Record pass/fail status before upgrade
- Capture baseline test execution time

**Execution Steps:**

1. **Discover Tests:**
   ```bash
   dotnet test --list-tests
   ```
   - ✅ All tests discovered correctly
   - ✅ Test count matches baseline

2. **Execute All Tests:**
   ```bash
   dotnet test --logger "console;verbosity=detailed"
   ```
   - ✅ All tests execute
   - ✅ Record pass/fail/skip counts
   - ✅ Compare to baseline

3. **Analyze Results:**
   - ✅ All previously passing tests still pass
   - ⚠️ Investigate any new failures
   - ⚠️ Investigate any new skips
   - ⚠️ Review any test warnings

**Success Criteria:**
- [ ] Test discovery succeeds (all tests found)
- [ ] Test execution completes without infrastructure errors
- [ ] All tests that passed pre-upgrade still pass
- [ ] Zero unexpected test failures
- [ ] Test execution time within acceptable range (±20% of baseline)

#### Test Failure Resolution

**If Tests Fail:**

1. **Categorize Failures:**
   - **API Behavior Changes:** Framework/package behavior changed
   - **Test Framework Issues:** NUnit compatibility (unlikely)
   - **Configuration Issues:** Test setup/teardown problems
   - **Timing Issues:** Async test timing changes
   - **Assertion Issues:** Exception messages or assertion logic

2. **Resolution Strategy:**
   - Review test failure messages and stack traces
   - Check for EF Core query behavior changes
   - Verify mock setups still valid (Moq compatibility)
   - Update test expectations if behavior legitimately changed
   - Fix bugs if tests exposed real issues

3. **Verification:**
   - Rerun failed tests after fixes
   - Ensure no regressions introduced
   - Document behavior changes if applicable

**Expected Failures:** Low probability - both projects have low complexity

---

### Phase 3: Integration & Functional Validation

#### API Endpoint Testing

**Objective:** Verify API functionality works end-to-end.

**Smoke Tests** (execute these manually or via tools like Postman/curl):

1. **Application Startup:**
   ```bash
   dotnet run --project EnsekMeterReadingAPI
   ```
   - ✅ Application starts without errors
   - ✅ Listens on configured ports
   - ✅ No startup exceptions in logs
   - ✅ Swagger/OpenAPI endpoint accessible (if configured)

2. **Seed Data Endpoint** (from launchSettings.json):
   ```
   GET http://localhost:5000/api/MeterReading/seed-accounts
   ```
   - ✅ Endpoint responds
   - ✅ Returns expected HTTP status code
   - ✅ Response body valid JSON
   - ✅ Database seeded correctly

3. **Core API Endpoints** (identify critical paths):
   - Test GET endpoints (retrieve data)
   - Test POST endpoints (create data)
   - Test PUT endpoints (update data)
   - Test DELETE endpoints (delete data)
   - Verify error handling (400, 404, 500 responses)

**Success Criteria:**
- [ ] Application starts successfully
- [ ] All critical endpoints respond correctly
- [ ] Request/response serialization works (JSON)
- [ ] Status codes appropriate
- [ ] Error handling functions correctly

#### Database Operations Testing

**Objective:** Verify Entity Framework Core and SQLite work correctly.

**Tests:**

1. **Database Connection:**
   - ✅ DbContext initializes successfully
   - ✅ Connection string parsed correctly
   - ✅ SQLite database file created/accessed

2. **CRUD Operations:**
   - ✅ Create: Insert new entities
   - ✅ Read: Query entities (simple queries)
   - ✅ Update: Modify existing entities
   - ✅ Delete: Remove entities

3. **Query Patterns:**
   - ✅ LINQ queries execute correctly
   - ✅ Include/ThenInclude (eager loading) works
   - ✅ Where/Select/OrderBy patterns work
   - ✅ Null handling in queries correct

4. **Transactions:**
   - ✅ Transaction commit works
   - ✅ Transaction rollback works
   - ✅ Concurrent operations handled

**Success Criteria:**
- [ ] Database context initialization succeeds
- [ ] All CRUD operations function correctly
- [ ] No query translation errors
- [ ] No null reference exceptions in queries
- [ ] Transaction behavior as expected

#### JSON Serialization Testing

**Objective:** Verify Newtonsoft.Json works correctly.

**Tests:**

1. **Request Deserialization:**
   - ✅ JSON request bodies parse correctly
   - ✅ Model binding works for complex types
   - ✅ Date/time formats handled

2. **Response Serialization:**
   - ✅ Objects serialize to JSON correctly
   - ✅ Null handling appropriate
   - ✅ Circular reference handling (if applicable)
   - ✅ Custom converters work (if any)

**Success Criteria:**
- [ ] All API requests deserialize correctly
- [ ] All API responses serialize correctly
- [ ] No serialization exceptions
- [ ] JSON format as expected

---

### Comprehensive Validation Checklist

#### Technical Validation

- [ ] **Framework Version:** Both projects target `net10.0` (verify in .csproj files)
- [ ] **Package Versions:** All packages at target versions (verify in project files)
- [ ] **Restore:** `dotnet restore` completes successfully
- [ ] **Build:** `dotnet build` completes with 0 errors
- [ ] **Warnings:** All warnings reviewed and acceptable
- [ ] **Tests:** All unit tests pass (dotnet test)
- [ ] **Startup:** Application starts without errors
- [ ] **Endpoints:** All API endpoints respond correctly
- [ ] **Database:** All database operations work
- [ ] **Serialization:** JSON serialization/deserialization works

#### Quality Validation

- [ ] **Code Quality:** No new code quality issues introduced
- [ ] **Performance:** Application performance acceptable (no significant regression)
- [ ] **Logs:** No unexpected errors or warnings in application logs
- [ ] **Configuration:** All configuration loads correctly
- [ ] **Dependencies:** No package conflicts or downgrades

#### Regression Validation

- [ ] **Functionality:** All existing functionality works as before
- [ ] **Test Coverage:** Test coverage percentage maintained
- [ ] **Error Handling:** Error handling behavior unchanged
- [ ] **Edge Cases:** Edge case handling still works

---

### Validation Tools

**Recommended Tools:**

1. **Build & Test:**
   - `dotnet build` - compilation validation
   - `dotnet test` - unit test execution
   - `dotnet run` - application startup

2. **API Testing:**
   - Postman - manual endpoint testing
   - curl - command-line endpoint testing
   - Swagger UI - interactive API testing (if configured)

3. **Monitoring:**
   - Console logs - application output
   - Debug output - detailed execution trace
   - Test logs - test execution details

---

### Validation Failure Response

**If Any Validation Fails:**

1. **Do Not Proceed** to next phase until resolved
2. **Investigate** root cause using Breaking Changes Catalog
3. **Fix** the issue (code change, configuration, or understanding)
4. **Re-validate** from beginning of failed phase
5. **Document** the issue and resolution for future reference

**Rollback Criteria:**

Rollback the upgrade if:
- Compilation errors cannot be resolved within reasonable timeframe
- Critical functionality broken with no clear fix
- Performance degradation unacceptable
- Data integrity concerns arise

**Rollback Process:** See Risk Management section for rollback procedure.

---

## Complexity & Effort Assessment

### Overall Complexity: 🟢 LOW

### Per-Project Complexity

| Project | Complexity | LOC | Packages | Dependencies | Risk | Rationale |
|---------|-----------|-----|----------|--------------|------|-----------|
| EnsekMeterReadingAPI.csproj | 🟢 Low | 640 | 3 updates | 0 projects | Low | ASP.NET Core API, standard EF Core usage, no complex dependencies |
| EnsekMEterReadingUnitTests.csproj | 🟢 Low | 280 | 0 updates | 1 project | Low | Test project, compatible packages, depends only on API |

### Phase Complexity Assessment

**Phase 1: Atomic Upgrade**
- **Complexity:** 🟢 Low
- **Projects:** Both (EnsekMeterReadingAPI + EnsekMEterReadingUnitTests)
- **Operations:** 2 project file updates, 3 package updates, build & fix
- **Dependency Ordering:** API provides foundation, test consumes it (simple linear)

**Phase 2: Test Validation**
- **Complexity:** 🟢 Low  
- **Projects:** EnsekMEterReadingUnitTests
- **Operations:** Execute NUnit tests, address failures if any
- **Dependencies:** Requires Phase 1 completion (successful build)

### Resource Requirements

**Skill Level:**
- **Minimum:** Intermediate .NET developer
- **Recommended:** Developer familiar with ASP.NET Core and Entity Framework Core

**Tooling:**
- .NET 10.0 SDK
- Visual Studio 2022 (17.8+) or VS Code with C# extension
- NUnit test runner (IDE or CLI)

**Parallel Execution Capacity:**
Not applicable - All-At-Once strategy performs atomic update, not suitable for parallelization across projects.

### Effort Factors

**Factors Reducing Complexity:**
- ✅ Small codebase (< 1,000 LOC)
- ✅ Modern starting framework (.NET Core 3.1)
- ✅ SDK-style projects (easier to modify)
- ✅ No legacy dependencies
- ✅ No security vulnerabilities requiring immediate attention
- ✅ Clear package upgrade paths
- ✅ No circular dependencies
- ✅ Existing test coverage

**Factors Requiring Attention:**
- ⚠️ Entity Framework Core major version jump (5.0 → 10.0) - review breaking changes
- ⚠️ ASP.NET Core version jump (3.1 → 10.0) - verify configuration patterns
- ⚠️ Test framework compatibility - verify NUnit works with .NET 10.0

### Relative Effort Scale

**Phase 1: Atomic Upgrade** - 🟢 Low Effort
- Project file updates: Trivial (2 simple changes)
- Package updates: Low (3 packages, clear versions)
- Build & compilation fixes: Low to Medium (depends on breaking changes)

**Phase 2: Test Validation** - 🟢 Low Effort  
- Test execution: Trivial (automated)
- Test fixes: Low (depends on behavior changes)

---

## Source Control Strategy

### Overview

**No Git Repository Detected** - Source control recommendations provided for reference if repository is initialized later.

If using source control, follow these guidelines to maintain clean history and enable easy rollback.

---

### Branching Strategy

#### Recommended Branch Structure

**Main Branch:** `main` or `master` (production-ready code)

**Upgrade Branch:** `upgrade/dotnet-10` or `feature/net10-upgrade`

**Workflow:**
```
main
  └─→ upgrade/dotnet-10 (create branch)
        ├─→ make changes
        ├─→ commit changes
        ├─→ test thoroughly
        └─→ merge back to main (after validation)
```

#### Branch Creation

```bash
# Create upgrade branch from main
git checkout main
git pull origin main
git checkout -b upgrade/dotnet-10
```

---

### Commit Strategy

#### Single Commit Approach (Recommended)

**Rationale:**
- Atomic upgrade semantics
- Simple rollback (single revert)
- Clear "before/after" boundary
- Matches All-At-Once strategy philosophy

**Commit Structure:**

```bash
# Stage all changes
git add .

# Commit with descriptive message
git commit -m "Upgrade solution to .NET 10.0

- Update EnsekMeterReadingAPI.csproj to net10.0
- Update EnsekMEterReadingUnitTests.csproj to net10.0
- Upgrade Entity Framework Core 5.0.2 → 10.0.3
- Upgrade EF Core SQLite 5.0.2 → 10.0.3
- Upgrade Newtonsoft.Json 13.0.1 → 13.0.4
- Fix compilation errors (if any)
- All tests passing

Closes #<issue-number> (if applicable)"
```

#### Multi-Commit Approach (Alternative)

If you prefer incremental commits for traceability:

**Commit 1: Project File Updates**
```bash
git add *.csproj
git commit -m "Update target framework to net10.0"
```

**Commit 2: Package Updates**
```bash
git add *.csproj
git commit -m "Upgrade packages for .NET 10.0 compatibility"
```

**Commit 3: Code Fixes**
```bash
git add .
git commit -m "Fix compilation errors from framework upgrade"
```

**Commit 4: Test Fixes** (if needed)
```bash
git add EnsekMEterReadingUnitTests/
git commit -m "Update tests for .NET 10.0 compatibility"
```

⚠️ **Caution:** Multi-commit approach creates intermediate non-working states. Avoid pushing these to shared branches.

---

### Commit Message Guidelines

**Format:**
```
<type>: <short summary>

<detailed description>

<references and notes>
```

**Example:**
```
feat: Upgrade solution to .NET 10.0 LTS

Migrated both projects from .NET Core 3.1 to .NET 10.0 using 
All-At-Once strategy. All package dependencies updated to 
compatible versions. Entity Framework Core upgraded from 5.0 
to 10.0 with breaking changes reviewed and addressed.

Changes:
- EnsekMeterReadingAPI.csproj: netcoreapp3.1 → net10.0
- EnsekMEterReadingUnitTests.csproj: netcoreapp3.1 → net10.0
- Microsoft.EntityFrameworkCore: 5.0.2 → 10.0.3
- Microsoft.EntityFrameworkCore.Sqlite: 5.0.2 → 10.0.3
- Newtonsoft.Json: 13.0.1 → 13.0.4

Testing:
- All unit tests passing (baseline maintained)
- API endpoints verified functional
- Database operations tested successfully

Refs: Assessment report .github/upgrades/scenarios/new-dotnet-version_09f25c/assessment.md
Refs: Upgrade plan .github/upgrades/scenarios/new-dotnet-version_09f25c/plan.md
```

---

### Review and Merge Process

#### Pre-Merge Checklist

Before merging upgrade branch to main:

**Technical Validation:**
- [ ] Solution builds without errors
- [ ] All tests pass
- [ ] Application starts successfully
- [ ] Core functionality validated
- [ ] No new warnings introduced (or documented)

**Code Quality:**
- [ ] Code review completed (if team process)
- [ ] Breaking changes documented
- [ ] README updated (if framework version listed)
- [ ] CI/CD pipeline passes (if configured)

**Documentation:**
- [ ] Upgrade notes documented (if needed)
- [ ] Known issues documented (if any)
- [ ] Migration guide created (if needed for deployments)

#### Merge Options

**Option 1: Merge Commit (Recommended)**
```bash
git checkout main
git merge --no-ff upgrade/dotnet-10 -m "Merge .NET 10.0 upgrade"
git push origin main
```
Preserves complete history of upgrade branch.

**Option 2: Squash Merge**
```bash
git checkout main
git merge --squash upgrade/dotnet-10
git commit -m "Upgrade solution to .NET 10.0"
git push origin main
```
Collapses all upgrade commits into single commit on main.

**Option 3: Rebase** (if branch not shared)
```bash
git checkout upgrade/dotnet-10
git rebase main
git checkout main
git merge upgrade/dotnet-10
git push origin main
```
Creates linear history without merge commit.

---

### Tagging Strategy

#### Create Release Tag

After successful merge and validation:

```bash
# Tag the upgrade commit
git tag -a v1.0.0-net10 -m "Release: .NET 10.0 upgrade complete"
git push origin v1.0.0-net10
```

**Tag Naming Conventions:**
- `v1.0.0-net10` - Version + framework identifier
- `net10-upgrade` - Framework upgrade marker
- `release/2024-01-15` - Date-based release

---

### Rollback Procedures

#### If Committed But Not Pushed

```bash
# Undo last commit, keep changes in working directory
git reset --soft HEAD~1

# Or: Undo last commit, discard changes
git reset --hard HEAD~1
```

#### If Pushed to Branch (Not Main)

```bash
# Force push reverted state
git reset --hard <commit-before-upgrade>
git push --force origin upgrade/dotnet-10
```

#### If Merged to Main

```bash
# Revert the merge commit
git revert -m 1 <merge-commit-hash>
git push origin main

# Or: Create hotfix branch with revert
git checkout -b hotfix/revert-net10-upgrade main
git revert -m 1 <merge-commit-hash>
git push origin hotfix/revert-net10-upgrade
# Then merge hotfix to main via PR
```

---

### Collaboration Guidelines

#### If Multiple Developers

**Communication:**
- Notify team before starting upgrade
- Coordinate to avoid conflicts in upgrade branch
- Schedule upgrade during low-activity period

**Coordination:**
- Single person performs upgrade (avoid conflicts)
- Other developers pause work on affected projects
- Code freeze during upgrade and validation

**Post-Upgrade:**
- Notify team when merged to main
- All developers pull latest main
- Verify everyone can build and run

#### Branch Protection

If using branch protection rules:

- Ensure upgrade branch can be created
- Verify CI/CD pipeline configured for upgrade branch
- Plan for merge approval process
- Consider temporarily relaxing rules if needed

---

### Continuous Integration Considerations

#### CI/CD Pipeline

**Expected CI Impacts:**

1. **Build Step:** May need .NET 10.0 SDK in CI environment
2. **Test Step:** Test execution should pass
3. **Deployment Step:** Target environment needs .NET 10.0 runtime

**Pre-Merge CI Validation:**
- [ ] CI pipeline updated with .NET 10.0 SDK
- [ ] Build succeeds in CI environment
- [ ] Tests pass in CI environment
- [ ] Deployment scripts updated (if needed)

**Post-Merge Actions:**
- Update deployment targets with .NET 10.0 runtime
- Verify production environment compatibility
- Update documentation for deployment process

---

### Git Ignore Considerations

**Verify .gitignore Includes:**

```gitignore
# Build results
[Bb]in/
[Oo]bj/

# NuGet packages
*.nupkg
**/packages/*
!**/packages/build/

# Visual Studio cache/options
.vs/
*.user
*.suo

# Rider
.idea/

# User-specific files
*.rsuser
*.userosscache
*.sln.docstates
```

Ensure no binary or package files committed during upgrade.

---

### Source Control Summary

**Recommended Workflow for This Upgrade:**

1. **Branch:** Create `upgrade/dotnet-10` from main
2. **Commit:** Single atomic commit with all changes
3. **Validate:** Complete all testing before push
4. **Review:** Code review if team process
5. **Merge:** Merge to main with `--no-ff`
6. **Tag:** Create release tag `v1.0.0-net10`
7. **Cleanup:** Delete upgrade branch after successful merge

**Simple and Clean:** Matches All-At-Once strategy philosophy.

---

## Success Criteria

### Technical Criteria

The upgrade is considered technically successful when all of the following conditions are met:

#### Framework Migration
- [x] **Target Framework:** Both projects target `net10.0` (verify in `.csproj` files)
  - `EnsekMeterReadingAPI.csproj` contains `<TargetFramework>net10.0</TargetFramework>`
  - `EnsekMEterReadingUnitTests.csproj` contains `<TargetFramework>net10.0</TargetFramework>`

#### Package Updates
- [x] **All Required Packages Updated:**
  - `Microsoft.EntityFrameworkCore`: 5.0.2 → 10.0.3 ✓
  - `Microsoft.EntityFrameworkCore.Sqlite`: 5.0.2 → 10.0.3 ✓
  - `Newtonsoft.Json`: 13.0.1 → 13.0.4 ✓

- [x] **Compatible Packages Verified:**
  - `Microsoft.NET.Test.Sdk` 16.5.0 compatible ✓
  - `Moq` 4.17.2 compatible ✓
  - `NUnit` 3.12.0 compatible ✓
  - `NUnit3TestAdapter` 3.16.1 compatible ✓

#### Build Success
- [x] **Dependency Restoration:**
  - `dotnet restore` completes successfully with exit code 0
  - No package conflict warnings
  - No package downgrade warnings

- [x] **Compilation:**
  - `dotnet build` completes successfully with exit code 0
  - Zero compilation errors
  - All warnings reviewed and documented (or resolved)
  - Both projects produce `net10.0` assemblies

#### Test Success
- [x] **Test Execution:**
  - `dotnet test` completes successfully
  - All tests discovered correctly (test count matches baseline)
  - All previously passing tests still pass
  - Zero unexpected test failures
  - Test execution time within acceptable range

- [x] **Test Coverage:**
  - Test coverage percentage maintained (or improved)
  - No reduction in tested functionality

#### Application Validation
- [x] **Startup:**
  - Application starts without errors (`dotnet run`)
  - Listens on configured ports
  - No startup exceptions in logs
  - Configuration loads correctly

- [x] **Endpoints:**
  - All API endpoints respond correctly
  - Request/response serialization works (JSON)
  - Appropriate HTTP status codes returned
  - Error handling functions correctly

- [x] **Database:**
  - Database context initializes successfully
  - All CRUD operations work correctly
  - LINQ queries execute without errors
  - No null reference exceptions in queries
  - Transaction behavior as expected

#### Security & Quality
- [x] **No Security Vulnerabilities:**
  - No packages with known vulnerabilities
  - All packages at secure versions

- [x] **No Regressions:**
  - All existing functionality works as before
  - No performance degradation (within acceptable range)
  - No new errors or warnings in application logs

---

### Quality Criteria

#### Code Quality
- [x] **Maintainability:** Code remains clean and maintainable
- [x] **Readability:** No obfuscated or unclear changes introduced
- [x] **Standards:** Code adheres to team coding standards
- [x] **Documentation:** Significant changes documented in code comments

#### Process Quality
- [x] **Testing:** Comprehensive testing performed at all levels
- [x] **Validation:** All validation checklists completed
- [x] **Documentation:** Upgrade process documented for future reference
- [x] **Knowledge Transfer:** Team aware of changes and impacts

---

### Process Criteria

#### Strategy Adherence
- [x] **All-At-Once Strategy Followed:**
  - Both projects upgraded simultaneously
  - All package updates applied together
  - Single atomic operation completed
  - No intermediate multi-targeting states

#### Dependency Ordering
- [x] **Dependency Constraints Respected:**
  - API project (foundation) updated first logically
  - Test project (consumer) updated in same operation
  - No dependency conflicts introduced

#### Source Control
- [x] **Clean History:**
  - Changes committed with clear, descriptive messages
  - Atomic commit contains all upgrade changes
  - Branch merged successfully (if using source control)
  - Tag created for upgrade milestone (if applicable)

#### Risk Management
- [x] **Risks Mitigated:**
  - All identified risks addressed
  - Contingency plans available if needed
  - Rollback procedure documented and understood

---

### All-At-Once Strategy Compliance

The All-At-Once strategy success criteria:

#### Simultaneity
- [x] **Atomic Update:** All projects upgraded in single coordinated operation
- [x] **No Phasing:** No incremental or phased approach used
- [x] **Unified State:** Solution remains in consistent state throughout

#### Completeness
- [x] **All Projects:** Both projects included in single upgrade operation
- [x] **All Packages:** All required package updates applied together
- [x] **All Validation:** Complete solution validated as single unit

#### Efficiency
- [x] **Single Pass:** Upgrade completed in one execution
- [x] **No Rework:** No need to revisit previously upgraded projects
- [x] **Clean Execution:** No partial states or intermediate checkpoints

---

### Rollback Criteria

The upgrade should be rolled back if any of the following occur:

#### Technical Failures
- ❌ Compilation errors cannot be resolved within reasonable effort
- ❌ Critical functionality broken with no clear fix
- ❌ Data integrity concerns arise
- ❌ Security vulnerabilities introduced

#### Quality Failures
- ❌ Unacceptable performance degradation (>20% regression)
- ❌ Test coverage significantly reduced
- ❌ Critical test failures cannot be resolved

#### Business Failures
- ❌ Deployment timeline at risk
- ❌ Team cannot support upgraded version
- ❌ External dependencies block upgrade completion

**Rollback Process:** Follow procedures in Risk Management section

---

### Definition of Done

**The .NET 10.0 upgrade is DONE when:**

✅ **All Technical Criteria Met** - Framework, packages, build, tests, application validation complete

✅ **All Quality Criteria Met** - Code quality maintained, process followed correctly

✅ **All Process Criteria Met** - Strategy adhered to, dependencies respected, source control clean

✅ **Documentation Complete** - Changes documented, team informed, knowledge transferred

✅ **Stakeholder Approval** - Team lead or project owner approves upgrade completion

✅ **Deployment Ready** - Solution ready for deployment to target environments

**At this point:**
- Solution is on .NET 10.0 LTS
- All functionality verified working
- Tests provide confidence in changes
- Team understands what changed
- Deployment can proceed safely

---

### Success Metrics

#### Quantitative Metrics

| Metric | Target | Actual | Status |
|--------|--------|--------|--------|
| Projects Upgraded | 2/2 (100%) | ___ | ⏳ |
| Packages Updated | 3/3 (100%) | ___ | ⏳ |
| Build Errors | 0 | ___ | ⏳ |
| Test Failures | 0 unexpected | ___ | ⏳ |
| Test Coverage | ≥ baseline | ___ | ⏳ |
| Performance | ±20% baseline | ___ | ⏳ |
| Warnings | All reviewed | ___ | ⏳ |

#### Qualitative Metrics

- **Team Confidence:** High - team comfortable with upgraded solution
- **Code Quality:** Maintained or improved - no technical debt introduced
- **Maintainability:** Maintained - solution remains easy to understand and modify
- **Documentation:** Complete - future developers can understand changes

---

### Post-Upgrade Checklist

After declaring success, complete these follow-up items:

- [ ] **Update Documentation:**
  - README.md reflects .NET 10.0 requirement
  - Developer setup guide updated
  - Deployment guide updated (if applicable)

- [ ] **Communicate Success:**
  - Notify team of completed upgrade
  - Share any lessons learned
  - Document any gotchas discovered

- [ ] **Update CI/CD:**
  - CI environment configured with .NET 10.0 SDK
  - Deployment targets have .NET 10.0 runtime
  - Pipeline validated in new configuration

- [ ] **Archive Artifacts:**
  - Assessment report archived
  - Upgrade plan archived
  - Execution log archived (if created)

- [ ] **Plan Improvements:**
  - Consider optional package updates (test frameworks)
  - Plan adoption of new .NET 10.0 features
  - Schedule review of deprecated patterns

---

### Success Declaration

**Once all criteria met, formally declare:**

> ✅ **UPGRADE SUCCESS**
> 
> The EnsekMeterReadingAPI solution has been successfully upgraded from .NET Core 3.1 to .NET 10.0 LTS using the All-At-Once strategy. All projects are building, all tests are passing, and the application is functioning correctly on the new framework.
>
> **Date Completed:** [Date]  
> **Completed By:** [Name/Team]  
> **Next Steps:** Deploy to target environments with .NET 10.0 runtime

**Celebration:** 🎉 Well done! The solution is now on the latest LTS framework with 5+ years of support!
