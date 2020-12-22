using OpenProtocolInterpreter.Mode;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.Converters
{
    internal class ModeIdListConverter : AsciiConverter<IEnumerable<ModeIdDataField>>
    {
        private readonly IValueConverter<int> _intConverter;

        public ModeIdListConverter(IValueConverter<int> intConverter)
        {
            _intConverter = intConverter;
        }

        public override IEnumerable<ModeIdDataField> Convert(string value)
        {
            int modeNameSize = 0;
            for (int i = 0; i < value.Length; i += 6 + modeNameSize)
            {
                modeNameSize = _intConverter.Convert(value.Substring(i + 4, 2));
                yield return new ModeIdDataField()
                {
                    ModeId = _intConverter.Convert(value.Substring(i, 4)),
                    ModeNameSize = modeNameSize,
                    ModeName = value.Substring(i + 6, modeNameSize)
                };
            }
        }

        public override string Convert(IEnumerable<ModeIdDataField> value)
        {
            string pack = string.Empty;
            foreach (var v in value)
            {
                pack += _intConverter.Convert('0', 4, DataField.PaddingOrientations.LEFT_PADDED, v.ModeId);
                pack += _intConverter.Convert('0', 2, DataField.PaddingOrientations.LEFT_PADDED, v.ModeNameSize);
                pack += GetPadded(' ', 1, DataField.PaddingOrientations.RIGHT_PADDED, v.ModeName);
            }
            return pack;
        }

        public override string Convert(char paddingChar, int size, DataField.PaddingOrientations orientation, IEnumerable<ModeIdDataField> value) => Convert(value);
    }
}
