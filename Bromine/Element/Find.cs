﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bromine.Core;

namespace Bromine.Element
{
    /// <summary>
    /// Find elements on the current page / screen.
    /// NOTE: All Find methods use the CSS selector locator strategy.
    /// </summary>
    public class Find
    {
        /// <summary>
        /// Construct a Find object to locate elements.
        /// </summary>
        /// <param name="browser">Browser used to navigate.</param>
        public Find(Browser browser)
        {
            SeleniumFind = new SeleniumFind(browser);
        }

        /// <summary>
        /// Find Element by a valid locator strategy.
        /// <see cref="Strategy"/> for supported options.
        /// </summary>
        /// <param name="locator">String to locate an element.</param>
        /// <returns></returns>
        public IElement Element(string locator)
        {
            var elements = Elements(locator);

            return elements.Count > 0 ? elements.First() : new Element();
        }

        /// <summary>
        /// Find Elements by a valid locator strategy.
        /// <see cref="Strategy"/> for supported options.
        /// </summary>
        /// <param name="locator">String to locate elements.</param>
        /// <returns></returns>
        public List<IElement> Elements(string locator)
        {
            var elements = SeleniumFind.ElementsByCssSelector(locator);

            if (elements.Count > 0) { return elements; }

            elements = SeleniumFind.ElementsById(locator);

            if (elements.Count > 0) { return elements; }

            if (!locator.Contains(" "))
            {
                elements = SeleniumFind.ElementsByClass(locator);

                if (elements.Count > 0) { return elements; }
            }

            elements = SeleniumFind.ElementsByText(locator);

            if (elements.Count > 0) { return elements; }

            return SeleniumFind.ElementsByPartialText(locator);
        }

        /// <summary>
        /// Find element by className or classNames.
        /// NOTE: If multiple inputs are used they are all expected in the given class attribute.
        /// </summary>
        /// <param name="classes">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public IElement ElementByClasses(string classes) => Element(BuildClasses(classes));

        /// <summary>
        /// Find elements by className or classNames.
        /// NOTE: If multiple inputs are used they are all expected in the given class attribute.
        /// </summary>
        /// <param name="classes">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public List<IElement> ElementsByClasses(string classes) => Elements(BuildClasses(classes));

        /// <summary>
        /// Find child Element by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentLocator">Locate element by CSS selector.</param>
        /// <param name="childLocator">Locate element by CSS selector.</param>
        /// <returns></returns>
        public IElement ChildElement(string parentLocator, string childLocator) => (Element(parentLocator) as Element).FindElement(childLocator);

        /// <summary>
        /// Find child Element by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentElement">Parent element to find child elements of.</param>
        /// <param name="childLocator">Locate child element by CSS selector.</param>
        /// <returns></returns>
        public IElement ChildElement(IElement parentElement, string childLocator) => (parentElement as Element).FindElement(childLocator);

        /// <summary>
        /// Find child Elements by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentLocator">Locate the parent element by CSS selector.</param>
        /// <param name="childLocator">Locate child element by CSS selector.</param>
        /// <returns></returns>
        public List<IElement> ChildElements(string parentLocator, string childLocator)
        {
            var elements = Elements(parentLocator);

            return elements.Count > 0 ? ChildElements(elements.First(), childLocator) : elements;
        }

        /// <summary>
        /// Find child Elements by CSS selector based on a parent element found by CSS selector.
        /// </summary>
        /// <param name="parentElement">Parent element to find child elements of.</param>
        /// <param name="childLocator">Locate child elements by CSS selector.</param>
        /// <returns></returns>
        public List<IElement> ChildElements(IElement parentElement, string childLocator) => (parentElement as Element).FindElements(childLocator);

        /// <summary>
        /// Find element by descendent className.
        /// NOTE: Class inputs should be organized based on the class node structure in the DOM.
        /// </summary>
        /// <param name="classes">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public IElement ElementByDescendentCss(string classes) => Element(classes);

        /// <summary>
        /// Find element by descendent className.
        /// NOTE: Class inputs should be organized based on the class node structure in the DOM.
        /// </summary>
        /// <param name="classes">Class name(s) of descendent class elements.</param>
        /// <returns></returns>
        public List<IElement> ElementsByDescendentCss(string classes) => Elements(classes);

        private string BuildClasses(string classes)
        {
            var classItems = classes.Split(' ');
            var builder = new StringBuilder();

            foreach (var item in classItems)
            {
                builder.Append(item.Class());
            }

            return builder.ToString();
        }

        private SeleniumFind SeleniumFind { get; }
    }
}
