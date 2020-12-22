using OpenProtocolInterpreter.MultiSpindle;
using System;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.Converters
{
    internal class SpindleOrPressStatusListConverter : AsciiConverter<IEnumerable<SpindleOrPressStatus>>
    {
        private readonly IValueConverter<decimal> _decimalConverter;
        private readonly IValueConverter<int> _intConverter;
        private readonly IValueConverter<bool> _boolConverter;

        public SpindleOrPressStatusListConverter(IValueConverter<int> intConverter, IValueConverter<bool> boolConverter, IValueConverter<decimal> decimalConverter)
        {
            _intConverter = intConverter;
            _boolConverter = boolConverter;
            _decimalConverter = decimalConverter;
        }

        public override IEnumerable<SpindleOrPressStatus> Convert(string value)
        {
            for (int i = 0; i < value.Length; i += 18)
            {
                var spindleValue = value.Substring(i, 18);
                yield return new SpindleOrPressStatus()
                {
                    SpindleOrPressNumber = _intConverter.Convert(spindleValue.Substring(0, 2)),
                    ChannelId = _intConverter.Convert(spindleValue.Substring(2, 2)),
                    OverallStatus = _boolConverter.Convert(spindleValue.Substring(4, 1)),
                    TorqueOrForceStatus = (TighteningValueStatus)_intConverter.Convert(spindleValue.Substring(5,1)),
                    TorqueOrForce = _decimalConverter.Convert(spindleValue.Substring(6, 6)),
                    AngleOrStrokeStatus = _boolConverter.Convert(spindleValue.Substring(12, 1)),
                    AngleOrStroke = _intConverter.Convert(spindleValue.Substring(13,5))
                };
            }
        }

        public override string Convert(IEnumerable<SpindleOrPressStatus> value)
        {
            string package = string.Empty;
            foreach (var v in value)
            {
                package += _intConverter.Convert('0', 2, DataField.PaddingOrientations.LEFT_PADDED, v.SpindleOrPressNumber) +
                           _intConverter.Convert('0', 2, DataField.PaddingOrientations.LEFT_PADDED, v.ChannelId) +
                           _boolConverter.Convert(v.OverallStatus) +
                           _intConverter.Convert((int)v.TorqueOrForceStatus) +
                           _decimalConverter.Convert('0', 6, DataField.PaddingOrientations.LEFT_PADDED, v.TorqueOrForce) +
                           _boolConverter.Convert(v.AngleOrStrokeStatus) +
                           _intConverter.Convert('0', 5, DataField.PaddingOrientations.LEFT_PADDED, v.AngleOrStroke);
            }

            return package;
        }

        public override string Convert(char paddingChar, int size, DataField.PaddingOrientations orientation, IEnumerable<SpindleOrPressStatus> value)
        {
            throw new NotImplementedException();
        }
    }
}
