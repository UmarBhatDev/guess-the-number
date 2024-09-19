using System;

namespace GTN.Features.SceneTransitions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeCurtainType : Attribute
    {
        public CurtainType CurtainType { get; }

        public AttributeCurtainType(CurtainType curtainType)
        {
            CurtainType = curtainType;
        }
    }
}