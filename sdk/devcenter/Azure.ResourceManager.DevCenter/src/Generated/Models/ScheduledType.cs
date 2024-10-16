// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace Azure.ResourceManager.DevCenter.Models
{
    /// <summary> The supported types for a scheduled task. </summary>
    public readonly partial struct ScheduledType : IEquatable<ScheduledType>
    {
        private readonly string _value;

        /// <summary> Initializes a new instance of <see cref="ScheduledType"/>. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public ScheduledType(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string StopDevBoxValue = "StopDevBox";

        /// <summary> StopDevBox. </summary>
        public static ScheduledType StopDevBox { get; } = new ScheduledType(StopDevBoxValue);
        /// <summary> Determines if two <see cref="ScheduledType"/> values are the same. </summary>
        public static bool operator ==(ScheduledType left, ScheduledType right) => left.Equals(right);
        /// <summary> Determines if two <see cref="ScheduledType"/> values are not the same. </summary>
        public static bool operator !=(ScheduledType left, ScheduledType right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="ScheduledType"/>. </summary>
        public static implicit operator ScheduledType(string value) => new ScheduledType(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is ScheduledType other && Equals(other);
        /// <inheritdoc />
        public bool Equals(ScheduledType other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
