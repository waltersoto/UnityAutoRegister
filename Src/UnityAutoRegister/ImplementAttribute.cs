using System;
namespace UnityAutoRegister
{
    public class ImplementAttribute : Attribute
    {
        public ImplementAttribute(Type fromType)
        {
            if (fromType == null)
            {
                 throw new ArgumentNullException(nameof(fromType));
            }

            FromType = fromType;
        }

        public Type FromType { get; protected set; }
    }
}
