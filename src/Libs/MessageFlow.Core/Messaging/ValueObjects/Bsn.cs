using MessageFlow.SharedKernel.GuardClauses;

namespace MessageFlow.Core.Messaging.ValueObjects
{
    public class Bsn
    {
        public string Value { get; }
        public Bsn(string value)
        {
            Guard.Against.InvalidBsn(value);
            Value = value;
        }

        public override string ToString() => Value;
        public override bool Equals(object? obj) => obj is Bsn bsn && bsn.Value == Value;
        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator string(Bsn bsn) => bsn.Value;
        public static implicit operator Bsn(string bsn) => new(bsn);

        public static bool operator ==(Bsn left, Bsn right) => left.Value == right.Value;
        public static bool operator !=(Bsn left, Bsn right) => left.Value != right.Value;
    }
}
