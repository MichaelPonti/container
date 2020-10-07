﻿using System;
using System.Reflection;

namespace Unity.Injection
{
    /// <summary>
    /// This class stores information about which properties to inject,
    /// and will configure the container accordingly.
    /// </summary>
    public class InjectionProperty : InjectionMemberInfo<PropertyInfo>
    {

        #region Constructors

        /// <summary>
        /// Configures the container to inject a specified property with a resolved value.
        /// </summary>
        /// <param name="property">Name of property to inject.</param>
        public InjectionProperty(string property)
            : base(property)
        {}

        [Obsolete("Use OptionalProperty(...)", true)]
        public InjectionProperty(string property, bool optional)
            : base(property) => throw new NotSupportedException();

        public InjectionProperty(string property, Type type)
            : base(property, type)
        {
        }

        public InjectionProperty(string property, Type type, string? name)
            : base(property, type, name)
        {
        }

        /// <summary>
        /// Configures the container to inject the given property with the provided value
        /// </summary>
        /// <param name="property">Name of property to inject.</param>
        /// <param name="value">Value to be injected into the property.</param>
        public InjectionProperty(string property, object value)
            : base(property, value)
        {
        }

        #endregion
    }
}
