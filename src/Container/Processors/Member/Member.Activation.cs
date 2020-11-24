﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Injection;
using Unity.Resolution;

namespace Unity.Container
{
    public abstract partial class MemberProcessor<TMemberInfo, TDependency, TData>
    {
        public override void PreBuild(ref PipelineContext context)
        {
            Debug.Assert(null != context.Target, "Target should never be null");
            var members = GetMembers(context.Type);

            if (0 == members.Length) return;

            ResolverOverride? @override;
            ImportInfo import = default;
            var injection  = GetInjectedMembers<InjectionMemberInfo<TMemberInfo>>(context.Registration);
            var injections = injection;

            for (var i = 0; i < members.Length && !context.IsFaulted; i++)
            {
                // Initialize member
                import.MemberInfo = Unsafe.As<TDependency>(members[i]);

                // Load attributes
                var attribute = LoadImportInfo(ref import);

                // Injection, if exists
                while (null != injection)
                {
                    if (MatchRank.ExactMatch == injection.Match(Unsafe.As<TMemberInfo>(import.MemberInfo)))
                    {
                        injection.GetImportInfo(ref import);
                        goto activate;
                    }

                    injection = Unsafe.As<InjectionMemberInfo<TMemberInfo>>(injection.Next);
                }

                // Attribute
                if (ImportType.Attribute != attribute) goto next;

                activate:

                // Use override if provided
                if (null != (@override = GetOverride(in context, in import)))
                    ProcessImport(ref import, @override.Value);

                var result = import.Data.IsValue
                    ? import.Data
                    : Build(ref context, ref import);

                if (result.IsValue) SetValue(Unsafe.As<TDependency>(import.MemberInfo), context.Target!, result.Value);

                // Rewind for the next member
                next: injection = injections;
            }
        }
    }
}