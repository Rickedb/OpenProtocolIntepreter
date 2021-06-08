using OpenProtocolInterpreter.Messages;
using System;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.TighteningResults
{
   /// <summary>
   /// Template for <see cref="ITighteningResults"/> implementers.
   /// </summary>
   internal class TighteningResultMessages : MessagesTemplate
   {
      public TighteningResultMessages() : base()
      {
         _templates = new Dictionary<int, MidCompiledInstance>()
            {
                { Mid0900.MID, new MidCompiledInstance(typeof(Mid0900)) },
            };
      }

      public TighteningResultMessages(IEnumerable<Type> selectedMids) : this()
      {
         FilterSelectedMids(selectedMids);
      }

      public TighteningResultMessages(InterpreterMode mode) : this()
      {
         FilterSelectedMids(mode);
      }

      public override bool IsAssignableTo(int mid) => mid > 899 && mid < 902;
   }
}
