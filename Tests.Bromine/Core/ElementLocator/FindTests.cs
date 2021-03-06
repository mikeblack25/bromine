﻿using System.Collections.Generic;
using Tests.Bromine.Common;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Bromine.Core.ElementLocator
{
    /// <inheritdoc />
    /// <summary>
    /// Test to verify the Find class is working as expected.
    /// </summary>
    public class FindTests : CoreTestsBase
    {
        /// <inheritdoc />
        public FindTests(ITestOutputHelper output) : base(output)
        {
            Browser.Navigate.ToUrl(TestSites.GoogleUrl);
        }

        /// <summary>
        /// Find element by all supported element location strategies.
        /// </summary>
        [Fact]
        public void FindElement()
        {
            var list = new List<string> { ".gNO89b", "gbqfbb", "gNO89b", "Gmail", "Gmai", "input" };

            foreach (var locator in list)
            {
                var element = Browser.Find.Element(locator);

                Browser.SoftVerify.True(element.IsInitialized, locator);
            }
        }

        /// <summary>
        /// Find element with all the following classes.
        /// gb_Oa gb_Fg gb_g gb_Eg gb_Jg gb_Wf
        /// </summary>
        [Fact]
        public void FindElementByClassesTest()
        {
            var classes = "gb_Oa gb_Fg gb_g gb_Eg gb_Jg";

            Browser.Wait.For.DisplayedElement(Browser.Find.ElementByClasses(classes), 5);

            Browser.Verify.True(Browser.Find.ElementByClasses(classes).Displayed);
        }

        /// <summary>
        /// Find the elements with all the following classes.
        /// "gb_f gb_g"
        /// </summary>
        [Fact]
        public void FindElementsByClassesTest()
        {
            var elements = Browser.Find.ElementsByClasses("gb_f gb_g");

            Browser.Verify.Equal(2, elements.Count);
        }

        /// <summary>
        /// Find the child element of an element. Both the parent and child elements are located by CSS selector.
        /// </summary>
        [Fact]
        public void FindChildElementTest()
        {
            var element = Browser.Find.ChildElement(ParentClassString, InputTagString);

            Browser.Verify.True(element.Displayed);
        }

        /// <summary>
        /// Find the child element of an element. The parent element is passed as an element, and the child element is located by CSS selector.
        /// </summary>
        [Fact]
        public void FindChildElementByElementTest()
        {
            var ele = Browser.Find.Element(ParentClassString);
            var element = Browser.Find.ChildElement(ele, InputTagString);

            Browser.Verify.True(element.Displayed);
        }

        /// <summary>
        /// Find the child elements of an element. Both the parent and child elements are located by CSS selector.
        /// </summary>
        [Fact]
        public void FindChildElementsTest()
        {
            var elements = Browser.Find.ChildElements(ParentClassString, InputTagString);

            Browser.Verify.Equal(2, elements.Count);
        }

        /// <summary>
        /// Find the child elements of an element. The parent element is passed as an element, and the child element is located by CSS selector.
        /// </summary>
        [Fact]
        public void FindChildElementsByElementTest()
        {
            var ele = Browser.Find.Element(ParentClassString);
            var elements = Browser.Find.ChildElements(ele, InputTagString);

            Browser.Verify.Equal(2, elements.Count);
        }

        /// <summary>
        /// Find element in the DOM by descendent CSS selection. Each element is separated by a space and is a CSS selector.
        /// The first element is the parent.
        /// If additional selectors are added they are expected under the previous element in the DOM structure.
        /// Id -> gbw
        ///   class -> gb_fe
        ///     tag -> div
        ///        attribute -> data-pid=23
        /// </summary>
        [Fact]
        public void FindElementByDescendentCssTest()
        {
            const string gmailString = "Gmail";

            var element = Browser.Find.ElementByDescendentCss("#gbw .gb_fe div [data-pid='23']");

            Browser.Verify.Equal(gmailString, element.Text);
        }

        /// <summary>
        /// Find elements in the DOM by descendent CSS selection. Each element is separated by a space and is a CSS selector.
        /// The first element is the parent.
        /// If additional selectors are added they are expected under the previous element in the DOM structure.
        /// id -> gbw
        ///   class -> gb_fe
        ///     tag -> div
        /// </summary>
        [Fact]
        public void FindElementsByDescendentCssTest()
        {
            var elements = Browser.Find.ElementsByDescendentCss("#gbw .gb_fe div");

            Browser.Verify.Equal(2, elements.Count);
        }

        /// <summary>
        /// Dispose of the browser when the test is done.
        /// All tests will ensure there are no errors finding elements.
        /// </summary>
        public override void Dispose()
        {
            Browser.Verify.Equal(0, Browser.LogManager.XunitConsoleLog.ErrorCount);

            Browser?.Dispose();
        }

        private static string ParentClassString => ".FPdoLc";
        private static string InputTagString => "input";
    }
}
