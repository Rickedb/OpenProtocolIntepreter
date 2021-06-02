using OpenProtocolInterpreter.Job.Advanced;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.Converters
{
    public class AdvancedJobListConverter : AsciiConverter<IEnumerable<AdvancedJob>>
    {
        private readonly IValueConverter<int> _intConverter;
        private readonly IValueConverter<bool> _boolConverter;
        private readonly int _revision;

        public AdvancedJobListConverter(IValueConverter<int> intConverter, IValueConverter<bool> boolConverter, int revision)
        {
            _intConverter = intConverter;
            _boolConverter = boolConverter;
            _revision = revision;
        }

        public override string Convert(IEnumerable<AdvancedJob> value)
        {
            List<string> advancedJobsList = new List<string>();

            foreach(var job in value)
            {
                List<string> fields = new List<string>
                {
                    _intConverter.Convert('0', 2, DataField.PaddingOrientations.LEFT_PADDED, job.ChannelId),
                    _intConverter.Convert('0', 3, DataField.PaddingOrientations.LEFT_PADDED, job.ProgramId),
                    _boolConverter.Convert(job.AutoSelect),
                    _intConverter.Convert('0', 2, DataField.PaddingOrientations.LEFT_PADDED, job.BatchSize),
                    _intConverter.Convert('0', 2, DataField.PaddingOrientations.LEFT_PADDED, job.MaxCoherentNok)
                };

                if (_revision == 999)
                    fields.Add(_intConverter.Convert('0', 2, DataField.PaddingOrientations.LEFT_PADDED, job.BatchCounter));
                advancedJobsList.Add(string.Join(":", fields));
            }

            return string.Join(";", advancedJobsList) + ";";
        }

        public override string Convert(char paddingChar, int size, DataField.PaddingOrientations orientation, IEnumerable<AdvancedJob> value) => Convert(value);

        public override IEnumerable<AdvancedJob> Convert(string value)
        {
            var list = value.Split(';');
            foreach(var advancedJob in list)
            {
                var fields = advancedJob.Split(':');
                yield return new AdvancedJob()
                {
                    ChannelId = _intConverter.Convert(fields[0]),
                    ProgramId = _intConverter.Convert(fields[1]),
                    AutoSelect = _boolConverter.Convert(fields[2]),
                    BatchSize = _intConverter.Convert(fields[3]),
                    MaxCoherentNok = _intConverter.Convert(fields[4]),
                    BatchCounter = _revision == 999 ? _intConverter.Convert(fields[5]) : 0
                };
            }
        }
    }
}
