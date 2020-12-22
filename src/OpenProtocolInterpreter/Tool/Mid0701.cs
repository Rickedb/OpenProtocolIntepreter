using OpenProtocolInterpreter.Converters;
using System.Collections.Generic;
using System.Linq;

namespace OpenProtocolInterpreter.Tool
{
    /// <summary>
    /// Tool list upload reply
    /// <para>Upload a list of connected tools from controller.</para>
    /// <para>Message sent by: Controller</para>
    /// <para>Answer: None</para>
    /// </summary>
    public class Mid0701 : Mid, ITool, IController
    {
        private readonly IValueConverter<int> _intConverter;
        private readonly IValueConverter<IEnumerable<ToolData>> _toolListConverter;
        private const int LAST_REVISION = 1;
        public const int MID = 701;

        public int NumberOfTools
        {
            get => GetField(1, (int)DataFields.TOTAL_NUMBER_OF_TOOLS).GetValue(_intConverter.Convert);
            private set => GetField(1, (int)DataFields.TOTAL_NUMBER_OF_TOOLS).SetValue(_intConverter.Convert, value);
        }

        public List<ToolData> Tools { get; set; }

        public Mid0701() : base(MID, LAST_REVISION)
        {
            _intConverter = new Int32Converter();
            _toolListConverter = new ToolListConverter(_intConverter);
            if (Tools == null)
                Tools = new List<ToolData>();
        }

        public Mid0701(IEnumerable<ToolData> modes) : this()
        {
            Tools = modes.ToList();
        }

        public override string Pack()
        {
            NumberOfTools = Tools.Count;
            var eachToolField = GetField(1, (int)DataFields.EACH_TOOL);
            eachToolField.Value = _toolListConverter.Convert(Tools);
            eachToolField.Size = eachToolField.Value.Length;
            return base.Pack();
        }

        public override Mid Parse(string package)
        {
            HeaderData = ProcessHeader(package);

            GetField(1, (int)DataFields.EACH_TOOL).Size = package.Length - GetField(1, (int)DataFields.EACH_TOOL).Index - 1;
            ProcessDataFields(package);
            Tools = _toolListConverter.Convert(GetField(1, (int)DataFields.EACH_TOOL).Value).ToList();
            return this;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                new DataField((int)DataFields.TOTAL_NUMBER_OF_TOOLS, 20, 3, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                                new DataField((int)DataFields.EACH_TOOL, 23, 94, false)
                            }
                }
            };
        }

        public enum DataFields
        {
            TOTAL_NUMBER_OF_TOOLS,
            EACH_TOOL
        }
    }
}
