﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.LanguageServer.EndpointContracts;
using Microsoft.AspNetCore.Razor.LanguageServer.ProjectSystem;
using Microsoft.CodeAnalysis.Razor.Completion;
using Microsoft.CodeAnalysis.Razor.ProjectSystem;
using Microsoft.VisualStudio.LanguageServer.Protocol;

namespace Microsoft.AspNetCore.Razor.LanguageServer.Completion;

[RazorLanguageServerEndpoint(Methods.TextDocumentCompletionResolveName)]
internal class RazorCompletionResolveEndpoint
    : IRazorDocumentlessRequestHandler<VSInternalCompletionItem, VSInternalCompletionItem>,
      ICapabilitiesProvider
{
    private readonly AggregateCompletionItemResolver _completionItemResolver;
    private readonly CompletionListCache _completionListCache;
    private readonly IProjectSnapshotManager _projectSnapshotManager;
    private VSInternalClientCapabilities? _clientCapabilities;

    public RazorCompletionResolveEndpoint(
        AggregateCompletionItemResolver completionItemResolver,
        CompletionListCache completionListCache,
        IProjectSnapshotManager projectSnapshotManager)
    {
        _completionItemResolver = completionItemResolver;
        _completionListCache = completionListCache;
        _projectSnapshotManager = projectSnapshotManager;
    }

    public bool MutatesSolutionState => false;

    public void ApplyCapabilities(VSInternalServerCapabilities serverCapabilities, VSInternalClientCapabilities clientCapabilities)
    {
        _clientCapabilities = clientCapabilities;
    }

    public async Task<VSInternalCompletionItem> HandleRequestAsync(VSInternalCompletionItem completionItem, RazorRequestContext requestContext, CancellationToken cancellationToken)
    {
        if (!completionItem.TryGetCompletionListResultIds(out var resultIds))
        {
            // Unable to lookup completion item result info
            return completionItem;
        }

        object? originalRequestContext = null;
        VSInternalCompletionList? containingCompletionList = null;
        foreach (var resultId in resultIds)
        {
            if (!_completionListCache.TryGet(resultId, out var cacheEntry))
            {
                continue;
            }

            // See if this is the right completion list for this corresponding completion item. We cross-check this based on label only given that
            // is what users interact with.
            if (cacheEntry.CompletionList.Items.Any(completion =>
                completionItem.Label == completion.Label
                // Check the Kind as well, e.g. we may have a Razor snippet and a C# keyword with the same label, etc.
                && completionItem.Kind == completion.Kind))
            {
                originalRequestContext = cacheEntry.Context;
                containingCompletionList = cacheEntry.CompletionList;
                break;
            }
        }

        if (containingCompletionList is null)
        {
            // Couldn't find an associated completion list
            return completionItem;
        }

        var resolvedCompletionItem = await _completionItemResolver.ResolveAsync(
            completionItem,
            containingCompletionList,
            originalRequestContext,
            _clientCapabilities,
            _projectSnapshotManager.GetQueryOperations(),
            cancellationToken).ConfigureAwait(false);
        resolvedCompletionItem ??= completionItem;

        return resolvedCompletionItem;
    }
}
