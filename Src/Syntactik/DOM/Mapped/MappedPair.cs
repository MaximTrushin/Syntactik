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
using System.Collections.Generic;

namespace Syntactik.DOM.Mapped
{
    internal class MappedPair: IMappedPair 
    {
        public string Name { get; set; }
        public Pair PairValue { get; set; }
        public string Value { get; set; }
        public AssignmentEnum Assignment { get; set; }

        public static MappedPair EmptyPair { get; } = new MappedPair{ Name = "EmptyPair" };

        public Interval NameInterval { get; set; }
        public int NameQuotesType { get; set; }
        public Interval ValueInterval { get; set; }
        public int ValueQuotesType { get; set; }
        public Interval AssignmentInterval { get; set; }
        public ValueType ValueType { get; set; }
        public BlockType BlockType { get; set; }
        public bool IsValueNode { get; set; }
        public bool MissingNameQuote { get; set; }
        public bool MissingValueQuote { get; set; }
        public List<object> InterpolationItems => null;
        public int ValueIndent { get; } = 0;
    }
}
