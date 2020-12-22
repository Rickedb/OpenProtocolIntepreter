using OpenProtocolInterpreter.Messages;
using System;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.Mode
{
    /// <summary>
    /// Template for <see cref="IParameterSet"/> implementers.
    /// </summary>
    internal class ModeMessages : MessagesTemplate
    {
        public ModeMessages() : base()
        {
            _templates = new Dictionary<int, MidCompiledInstance>()
            {
                { Mid2600.MID, new MidCompiledInstance(typeof(Mid2600)) },
                { Mid2601.MID, new MidCompiledInstance(typeof(Mid2601)) },
                { Mid2602.MID, new MidCompiledInstance(typeof(Mid2602)) },
                { Mid2603.MID, new MidCompiledInstance(typeof(Mid2603)) },
                { Mid2604.MID, new MidCompiledInstance(typeof(Mid2604)) },
                { Mid2605.MID, new MidCompiledInstance(typeof(Mid2605)) },
                { Mid2606.MID, new MidCompiledInstance(typeof(Mid2606)) }
            };
        }

        public ModeMessages(IEnumerable<Type> selectedMids) : this()
        {
            FilterSelectedMids(selectedMids);
        }

        public ModeMessages(InterpreterMode mode) : this()
        {
            FilterSelectedMids(mode);
        }

        public override bool IsAssignableTo(int mid) => mid >= 2600 && mid <= 2606;
    }
}
