namespace OpenProtocolInterpreter.Mode
{
    /// <summary>
    /// Mode ID upload request
    /// <para>A request to get the valid mode IDs from the controller.</para>
    /// <para>Message sent by: Integrator</para>
    /// <para>Answer: <see cref="Mid2601"/> Mode ID upload reply</para>
    /// </summary>
    public class Mid2600 : Mid, IMode, IIntegrator
    {
        private const int LAST_REVISION = 1;
        public const int MID = 2600;

        public Mid2600() : base(MID, LAST_REVISION) { }
    }
}
