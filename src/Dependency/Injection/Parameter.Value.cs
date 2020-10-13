﻿using System;
using System.Reflection;
using Unity.Container;

namespace Unity.Injection
{
    /// <summary>
    /// Base type for objects that are used to configure parameters for
    /// constructor or method injection, or for getting the value to
    /// be injected into a property.
    /// </summary>
    public abstract class ParameterValue : IInjectionInfoProvider<ParameterInfo>, 
                                           IMatch<ParameterInfo>, 
                                           IMatch<Type>
    {
        public abstract InjectionInfo<ParameterInfo> GetInfo(ParameterInfo member);

        /// <summary>
        /// Checks if this parameter is compatible with the <see cref="ParameterInfo"/>
        /// </summary>
        /// <param name="other"><see cref="ParameterInfo"/> to compare to</param>
        /// <returns>True if <see cref="ParameterInfo"/> is compatible</returns>
        public abstract MatchRank Match(ParameterInfo parameter);

        /// <summary>
        /// Checks if this parameter is compatible with the type
        /// </summary>
        /// <param name="type"><see cref="Type"/> to compare to</param>
        /// <returns>True if <see cref="Type"/> is equal</returns>
        public abstract MatchRank Match(Type type);

    }
}