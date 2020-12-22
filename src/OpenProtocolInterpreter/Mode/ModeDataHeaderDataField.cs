namespace OpenProtocolInterpreter.Mode
{
    /// <summary>
    /// Represents a Mode Data header entity
    /// </summary>
    public class ModeDataHeaderDataField
    {
        public int ModeId { get; set; }
        public int ModeNameSize { get; set; }
        public string ModeName { get; set; }
        public int NumberOfBolts { get; set; }

    }
}
