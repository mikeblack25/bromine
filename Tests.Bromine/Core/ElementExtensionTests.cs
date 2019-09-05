﻿using System.Collections.Generic;

using Bromine.Core.ElementInteraction;
using Bromine.Core.ElementLocator;

using Tests.Bromine.Common;

using Xunit;
using static Xunit.Assert;

namespace Tests.Bromine.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Tests to verify <see cref="ElementExtensions"/> in the <see cref="Element"/> class is working as expected.
    /// </summary>
    public class ElementExtensionTests : CoreTestsBase
    {
        /// <inheritdoc />
        /// <summary>
        /// Navigate to Amazon and find an element by class.
        /// </summary>
        public ElementExtensionTests()
        {
            Browser.Navigate.ToUrl(TestSites.AmazonUrl);
            _element = Browser.Find.Element(".nav-fill");
        }

        /// <summary>
        /// Locate div elements of the element found in the test setup.
        /// </summary>
        [Fact]
        public void VerifyFindElementsOfElement()
        {
            _elementsOfElement = _element.FindElements(LocatorStrategy.Tag, "div");
        }

        /// <summary>
        /// Locate a div element of the element found in the test setup.
        /// </summary>
        [Fact]
        public void VerifyFindElementOfElement()
        {
            _elementsOfElement = new List<Element> {_element.FindElement(LocatorStrategy.Tag, "div")};
        }

        /// <summary>
        /// Verify one or more child elements are found from the element found in the test setup.
        /// </summary>
        public override void Dispose()
        {
            try
            {
                InRange(_elementsOfElement.Count, 1, int.MaxValue);
            }
            finally
            {
                base.Dispose();
            }
        }

        private readonly Element _element;
        private List<Element> _elementsOfElement;
    }
}