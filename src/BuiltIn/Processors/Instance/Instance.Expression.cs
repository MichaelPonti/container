﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Unity.Container;
using Unity.Resolution;

namespace Unity.BuiltIn
{
    public partial class InstanceProcessor
    {
        public override IEnumerable<Expression> Express(ref PipelineBuilder<IEnumerable<Expression>> builder)
        {
            throw new NotImplementedException();
            //

            //// Skip if already have a resolver expression
            //if (null != builder.SeedExpression) return builder.Express();

            //var registration = builder.Registration as FactoryRegistration ??
            //                   builder.Factory      as FactoryRegistration;

            //Debug.Assert(null != registration);

            //var factory = Expression.Constant(registration!.Factory, typeof(Func<IResolveContext, object?>));
            //var expression = Expression.Assign(PipelineContext.ExistingExpression,
            //        Expression.Invoke(factory, Expression.Convert(PipelineContext.ContextExpression, typeof(IResolveContext))));

            //return builder.Express(new[] { expression });
        }
    }
}
