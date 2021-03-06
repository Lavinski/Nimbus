﻿using System;
using System.Threading.Tasks;
using Nimbus.Configuration;
using Nimbus.MessageContracts.Exceptions;
using Nimbus.Tests.Common;
using NUnit.Framework;

namespace Nimbus.IntegrationTests.Tests.BusBuilderTests
{
    [TestFixture]
    public class WhenStartingABusWithAnEndpointThatDoesNotExist
    {
        [Test]
        [Timeout(15*1000)]
        [ExpectedException(typeof (BusException))]
        public async Task ItShouldGoBangQuickly()
        {
            var typeProvider = new TestHarnessTypeProvider(new[] {GetType().Assembly}, new[] {GetType().Namespace});

            var logger = TestHarnessLoggerFactory.Create();

            var bus = new BusBuilder().Configure()
                                      .WithNames("IntegrationTestHarness", Environment.MachineName)
                                      .WithConnectionString(@"Endpoint=sb://shouldnotexist.example.com/;SharedAccessKeyName=IntegrationTestHarness;SharedAccessKey=borkborkbork=")
                                      .WithTypesFrom(typeProvider)
                                      .WithDefaultTimeout(TimeSpan.FromSeconds(10))
                                      .WithLogger(logger)
                                      .Build();

            await bus.Start();
        }
    }
}