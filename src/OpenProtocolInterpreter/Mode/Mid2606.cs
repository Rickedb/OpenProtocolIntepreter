using OpenProtocolInterpreter.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenProtocolInterpreter.Mode
{
    /// <summary>
    /// Select Mode
    /// <para>Message sent by: Integrator</para>
    /// <para>
    ///     Answer: <see cref="Communication.Mid0005"/> Command accepted or 
    ///     <see cref="Communication.Mid0004"/> Command error, Parameter set can not be set
    /// </para>
    /// </summary>
    public class Mid2606 : Mid, IMode, IIntegrator
    {
        private readonly IValueConverter<int> _intConverter;
        private const int LAST_REVISION = 1;
        public const int MID = 2606;

        public int ModeId
        {
            get => GetField(1,(int)DataFields.MODE_ID).GetValue(_intConverter.Convert);
            set => GetField(1,(int)DataFields.MODE_ID).SetValue(_intConverter.Convert, value);
        }

        public Mid2606() : base(MID, LAST_REVISION) => _intConverter = new Int32Converter();

        /// <summary>
        /// Revision 1 Constructor
        /// </summary>
        /// <param name="modeId">Four ASCII digits, range 0000-9999</param>
        public Mid2606(int modeId) : this() => ModeId = modeId;

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                new DataField((int)DataFields.MODE_ID, 20, 4, '0', DataField.PaddingOrientations.LEFT_PADDED, false)
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
            if (ModeId< 0 || ModeId > 9999)
                failed.Add(new ArgumentOutOfRangeException(nameof(ModeId), "Range: 0000-9999").Message);

            errors = failed;
            return errors.Any();
        }

        public enum DataFields
        {
            MODE_ID
        }
    }
}
