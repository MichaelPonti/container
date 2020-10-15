﻿using System;
using System.ComponentModel.Composition;
using System.Reflection;
using Unity.Container;
using Unity.Injection;
using Unity.Resolution;

namespace Unity.BuiltIn
{
    public partial class ConstructorProcessor
    {
        #region PipelineBuilder

        public override ResolveDelegate<PipelineContext>? Build(ref PipelineBuilder<ResolveDelegate<PipelineContext>?> builder)
        {
            // Do nothing if seed method exists
            // TODO: if (null != builder.Target) return builder.Build();

            Type type = builder.Context.Type;
            var members = type.GetConstructors(BindingFlags);
            var downstream = builder.Build();

            ///////////////////////////////////////////////////////////////////
            // Check if any constructors are available
            if (0 == members.Length)
            {
                // Pipeline for BuildUp only, it throws if no object provided
                return (ref PipelineContext c) => (c.Target is null)
                    ? c.Error($"No accessible constructors on type {type}")
                    : downstream?.Invoke(ref c);
            }

            /////////////////////////////////////////////////////////////////
            // Build from Injected Constructor, if present
            if (builder.Context.Registration?.Constructor is InjectionConstructor injected)
            {
                int index;

                if (-1 == (index = injected.SelectFrom(members)))
                {
                    // Pipeline for BuildUp only, it throws if no object provided
                    return (ref PipelineContext c) => (c.Target is null)
                        ? c.Error($"Injected constructor '{injected}' doesn't match any accessible constructors on type {type}")
                        : downstream?.Invoke(ref c);
                }

                return CreatePipeline(members[index], injected.Data, downstream);
            }

            ///////////////////////////////////////////////////////////////////
            // Only one constructor, nothing to select
            if (1 == members.Length)
            {
                return CreatePipeline(members[0], downstream);
            }


            ///////////////////////////////////////////////////////////////////
            // Check for annotated constructor
            foreach (var info in members)
            {
                if (!info.IsDefined(typeof(ImportingConstructorAttribute))) continue;

                return CreatePipeline(info, downstream);
            }


            //ConstructorInfo? info;

            //var selection = Select(ref builder);

            throw new NotImplementedException();

            #region
            //switch (selection)
            //{
            //    case ConstructorInfo memberInfo:
            //        info = memberInfo;
            //        resolvers = ParameterResolvers(info);
            //        break;

            //    case InjectionMethodBase<ConstructorInfo> injectionMember:
            //        info = injectionMember.MemberInfo(builder.Type);
            //        resolvers = null != injectionMember.Data && injectionMember.Data is object[] injectors && 0 != injectors.Length
            //                  ? ParameterResolvers(info, injectors)
            //                  : ParameterResolvers(info);
            //        break;

            //    case Exception exception:
            //        return (ref PipelineContext c) =>
            //        {
            //            if (null == c.Existing)
            //                throw exception;

            //            return null == pipeline ? c.Existing : pipeline.Invoke(ref c);
            //        };

            //    default:
            //        return (ref PipelineContext c) =>
            //        {
            //            if (null == c.Existing)
            //                throw new InvalidRegistrationException($"No public constructor is available for type {c.Type}.");

            //            return null == pipeline ? c.Existing : pipeline.Invoke(ref c);
            //        };
            //}

            //return GetResolverDelegate(info, resolvers, pipeline, builder.LifetimeManager is PerResolveLifetimeManager);
            #endregion
        }

        #endregion


        #region Implementation

        private ResolveDelegate<PipelineContext> CreatePipeline(ConstructorInfo info, object?[]? data, ResolveDelegate<PipelineContext>? pipeline)
        {
            var parameters = info.GetParameters();
            if (null == parameters) return ParameterlessPipeline(info, pipeline);

            var imports = new ReflectionInfo<ParameterInfo>[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
                imports[i] = parameters[i].AsInjectionInfo(data![i]);

            return (ref PipelineContext context) =>
            {
                if (null == context.Target)
                {
                    ResolverOverride? @override;
                    object?[] arguments = new object?[imports.Length];

                    for (var i = 0; i < arguments.Length && !context.IsFaulted; i++)
                    {
                        ref var parameter = ref imports[i];

                        // Check for override
                        arguments[i] = (null != (@override = context.GetOverride(in parameter.Import)))
                            ? Build(ref context, in parameter.Import, parameter.Import.Element.AsImportData(@override.Value))
                            : Build(ref context, in parameter.Import, in parameter.Data);
                    }
                    
                    if (!context.IsFaulted) context.Target = info.Invoke(arguments);
                }

                return null == pipeline
                    ? context.Target
                    : pipeline?.Invoke(ref context);
            };
        }


        private ResolveDelegate<PipelineContext> CreatePipeline(ConstructorInfo info, ResolveDelegate<PipelineContext>? pipeline)
        {
            var parameters = info.GetParameters();
            if (null == parameters) return ParameterlessPipeline(info, pipeline);

            var imports = new ReflectionInfo<ParameterInfo>[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
                imports[i] = parameters[i].AsInjectionInfo();

            return (ref PipelineContext context) =>
            {
                if (null == context.Target)
                {
                    ResolverOverride? @override;
                    object?[] arguments = new object?[imports.Length];

                    for (var i = 0; i < arguments.Length && !context.IsFaulted; i++)
                    {
                        ref var parameter = ref imports[i];

                        // Check for override
                        arguments[i] = (null != (@override = context.GetOverride(in parameter.Import)))
                            ? Build(ref context, in parameter.Import, parameter.Import.Element.AsImportData(@override.Value))
                            : Build(ref context, in parameter.Import, in parameter.Data);
                    }

                    if (!context.IsFaulted) context.Target = info.Invoke(arguments);
                }

                return null == pipeline
                    ? context.Target
                    : pipeline?.Invoke(ref context);
            };
        }

        protected ResolveDelegate<PipelineContext> ParameterlessPipeline(ConstructorInfo info, ResolveDelegate<PipelineContext>? pipeline)
        {
            if (null == pipeline)
            {
                return (ref PipelineContext context) =>
                {
                    if (null == context.Target)
                    {
                        using var action = context.Start(info);
                        context.Target = info.Invoke(EmptyParametersArray);
                    }

                    return context.Target;
                };
            }

            return (ref PipelineContext context) =>
            {
                if (null == context.Target)
                {
                    using var action = context.Start(info);
                    context.Target = info.Invoke(EmptyParametersArray);
                }

                return pipeline(ref context);
            };
        }

        #endregion
    }

}
