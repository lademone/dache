﻿using System;
using Dache.CacheHost.Communication;
using Dache.CacheHost.Configuration;
using Dache.CacheHost.Routing;
using Dache.CacheHost.Storage;
using Dache.Core.Communication;
using Dache.Core.Logging;
using Dache.Core.Performance;

namespace Dache.CacheHost
{
    /// <summary>
    /// The cache host engine. Instantiate this to host an instance of the cache host within your own process.
    /// </summary>
    public class CacheHostEngine
    {
        // The cache host runner
        private readonly IRunnable _cacheHostRunner = null;

        /// <summary>
        /// The constructor that derives configuration from file.
        /// </summary>
        public CacheHostEngine() : this(CacheHostConfigurationSection.Settings)
        {

        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="configuration">The configuration to use for the cache host.</param>
        public CacheHostEngine(CacheHostConfigurationSection configuration)
        {
            // Sanitize
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            // Set default logger to file if necessary
            if (CustomLoggerLoader.DefaultLogger == null)
            {
                CustomLoggerLoader.DefaultLogger = new FileLogger();
            }

            var port = configuration.Port;
            var physicalMemoryLimitPercentage = configuration.CacheMemoryLimitPercentage;
            var maximumConnections = configuration.MaximumConnections;

            // Configure the performance counter data manager
            PerformanceDataManager performanceDataManager = null;
            try
            {
                performanceDataManager = new PerformanceCounterPerformanceDataManager(port);
            }
            catch (InvalidOperationException)
            {
                // Performance counters aren't installed, so don't use them
                performanceDataManager = new PerformanceDataManager();
            }

            // Determine the MemCache to use
            IMemCache memCache = new MemCache(physicalMemoryLimitPercentage, performanceDataManager);

            if (configuration.CompressData)
            {
                memCache = new GZipMemCache(memCache);
            }

            // Initialize the tag routing table
            var tagRoutingTable = new TagRoutingTable();

            CacheHostServer cacheHostServer;

            if (configuration.DirectHost)
            {
                // Initialize the cache host server
                //var hostInstance = new DirectCacheHostServer(memCache, tagRoutingTable, CustomLoggerLoader.LoadLogger()); ;
                //DirectHost.Instance = hostInstance;

                cacheHostServer = new CacheHostServer(memCache, CustomLoggerLoader.LoadLogger());
                DirectHost.Instance = cacheHostServer;
            }
            else
            {
                // Initialize the cache host server
                cacheHostServer = new CacheHostServer(memCache, tagRoutingTable, CustomLoggerLoader.LoadLogger(), configuration.Port,
                    configuration.MaximumConnections, configuration.MessageBufferSize, configuration.CommunicationTimeoutSeconds * 1000, configuration.MaximumMessageSizeKB * 1024);
            }

            // Instantiate the cache host runner
            _cacheHostRunner = new CacheHostRunner(cacheHostServer);
        }

        /// <summary>
        /// Starts the cache host.
        /// </summary>
        public void Start()
        {
            _cacheHostRunner.Start();
        }

        /// <summary>
        /// Stops the cache host.
        /// </summary>
        public void Stop()
        {
            _cacheHostRunner.Stop();
        }
    }
}
