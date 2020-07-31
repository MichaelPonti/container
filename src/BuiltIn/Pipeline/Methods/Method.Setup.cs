﻿using System;
using System.Reflection;
using Unity.Container;
using Unity.Extension;

namespace Unity.Pipeline
{
    public partial class MethodProcessor : MethodBaseProcessor<MethodInfo>
    {
        public static void SetupProcessor(ExtensionContext context)
        {
            // Create processor
            var processor = new MethodProcessor();

            // Add to pipeline chain
            context.TypePipeline.Add(processor, BuilderStage.Methods);

            // Subscribe to updates
            var defaults = (Defaults)context.Policies;

            defaults.DefaultPolicyChanged += OnDefaultsChanged;
        }

        private static void OnDefaultsChanged(Type type, object? value)
        {
        }
    }
}