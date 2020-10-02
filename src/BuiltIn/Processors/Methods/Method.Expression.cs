﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using Unity.Container;
using Unity.Exceptions;

namespace Unity.BuiltIn
{
    public partial class MethodProcessor : ParameterProcessor<MethodInfo>
    {
        protected override Expression GetResolverExpression(MethodInfo info)
        {
            throw new System.NotImplementedException();
            //try
            //{
            //    return Expression.Call(
            //        Expression.Convert(PipelineContext.ExistingExpression, info.DeclaringType),
            //        info, ParameterExpressions(info));
            //}
            //catch (InvalidRegistrationException reg)
            //{
            //    // Throw if parameters invalid
            //    return Expression.Throw(Expression.Constant(reg));
            //}
            //catch (Exception ex)
            //{
            //    // Throw if parameters invalid
            //    return Expression.Throw(Expression.Constant(new InvalidRegistrationException(ex.Message, ex)));
            //}
        }

        protected override Expression GetResolverExpression(MethodInfo info, object? data)
        {
            throw new System.NotImplementedException();
            //object[]? injectors = null != data && data is object[] array && 0 != array.Length ? array : null;

            //if (null == injectors) return GetResolverExpression(info);

            //try
            //{
            //    return Expression.Call(
            //        Expression.Convert(PipelineContext.ExistingExpression, info.DeclaringType),
            //        info, ParameterExpressions(info, injectors));
            //}
            //catch (InvalidRegistrationException reg)
            //{
            //    // Throw if parameters invalid
            //    return Expression.Throw(Expression.Constant(reg));
            //}
            //catch (Exception ex)
            //{
            //    // Throw if parameters invalid
            //    return Expression.Throw(Expression.Constant(new InvalidRegistrationException(ex.Message, ex)));
            //}
        }
    }
}
