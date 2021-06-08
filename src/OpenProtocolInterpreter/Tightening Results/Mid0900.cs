using System;
using System.Collections.Generic;
using OpenProtocolInterpreter.Converters;
using OpenProtocolInterpreter.Result;
using System.Linq;

namespace OpenProtocolInterpreter.TighteningResults
{
   public class Mid0900 : Mid, ITighteningResults, IController
   {
      private readonly IValueConverter<int> _intConverter;
      private readonly IValueConverter<IEnumerable<VariableDataField>> _variableDataFieldListConverter;
      private readonly IValueConverter<DateTime> _dateConverter;

      private const int LAST_REVISION = 1;
      public const int MID = 900;

      public Mid0900() : base(MID, LAST_REVISION)
      {
         _intConverter = new Int32Converter();
         _dateConverter = new DateConverter();
         _variableDataFieldListConverter = new VariableDataFieldListConverter(_intConverter);
         VariableDataFields = new List<VariableDataField>();
      }

      // DEV NOTE: All fields with strings are left adjusted and padded with spaces. All numerical fields are right adjusted and padded with 0's
      protected override Dictionary<int, List<DataField>> RegisterDatafields()
      {
         return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                    {
                        new DataField((int) DataFields.RESULT_DATA_IDENTIFIER, 20, 10, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                        new DataField((int) DataFields.TIME_STAMP, 30, 19, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                        new DataField((int) DataFields.NUMBER_OF_PID_DATA_FIELDS, 49, 3, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                        new DataField((int) DataFields.TRACE_TYPE, 52, 2, '0', DataField.PaddingOrientations.LEFT_PADDED, false), // potential will be a problem as index 52 might be correct is datafields isnt 000 and is defined
                        new DataField((int) DataFields.TRANSDUCER_TYPE, 54, 2, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                        new DataField((int) DataFields.Unit, 56, 3, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                        new DataField((int) DataFields.NUMBER_OF_PID_DATA_FIELDS2, 59, 3, '0', DataField.PaddingOrientations.LEFT_PADDED, false),
                        new DataField((int) DataFields.NUMBER_OF_RESOLUTION_FIELDS, 0, 3, '0', DataField.PaddingOrientations.LEFT_PADDED, false), // defined at runtime because unknown data field length that before it
                        new DataField((int) DataFields.NUMBER_OF_TRACE_SAMPLES, 0, 5, '0', DataField.PaddingOrientations.LEFT_PADDED, false), // defined at rrunetime because unknown resolution field length before it
                        new DataField((int) DataFields.NUL_CHAR, 0, 1, false), // delimits ascii data from binary. // defined at runtime

                        new DataField((int)DataFields.VARIABLE_DATA_FIELDS, 0, 0, false), //defined at runtime
                        new DataField((int)DataFields.RESOLUTION_FIELDS, 0, 0, false), //defined at runtime
                    }
                }
            };
      }

      public int ResultDataIdentifier
      {
         get => GetField(1, (int)DataFields.RESULT_DATA_IDENTIFIER).GetValue(_intConverter.Convert);
         set => GetField(1, (int)DataFields.RESULT_DATA_IDENTIFIER).SetValue(_intConverter.Convert, value);
      }
      public DateTime TimeStamp
      {
         get => GetField(1, (int)DataFields.TIME_STAMP).GetValue(_dateConverter.Convert);
         set => GetField(1, (int)DataFields.TIME_STAMP).SetValue(_dateConverter.Convert, value);
      }

      public int NumberOfPIDDataFields
      {
         get => GetField(1, (int)DataFields.NUMBER_OF_PID_DATA_FIELDS).GetValue(_intConverter.Convert);
         set => GetField(1, (int)DataFields.NUMBER_OF_PID_DATA_FIELDS).SetValue(_intConverter.Convert, value);
      }

      public int NumberOfPIDDataFields2
      {
         get => GetField(1, (int)DataFields.NUMBER_OF_PID_DATA_FIELDS2).GetValue(_intConverter.Convert);
         set => GetField(1, (int)DataFields.NUMBER_OF_PID_DATA_FIELDS2).SetValue(_intConverter.Convert, value);
      }

      public List<VariableDataField> VariableDataFields { get; set; }

      public List<ResolutionField> ResolutionFields { get; set; }

      public int TraceType
      {
         get => GetField(1, (int)DataFields.TRACE_TYPE).GetValue(_intConverter.Convert);
         set => GetField(1, (int)DataFields.TRACE_TYPE).SetValue(_intConverter.Convert, value);
      }

      public int TransducerType
      {
         get => GetField(1, (int)DataFields.TRANSDUCER_TYPE).GetValue(_intConverter.Convert);
         set => GetField(1, (int)DataFields.TRANSDUCER_TYPE).SetValue(_intConverter.Convert, value);
      }

      public int Unit
      {
         get => GetField(1, (int)DataFields.Unit).GetValue(_intConverter.Convert);
         set => GetField(1, (int)DataFields.Unit).SetValue(_intConverter.Convert, value);
      }

      public int NumberOfResolutionFields
      {
         get => GetField(1, (int)DataFields.NUMBER_OF_RESOLUTION_FIELDS).GetValue(_intConverter.Convert);
         set => GetField(1, (int)DataFields.NUMBER_OF_RESOLUTION_FIELDS).SetValue(_intConverter.Convert, value);
      }

      public int NumberOfTraceSamples
      {
         get => GetField(1, (int)DataFields.NUMBER_OF_TRACE_SAMPLES).GetValue(_intConverter.Convert);
         set => GetField(1, (int)DataFields.NUMBER_OF_TRACE_SAMPLES).SetValue(_intConverter.Convert, value);
      }

      public int NullChar
      {
         get => GetField(1, (int)DataFields.NUL_CHAR).GetValue(_intConverter.Convert);
         set => GetField(1, (int)DataFields.NUL_CHAR).SetValue(_intConverter.Convert, value);
      }

      public double Coefficient { get; private set; }


      public byte[] RawTraceSamples { get; set; }

      public float[] TraceSamples { get; set; }

      public CoefficientOperation OperationType { get; private set; }

      public enum CoefficientOperation
      {
         DIVISION = 02213,
         MULTIPLICATION = 02214
      }

      public override string Pack()
      {
         return base.Pack();
      }

      public override Mid Parse(byte[] package)
      {
         var asciiLength = 0;
         for (int i = 0; i < package.Length; i++)
         {
            // found ASCII delimiter
            if (package[i] == 0x00)
            {
               asciiLength = i + 1; // plus 1 to include null terminator/delimiter
               break;
            }
         }

         var asciiMessage = System.Text.Encoding.ASCII.GetString(package, 0, asciiLength);

         // Parse the 20 byte header
         HeaderData = ProcessHeader(asciiMessage);

         var numberOfPidDataFields = GetField(1, (int)DataFields.NUMBER_OF_PID_DATA_FIELDS);
         var dataFieldListField = GetField(1, (int)DataFields.VARIABLE_DATA_FIELDS);
         var numPids = GetValue(numberOfPidDataFields, asciiMessage);

         if ((Convert.ToInt32(numPids) > 0))
         {
            dataFieldListField.Index = numberOfPidDataFields.Index + numberOfPidDataFields.Size;
            dataFieldListField.Size = asciiMessage.Length - dataFieldListField.Index;
         }


         // num of pid datafields 2
         var numberOfPidDataFields2 = GetField(1, (int)DataFields.NUMBER_OF_PID_DATA_FIELDS2);
         var dataFieldListField2 = GetField(1, (int)DataFields.VARIABLE_DATA_FIELDS);

         if (!Int32.TryParse(GetValue(numberOfPidDataFields2, asciiMessage), out int numPids2))
            throw new Exception("Cannot parse Variable DataField (2)");

         // We don't know the total length of the all datafields so GetDataFieldsByFieldCount loops through 
         // n datafields and accumalates the total length
         if (numPids2 > 0)
         {
            dataFieldListField2.Index = numberOfPidDataFields2.Index + numberOfPidDataFields2.Size;
            VariableDataFields = GetDataFieldsByFieldCount(asciiMessage.Substring(dataFieldListField2.Index), Convert.ToInt32(numPids2), out int endLength);
            dataFieldListField2.Size = endLength;

            // Parse the datafieldList. Look up coefficient 
            var coefficientPID = VariableDataFields.Where(field => field.ParameterId == 02213 || field.ParameterId == 02214).FirstOrDefault(); ;
            if (!Double.TryParse(coefficientPID.RealValue, out double coeff))
               throw new Exception("Coefficient not found!");

            if (!Enum.TryParse(coefficientPID.ParameterId.ToString(), out CoefficientOperation op))
               throw new Exception("Coefficient operation type not found!");

            Coefficient = coeff;
            OperationType = op;
         }

         // defining this at runtime
         var numberOfResolutionFields = GetField(1, (int)DataFields.NUMBER_OF_RESOLUTION_FIELDS);
         numberOfResolutionFields.Index = dataFieldListField2.Index + dataFieldListField2.Size;
         if (!Int32.TryParse(GetValue(numberOfResolutionFields, asciiMessage), out int numberOfResolutionValue))
            throw new Exception("Cannot parse Number of Resolutions DataField");


         // There is a resolution field
         var resolutionField = GetField(1, (int)DataFields.RESOLUTION_FIELDS);
         if (numberOfResolutionValue > 0)
         {
            // Get field, set index to end of datafieldList2. Set size to end length of the resolutionfields
            resolutionField.Index = numberOfResolutionFields.Index + numberOfResolutionFields.Size;
            ResolutionFields = GetResolutionFieldsByFieldCount(asciiMessage.Substring(resolutionField.Index), numberOfResolutionValue, out int endLength);
            resolutionField.Size = endLength;
         }
         else
         {
            throw new Exception("No resolution datafield found!");
         }

         var numberOfTraceSamples = GetField(1, (int)DataFields.NUMBER_OF_TRACE_SAMPLES);
         numberOfTraceSamples.Index = resolutionField.Index + resolutionField.Size;

         var nullCharacter = GetField(1, (int)DataFields.NUL_CHAR);
         nullCharacter.Index = numberOfTraceSamples.Index + numberOfTraceSamples.Size;

         // Substring all the fields into their correct property. Once this is called. All the properties are set based on the index and size given
         ProcessDataFields(asciiMessage);
         // END OF ASCII DATA. 

         // START OF BINARY DATA

         // Store the raw binary data
         RawTraceSamples = new byte[package.Length - asciiLength];
         Array.Copy(package, asciiLength, RawTraceSamples, 0, package.Length - asciiLength);

         // Convert raw binary into 2 byte trace samples
         TraceSamples = new float[RawTraceSamples.Length / 2];
         for (int i = 0, j = 0; j < TraceSamples.Length; i += 2, j++)
         {
            TraceSamples[j] = ((short)RawTraceSamples[i] << 8 | (ushort)RawTraceSamples[i + 1]);
         }

         return this;
      }


      public enum DataFields
      {
         //rev 1
         RESULT_DATA_IDENTIFIER = 0,
         TIME_STAMP = 1,
         NUMBER_OF_PID_DATA_FIELDS = 2,
         VARIABLE_DATA_FIELDS = 3,
         TRACE_TYPE = 4,
         TRANSDUCER_TYPE = 5,
         Unit = 6,
         NUMBER_OF_PID_DATA_FIELDS2 = 7,

         NUMBER_OF_RESOLUTION_FIELDS = 8,
         RESOLUTION_FIELDS = 9,
         NUMBER_OF_TRACE_SAMPLES = 10,
         NUL_CHAR = 11,
      }

      public static List<VariableDataField> GetDataFieldsByFieldCount(string value, int fieldCount, out int endLength)
      {
         var list = new List<VariableDataField>();
         int length = 0;
         endLength = length;
         for (int i = 0, j = 0; i < fieldCount; i++, j += 17 + length)
         {

            length = Convert.ToInt32(value.Substring(j + 5, 3));
            var vdf = new VariableDataField();
            vdf.ParameterId = Convert.ToInt32(value.Substring(j, 5));
            vdf.Length = length;
            vdf.DataType = Convert.ToInt32(value.Substring(j + 8, 2));
            vdf.Unit = Convert.ToInt32(value.Substring(j + 10, 3));
            vdf.StepNumber = Convert.ToInt32(value.Substring(j + 13, 4));
            vdf.RealValue = value.Substring(j + 17, length);

            list.Add(vdf);
            endLength += 17 + length;
         }
         return list;
      }

      public static List<ResolutionField> GetResolutionFieldsByFieldCount(string value, int fieldCount, out int endLength)
      {
         var list = new List<ResolutionField>();
         int length = 0;
         endLength = length;
         for (int i = 0, j = 0; i < fieldCount; i++, j += 18 + length)
         {
            length = Convert.ToInt32(value.Substring(j + 10, 3));
            var rf = new ResolutionField();
            rf.FirstIndex = Convert.ToInt32(value.Substring(j, 5));
            rf.LastIndex = Convert.ToInt32(value.Substring(j + 5, 5));
            rf.Length = length;
            rf.DataType = Convert.ToInt32(value.Substring(j + 13, 2));
            rf.Unit = Convert.ToInt32(value.Substring(j + 15, 3));
            rf.TimeValue = Convert.ToDouble(value.Substring(j + 18, length));

            list.Add(rf);
            endLength += 18 + length;
         }
         return list;
      }

   }
}
