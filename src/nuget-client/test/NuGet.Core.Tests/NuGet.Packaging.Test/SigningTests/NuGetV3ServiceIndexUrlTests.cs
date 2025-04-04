// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Formats.Asn1;
using System.Security.Cryptography;
using NuGet.Packaging.Signing;
using Xunit;

namespace NuGet.Packaging.Test
{
    public class NuGetV3ServiceIndexUrlTests
    {
        [Fact]
        public void Constructor_WhenV3ServiceIndexUrlNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new NuGetV3ServiceIndexUrl(v3ServiceIndexUrl: null));

            Assert.Equal("v3ServiceIndexUrl", exception.ParamName);
        }

        [Fact]
        public void Constructor_WhenV3ServiceIndexNotAbsolute_Throws()
        {
            var url = new Uri("/", UriKind.Relative);
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new NuGetV3ServiceIndexUrl(url));

            Assert.Equal("v3ServiceIndexUrl", exception.ParamName);
            Assert.StartsWith("The URL value is invalid.", exception.Message);
        }

        [Fact]
        public void Constructor_WhenV3ServiceIndexSchemeIsNotHttps_Throws()
        {
            var url = new Uri("http://test.test", UriKind.Absolute);
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new NuGetV3ServiceIndexUrl(url));

            Assert.Equal("v3ServiceIndexUrl", exception.ParamName);
            Assert.StartsWith("The URL value is invalid.", exception.Message);
        }

        [Fact]
        public void Constructor_WithValidInput_InitializesProperty()
        {
            var url = new Uri("https://test.test", UriKind.Absolute);
            var nugetV3ServiceIndexUrl = new NuGetV3ServiceIndexUrl(url);

            Assert.True(nugetV3ServiceIndexUrl.V3ServiceIndexUrl.IsAbsoluteUri);
            Assert.Equal(url.OriginalString, nugetV3ServiceIndexUrl.V3ServiceIndexUrl.OriginalString);
        }

        [Fact]
        public void Encode_ReturnsValidDer()
        {
            const string url = "https://test.test";

            AsnWriter writer = new(AsnEncodingRules.DER);

            writer.WriteCharacterString(UniversalTagNumber.IA5String, url);

            byte[] expectedBytes = writer.Encode();
            var nugetV3ServiceIndexUrl = new NuGetV3ServiceIndexUrl(new Uri(url));

            byte[] actualBytes = nugetV3ServiceIndexUrl.Encode();

            Assert.Equal(expectedBytes, actualBytes);
        }

        [Fact]
        public void Read_WithInvalidAsn1_Throws()
        {
            Assert.Throws<CryptographicException>(() => NuGetV3ServiceIndexUrl.Read(new byte[] { 0x30, 0x0b }));
        }

        [Fact]
        public void Read_WithInvalidType_Throws()
        {
            AsnWriter writer = new(AsnEncodingRules.DER);

            writer.WriteCharacterString(UniversalTagNumber.UTF8String, "a");

            byte[] bytes = writer.Encode();

            Assert.Throws<CryptographicException>(() => NuGetV3ServiceIndexUrl.Read(bytes));
        }

        [Fact]
        public void Read_WithValidInput_ReturnsInstance()
        {
            const string url = "https://test.test";

            AsnWriter writer = new(AsnEncodingRules.DER);

            writer.WriteCharacterString(UniversalTagNumber.IA5String, url);

            byte[] bytes = writer.Encode();
            NuGetV3ServiceIndexUrl nugetV3ServiceIndexUrl = NuGetV3ServiceIndexUrl.Read(bytes);

            Assert.True(nugetV3ServiceIndexUrl.V3ServiceIndexUrl.IsAbsoluteUri);
            Assert.Equal(url, nugetV3ServiceIndexUrl.V3ServiceIndexUrl.OriginalString);
        }
    }
}
