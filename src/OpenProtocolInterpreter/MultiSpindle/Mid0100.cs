using OpenProtocolInterpreter.Converters;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.MultiSpindle
{
    /// <summary>
    /// Multi-spindle result subscribe
    /// <para>
    ///     A subscription for the multi-spindle status. For Power Focus, the subscription must 
    ///     be addressed to a sync Master. 
    /// </para>    
    /// <para>
    ///     This telegram is also used for a PowerMACS 4000 system 
    ///     running a press instead of a spindle. A press system only supports revision 4 and higher 
    ///     of the telegram and will answer with <see cref="Communication.Mid0004"/>, MID revision unsupported if a subscription 
    ///     is made with a lower revision.
    /// </para>
    /// <para>Message sent by: Integrator</para>
    /// <para>
    ///     Answer: <see cref="Communication.Mid0005"/> Command accepted or 
    ///     <see cref="Communication.Mid0004"/> Command error, Controller is not a sync master/station controller, 
    ///     Multi-spindle result subscription already exists or MID revision unsupported
    /// </para>
    /// </summary>
    public class Mid0100 : Mid, IMultiSpindle, IIntegrator
    {
        private readonly IValueConverter<long> _longConverter;
        private readonly IValueConverter<bool> _boolConverter;
        private const int LAST_REVISION = 4;
        public const int MID = 100;

        public long DataNumberSystem
        {
            get => GetField(2, (int)DataFields.DATA_NUMBER_SYSTEM).GetValue(_longConverter.Convert);
            set => GetField(2, (int)DataFields.DATA_NUMBER_SYSTEM).SetValue(_longConverter.Convert, value);
        }

        public bool SendOnlyNewData
        {
            get => GetField(3, (int)DataFields.SEND_ONLY_NEW_DATA).GetValue(_boolConverter.Convert);
            set => GetField(3, (int)DataFields.SEND_ONLY_NEW_DATA).SetValue(_boolConverter.Convert, value);
        }

        public Mid0100() : this(LAST_REVISION)
        {

        }

        public Mid0100(int revision = LAST_REVISION) : base(MID, revision)
        {
            _longConverter = new Int64Converter();
            _boolConverter = new BoolConverter();
        }

        public Mid0100(int revision, bool sendOnlyNewData) : this(revision)
        {
            SendOnlyNewData = sendOnlyNewData;
        }

        public Mid0100(int revision, int dataNumberSysten, bool sendOnlyNewData) : this(revision)
        {
            DataNumberSystem = dataNumberSysten;
            SendOnlyNewData = sendOnlyNewData;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                { 1, new List<DataField>() },
                {
                    2, new List<DataField>()
                            {
                                new DataField((int)DataFields.DATA_NUMBER_SYSTEM, 20, 10, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                            }
                },
                {
                    3, new List<DataField>()
                            {
                                new DataField((int)DataFields.SEND_ONLY_NEW_DATA, 30, 1, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                            }
                },
                {
                    4, new List<DataField>()
                }
            };
        }

        public enum DataFields
        {
            DATA_NUMBER_SYSTEM,
            SEND_ONLY_NEW_DATA
        }
    }
}
