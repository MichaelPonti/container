﻿using System;
using System.Reflection;
using Unity.Extension;
using Unity.Storage;

namespace Unity.Container
{
    internal static partial class Factories<TContext>
    {
        #region Fields

        private static MethodInfo? EnumerablePipelineMethodInfo;

        #endregion


        #region Factory

        public static ResolveDelegate<TContext> Enumerable(ref TContext context)
        {
            var target = context.Type.GenericTypeArguments[0];
            var state = target.IsGenericType
                ? new State(target, target.GetGenericTypeDefinition())
                : new State(target);

            state.Pipeline = (EnumerablePipelineMethodInfo ??= typeof(Factories<TContext>)
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(EnumerablePipeline))!)
                .CreatePipeline<TContext>(target, state);

            return state.Pipeline;
        }

        #endregion


        #region Implementation

        private static object? EnumerablePipeline<TElement>(State state, ref TContext context)
        {
            var metadata = (Metadata[]?)(context.Registration?.Data as WeakReference)?.Target;
            if (metadata is null || context.Container.Scope.Version != metadata.Version())
            {
                var manager = context.Container.Scope.GetCache(in context.Contract,
                    () => new InternalLifetimeManager(RegistrationCategory.Cache));

                lock (manager)
                {
                    metadata = (Metadata[]?)(manager.Data as WeakReference)?.Target;
                    if (metadata is null || context.Container.Scope.Version != metadata.Version())
                    {
                        metadata = context.Container.Scope.ToEnumerableSet(state.Types);
                        manager.Data = new WeakReference(metadata);
                    }

                    if (!ReferenceEquals(context.Registration, manager))
                        manager.SetPipeline(context.Container.Scope, state.Pipeline);
                }
            }

            TElement[] array;
            var count = metadata.Count();
            var typeHash = typeof(TElement).GetHashCode();

            if (0 < count)
            {
                array = new TElement[count];
                count = 0;

                for (var i = array.Length; i > 0; i--)
                {
                    var name = context.Container.Scope[in metadata[i]].Internal.Contract.Name;
                    var hash = Contract.GetHashCode(typeHash, name?.GetHashCode() ?? 0);
                    var error = new ErrorInfo();
                    var contract = new Contract(hash, typeof(TElement), name);
                    var value = context.Resolve(ref contract, ref error);

                    if (error.IsFaulted)
                    {
                        if (error.Exception is ArgumentException ex && ex.InnerException is TypeLoadException)
                        {
                            continue; // Ignore
                        }
                        else
                        {
                            context.ErrorInfo = error;
                            return UnityContainer.NoValue;
                        }
                    }

                    array[count++] = (TElement)value!;
                }
            }
            else
            {
                // Nothing is registered, try to resolve optional contract
                try
                {
                    var name = context.Contract.Name;
                    var hash = Contract.GetHashCode(typeHash, name?.GetHashCode() ?? 0);
                    var error = new ErrorInfo();
                    var contract = new Contract(hash, typeof(TElement), name);
                    var value = context.Resolve(ref contract, ref error);

                    if (error.IsFaulted)
                    {
#if NET45
                        array = new TElement[0];
#else
                        array = System.Array.Empty<TElement>();
#endif
                    }
                    else
                    {
                        count = 1;
                        array = new TElement[] { (TElement)value! };
                    };
                }
                catch (ArgumentException ex) when (ex.InnerException is TypeLoadException)
                {
#if NET45
                    array = new TElement[0];
#else
                    array = System.Array.Empty<TElement>();
#endif
                }
            }

            if (count < array.Length) System.Array.Resize(ref array, count);

            context.Target = array;

            return array;
        }

        #endregion
    }
}