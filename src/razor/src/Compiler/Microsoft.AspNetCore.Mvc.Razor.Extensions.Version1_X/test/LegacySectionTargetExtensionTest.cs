﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Xunit;

namespace Microsoft.AspNetCore.Razor.Language.Extensions;

public class LegacySectionTargetExtensionTest
{
    [Fact]
    public void WriteSection_WritesSectionCode_DesignTime()
    {
        // Arrange
        var node = new SectionIntermediateNode()
        {
            Children =
                {
                    new CSharpExpressionIntermediateNode(),
                },
            SectionName = "MySection"
        };

        var extension = new LegacySectionTargetExtension()
        {
            SectionMethodName = "CreateSection"
        };

        using var context = TestCodeRenderingContext.CreateDesignTime();

        // Act
        extension.WriteSection(context, node);

        // Assert
        var expected = @"CreateSection(""MySection"", async(__razor_section_writer) => {
    Render Children
}
);
";

        var output = context.CodeWriter.GetText().ToString();
        Assert.Equal(expected, output, ignoreLineEndingDifferences: true);
    }
}
