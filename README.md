Features of this fork
---------------

### New Features
## Direct host communication
When use Dache in the same process with not external communication (client), set the DirectHost to TRUE for a major boost of performance (52% to 79%). This setting drop the socket communication layer and use directly the host engine.

## PowerJSON
It's just a replacement of Newtonsoft JSON for [PowerJSON](http://github.com/wmjordan/PowerJSON)

# DACHE

Distributed caching for .NET applications 

Fast, scalable distributed caching with meaningful performance metrics for your managers and a simple API for your development team

**WEB:**   http://www.dache.io

**EMAIL:** [info@dache.io](mailto:info@dache.io)

**NUGET:** [Dache.Client](http://www.nuget.org/packages/Dache.Client) and [Dache.CacheHost](http://www.nuget.org/packages/Dache.CacheHost)

**DOWNLOAD:** http://www.dache.io/download

**WIKI:** http://www.github.com/ironyx/dache/wiki

# VERSION INFORMATION

## 1.5.9

- Added ProtoBufSerializer and JsonSerializer
- Added DebugLogger
- Added some unit tests
- Fixed a bug in DacheSessionStateProvider where a CacheClient was created per pooled instance.

# INSTALLATION INSTRUCTIONS

Getting started quickly involves standing up the Dache Client and a Dache Host for the client to communicate with.

## Client

The Dache Client is a single DLL which you reference in any application which you would like to use Dache with. You have 2 options for installing the Dache Client:

- Install the [Dache.Client NuGet package](http://www.nuget.org/packages/Dache.Client)
- Reference `Dache.Client.dll` manually from the [latest release download](http://www.dache.io/download)

### NuGet Client

Install the Dache Client via [NuGet](http://www.nuget.org/packages/Dache.CacheHost). Your `web.config` or `app.config` will be automatically modified to include the default Dache client configuration:

```xml
<configuration>
  <configSections>
    <section name="cacheClientSettings"
      type="Dache.Client.Configuration.CacheClientConfigurationSection, Dache.Client"/>
  </configSections>
  <cacheClientSettings>
    <cacheHosts>
      <add address="localhost" port="33333" />
    </cacheHosts>
  </cacheClientSettings>
</configuration>
```

Next, instantiate the `CacheClient`:

```csharp
// Using the settings from app.config or web.config
var cacheClient = new Dache.Client.CacheClient();
```

or

```csharp
// Using programmatically created settings
var settings = new CacheClientConfigurationSettings { ... };
var cacheClient = new Dache.Client.CacheClient(settings);
```

A file called `CacheProvider.cs` will also be installed at the root of your project. It is a working example of using the `CacheClient` and is intended for experimentation and getting a quick-start with Dache. You can build on top of this implementation or discard it completely. The purpose of it is to show you how to use the Dache client in your code.

### Manual DLL Reference

To install and the Dache Client manually, first download the binaries from http://www.dache.io/download and then copy the files located in the `Client` folder to your solution's folder structure. Then, add a reference to `Dache.Client.dll` to your project.

Next, instantiate the `CacheClient` as demonstrated above. You'll also need to include the configuration above in your `app.config` or `web.config` file.

### Client Notes And Next Steps

`CacheClient` is intended to be used as a singleton. **Do not create a new `CacheClient` per request.**

**IMPORTANT:** all clients should be configured with the same list of servers. The list of servers does not have to be in the same order, but each client's list should contain the same servers.

To learn more about using Dache, check out the [wiki](https://github.com/ironyx/dache/wiki).

## Host

The host is the actual process that does the caching work. You have 3 options for hosting Dache:

- Run the **quick and easy console host** provided in the [latest release download](http://www.dache.io/download)
- Install the **Windows service** provided in the [latest release download](http://www.dache.io/download)
- Host Dache **in your own process** by installing the [Dache.CacheHost NuGet package](http://www.nuget.org/packages/Dache.CacheHost)

### Quick And Easy Console Host

To use the console host, first download the [latest release](http://www.dache.io/download) and then run (or double click) `CacheHost/Dache.CacheHost.exe`. A console will open that verifies the Dache settings and then gives you information about Dache as it is used.

### Windows Service

To install and use the provided Windows service, first download the binaries from http://www.dache.io/download and then run `CacheHost/install.bat`. You will be offered custom installation settings, including the ability to rename the service if you want to install multiple Dache hosts on a single server under unique names.

After successful installation, you can run the service from Windows Services.

To uninstall Dache, run `CacheHost/uninstall.bat`.

### Host In Your Own Process

To host Dache in your own process, install the Dache Host via [NuGet](http://www.nuget.org/packages/Dache.CacheHost). Your `web.config` or `app.config` will be automatically modified to include the default Dache host configuration:

```xml
<configuration>
  <configSections>
    <section name="cacheHostSettings"
      type="Dache.CacheHost.Configuration.CacheHostConfigurationSection, Dache.CacheHost"
      allowExeDefinition="MachineToApplication" />
  </configSections>
  <cacheHostSettings port="33333" />
</configuration>
```

Next, instantiate the `CacheHostEngine`:

```csharp
// Using the settings from app.config or web.config
var cacheHost = new Dache.CacheHost.CacheHostEngine();
```

or

```csharp
// Using programmatically created settings
var settings = new CacheHostConfigurationSettings { ... };
var cacheHost = new Dache.CacheHost.CacheHostEngine(settings);
```

### Host Notes And Next Steps

To learn more about using Dache, check out the [wiki](https://github.com/ironyx/dache/wiki).

# LICENSE INFORMATION

Dache software is dual licensed. You must choose which license you 
would like to use Dache under from the following 2 options:

The **GNU General Public License Version 3** available for review 
at http://www.gnu.org/copyleft/gpl.html

-or-

The **Commercial Dache License**, which must be purchased directly 
from Imperative Bytes, LLC - the Limited Liability Company which 
owns the Dache source code. You may purchase the Commercial Dache 
License by contacting us at [info@dache.io](mailto:info@dache.io).

Please see `LICENSE.txt` for more information.

# IMPORTANT NOTE TO SOURCE CODE CONTRIBUTORS

In order to clarify the intellectual property license granted with Contributions from any person or entity, Imperative Bytes, LLC. 
("Imperative Bytes") must have a Contributor License Agreement ("CLA") on file that has been signed by each Contributor, indicating 
agreement to the license terms of the **Dache Individual Contributor License Agreement** (located in `INDIVIDUAL.txt`). This license 
is for your protection as a Contributor as well as the protection of Imperative Bytes; it does not change your rights to use your own 
Contributions for any other purpose. If you have not already done so, please complete, scan, and e-mail an original signed Agreement 
to [info@dache.io](mailto:info@dache.io).
