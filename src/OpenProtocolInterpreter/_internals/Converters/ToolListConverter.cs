using OpenProtocolInterpreter.Mode;
using OpenProtocolInterpreter.Tool;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.Converters
{
    internal class ToolListConverter : AsciiConverter<IEnumerable<ToolData>>
    {
        private readonly IValueConverter<int> _intConverter;

        public ToolListConverter(IValueConverter<int> intConverter)
        {
            _intConverter = intConverter;
        }

        public override IEnumerable<ToolData> Convert(string value)
        {
            for (int i = 0; i < value.Length; i += 94)
            {
                yield return new ToolData()
                {
                    ToolNumber = _intConverter.Convert(value.Substring(i, 4)),
                    ToolSerialNumber = value.Substring(i + 4, 30),
                    ToolModelName = value.Substring(i + 34, 30),
                    ToolModelArticleNumber = value.Substring(i + 64, 30)
                };
            }
        }

        public override string Convert(IEnumerable<ToolData> value)
        {
            string pack = string.Empty;
            foreach (var v in value)
            {
                pack += _intConverter.Convert('0', 4, DataField.PaddingOrientations.LEFT_PADDED, v.ToolNumber);
                pack += GetPadded(' ', 30, DataField.PaddingOrientations.RIGHT_PADDED, v.ToolSerialNumber);
                pack += GetPadded(' ', 30, DataField.PaddingOrientations.RIGHT_PADDED, v.ToolModelName);
                pack += GetPadded(' ', 30, DataField.PaddingOrientations.RIGHT_PADDED, v.ToolModelArticleNumber);
            }
            return pack;
        }

        public override string Convert(char paddingChar, int size, DataField.PaddingOrientations orientation, IEnumerable<ToolData> value) => Convert(value);
    }
}
