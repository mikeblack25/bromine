﻿using System.Drawing;
using Bromine.Core;

using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.Bromine.Core
{
    /// <summary>
    /// Tests the behavior of Core.Window.
    /// </summary>
    public class WindowTests : Framework
    {
        /// <summary>
        /// Launch a browser and set the initial windows size to width = 200, height = 200.
        /// </summary>
        public WindowTests(ITestOutputHelper output) : base(output, false, LogLevels.Framework)
        {
            Window.Size = new Size(200, 200);
        }

        /// <summary>
        /// Can the browser window be minimized?
        /// </summary>
        [Fact]
        public void MinimizeWindowTest()
        {
            Browser.ConditionalVerify.False(Window.IsMinimized, Window.WindowIsMinimizedMessage);

            Browser.Window.Minimize();

            Browser.Verify.True(Window.IsMinimized, Window.WindowIsMinimizedMessage);
        }

        /// <summary>
        /// Can the browser window be resized?
        /// </summary>
        [Fact]
        public void CustomWindowSizeTest()
        {
            var expectedSize = new Size(600, 500);

            Window.Minimize();

            Browser.Verify.False(Window.IsCustom, CustomMessage);
            Browser.ConditionalVerify.NotEqual(expectedSize, Window.Size);

            Window.Size = expectedSize;

            Browser.Verify.True(Window.IsCustom, CustomMessage);
            Browser.Verify.Equal(expectedSize, Window.Size);
        }

        /// <summary>
        /// Widths below 516 are not returned correctly from the call to Window.Size.
        /// </summary>
        [Fact]
        public void MinimumCustomWindowSizeTest()
        {
            var expectedSize = new Size(515, 500);

            Window.Size = expectedSize;

            Assert.Throws<EqualException>(() => Browser.Verify.Equal(expectedSize, Window.Size, "Expected to fail to prove size lower than 516 is not supported"));
        }

        /// <summary>
        /// Can the browser window position be changed?
        /// </summary>
        [Fact]
        public void CustomWindowPositionTest()
        {
            var initialPosition = Window.Position;
            var expectedPosition = new Point(initialPosition.X + 50, initialPosition.Y + 50);

            Window.Position = expectedPosition;

            Browser.Verify.True(Window.IsCustom, CustomMessage);
            Browser.Verify.Equal(expectedPosition, Window.Position);
        }

        /// <summary>
        /// Can the browser window be maximized?
        /// </summary>
        [Fact]
        public void MaximizeWindowTest()
        {
            Browser.ConditionalVerify.False(Window.IsMaximized, Window.WindowIsMaximizedMessage);

            Browser.Window.Maximize();

            Browser.Verify.True(Window.IsMaximized, Window.WindowIsMaximizedMessage);
        }

        /// <summary>
        /// Can the browser window be in full screen?
        /// </summary>
        [Fact]
        public void FullScreenWindowTest()
        {
            Browser.ConditionalVerify.False(Window.IsFullScreen, Window.WindowIsFullScreenMessage);

            Browser.Window.FullScreen();

            Browser.Verify.True(Window.IsFullScreen, Window.WindowIsFullScreenMessage);
        }

        private const string CustomMessage = "Window.IsCustom";

        private Window Window => Browser.Window;
    }
}
