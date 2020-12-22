using OpenProtocolInterpreter.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenProtocolInterpreter.Tightening
{
    /// <summary>
    /// Last tightening result data
    /// <para>Upload the last tightening result.</para>
    /// <para>Message sent by: Controller</para>
    /// <para>Answer: <see cref="Mid0062"/> Last tightening result data acknowledge</para>
    /// </summary>
    public class Mid0061 : Mid, ITightening, IController
    {
        private readonly IValueConverter<int> _intConverter;
        private readonly IValueConverter<long> _longConverter;
        private readonly IValueConverter<bool> _boolConverter;
        private readonly IValueConverter<decimal> _decimalConverter;
        private readonly IValueConverter<DateTime> _dateConverter;
        private readonly IValueConverter<StrategyOptions> _strategyOptionsConverter;
        private readonly IValueConverter<TighteningErrorStatus> _tighteningErrorStatusConverter;
        private readonly IValueConverter<TighteningErrorStatus2> _tighteningErrorStatus2Converter;
        private readonly IValueConverter<IEnumerable<StageResult>> _stageResultListConverter;
        private const int LAST_REVISION = 7;
        public const int MID = 61;

        public int CellId
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.CELL_ID).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.CELL_ID).SetValue(_intConverter.Convert, value);
        }
        public int ChannelId
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.CHANNEL_ID).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.CHANNEL_ID).SetValue(_intConverter.Convert, value);
        }
        public string TorqueControllerName
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_CONTROLLER_NAME).Value;
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_CONTROLLER_NAME).SetValue(value);
        }
        public string VinNumber
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.VIN_NUMBER).Value;
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.VIN_NUMBER).SetValue(value);
        }
        public int JobId
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.JOB_ID).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.JOB_ID).SetValue(_intConverter.Convert, value);
        }
        public int ParameterSetId
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.PARAMETER_SET_ID).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.PARAMETER_SET_ID).SetValue(_intConverter.Convert, value);
        }
        public int BatchSize
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.BATCH_SIZE).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.BATCH_SIZE).SetValue(_intConverter.Convert, value);
        }
        public int BatchCounter
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.BATCH_COUNTER).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.BATCH_COUNTER).SetValue(_intConverter.Convert, value);
        }
        public bool TighteningStatus
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.TIGHTENING_STATUS).GetValue(_boolConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.TIGHTENING_STATUS).SetValue(_boolConverter.Convert, value);
        }
        public TighteningValueStatus TorqueStatus
        {
            get => (TighteningValueStatus)GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_STATUS).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_STATUS).SetValue(_intConverter.Convert, (int)value);
        }
        public TighteningValueStatus AngleStatus
        {
            get => (TighteningValueStatus)GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE_STATUS).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE_STATUS).SetValue(_intConverter.Convert, (int)value);
        }
        public decimal TorqueMinLimit
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_MIN_LIMIT).GetValue(_decimalConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_MIN_LIMIT).SetValue(_decimalConverter.Convert, value);
        }
        public decimal TorqueMaxLimit
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_MAX_LIMIT).GetValue(_decimalConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_MAX_LIMIT).SetValue(_decimalConverter.Convert, value);
        }
        public decimal TorqueFinalTarget
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_FINAL_TARGET).GetValue(_decimalConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE_FINAL_TARGET).SetValue(_decimalConverter.Convert, value);
        }
        public decimal Torque
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE).GetValue(_decimalConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.TORQUE).SetValue(_decimalConverter.Convert, value);
        }
        public int AngleMinLimit
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE_MIN_LIMIT).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE_MIN_LIMIT).SetValue(_intConverter.Convert, value);
        }
        public int AngleMaxLimit
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE_MAX_LIMIT).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE_MAX_LIMIT).SetValue(_intConverter.Convert, value);
        }
        public int AngleFinalTarget
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE_FINAL_TARGET).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE_FINAL_TARGET).SetValue(_intConverter.Convert, value);
        }
        public int Angle
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.ANGLE).SetValue(_intConverter.Convert, value);
        }
        public DateTime Timestamp
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.TIMESTAMP).GetValue(_dateConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.TIMESTAMP).SetValue(_dateConverter.Convert, value);
        }
        public DateTime LastChangeInParameterSet
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.LAST_CHANGE_IN_PARAMETER_SET).GetValue(_dateConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.LAST_CHANGE_IN_PARAMETER_SET).SetValue(_dateConverter.Convert, value);
        }
        public BatchStatus BatchStatus
        {
            get => (BatchStatus)GetField(GetCurrentRevisionIndex(), (int)DataFields.BATCH_STATUS).GetValue(_intConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.BATCH_STATUS).SetValue(_intConverter.Convert, (int)value);
        }
        public long TighteningId
        {
            get => GetField(GetCurrentRevisionIndex(), (int)DataFields.TIGHTENING_ID).GetValue(_longConverter.Convert);
            set => GetField(GetCurrentRevisionIndex(), (int)DataFields.TIGHTENING_ID).SetValue(_longConverter.Convert, value);
        }
        //Rev 2 Addition
        public Strategy Strategy
        {
            get => (Strategy)GetField(2, (int)DataFields.STRATEGY).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.STRATEGY).SetValue(_intConverter.Convert, (int)value);
        }
        public StrategyOptions StrategyOptions
        {
            get => GetField(2, (int)DataFields.STRATEGY_OPTIONS).GetValue(_strategyOptionsConverter.ConvertFromBytes);
            set => GetField(2, (int)DataFields.STRATEGY_OPTIONS).SetRawValue(_strategyOptionsConverter.ConvertToBytes, value);
        }
        public TighteningValueStatus RundownAngleStatus
        {
            get => (TighteningValueStatus)GetField(2, (int)DataFields.RUNDOWN_ANGLE_STATUS).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.RUNDOWN_ANGLE_STATUS).SetValue(_intConverter.Convert, (int)value);
        }
        public TighteningValueStatus CurrentMonitoringStatus
        {
            get => (TighteningValueStatus)GetField(2, (int)DataFields.CURRENT_MONITORING_STATUS).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.CURRENT_MONITORING_STATUS).SetValue(_intConverter.Convert, (int)value);
        }
        public TighteningValueStatus SelftapStatus
        {
            get => (TighteningValueStatus)GetField(2, (int)DataFields.SELFTAP_STATUS).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.SELFTAP_STATUS).SetValue(_intConverter.Convert, (int)value);
        }
        public TighteningValueStatus PrevailTorqueMonitoringStatus
        {
            get => (TighteningValueStatus)GetField(2, (int)DataFields.PREVAIL_TORQUE_MONITORING_STATUS).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.PREVAIL_TORQUE_MONITORING_STATUS).SetValue(_intConverter.Convert, (int)value);
        }
        public TighteningValueStatus PrevailTorqueCompensateStatus
        {
            get => (TighteningValueStatus)GetField(2, (int)DataFields.PREVAIL_TORQUE_COMPENSATE_STATUS).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.PREVAIL_TORQUE_COMPENSATE_STATUS).SetValue(_intConverter.Convert, (int)value);
        }
        public TighteningErrorStatus TighteningErrorStatus
        {
            get => GetField(2, (int)DataFields.TIGHTENING_ERROR_STATUS).GetValue(_tighteningErrorStatusConverter.ConvertFromBytes);
            set => GetField(2, (int)DataFields.TIGHTENING_ERROR_STATUS).SetRawValue(_tighteningErrorStatusConverter.ConvertToBytes, value);
        }
        public int RundownAngleMin
        {
            get => GetField(2, (int)DataFields.RUNDOWN_ANGLE_MIN).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.RUNDOWN_ANGLE_MIN).SetValue(_intConverter.Convert, value);
        }
        public int RundownAngleMax
        {
            get => GetField(2, (int)DataFields.RUNDOWN_ANGLE_MAX).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.RUNDOWN_ANGLE_MAX).SetValue(_intConverter.Convert, value);
        }
        public int RundownAngle
        {
            get => GetField(2, (int)DataFields.RUNDOWN_ANGLE).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.RUNDOWN_ANGLE).SetValue(_intConverter.Convert, value);
        }
        public int CurrentMonitoringMin
        {
            get => GetField(2, (int)DataFields.CURRENT_MONITORING_MIN).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.CURRENT_MONITORING_MIN).SetValue(_intConverter.Convert, value);
        }
        public int CurrentMonitoringMax
        {
            get => GetField(2, (int)DataFields.CURRENT_MONITORING_MAX).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.CURRENT_MONITORING_MAX).SetValue(_intConverter.Convert, value);
        }
        public int CurrentMonitoringValue
        {
            get => GetField(2, (int)DataFields.CURRENT_MONITORING_VALUE).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.CURRENT_MONITORING_VALUE).SetValue(_intConverter.Convert, value);
        }
        public decimal SelftapMin
        {
            get => GetField(2, (int)DataFields.SELFTAP_MIN).GetValue(_decimalConverter.Convert);
            set => GetField(2, (int)DataFields.SELFTAP_MIN).SetValue(_decimalConverter.Convert, value);
        }
        public decimal SelftapMax
        {
            get => GetField(2, (int)DataFields.SELFTAP_MAX).GetValue(_decimalConverter.Convert);
            set => GetField(2, (int)DataFields.SELFTAP_MAX).SetValue(_decimalConverter.Convert, value);
        }
        public decimal SelftapTorque
        {
            get => GetField(2, (int)DataFields.SELFTAP_TORQUE).GetValue(_decimalConverter.Convert);
            set => GetField(2, (int)DataFields.SELFTAP_TORQUE).SetValue(_decimalConverter.Convert, value);
        }
        public decimal PrevailTorqueMonitoringMin
        {
            get => GetField(2, (int)DataFields.PREVAIL_TORQUE_MONITORING_MIN).GetValue(_decimalConverter.Convert);
            set => GetField(2, (int)DataFields.PREVAIL_TORQUE_MONITORING_MIN).SetValue(_decimalConverter.Convert, value);
        }
        public decimal PrevailTorqueMonitoringMax
        {
            get => GetField(2, (int)DataFields.PREVAIL_TORQUE_MONITORING_MAX).GetValue(_decimalConverter.Convert);
            set => GetField(2, (int)DataFields.PREVAIL_TORQUE_MONITORING_MAX).SetValue(_decimalConverter.Convert, value);
        }
        public decimal PrevailTorque
        {
            get => GetField(2, (int)DataFields.PREVAIL_TORQUE).GetValue(_decimalConverter.Convert);
            set => GetField(2, (int)DataFields.PREVAIL_TORQUE).SetValue(_decimalConverter.Convert, value);
        }
        public int JobSequenceNumber
        {
            get => GetField(2, (int)DataFields.JOB_SEQUENCE_NUMBER).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.JOB_SEQUENCE_NUMBER).SetValue(_intConverter.Convert, value);
        }
        public int SyncTighteningId
        {
            get => GetField(2, (int)DataFields.SYNC_TIGHTENING_ID).GetValue(_intConverter.Convert);
            set => GetField(2, (int)DataFields.SYNC_TIGHTENING_ID).SetValue(_intConverter.Convert, value);
        }
        public string ToolSerialNumber
        {
            get => GetField(2, (int)DataFields.TOOL_SERIAL_NUMBER).Value;
            set => GetField(2, (int)DataFields.TOOL_SERIAL_NUMBER).SetValue(value);
        }
        //Rev 3 Addition
        public string ParameterSetName
        {
            get => GetField(3, (int)DataFields.PARAMETER_SET_NAME).Value;
            set => GetField(3, (int)DataFields.PARAMETER_SET_NAME).SetValue(value);
        }
        public TorqueValuesUnit TorqueValuesUnit
        {
            get => (TorqueValuesUnit)GetField(3, (int)DataFields.TORQUE_VALUES_UNIT).GetValue(_intConverter.Convert);
            set => GetField(3, (int)DataFields.TORQUE_VALUES_UNIT).SetValue(_intConverter.Convert, (int)value);
        }
        public ResultType ResultType
        {
            get => (ResultType)GetField(3, (int)DataFields.RESULT_TYPE).GetValue(_intConverter.Convert);
            set => GetField(3, (int)DataFields.RESULT_TYPE).SetValue(_intConverter.Convert, (int)value);
        }
        //Rev 4 addition
        public string IdentifierResultPart2
        {
            get => GetField(4, (int)DataFields.IDENTIFIER_RESULT_PART_2).Value;
            set => GetField(4, (int)DataFields.IDENTIFIER_RESULT_PART_2).SetValue(value);
        }
        public string IdentifierResultPart3
        {
            get => GetField(4, (int)DataFields.IDENTIFIER_RESULT_PART_3).Value;
            set => GetField(4, (int)DataFields.IDENTIFIER_RESULT_PART_3).SetValue(value);
        }
        public string IdentifierResultPart4
        {
            get => GetField(4, (int)DataFields.IDENTIFIER_RESULT_PART_4).Value;
            set => GetField(4, (int)DataFields.IDENTIFIER_RESULT_PART_4).SetValue(value);
        }
        //Rev 5 addition
        public string CustomerTighteningErrorCode
        {
            get => GetField(5, (int)DataFields.CUSTOMER_TIGHTENING_ERROR_CODE).Value;
            set => GetField(5, (int)DataFields.CUSTOMER_TIGHTENING_ERROR_CODE).SetValue(value);
        }
        //Rev 6 Addition
        public decimal PrevailTorqueCompensateValue
        {
            get => GetField(6, (int)DataFields.PREVAIL_TORQUE_COMPENSATE_VALUE).GetValue(_decimalConverter.Convert);
            set => GetField(6, (int)DataFields.PREVAIL_TORQUE_COMPENSATE_VALUE).SetValue(_decimalConverter.Convert, value);
        }
        public TighteningErrorStatus2 TighteningErrorStatus2
        {
            get => GetField(6, (int)DataFields.TIGHTENING_ERROR_STATUS_2).GetValue(_tighteningErrorStatus2Converter.ConvertFromBytes);
            set => GetField(6, (int)DataFields.TIGHTENING_ERROR_STATUS_2).SetRawValue(_tighteningErrorStatus2Converter.ConvertToBytes, value);
        }
        //Rev 7 addition
        public decimal CompensatedAngle
        {
            get => GetField(7, (int)DataFields.COMPENSATED_ANGLE).GetValue(_decimalConverter.Convert);
            set => GetField(7, (int)DataFields.COMPENSATED_ANGLE).SetValue(_decimalConverter.Convert, value);
        }
        public decimal FinalAngleDecimal
        {
            get => GetField(7, (int)DataFields.FINAL_ANGLE_DECIMAL).GetValue(_decimalConverter.Convert);
            set => GetField(7, (int)DataFields.FINAL_ANGLE_DECIMAL).SetValue(_decimalConverter.Convert, value);
        }
        //Rev 998 addition
        public int NumberOfStagesInMultistage
        {
            get => GetField(998, (int)DataFields.NUMBER_OF_STAGES_IN_MULTISTAGE).GetValue(_intConverter.Convert);
            set => GetField(998, (int)DataFields.NUMBER_OF_STAGES_IN_MULTISTAGE).SetValue(_intConverter.Convert, value);
        }
        public int NumberOfStageResults
        {
            get => GetField(998, (int)DataFields.NUMBER_OF_STAGE_RESULTS).GetValue(_intConverter.Convert);
            set => GetField(998, (int)DataFields.NUMBER_OF_STAGE_RESULTS).SetValue(_intConverter.Convert, value);
        }
        public List<StageResult> StageResults { get; set; }

        public Mid0061() : this(LAST_REVISION)
        {

        }

        public Mid0061(int revision = LAST_REVISION) : base(MID, revision)
        {
            var byteArrayConverter = new ByteArrayConverter();
            _intConverter = new Int32Converter();
            _longConverter = new Int64Converter();
            _boolConverter = new BoolConverter();
            _decimalConverter = new DecimalTrucatedConverter(2);
            _dateConverter = new DateConverter();
            _strategyOptionsConverter = new StrategyOptionsConverter(byteArrayConverter);
            _tighteningErrorStatusConverter = new TighteningErrorStatusConverter(byteArrayConverter);
            _tighteningErrorStatus2Converter = new TighteningErrorStatus2Converter(byteArrayConverter);
            _stageResultListConverter = new StageResultListConverter(_intConverter, _decimalConverter);
        }

        protected override string BuildHeader()
        {
            if (RevisionsByFields.Any())
            {
                HeaderData.Length = 20;
                HeaderData.Revision = HeaderData.Revision > 0 ? HeaderData.Revision : 1;
                if (HeaderData.Revision == 1 || HeaderData.Revision == 999)
                {
                    foreach (var dataField in RevisionsByFields[HeaderData.Revision])
                        HeaderData.Length += (dataField.HasPrefix ? 2 : 0) + dataField.Size;
                }
                else
                {
                    int processUntil = HeaderData.Revision != 998 ? HeaderData.Revision : 6;

                    for (int i = 2; i <= processUntil; i++)
                        foreach (var dataField in RevisionsByFields[i])
                            HeaderData.Length += (dataField.HasPrefix ? 2 : 0) + dataField.Size;

                    if (HeaderData.Revision == 998)
                    {
                        GetField(998, (int)DataFields.STAGE_RESULT).Size = StageResults.Count * 11;
                        foreach (var dataField in RevisionsByFields[998])
                            HeaderData.Length += (dataField.HasPrefix ? 2 : 0) + dataField.Size;
                    }
                }
            }
            return HeaderData.ToString();
        }

        public override string Pack()
        {
            string package = string.Empty;
            if (HeaderData.Revision > 1 && HeaderData.Revision != 999)
            {
                var strategyOptionsField = GetField(2, (int)DataFields.STRATEGY_OPTIONS);
                strategyOptionsField.SetValue(_intConverter.Convert, System.BitConverter.ToInt32(strategyOptionsField.RawValue, 0));

                var tighteningErrorStatusField = GetField(2, (int)DataFields.TIGHTENING_ERROR_STATUS);
                tighteningErrorStatusField.SetValue(_intConverter.Convert, System.BitConverter.ToInt32(tighteningErrorStatusField.RawValue, 0));
                if (HeaderData.Revision > 5)
                {
                    var tighteningErrorStatus2Field = GetField(6, (int)DataFields.TIGHTENING_ERROR_STATUS_2);
                    tighteningErrorStatus2Field.RawValue = System.BitConverter.GetBytes(_intConverter.Convert(tighteningErrorStatus2Field.Value));
                }

                if (HeaderData.Revision == 998)
                {
                    NumberOfStageResults = StageResults.Count;
                    var stageResultField = GetField(998, (int)DataFields.STAGE_RESULT);
                    stageResultField.Size = StageResults.Count * 11;
                    stageResultField.SetValue(_stageResultListConverter.Convert(StageResults));
                }

                package = BuildHeader();
                int processUntil = HeaderData.Revision != 998 ? HeaderData.Revision : 6;
                int prefixIndex = 1;
                for (int i = 2; i <= processUntil; i++)
                {
                    package += BuildDataFieldsPackage(prefixIndex, RevisionsByFields[i]);
                    prefixIndex += RevisionsByFields[i].Count(x => x.HasPrefix);
                }

                if (HeaderData.Revision == 998)
                {
                    package += BuildDataFieldsPackage(56, RevisionsByFields[998]);
                }
            }
            else
            {
                package = BuildHeader();
                package += BuildDataFieldsPackage(1, RevisionsByFields[HeaderData.Revision]);
            }

            return package;
        }

        public override byte[] PackBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BuildRawHeader());
            if (HeaderData.Revision == 1 || HeaderData.Revision == 999)
            {
                var asciiBytes = ToBytes(BuildDataFieldsPackage(1, RevisionsByFields[HeaderData.Revision]));
                bytes.AddRange(asciiBytes);
            }
            else
            {
                int processUntil = HeaderData.Revision != 998 ? HeaderData.Revision : 6;
                int prefixIndex = 1;
                for (int i = 2; i <= processUntil; i++)
                {
                    var fieldBytes = BuildDataFieldsRawPackage(prefixIndex, RevisionsByFields[i]);
                    bytes.AddRange(fieldBytes);
                    prefixIndex += RevisionsByFields[i].Count(x => x.HasPrefix);
                }

                if (HeaderData.Revision == 998)
                {
                    NumberOfStageResults = StageResults.Count;
                    GetField(998, (int)DataFields.STAGE_RESULT).SetValue(_stageResultListConverter.Convert(StageResults));
                    var fieldBytes = BuildDataFieldsRawPackage(prefixIndex, RevisionsByFields[998]);
                    bytes.AddRange(fieldBytes);
                }
            }

            return bytes.ToArray();
        }

        public override Mid Parse(string package)
        {
            base.Parse(package);
            if (HeaderData.Revision > 1 && HeaderData.Revision != 999)
            {
                var strategyOptionsField = GetField(2, (int)DataFields.STRATEGY_OPTIONS);
                strategyOptionsField.RawValue = System.BitConverter.GetBytes(_intConverter.Convert(strategyOptionsField.Value));

                var tighteningErrorStatusField = GetField(2, (int)DataFields.TIGHTENING_ERROR_STATUS);
                tighteningErrorStatusField.RawValue = System.BitConverter.GetBytes(_intConverter.Convert(tighteningErrorStatusField.Value));
                if (HeaderData.Revision > 5)
                {
                    var tighteningErrorStatus2Field = GetField(6, (int)DataFields.TIGHTENING_ERROR_STATUS_2);
                    tighteningErrorStatus2Field.RawValue = System.BitConverter.GetBytes(_intConverter.Convert(tighteningErrorStatus2Field.Value));
                }

                if (HeaderData.Revision == 998)
                {
                    var stageResultField = GetField(998, (int)DataFields.STAGE_RESULT);
                    StageResults = _stageResultListConverter.Convert(stageResultField.Value).ToList();
                }
            }

            return this;
        }

        public override Mid Parse(byte[] package)
        {
            var headerData = ToAscii(package.Take(20).ToArray());
            HeaderData = ProcessHeader(headerData);
            if (HeaderData.Revision <=1 || HeaderData.Revision == 999)
            {
                int revision = HeaderData.Revision > 0 ? HeaderData.Revision : 1;
                ProcessDataFields(package, RevisionsByFields[revision]);
            }
            else
            {
                int processUntil = HeaderData.Revision;
                if (HeaderData.Revision == 998)
                {
                    processUntil = 6;
                    var stageResultField = GetField(998, (int)DataFields.STAGE_RESULT);
                    stageResultField.Size = package.Length - stageResultField.Index - 2;
                    ProcessDataFields(package, RevisionsByFields[998]);
                    StageResults = _stageResultListConverter.Convert(stageResultField.Value).ToList();
                }

                for (int i = 2; i <= processUntil; i++)
                    ProcessDataFields(package, RevisionsByFields[i]);
            }

            return this;
        }

        protected override void ProcessDataFields(string package)
        {
            if (HeaderData.Revision < 2 || HeaderData.Revision == 999)
            {
                int revision = HeaderData.Revision > 0 ? HeaderData.Revision : 1;
                ProcessDataFields(RevisionsByFields[revision], package);
            }
            else
            {
                int processUntil = HeaderData.Revision;
                if (HeaderData.Revision == 998)
                {
                    processUntil = 6;
                    var stageResultField = GetField(998, (int)DataFields.STAGE_RESULT);
                    stageResultField.Size = package.Length - stageResultField.Index - 2;
                    ProcessDataFields(RevisionsByFields[998], package);
                }

                for (int i = 2; i <= processUntil; i++)
                    ProcessDataFields(RevisionsByFields[i], package);
            }
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            //opted to work with a different approuch (since it would need to modify too much fields)
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                new DataField((int)DataFields.CELL_ID, 20, 4, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.CHANNEL_ID, 26, 2, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TORQUE_CONTROLLER_NAME, 30, 25, ' '),
                                new DataField((int)DataFields.VIN_NUMBER, 57, 25, ' '),
                                new DataField((int)DataFields.JOB_ID, 84, 2, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.PARAMETER_SET_ID, 88, 3, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.BATCH_SIZE, 93, 4, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.BATCH_COUNTER, 99, 4, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TIGHTENING_STATUS, 105, 1),
                                new DataField((int)DataFields.TORQUE_STATUS, 108, 1),
                                new DataField((int)DataFields.ANGLE_STATUS, 111, 1),
                                new DataField((int)DataFields.TORQUE_MIN_LIMIT, 114, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TORQUE_MAX_LIMIT, 122, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TORQUE_FINAL_TARGET, 130, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TORQUE, 138, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.ANGLE_MIN_LIMIT, 146, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.ANGLE_MAX_LIMIT, 153, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.ANGLE_FINAL_TARGET, 160, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.ANGLE, 167, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TIMESTAMP, 174, 19),
                                new DataField((int)DataFields.LAST_CHANGE_IN_PARAMETER_SET, 195, 19),
                                new DataField((int)DataFields.BATCH_STATUS, 216, 1),
                                new DataField((int)DataFields.TIGHTENING_ID, 219, 10, '0', DataField.PaddingOrientations.LEFT_PADDED)
                            }
                },
                {
                    2, new List<DataField>()
                            {
                                new DataField((int)DataFields.CELL_ID, 20, 4, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.CHANNEL_ID, 26, 2, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TORQUE_CONTROLLER_NAME, 30, 25, ' '),
                                new DataField((int)DataFields.VIN_NUMBER, 57, 25, ' '),
                                new DataField((int)DataFields.JOB_ID, 84, 4, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.PARAMETER_SET_ID, 90, 3, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.STRATEGY, 95, 2, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.STRATEGY_OPTIONS, 99, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.BATCH_SIZE, 106, 4, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.BATCH_COUNTER, 112, 4, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TIGHTENING_STATUS, 118, 1),
                                new DataField((int)DataFields.BATCH_STATUS, 121, 1),
                                new DataField((int)DataFields.TORQUE_STATUS, 124, 1),
                                new DataField((int)DataFields.ANGLE_STATUS, 127, 1),
                                new DataField((int)DataFields.RUNDOWN_ANGLE_STATUS, 130, 1),
                                new DataField((int)DataFields.CURRENT_MONITORING_STATUS, 133, 1),
                                new DataField((int)DataFields.SELFTAP_STATUS, 136, 1),
                                new DataField((int)DataFields.PREVAIL_TORQUE_MONITORING_STATUS, 139, 1),
                                new DataField((int)DataFields.PREVAIL_TORQUE_COMPENSATE_STATUS, 142, 1),
                                new DataField((int)DataFields.TIGHTENING_ERROR_STATUS, 145, 10, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TORQUE_MIN_LIMIT, 157, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TORQUE_MAX_LIMIT, 165, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TORQUE_FINAL_TARGET, 173, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TORQUE, 181, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.ANGLE_MIN_LIMIT, 189, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.ANGLE_MAX_LIMIT, 196, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.ANGLE_FINAL_TARGET, 203, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.ANGLE, 210, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.RUNDOWN_ANGLE_MIN, 217, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.RUNDOWN_ANGLE_MAX, 224, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.RUNDOWN_ANGLE, 231, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.CURRENT_MONITORING_MIN, 238, 3, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.CURRENT_MONITORING_MAX, 243, 3, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.CURRENT_MONITORING_VALUE, 248, 3, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.SELFTAP_MIN, 253, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.SELFTAP_MAX, 261, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.SELFTAP_TORQUE, 269, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.PREVAIL_TORQUE_MONITORING_MIN, 277, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.PREVAIL_TORQUE_MONITORING_MAX, 285, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.PREVAIL_TORQUE, 293, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TIGHTENING_ID, 301, 10, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.JOB_SEQUENCE_NUMBER, 313, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.SYNC_TIGHTENING_ID, 320, 5, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TOOL_SERIAL_NUMBER, 327, 14, ' '),
                                new DataField((int)DataFields.TIMESTAMP, 343, 19),
                                new DataField((int)DataFields.LAST_CHANGE_IN_PARAMETER_SET, 364, 19)
                            }
                },
                {
                    3, new List<DataField>()
                            {
                                new DataField((int)DataFields.PARAMETER_SET_NAME, 385, 25, ' '),
                                new DataField((int)DataFields.TORQUE_VALUES_UNIT, 412, 1),
                                new DataField((int)DataFields.RESULT_TYPE, 415, 2, '0', DataField.PaddingOrientations.LEFT_PADDED)
                            }
                },
                {
                    4, new List<DataField>()
                            {
                                new DataField((int)DataFields.IDENTIFIER_RESULT_PART_2, 419, 25, ' '),
                                new DataField((int)DataFields.IDENTIFIER_RESULT_PART_3, 446, 25, ' '),
                                new DataField((int)DataFields.IDENTIFIER_RESULT_PART_4, 473, 25, ' ')
                            }
                },
                {
                    5, new List<DataField>()
                            {
                                new DataField((int)DataFields.CUSTOMER_TIGHTENING_ERROR_CODE, 500, 4, ' '),
                            }
                },
                {
                    6, new List<DataField>()
                            {
                                new DataField((int)DataFields.PREVAIL_TORQUE_COMPENSATE_VALUE, 506, 6, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.TIGHTENING_ERROR_STATUS_2, 514, 10, '0', DataField.PaddingOrientations.LEFT_PADDED)
                            }
                },
                {
                    7, new List<DataField>()
                            {
                                new DataField((int)DataFields.COMPENSATED_ANGLE, 526, 7, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.FINAL_ANGLE_DECIMAL, 535, 7, '0', DataField.PaddingOrientations.LEFT_PADDED)
                            }
                },
                {
                    998, new List<DataField>()
                            {
                                new DataField((int)DataFields.NUMBER_OF_STAGES_IN_MULTISTAGE, 526, 2, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.NUMBER_OF_STAGE_RESULTS, 530, 2, '0', DataField.PaddingOrientations.LEFT_PADDED),
                                new DataField((int)DataFields.STAGE_RESULT, 534, 11)
                            }
                },
                {
                    999, new List<DataField>()
                            {
                                new DataField((int)DataFields.VIN_NUMBER, 20, 25, ' ', hasPrefix: false),
                                new DataField((int)DataFields.JOB_ID, 45, 2, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                                new DataField((int)DataFields.PARAMETER_SET_ID, 47, 3, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                                new DataField((int)DataFields.BATCH_SIZE, 50, 4, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                                new DataField((int)DataFields.BATCH_COUNTER, 54, 4, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                                new DataField((int)DataFields.BATCH_STATUS, 58, 1, false),
                                new DataField((int)DataFields.TIGHTENING_STATUS, 59, 1, false),
                                new DataField((int)DataFields.TORQUE_STATUS, 60, 1, false),
                                new DataField((int)DataFields.ANGLE_STATUS, 61, 1, false),
                                new DataField((int)DataFields.TORQUE, 62, 6, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                                new DataField((int)DataFields.ANGLE, 68, 5, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                                new DataField((int)DataFields.TIMESTAMP, 73, 19, false),
                                new DataField((int)DataFields.LAST_CHANGE_IN_PARAMETER_SET, 92, 19, false),
                                new DataField((int)DataFields.TIGHTENING_ID, 111, 10, '0', DataField.PaddingOrientations.LEFT_PADDED, false)
                            }
                }
            };
        }

        /// <summary>
        /// Obtain which revision we will work with for shared properties
        /// (since rev 1, 2 and 999 are way too different, they are processed in different datafields)
        /// </summary>
        /// <returns>Datafield Revision Index</returns>
        private int GetCurrentRevisionIndex()
        {
            if (HeaderData.Revision == 999)
                return 999;

            if (HeaderData.Revision > 1)
                return 2;
            return 1;
        }

        private void ProcessDataFields(byte[] package, List<DataField> fields)
        {
            foreach (var dataField in fields)
                try
                {
                    IEnumerable<byte> value = Enumerable.Empty<byte>();
                    value = GetValue(dataField, package);

                    if (IsByteField(dataField))
                        dataField.RawValue = value.ToArray();
                    else
                        dataField.Value = ToAscii(value.ToArray());
                }
                catch (ArgumentOutOfRangeException)
                {
                    //null value
                }
        }

        private string BuildDataFieldsPackage(int prefixIndex, List<DataField> fields)
        {
            string package = string.Empty;
            foreach (var dataField in fields)
                if (dataField.HasPrefix)
                {
                    package += prefixIndex.ToString().PadLeft(2, '0') + dataField.Value;
                    prefixIndex++;
                }
                else
                    package += dataField.Value;

            return package;
        }

        private byte[] BuildDataFieldsRawPackage(int prefixIndex, List<DataField> fields)
        {
            List<byte> bytes = new List<byte>();
            foreach (var dataField in fields)
            {
                if (dataField.HasPrefix)
                {
                    var prefix = ToBytes(prefixIndex.ToString().PadLeft(2, '0'));
                    bytes.AddRange(prefix);
                    prefixIndex++;
                }

                if (IsByteField(dataField))
                    bytes.AddRange(dataField.RawValue);
                else
                    bytes.AddRange(ToBytes(dataField.Value));
            }

            return bytes.ToArray();
        }

        private bool IsByteField(DataField dataField)
        {
            var field = (DataFields)dataField.Field;
            return field == DataFields.STRATEGY_OPTIONS || field == DataFields.TIGHTENING_ERROR_STATUS || field == DataFields.TIGHTENING_ERROR_STATUS_2;
        }

        public enum DataFields
        {
            CELL_ID,
            CHANNEL_ID,
            TORQUE_CONTROLLER_NAME,
            VIN_NUMBER,
            JOB_ID,
            PARAMETER_SET_ID,
            BATCH_SIZE,
            BATCH_COUNTER,
            TIGHTENING_STATUS,
            TORQUE_STATUS,
            ANGLE_STATUS,
            TORQUE_MIN_LIMIT,
            TORQUE_MAX_LIMIT,
            TORQUE_FINAL_TARGET,
            TORQUE,
            ANGLE_MIN_LIMIT,
            ANGLE_MAX_LIMIT,
            ANGLE_FINAL_TARGET,
            ANGLE,
            TIMESTAMP,
            LAST_CHANGE_IN_PARAMETER_SET,
            BATCH_STATUS,
            TIGHTENING_ID,
            //Rev 2
            STRATEGY,
            STRATEGY_OPTIONS,
            RUNDOWN_ANGLE_STATUS,
            CURRENT_MONITORING_STATUS,
            SELFTAP_STATUS,
            PREVAIL_TORQUE_MONITORING_STATUS,
            PREVAIL_TORQUE_COMPENSATE_STATUS,
            TIGHTENING_ERROR_STATUS,
            RUNDOWN_ANGLE_MIN,
            RUNDOWN_ANGLE_MAX,
            RUNDOWN_ANGLE,
            CURRENT_MONITORING_MIN,
            CURRENT_MONITORING_MAX,
            CURRENT_MONITORING_VALUE,
            SELFTAP_MIN,
            SELFTAP_MAX,
            SELFTAP_TORQUE,
            PREVAIL_TORQUE_MONITORING_MIN,
            PREVAIL_TORQUE_MONITORING_MAX,
            PREVAIL_TORQUE,
            JOB_SEQUENCE_NUMBER,
            SYNC_TIGHTENING_ID,
            TOOL_SERIAL_NUMBER,
            //Rev 3
            PARAMETER_SET_NAME,
            TORQUE_VALUES_UNIT,
            RESULT_TYPE,
            //Rev 4
            IDENTIFIER_RESULT_PART_2,
            IDENTIFIER_RESULT_PART_3,
            IDENTIFIER_RESULT_PART_4,
            //Rev 5
            CUSTOMER_TIGHTENING_ERROR_CODE,
            //Rev 6
            PREVAIL_TORQUE_COMPENSATE_VALUE,
            TIGHTENING_ERROR_STATUS_2,
            //Rev 7
            COMPENSATED_ANGLE,
            FINAL_ANGLE_DECIMAL,
            //Rev 998 (Go over 7)
            NUMBER_OF_STAGES_IN_MULTISTAGE,
            NUMBER_OF_STAGE_RESULTS,
            STAGE_RESULT,
            //Rev 999 => all registered
        }
    }
}
