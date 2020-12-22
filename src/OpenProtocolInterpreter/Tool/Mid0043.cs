using OpenProtocolInterpreter.Converters;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.Tool
{
    /// <summary>
    /// Enable tool
    /// <para>Message sent by: Integrator</para>
    /// <para>Answer: <see cref="Communication.Mid0005"/>Command accepted</para>
    /// <para>Answer: <see cref="Communication.Mid0004"/>Command error, with code:
    /// <list type="table">
    /// <listheader>
    /// <term>Code</term>
    /// <description>Error description</description>
    /// </listheader>
    /// <item>
    /// <term>29</term>
    /// <description>Tool does not exist</description>
    /// </item>
    /// </list>
    /// </para>
    /// </summary>
    public class Mid0043 : Mid, ITool, IIntegrator
    {
        private readonly IValueConverter<int> _intConverter;
        private const int LAST_REVISION = 2;
        public const int MID = 43;

        public int ToolNumber
        {
            get => GetField(2, (int)DataFields.TOOL_NUMBER).GetValue(_intConverter.Convert);
            private set => GetField(2, (int)DataFields.TOOL_NUMBER).SetValue(_intConverter.Convert, value);
        }

        public Mid0043() : base(MID, LAST_REVISION)
        {
        }

        public Mid0043(int revision = LAST_REVISION) : base(MID, revision)
        {
            _intConverter = new Int32Converter();
        }

        /// <summary>
        /// Revision 2 Constructor
        /// </summary>
        /// <param name="toolNumber">The number of the tool to disable</param>
        /// <param name="revision">Revision</param>
        public Mid0043(int toolNumber, int revision) : this(revision)
        {
            ToolNumber = toolNumber;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                { 1, new List<DataField>() },
                {
                    2, new List<DataField>()
                            {
                                new DataField((int)DataFields.TOOL_NUMBER, 20, 4, '0', DataField.PaddingOrientations.LEFT_PADDED, true)
                            }
                }
            };
        }

        public enum DataFields
        {
            //Revision 2
            TOOL_NUMBER,
        }
    }
}
