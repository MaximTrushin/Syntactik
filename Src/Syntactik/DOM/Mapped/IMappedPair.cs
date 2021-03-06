﻿#region license
// Copyright © 2017 Maxim O. Trushin (trushin@gmail.com)
//
// This file is part of Syntactik.
// Syntactik is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Syntactik is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with Syntactik.  If not, see <http://www.gnu.org/licenses/>.
#endregion

namespace Syntactik.DOM.Mapped
{
    /// <summary>
    /// Represents a parsing information of pairs.
    /// </summary>
    public interface IMappedPair
    {
        /// <summary>
        /// <see cref="Interval"/> used to define name of the pair.
        /// </summary>
        Interval NameInterval { get; }

        /// <summary>
        /// Stores info about quotes used to define name of the pair.
        /// 0 - no quotes
        /// 1 - single quotes
        /// 2 - double quotes
        /// </summary>
        int NameQuotesType { get; }

        /// <summary>
        /// <see cref="Interval"/> used to define literal value of the pair.
        /// </summary>
        Interval ValueInterval { get; }

        /// <summary>
        /// Stores info about quotes used to define value of the pair.
        /// 0 - no quotes
        /// 1 - single quotes
        /// 2 - double quotes
        /// </summary>
        int ValueQuotesType { get; }

        /// <summary>
        /// <see cref="Interval"/> used to define pair assignment.
        /// </summary>
        Interval AssignmentInterval { get; }

        /// <summary>
        /// Type of the pair value.
        /// </summary>
        ValueType ValueType { get; }

        /// <summary>
        /// Type of the pair value.
        /// </summary>
        BlockType BlockType { get; set; }

        /// <summary>
        /// True if pair has a literal value or pair value.
        /// </summary>
        bool IsValueNode { get; }

        /// <summary>
        /// Indent of pair value.
        /// </summary>
        int ValueIndent { get; }
    }
}
