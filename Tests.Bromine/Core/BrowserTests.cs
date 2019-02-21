﻿using System;

using Bromine.Core;

using Xunit;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Test Browser behavior.
    /// </summary>
    public class BrowserTests: IDisposable
    {
        /// <summary>
        /// Construct a Browser and test Browser behavior.
        /// </summary>
        public BrowserTests()
        {
            Browser = new Browser(BrowserType.Chrome);
        }

        /// <summary>
        /// Verify navigation to a URL works.
        /// </summary>
        [Fact]
        public void VerifyNavigate()
        {
            Browser.NavigateToUrl(AmazonUrl);

            Assert.Equal(AmazonUrl, Browser.Url);
        }

        /// <summary>
        /// Verify page source and title properties return the expected values.
        /// </summary>
        [Fact]
        public void VerifySourceAndTitle()
        {
            Browser.NavigateToUrl(AmazonUrl);

            Assert.Contains(Amazon, Browser.Source);
            Assert.Contains(Amazon, Browser.Title);
        }

        /// <summary>
        /// Dispose of the Browser resource.
        /// </summary>
        public void Dispose()
        {
            Browser.Dispose();
        }

        private const string Amazon = "Amazon";
        private const string AmazonUrl = "https://www.amazon.com/";
        private Browser Browser { get; }
    }
}
