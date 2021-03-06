#region license
// Copyright � 2017 Maxim O. Trushin (trushin@gmail.com)
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
using System.Text;
using Syntactik.DOM;
using Syntactik.DOM.Mapped;
using Alias = Syntactik.DOM.Alias;
using AliasDefinition = Syntactik.DOM.AliasDefinition;
using Argument = Syntactik.DOM.Argument;
using Attribute = Syntactik.DOM.Attribute;
using Document = Syntactik.DOM.Document;
using Element = Syntactik.DOM.Element;
using Module = Syntactik.DOM.Module;
using NamespaceDefinition = Syntactik.DOM.NamespaceDefinition;
using Parameter = Syntactik.DOM.Parameter;
using Scope = Syntactik.DOM.Scope;


namespace TestEditor
{
    public class DomPrinter: SyntactikDepthFirstVisitor
    {
        private readonly Stack<bool> _valueNodeExpected = new Stack<bool>();
        private readonly StringBuilder _sb = new StringBuilder();
        private int _indent;

        public string Text => _sb.ToString();

        public DomPrinter()
        {
            _valueNodeExpected.Push(false);
        }


        public override void Visit(Module pair)
        {
            PrintNodeName(pair);
            PrintNodeStart(pair);
            base.Visit(pair);
            PrintNodeEnd(pair);
        }

        public override void Visit(Document pair)
        {
            PrintNodeName(pair);
            PrintNodeStart(pair);
            base.Visit(pair);
            PrintNodeEnd(pair);

        }

        public override void Visit(Element pair)
        {
            PrintNodeName(pair);
            PrintNodeStart(pair);
            base.Visit(pair);
            PrintNodeEnd(pair);
        }



        public override void Visit(Attribute pair)
        {
            PrintNodeName(pair);
            PrintNodeStart(pair);
            base.Visit(pair);
            PrintNodeEnd(pair);
        }

        public override void Visit(Alias pair)
        {
            PrintNodeName(pair);
            PrintNodeStart(pair);
            base.Visit(pair);
            PrintNodeEnd(pair);
        }
        public override void Visit(Argument pair)
        {
            PrintNodeName(pair);
            PrintNodeStart(pair);
            base.Visit(pair);
            PrintNodeEnd(pair);
        }
        public override void Visit(Parameter pair)
        {
            PrintNodeName(pair);
            PrintNodeStart(pair);
            base.Visit(pair);
            PrintNodeEnd(pair);
        }

        public override void Visit(AliasDefinition aliasDefinition)
        {
            PrintNodeName(aliasDefinition);
            PrintNodeStart(aliasDefinition);
            base.Visit(aliasDefinition);
            PrintNodeEnd(aliasDefinition);
        }

        public override void Visit(NamespaceDefinition pair)
        {
            PrintNodeName(pair);
            PrintNodeStart(pair);
            base.Visit(pair);
            PrintNodeEnd(pair);
        }

        public override void Visit(Scope pair)
        {
            PrintNodeName(pair);
            PrintNodeStart(pair);
            base.Visit(pair);
            PrintNodeEnd(pair);
        }

        private void PrintValue(Pair pair)
        {
            _sb.Append(pair.Value.Replace("\r\n", "\n").Replace("\n", "\\n").Replace("\t", "\\t"));
        }

        private void PrintNodeName(Pair pair)
        {
            if (_valueNodeExpected.Peek())
            {
                _sb.Append("\t");
                _sb.Append(pair.GetType().Name);
                _sb.Append(" ");
                _sb.Append(((IMappedPair)pair).NameQuotesType > 0 ? (char)((IMappedPair)pair).NameQuotesType : '`');
                PrintNsPrefix(pair);
                _sb.Append(pair.Name);
                _sb.Append(((IMappedPair)pair).NameQuotesType > 0 ? (char)((IMappedPair)pair).NameQuotesType : '`');
            }
            else
            {
                var mappedPair = pair as IMappedPair;
                _sb.Append(mappedPair?.NameInterval.Begin.Line.ToString().PadLeft(2, '0') ?? "00");
                _sb.Append(":");
                _sb.Append(mappedPair?.NameInterval.Begin.Column.ToString().PadLeft(2, '0') ?? "00");
                _sb.Append('\t', _indent);
                _sb.Append("\t");
                _sb.Append(pair.GetType().Name);
                _sb.Append(" ");
                _sb.Append(((IMappedPair)pair).NameQuotesType > 0 ? (char)((IMappedPair)pair).NameQuotesType : '`');
                PrintNsPrefix(pair);
                _sb.Append(pair.Name);
                _sb.Append(((IMappedPair)pair).NameQuotesType > 0 ? (char)((IMappedPair)pair).NameQuotesType : '`');
            }
        }

        private void PrintNsPrefix(Pair pair)
        {
            if (!string.IsNullOrEmpty((pair as INsNode)?.NsPrefix))
            {
                _sb.Append(((INsNode)pair).NsPrefix);
                _sb.Append(".");
            }
        }

        private void PrintNodeEnd(Pair pair)
        {
            _valueNodeExpected.Pop();

            if (pair.Value != null)
            {
                _sb.AppendLine();
            }
            else if (pair.PairValue != null)
            {
            }
            else
            {
                _indent--;
            }
        }

        private void PrintNodeStart(Pair pair)
        {
            if (pair.Value != null)
            {
                _sb.Append(AssignmentToString(pair.Assignment));
                _sb.Append(" ");
                _sb.Append(((IMappedPair)pair).ValueQuotesType > 0 ? (char)((IMappedPair)pair).ValueQuotesType : '`');
                PrintValue(pair);
                _sb.Append(((IMappedPair)pair).ValueQuotesType > 0 ? (char)((IMappedPair)pair).ValueQuotesType : '`');
            }
            else if (pair.PairValue != null)
            {
                _sb.Append(":= ");
                _valueNodeExpected.Push(true);
                Visit(pair.PairValue);
                _valueNodeExpected.Pop();
            }
            else
            {
                _sb.AppendLine(AssignmentToString(pair.Assignment));
                _indent++;
            }
            _valueNodeExpected.Push(false);
        }
        /// <summary>
        /// Auxiliary function to convert assignment value to its string representation.
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        internal static string AssignmentToString(AssignmentEnum assignment)
        {
            switch (assignment)
            {
                case AssignmentEnum.C:
                    return ":";
                case AssignmentEnum.CC:
                    return "::";
                case AssignmentEnum.CCC:
                    return ":::";
                case AssignmentEnum.E:
                    return "=";
                case AssignmentEnum.EC:
                    return "=:";
                case AssignmentEnum.ECC:
                    return "=::";
                case AssignmentEnum.EE:
                    return "==";
                case AssignmentEnum.CE:
                    return ":=";
            }
            return "";
        }
    }
}