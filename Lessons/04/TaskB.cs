﻿using System;
using Xunit;

namespace Lessons._04
{
    /// <summary>
    /// Write tests for all possible cases. 
    /// Each of possible exceptions should have a test case.
    /// Implement the method ParseUnsignedInteger.
    /// </summary>
    public class TaskB
    {
        [Fact]
        public void ParseUnsignedInteger_WhenNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ParseUnsignedInteger(null));
        }

        [Fact]
        public void ParseUnsignedInteger_WhenIncorrectFormat_ShouldThrowFormatException()
        {
            Assert.Throws<FormatException>(() => ParseUnsignedInteger("My name is Inigo Montoya. Prepare to be parsed"));
        }

        [Fact]
        public void ParseUnsignedInteger_WhenNumberIsLessThanZero_ShouldThrowOverflowException()
        {
            Assert.Throws<OverflowException>(() => ParseUnsignedInteger("-1"));
        }

        [Fact]
        public void ParseUnsignedInteger_WhenNumberIsMoreThanUint32Max_ShouldThrowOverflowException()
        {
            Assert.Throws<OverflowException>(() => ParseUnsignedInteger("4294967296"));
        }

        [Fact]
        public void ParseUnsignedInteger_WhenNumberIsJustRight_ShouldReturnNumber()
        {
            Assert.True(ParseUnsignedInteger("5") == 5);
        }

        ///
        /// Summary:
        ///     Converts the string representation of a number to its 32-bit unsigned integer
        ///     equivalent.
        ///
        /// Parameters:
        ///   s:
        ///     A string representing the number to convert.
        ///
        /// Returns:
        ///     A 32-bit unsigned integer equivalent to the number contained in s.
        ///
        /// Exceptions:
        ///   T:System.ArgumentNullException:
        ///     The s parameter is null.
        ///
        ///   T:System.FormatException:
        ///     The s parameter is not of the correct format.
        ///
        ///   T:System.OverflowException:
        ///     The s parameter represents a number that is less than System.UInt32.MinValue
        ///     or greater than System.UInt32.MaxValue.
        public uint ParseUnsignedInteger(string value)
        {
            if (value == null) {
                throw new ArgumentNullException();
            }

            return Convert.ToUInt32(value);
        }
    }
}