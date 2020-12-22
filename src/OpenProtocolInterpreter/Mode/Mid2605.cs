namespace OpenProtocolInterpreter.Mode
{
    /// <summary>
    /// Acknowledgement for a new mode selected
    /// <para>This message is an acknowledge to <see cref="Mid2604"/>.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>Answer: None</para>
    /// </summary>
    public class Mid2605 : Mid, IMode, IIntegrator
    {
        private const int LAST_REVISION = 1;
        public const int MID = 2605;

        public Mid2605() : base(MID, LAST_REVISION) { }
    }
}
