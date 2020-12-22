using OpenProtocolInterpreter.Converters;
using System.Collections.Generic;
using System.Linq;

namespace OpenProtocolInterpreter.Mode
{
    /// <summary>
    /// Mode ID upload reply
    /// <para>The transmission of all the valid mode IDs of the controller.</para>
    /// <para>Message sent by: Controller</para>
    /// <para>Answer: None</para>
    /// </summary>
    public class Mid2601 : Mid, IMode, IController
    {
        private readonly IValueConverter<int> _intConverter;
        private readonly IValueConverter<IEnumerable<ModeIdDataField>> _modeIdListConverter;
        private const int LAST_REVISION = 1;
        public const int MID = 2601;

        public int TotalModes
        {
            get => GetField(1, (int)DataFields.TOTAL_NUMBER_OF_MODES).GetValue(_intConverter.Convert);
            private set => GetField(1, (int)DataFields.TOTAL_NUMBER_OF_MODES).SetValue(_intConverter.Convert, value);
        }

        public List<ModeIdDataField> Modes { get; set; }

        public Mid2601() : base(MID, LAST_REVISION)
        {
            _intConverter = new Int32Converter();
            _modeIdListConverter = new ModeIdListConverter(_intConverter);
            if (Modes == null)
                Modes = new List<ModeIdDataField>();
        }

        public Mid2601(IEnumerable<ModeIdDataField> modes) : this()
        {
            Modes = modes.ToList();
        }

        public override string Pack()
        {
            TotalModes = Modes.Count;
            var eachModeField = GetField(1, (int)DataFields.MODE_LIST);
            eachModeField.Value = _modeIdListConverter.Convert(Modes);
            eachModeField.Size = eachModeField.Value.Length;
            return base.Pack();
        }

        public override Mid Parse(string package)
        {
            HeaderData = ProcessHeader(package);

            GetField(1, (int)DataFields.MODE_LIST).Size = package.Length - GetField(1, (int)DataFields.MODE_LIST).Index;
            ProcessDataFields(package);
            Modes = _modeIdListConverter.Convert(GetField(1, (int)DataFields.MODE_LIST).Value).ToList();
            return this;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                new DataField((int)DataFields.TOTAL_NUMBER_OF_MODES, 20, 3, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                                new DataField((int)DataFields.MODE_LIST, 23, 0, false)
                            }
                }
            };
        }

        public enum DataFields
        {
            TOTAL_NUMBER_OF_MODES,
            MODE_LIST
        }
    }
}
