﻿namespace OpenProtocolInterpreter.IOInterface
{
    /// <summary>
    /// MID: Status externally monitored inputs unsubscribe
    /// Description: 
    ///     Unsubscribe for the MID 0211 Status externally monitored inputs.
    /// Message sent by: Integrator
    /// Answer: MID 0005 Command accepted or 
    /// MID 0004 Command error, 
    /// Status externally monitored inputs subscription does not exist
    /// </summary>
    public class MID_0213 : Mid, IIOInterface
    {
        private const int LAST_REVISION = 1;
        public const int MID = 213;

        public MID_0213() : base(MID, LAST_REVISION) { }

        internal MID_0213(IMid nextTemplate) : this() => NextTemplate = nextTemplate;
        
    }
}
