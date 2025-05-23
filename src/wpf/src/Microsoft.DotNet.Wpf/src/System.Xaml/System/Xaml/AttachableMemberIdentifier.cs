// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

namespace System.Xaml
{
    public class AttachableMemberIdentifier : IEquatable<AttachableMemberIdentifier>
    {
        private readonly Type declaringType;
        private readonly string memberName;

        public AttachableMemberIdentifier(Type declaringType, string memberName)
        {
            this.declaringType = declaringType;
            this.memberName = memberName;
        }

        public string MemberName
        {
            get
            {
                return memberName;
            }
        }

        public Type DeclaringType
        {
            get
            {
                return declaringType;
            }
        }

        public static bool operator !=(AttachableMemberIdentifier left, AttachableMemberIdentifier right)
        {
            return !(left == right);
        }

        public static bool operator ==(AttachableMemberIdentifier left, AttachableMemberIdentifier right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AttachableMemberIdentifier);
        }

        public bool Equals(AttachableMemberIdentifier other)
        {
            if (other is null)
            {
                return false;
            }

            return declaringType == other.declaringType && memberName == other.memberName;
        }

        public override int GetHashCode()
        {
            int a = declaringType is null ? 0 : declaringType.GetHashCode();
            int b = memberName is null ? 0 : memberName.GetHashCode();
            return ((a << 5) + a) ^ b;
        }

        public override string ToString()
        {
            if (declaringType is null)
            {
                return memberName;
            }

            return $"{declaringType}.{memberName}";
        }
    }
}
