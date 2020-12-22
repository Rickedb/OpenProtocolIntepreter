using OpenProtocolInterpreter.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenProtocolInterpreter.Mode
{
    /// <summary>
    /// Mode selected
    /// <para>
    ///     A new mode is selected in the controller. The message includes the ID of the mode selected as well as the date and time of the last change in the parameter set settings.
    ///     Use <see cref="Communication.Mid0008"/> to start subscription. Note that the immediate response is <see cref="Communication.Mid0005"/> Command accepted
    ///     and <see cref="Mid2604"/> Mode selected.
    /// </para>
    /// <para>Message sent by: Controller</para>
    /// <para>Answer: <see cref="ParameterSet.Mid0016"/> New parameter set selected acknowledge</para>
    /// </summary>
    public class Mid2604 : Mid, IMode, IController
    {
        private readonly IValueConverter<int> _intConverter;
        private readonly IValueConverter<DateTime> _datetimeConverter;
        private const int LAST_REVISION = 1;
        public const int MID = 2604;

        public int ModeId
        {
            get => GetField(HeaderData.Revision, (int)DataFields.MODE_ID).GetValue(_intConverter.Convert);
            set => GetField(HeaderData.Revision, (int)DataFields.MODE_ID).SetValue(_intConverter.Convert, value);
        }
        public DateTime LastChangeInMode
        {
            get => GetField(HeaderData.Revision, (int)DataFields.LAST_CHANGE_IN_MODE).GetValue(_datetimeConverter.Convert);
            set => GetField(HeaderData.Revision, (int)DataFields.LAST_CHANGE_IN_MODE).SetValue(_datetimeConverter.Convert, value);
        }
        public int NoBolts
        {
            get => GetField(HeaderData.Revision, (int)DataFields.NUMBER_OF_BOLTS).GetValue(_intConverter.Convert);
            set => GetField(HeaderData.Revision, (int)DataFields.NUMBER_OF_BOLTS).SetValue(_intConverter.Convert, value);
        }

        public Mid2604() : this(LAST_REVISION)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ackFlag">0=Ack needed, 1=No Ack needed (Default = 1)</param>
        /// <param name="revision">Range: 000-002</param>
        public Mid2604(int revision = LAST_REVISION, int? noAckFlag = 0) : base(MID, revision, noAckFlag)
        {
            _intConverter = new Int32Converter();
            _datetimeConverter = new DateConverter();
        }

        /// <summary>
        /// Revision 1 Constructor
        /// </summary>
        /// <param name="modeId">Four ASCII digits, range 0000-9999</param>
        /// <param name="lastChangeInMode">19 ASCII characters. YYYY-MM-DD:HH:MM:SS</param>
        /// <param name="noBolts">Number of bolts in the mode, range 000-999</param>
        /// <param name="ackFlag">0=Ack needed, 1=No Ack needed (Default = 1)</param>
        public Mid2604(int modeId, DateTime lastChangeInMode, int noBolts, int? noAckFlag = 0, int revision = 1)
            : this(revision, noAckFlag)
        {
            ModeId = modeId;
            LastChangeInMode = lastChangeInMode;
            NoBolts = noBolts;
        }

        protected override string BuildHeader()
        {
            HeaderData.Length = 20;
            foreach (var dataField in RevisionsByFields[HeaderData.Revision])
                HeaderData.Length += (dataField.HasPrefix ? 2 : 0) + dataField.Size;

            return HeaderData.ToString();
        }

        public override string Pack()
        {
            int index = 1;
            return BuildHeader() + base.Pack(RevisionsByFields[HeaderData.Revision], ref index);
        }

        public override Mid Parse(string package)
        {
            HeaderData = ProcessHeader(package);
            HeaderData.Revision = HeaderData.Revision > 0 ? HeaderData.Revision : 1;
            ProcessDataFields(RevisionsByFields[HeaderData.Revision], package);
            return this;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                new DataField((int)DataFields.MODE_ID, 20, 4, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                                new DataField((int)DataFields.LAST_CHANGE_IN_MODE, 24, 19, false),
                                new DataField((int)DataFields.NUMBER_OF_BOLTS, 43, 3, '0', DataField.PaddingOrientations.LEFT_PADDED, false)
                            }
                }
            };
        }

        /// <summary>
        /// Validate all fields size
        /// </summary>
        public bool Validate(out IEnumerable<string> errors)
        {
            List<string> failed = new List<string>();
            if (ModeId < 0 || ModeId > 9999)
                failed.Add(new ArgumentOutOfRangeException(nameof(ModeId), "Range: 0000-9999").Message);
            if (NoBolts < 0 || ModeId > 999)
                failed.Add(new ArgumentOutOfRangeException(nameof(NoBolts), "Range: 000-999").Message);

            errors = failed;
            return errors.Any();
        }

        public enum DataFields
        {
            MODE_ID,
            LAST_CHANGE_IN_MODE,
            NUMBER_OF_BOLTS
        }
    }
}
