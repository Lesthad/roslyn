﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Classification.Classifiers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Classification
{
    internal class OperatorOverloadSyntaxClassifier : AbstractSyntaxClassifier
    {
        public override void AddClassifications(
            Workspace workspace,
            SyntaxNode syntax,
            SemanticModel semanticModel,
            ArrayBuilder<ClassifiedSpan> result,
            CancellationToken cancellationToken)
        {
            var symbolInfo = semanticModel.GetSymbolInfo(syntax, cancellationToken);
            if (symbolInfo.Symbol is IMethodSymbol methodSymbol
                && methodSymbol.MethodKind == MethodKind.UserDefinedOperator)
            {
                result.Add(new ClassifiedSpan(syntax.Span, ClassificationTypeNames.OperatorOverload));
            }
        }

        public override ImmutableArray<Type> SyntaxNodeTypes { get; } = ImmutableArray.Create(
            typeof(BinaryExpressionSyntax), 
            typeof(PrefixUnaryExpressionSyntax), 
            typeof(PostfixUnaryExpressionSyntax),
            typeof(ConditionalExpressionSyntax));
    }
}
