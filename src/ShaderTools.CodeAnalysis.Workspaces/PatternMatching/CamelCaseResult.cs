﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using ShaderTools.CodeAnalysis.Shared.Utilities;
using ShaderTools.Utilities.PooledObjects;

namespace ShaderTools.CodeAnalysis.PatternMatching
{
    internal partial class PatternMatcher
    {
        private struct CamelCaseResult
        {
            public readonly bool FromStart;
            public readonly bool Contiguous;
            public readonly int MatchCount;
            public readonly ArrayBuilder<TextSpan> MatchedSpansInReverse;

            public CamelCaseResult(bool fromStart, bool contiguous, int matchCount, ArrayBuilder<TextSpan> matchedSpansInReverse)
            {
                FromStart = fromStart;
                Contiguous = contiguous;
                MatchCount = matchCount;
                MatchedSpansInReverse = matchedSpansInReverse;

                Debug.Assert(matchedSpansInReverse == null || matchedSpansInReverse.Count == matchCount);
            }

            public void Free()
            {
                MatchedSpansInReverse?.Free();
            }

            public CamelCaseResult WithFromStart(bool fromStart)
                => new CamelCaseResult(fromStart, Contiguous, MatchCount, MatchedSpansInReverse);

            public CamelCaseResult WithAddedMatchedSpan(TextSpan value)
            {
                MatchedSpansInReverse?.Add(value);
                return new CamelCaseResult(FromStart, Contiguous, MatchCount + 1, MatchedSpansInReverse);
            }
        }

        private static PatternMatchKind GetCamelCaseKind(CamelCaseResult result, StringBreaks candidateHumps)
        {
            var toEnd = result.MatchCount == candidateHumps.GetCount();
            if (result.FromStart)
            {
                if (result.Contiguous)
                {
                    // We contiguously matched humps from the start of this candidate.  If we 
                    // matched all the humps, then this was an exact match, otherwise it was a 
                    // contiguous prefix match
                    return toEnd
                        ? PatternMatchKind.CamelCaseExact
                        : PatternMatchKind.CamelCasePrefix;
                }
                else
                {
                    return PatternMatchKind.CamelCaseNonContiguousPrefix;
                }
            }
            else
            {
                // We didn't match from the start.  Distinguish between a match whose humps are all
                // contiguous, and one that isn't.
                return result.Contiguous
                    ? PatternMatchKind.CamelCaseSubstring
                    : PatternMatchKind.CamelCaseNonContiguousSubstring;
            }
        }
    }
}