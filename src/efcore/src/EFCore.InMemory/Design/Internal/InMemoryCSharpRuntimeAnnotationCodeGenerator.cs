// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.InMemory.Internal;

namespace Microsoft.EntityFrameworkCore.InMemory.Design.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
#pragma warning disable EF1001 // Internal EF Core API usage.
public class InMemoryCSharpRuntimeAnnotationCodeGenerator : CSharpRuntimeAnnotationCodeGenerator
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public InMemoryCSharpRuntimeAnnotationCodeGenerator(
        CSharpRuntimeAnnotationCodeGeneratorDependencies dependencies)
        : base(dependencies)
    {
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public override void Generate(IEntityType entityType, CSharpRuntimeAnnotationCodeGeneratorParameters parameters)
    {
        if (entityType.GetInMemoryQuery() != null)
        {
            throw new InvalidOperationException(InMemoryStrings.CompiledModelDefiningQuery(entityType.DisplayName()));
        }

        base.Generate(entityType, parameters);
    }
}
