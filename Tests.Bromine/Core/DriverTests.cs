﻿using System;

using Bromine.Core;

using Tests.Bromine.Common;

using OpenQA.Selenium;

using Xunit;

using DriverOptions = Bromine.Models.DriverOptions;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Test the behavior of the Driver class.
    /// </summary>
    public class DriverTests: IDisposable
    {
        /// <summary>
        /// Test default Driver constructor.
        /// </summary>
        /// <param name="browser">Browser to launch.</param>
        [Theory]
        [InlineData(BrowserType.Chrome), Trait(Category.Browser, Category.Chrome)]
        [InlineData(BrowserType.Edge), Trait(Category.Browser, Category.Edge)]
        [InlineData(BrowserType.Firefox), Trait(Category.Browser, Category.Firefox)]
        public void InitializeBrowserDefaultsTest(BrowserType browser)
        {
            var driverOptions = new DriverOptions(browser);

            BrowserInit(driverOptions);
        }

        /// <summary>
        /// Test Driver constructor with headless mode.
        /// </summary>
        /// <param name="browser">Browser to launch.</param>
        [Theory]
        [InlineData(BrowserType.Chrome), Trait(Category.Browser, Category.Chrome)]
        [InlineData(BrowserType.Firefox), Trait(Category.Browser, Category.Firefox)]
        public void InitializeBrowserIsHeadlessTest(BrowserType browser)
        {
            var driverOptions = new DriverOptions(browser, true);

            BrowserInit(driverOptions);
        }

        /// <summary>
        /// Dispose of the Driver resource.
        /// </summary>
        public void Dispose()
        {
            Driver?.Dispose();
        }

        private void BrowserInit(DriverOptions driverOptions)
        {
            try
            {
                Driver = new Driver(driverOptions);

                Driver.NavigateToUrl(GoogleUrl);

                Assert.Equal(GoogleUrl, Driver.Url);
            }
            catch (WebDriverException e)
            {
                // The driver is not loaded on the computer.
                if (e.Message.Contains("Cannot start the driver service on"))
                {

                }
                else
                {
                    throw;
                }
            }
            catch (InvalidOperationException e)
            {
                // The driver is not loaded on the computer.
                if (e.Message.Contains("Expected browser binary location, but unable to find binary in default location"))
                {

                }
                else
                {
                    throw;
                }
            }
        }

        private const string GoogleUrl = "https://www.google.com/?gws_rd=ssl";
        private Driver Driver { get; set; }
    }
}
