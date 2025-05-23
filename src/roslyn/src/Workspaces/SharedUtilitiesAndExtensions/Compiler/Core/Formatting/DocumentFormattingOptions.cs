﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.Serialization;

namespace Microsoft.CodeAnalysis.Formatting;

[DataContract]
internal sealed record class DocumentFormattingOptions
{
    public static readonly DocumentFormattingOptions Default = new();

    [DataMember] public string FileHeaderTemplate { get; init; } = "";
    [DataMember] public bool InsertFinalNewLine { get; init; } = false;
}
