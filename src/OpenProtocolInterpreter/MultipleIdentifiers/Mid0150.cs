using System.Collections.Generic;

namespace OpenProtocolInterpreter.MultipleIdentifiers
{
    /// <summary>
    /// Identifier download request
    /// <para>Used by the integrator to send an identifier to the controller.</para>
    /// <para>Message sent by: Integrator</para>
    ///<para>Answer: <see cref="Communication.Mid0005"/> Command accepted or <see cref="Communication.Mid0004"/> Command error, Identifier input source not granted</para> 
    /// </summary>
    public class Mid0150 : Mid, IMultipleIdentifier, IIntegrator
    {
        private const int LAST_REVISION = 1;
        public const int MID = 150;

        private int identifierDataSize;

        public string IdentifierData
        {
            get => GetField(1, (int)DataFields.IDENTIFIER_DATA).Value;
            set => GetField(1, (int)DataFields.IDENTIFIER_DATA).SetValue(value);
        }

        public Mid0150() : base(MID, LAST_REVISION) { }

        public Mid0150(string identifierData) : base(MID, LAST_REVISION)
        {
            identifierDataSize = identifierData.Length;
            IdentifierData = identifierData;
        }

        public override Mid Parse(string package)
        {
            HeaderData = ProcessHeader(package);
            GetField(1, (int)DataFields.IDENTIFIER_DATA).Size = package.Length - 20;
            ProcessDataFields(package);
            return this;
        }

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                    {
                        new DataField((int)DataFields.IDENTIFIER_DATA, 20, identifierDataSize, false)
                    }
                }
            };
        }

        public enum DataFields
        {
            IDENTIFIER_DATA
        }
    }
}
