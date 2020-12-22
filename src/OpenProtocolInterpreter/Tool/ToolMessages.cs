using OpenProtocolInterpreter.Messages;
using System;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.Tool
{
    /// <summary>
    /// Template for <see cref="ITool"/> implementers.
    /// </summary>
    internal class ToolMessages : MessagesTemplate
    {
        public ToolMessages() : base()
        {
            _templates = new Dictionary<int, MidCompiledInstance>()
            {
                { Mid0040.MID, new MidCompiledInstance(typeof(Mid0040)) },
                { Mid0041.MID, new MidCompiledInstance(typeof(Mid0041)) },
                { Mid0042.MID, new MidCompiledInstance(typeof(Mid0042)) },
                { Mid0043.MID, new MidCompiledInstance(typeof(Mid0043)) },
                { Mid0044.MID, new MidCompiledInstance(typeof(Mid0044)) },
                { Mid0045.MID, new MidCompiledInstance(typeof(Mid0045)) },
                { Mid0046.MID, new MidCompiledInstance(typeof(Mid0046)) },
                { Mid0047.MID, new MidCompiledInstance(typeof(Mid0047)) },
                { Mid0048.MID, new MidCompiledInstance(typeof(Mid0048)) },
                { Mid0701.MID, new MidCompiledInstance(typeof(Mid0701)) }
            };
        }

        public ToolMessages(IEnumerable<Type> selectedMids) : this()
        {
            FilterSelectedMids(selectedMids);
        }

        public ToolMessages(InterpreterMode mode) : this()
        {
            FilterSelectedMids(mode);
        }

        public override bool IsAssignableTo(int mid) => mid > 39 && mid < 49 || mid == 701;
    }
}
