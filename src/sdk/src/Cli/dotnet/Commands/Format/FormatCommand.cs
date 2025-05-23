﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.CommandLine;
using Microsoft.DotNet.Cli;
using Microsoft.DotNet.Cli.Extensions;
using Microsoft.DotNet.Cli.Utils;

namespace Microsoft.DotNet.Tools.Format;

public class FormatCommand(IEnumerable<string> argsToForward) : DotnetFormatForwardingApp(argsToForward)
{
    public static FormatCommand FromArgs(string[] args)
    {
        var parser = Parser.Instance;
        var result = parser.ParseFrom("dotnet format", args);
        return FromParseResult(result);
    }

    public static FormatCommand FromParseResult(ParseResult result)
    {
        return new FormatCommand(result.GetValue(FormatCommandParser.Arguments));
    }

    public static int Run(ParseResult parseResult)
    {
        parseResult.HandleDebugSwitch();

        return FromParseResult(parseResult).Execute();
    }

    public static int Run(string[] args)
    {
        DebugHelper.HandleDebugSwitch(ref args);
        return new DotnetFormatForwardingApp(args).Execute();
    }
}
