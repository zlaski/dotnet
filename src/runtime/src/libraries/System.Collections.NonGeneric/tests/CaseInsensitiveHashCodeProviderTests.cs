// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using System.Diagnostics;
using Microsoft.DotNet.RemoteExecutor;
using Xunit;
using System.Tests;

#pragma warning disable 618 // obsolete types

namespace System.Collections.Tests
{
    public class CaseInsensitiveHashCodeProviderTests
    {
        [Theory]
        [InlineData("hello", "HELLO", true)]
        [InlineData("hello", "hello", true)]
        [InlineData("HELLO", "HELLO", true)]
        [InlineData("hello", "goodbye", false)]
        [InlineData(5, 5, true)]
        [InlineData(10, 5, false)]
        [InlineData(5, 10, false)]
        public void Ctor_Empty_GetHashCodeCompare(object a, object b, bool expected)
        {
            var provider = new CaseInsensitiveHashCodeProvider();
            Assert.Equal(provider.GetHashCode(a), provider.GetHashCode(a));
            Assert.Equal(provider.GetHashCode(b), provider.GetHashCode(b));
            Assert.Equal(expected, provider.GetHashCode(a) == provider.GetHashCode(b));
        }

        [Theory]
        [InlineData("hello", "HELLO", true)]
        [InlineData("hello", "hello", true)]
        [InlineData("HELLO", "HELLO", true)]
        [InlineData("hello", "goodbye", false)]
        [InlineData(5, 5, true)]
        [InlineData(10, 5, false)]
        [InlineData(5, 10, false)]
        public void Ctor_Empty_ChangeCurrentCulture_GetHashCodeCompare(object a, object b, bool expected)
        {
            var cultureNames = Helpers.TestCultureNames;

            foreach (string cultureName in cultureNames)
            {
                CultureInfo newCulture;
                try
                {
                    newCulture = new CultureInfo(cultureName);
                }
                catch (CultureNotFoundException)
                {
                    continue;
                }

                using (new ThreadCultureChange(newCulture))
                {
                    var provider = new CaseInsensitiveHashCodeProvider();
                    Assert.Equal(provider.GetHashCode(a), provider.GetHashCode(a));
                    Assert.Equal(provider.GetHashCode(b), provider.GetHashCode(b));
                    Assert.Equal(expected, provider.GetHashCode(a) == provider.GetHashCode(b));
                }
            }
        }

        [Theory]
        [InlineData("hello", "HELLO", true)]
        [InlineData("hello", "hello", true)]
        [InlineData("HELLO", "HELLO", true)]
        [InlineData("hello", "goodbye", false)]
        [InlineData(5, 5, true)]
        [InlineData(10, 5, false)]
        [InlineData(5, 10, false)]
        public void Ctor_CultureInfo_ChangeCurrentCulture_GetHashCodeCompare(object a, object b, bool expected)
        {
            var cultureNames = Helpers.TestCultureNames;

            foreach (string cultureName in cultureNames)
            {
                CultureInfo culture;
                try
                {
                    culture = new CultureInfo(cultureName);
                }
                catch (CultureNotFoundException)
                {
                    continue;
                }

                var provider = new CaseInsensitiveHashCodeProvider(culture);
                Assert.Equal(provider.GetHashCode(a), provider.GetHashCode(a));
                Assert.Equal(provider.GetHashCode(b), provider.GetHashCode(b));
                Assert.Equal(expected, provider.GetHashCode(a) == provider.GetHashCode(b));
            }
        }

        [Fact]
        [ActiveIssue("https://github.com/dotnet/runtime/issues/37069", TestPlatforms.Android | TestPlatforms.LinuxBionic)]
        [ActiveIssue("https://github.com/dotnet/runtime/issues/95338", typeof(PlatformDetection), nameof(PlatformDetection.IsHybridGlobalizationOnApplePlatform))]
        public void Ctor_CultureInfo_GetHashCodeCompare_TurkishI()
        {
            var cultureNames = Helpers.TestCultureNames;

            foreach (string cultureName in cultureNames)
            {
                CultureInfo culture;
                try
                {
                    culture = new CultureInfo(cultureName);
                }
                catch (CultureNotFoundException)
                {
                    continue;
                }

                var provider = new CaseInsensitiveHashCodeProvider(culture);

                // Turkish has lower-case and upper-case version of the dotted "i", so the upper case of "i" (U+0069) isn't "I" (U+0049)
                // but rather U+0130.
                Assert.Equal(
                    culture.Name != "tr-TR",
                    provider.GetHashCode("file") == provider.GetHashCode("FILE"));
            }
        }

        [Fact]
        public void Ctor_CultureInfo_NullCulture_ThrowsArgumentNullException()
        {
            AssertExtensions.Throws<ArgumentNullException>("culture", () => new CaseInsensitiveHashCodeProvider(null));
        }

        [Fact]
        public void GetHashCode_NullObj_ThrowsArgumentNullException()
        {
            AssertExtensions.Throws<ArgumentNullException>("obj", () => new CaseInsensitiveHashCodeProvider().GetHashCode(null));
        }

        [Theory]
        [InlineData("hello", "HELLO", true)]
        [InlineData("hello", "hello", true)]
        [InlineData("HELLO", "HELLO", true)]
        [InlineData("hello", "goodbye", false)]
        [InlineData(5, 5, true)]
        [InlineData(10, 5, false)]
        [InlineData(5, 10, false)]
        public void Default_GetHashCodeCompare(object a, object b, bool expected)
        {
            Assert.Equal(expected,
                CaseInsensitiveHashCodeProvider.Default.GetHashCode(a) == CaseInsensitiveHashCodeProvider.Default.GetHashCode(b));
            Assert.Equal(expected,
                CaseInsensitiveHashCodeProvider.DefaultInvariant.GetHashCode(a) == CaseInsensitiveHashCodeProvider.DefaultInvariant.GetHashCode(b));
        }

        [ConditionalFact(typeof(PlatformDetection), nameof(PlatformDetection.IsNotInvariantGlobalization))]
        [ActiveIssue("https://github.com/dotnet/runtime/issues/37069", TestPlatforms.Android | TestPlatforms.LinuxBionic)]
        [ActiveIssue("https://github.com/dotnet/runtime/issues/95338", typeof(PlatformDetection), nameof(PlatformDetection.IsHybridGlobalizationOnApplePlatform))]
        public void Default_Compare_TurkishI()
        {
            // Turkish has lower-case and upper-case version of the dotted "i", so the upper case of "i" (U+0069) isn't "I" (U+0049)
            // but rather U+0130.
            using (new ThreadCultureChange("tr-TR"))
            {
                Assert.False(CaseInsensitiveHashCodeProvider.Default.GetHashCode("file") == CaseInsensitiveHashCodeProvider.Default.GetHashCode("FILE"));
                Assert.True(CaseInsensitiveHashCodeProvider.DefaultInvariant.GetHashCode("file") == CaseInsensitiveHashCodeProvider.DefaultInvariant.GetHashCode("FILE"));
            }

            using (new ThreadCultureChange("en-US"))
            {
                Assert.True(CaseInsensitiveHashCodeProvider.Default.GetHashCode("file") == CaseInsensitiveHashCodeProvider.Default.GetHashCode("FILE"));
            }
        }
    }
}
