﻿namespace Unity.BuiltIn
{
    public partial class FieldProcessor
    {
        public override object? Activate(ref MemberDependency dependency, object? data)
        {
            dependency.Contract = (null == dependency.Import)
                ? new Contract(dependency.Info.FieldType)
                : new Contract(dependency.Import.ContractType ?? dependency.Info.FieldType,
                               dependency.Import.ContractName);

            var @override = dependency.GetOverride();

            var value = (null != @override)
                ? dependency.GetValue(@override.Value)
                : dependency.GetValue(data);

            if (!dependency.Parent.IsFaulted) dependency.Info.SetValue(dependency.Parent.Target, value);

            return value;
        }

        public override object? Activate(ref MemberDependency dependency)
        {
            dependency.Contract = (null == dependency.Import)
                ? new Contract(dependency.Info.FieldType)
                : new Contract(dependency.Import.ContractType ?? dependency.Info.FieldType,
                               dependency.Import.ContractName);

            var value = dependency.Parent
                                  .Container
                                  .Resolve(ref dependency.Contract, ref dependency.Parent);

            if (!dependency.Parent.IsFaulted)
            { 
                dependency.Info.SetValue(dependency.Parent.Target, value);
            }

            return value;
        }
    }
}