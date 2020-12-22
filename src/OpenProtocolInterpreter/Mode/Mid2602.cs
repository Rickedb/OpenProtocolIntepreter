namespace OpenProtocolInterpreter.Mode
{
    /// <summary>
    /// Lock at batch done upload Acknowledge
    /// <para>This message is an acknowledge to <see cref="Mid0022"/>.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>Answer: <see cref="Communication.Mid0005"/> Command accepted</para>
    /// </summary>
    public class Mid2602 : Mid, IMode, IIntegrator
    {
        private const int LAST_REVISION = 1;
        public const int MID = 2602;

        public Mid2602() : base(MID, LAST_REVISION) { }
    }
}
