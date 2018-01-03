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
namespace Syntactik.DOM
{
    /// <summary>
    /// Represents an Element.
    /// </summary>
    public class Element : Entity, INsNode, IContainer
    {
        private PairCollection<Entity> _entities;

        /// <inheritdoc />
        public override void Accept(IDomVisitor visitor)
        {
            visitor.OnElement(this);
        }

        /// <inheritdoc />
        public override void AppendChild(Pair child)
        {
            Value = null;
            PairValue = null;

            if (child is Entity item)
            {
                Entities.Add(item);
            }
            else
            {
                base.AppendChild(child);
            }
        }


        /// <inheritdoc />
        public virtual PairCollection<Entity> Entities
        {
            get => _entities ?? (_entities = new PairCollection<Entity>(this));
            set
            {
                if (value == _entities) return;

                value?.InitializeParent(this);
                _entities = value;
            }
        }

        /// <summary>
        /// Namespace prefix of the element.
        /// </summary>
        public virtual string NsPrefix { get; set; }
    }
}
