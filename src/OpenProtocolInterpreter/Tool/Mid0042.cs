using OpenProtocolInterpreter.Converters;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.Tool
{
    /// <summary>
    /// Disable tool
    /// <para>Message sent by: Integrator</para>
    /// <para>Answer: <see cref="Communication.Mid0005"/>Command accepted</para>
    /// <para>Answer: <see cref="Communication.Mid0004"/>Command error, with codes:
    /// <list type="table">
    /// <listheader>
    /// <term>Code</term>
    /// <description>Error description</description>
    /// </listheader>
    /// <item>
    /// <term>29</term>
    /// <description>Tool does not exist</description>
    /// </item>
    /// <item>
    /// <term>36</term>
    /// <description>Lock type not supported</description>
    /// </item>
    /// </list>
    /// </para>
    /// </summary>
    public class Mid0042 : Mid, ITool, IIntegrator
    {
        private readonly IValueConverter<int> _intConverter;
        private const int LAST_REVISION = 2;
        public const int MID = 42;

        public int ToolNumber
        {
            get => GetField(2, (int)DataFields.TOOL_NUMBER).GetValue(_intConverter.Convert);
            private set => GetField(2, (int)DataFields.TOOL_NUMBER).SetValue(_intConverter.Convert, value);
        }

        public DisableToolType DisableType
        {
            get => (DisableToolType)GetField(2, (int)DataFields.DISABLE_TYPE).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.DISABLE_TYPE).SetValue(_intConverter.Convert, (int)value);
        }

        public Mid0042() : base(MID, LAST_REVISION)
        {
        }

        public Mid0042(int revision = LAST_REVISION) : base(MID, revision)
        {
            _intConverter = new Int32Converter();
        }

        /// <summary>
        /// Revision 2 Constructor
        /// </summary>
        /// <param name="toolNumber">The number of the tool to disable</param>
        /// <param name="disableType">The type of disable</param>
        /// <param name="revision">Revision</param>
        public Mid0042(int toolNumber, DisableToolType disableType, int revision) : this(revision)
        {
            ToolNumber = toolNumber;
            DisableType = disableType;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                { 1, new List<DataField>() },
                {
                    2, new List<DataField>()
                            {
                                new DataField((int)DataFields.TOOL_NUMBER, 20, 4, '0', DataField.PaddingOrientations.LEFT_PADDED, true),
                                new DataField((int)DataFields.DISABLE_TYPE, 26, 2, '0', DataField.PaddingOrientations.LEFT_PADDED, true)
                            }
                }
            };
        }

        public enum DataFields
        {
            //Revision 2
            TOOL_NUMBER,
            DISABLE_TYPE,
        }
    }
}
