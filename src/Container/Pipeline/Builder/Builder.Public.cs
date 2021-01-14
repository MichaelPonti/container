﻿using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using Unity.Extension;

namespace Unity.Container
{
    public partial struct PipelineBuilder<TContext> : IBuildPipeline<TContext>,
                                                      IExpressPipeline<TContext>
    {
        #region Fields

        public static IEnumerable<Expression> EmptyExpression
            = Enumerable.Empty<Expression>();

        #endregion



        #region Analysis

        public object[] Analyse(ref TContext context)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Chain

        public ResolveDelegate<TContext> ExpressBuildUp()
        {
            var expressions = BuildUp();
            // TODO: Optimization

            var postfix = new Expression[] { PipelineBuilder<TContext>.Label, TargetExpression };
            // TODO: Optimization
            var lambda = Expression.Lambda<ResolveDelegate<TContext>>(
               Expression.Block(expressions.Concat(postfix)),
               PipelineBuilder<TContext>.ContextExpression);

            return lambda.Compile();
        }

        public IEnumerable<Expression> BuildUp()
        {
            if (!_enumerator.MoveNext()) return EmptyExpression;

            return _enumerator.Current.ExpressBuildUp<PipelineBuilder<TContext>, TContext>(ref this);
        }

        #endregion
    }
}
