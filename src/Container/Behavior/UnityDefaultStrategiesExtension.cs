﻿using System;
using Unity.Container;

namespace Unity.Extension
{
    /// <summary>
    /// This extension installs the default strategies and policies into the container
    /// to implement the standard behavior of the Unity container.
    /// </summary>
    public partial class UnityDefaultStrategiesExtension<TContext>
            where TContext : IBuilderContext
    {
        /// <summary>
        /// Add the default <see cref="BuilderStrategy"/> strategies and policies to the container.
        /// </summary>
        public static void Initialize(ExtensionContext context)
        {
            var policies = context.Policies;

            // Setup build on change for the chains
            context.TypePipelineChain.Invalidated     += OnBuildChainChanged;
            context.FactoryPipelineChain.Invalidated  += OnBuildChainChanged;
            context.InstancePipelineChain.Invalidated += OnBuildChainChanged;


            // Populate Stages

            // Type Build Stages
            context.TypePipelineChain.Add(new (UnityBuildStage, BuilderStrategy)[] 
            { 
                (UnityBuildStage.Fields,     new FieldStrategy(policies)),
                (UnityBuildStage.Methods,    new MethodStrategy(policies)),
                (UnityBuildStage.Creation,   new ConstructorStrategy(policies)),
                (UnityBuildStage.Properties, new PropertyStrategy(policies))
            });

            // Factory Build Stages
            context.FactoryPipelineChain.Add(UnityBuildStage.Creation,  new FactoryStrategy());

            // Instance Build Stages
            context.InstancePipelineChain.Add(UnityBuildStage.Creation, new InstanceStrategy());


            // Rebuilds stage chain when modified
            void OnBuildChainChanged(IStagedStrategyChain chain, Type target) 
                => policies.Set<ResolveDelegate<TContext>>(target, chain.BuildUpPipeline<TContext>());
        }
    }
}