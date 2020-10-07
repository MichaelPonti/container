﻿using System;
using System.Reflection;

namespace Unity.Injection
{
    public class InjectionField : InjectionMemberInfo<FieldInfo>
    {
        #region Constructors

        /// <summary>
        /// Configures the container to inject a specified field with a resolved value.
        /// </summary>
        /// <param name="field">Name of field to inject.</param>
        public InjectionField(string field)
            : base(field)
        {
        }

        [Obsolete("Use OptionalField(...)", true)]
        public InjectionField(string field, bool optional)
            : base(field) => throw new NotSupportedException();

        public InjectionField(string field, Type type)
            : base(field, type)
        {
        }

        public InjectionField(string field, Type type, string? name)
            : base(field, type, name)
        {
        }

        /// <summary>
        /// Configures the container to inject the given field with provided value.
        /// </summary>
        /// <param name="field">Name of the field to inject.</param>
        /// <param name="value">Value to be injected into the field</param>
        public InjectionField(string field, object value)
            : base(field, value)
        {
        }

        #endregion
    }
}
