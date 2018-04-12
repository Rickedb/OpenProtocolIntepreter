﻿namespace OpenProtocolInterpreter.Job.Advanced
{
    /// <summary>
    /// MID: Job line control alert 2
    /// Description: This message tells the integrator that the Job line control alert 2 is set in the controller.
    /// Only available when a job has been selected.
    /// Message sent by: Controller
    /// Answer: MID 0125 Job line control info acknowledged
    /// </summary>
    public class MID_0123 : MID, IAdvancedJob
    {
        private const int length = 20;
        public const int MID = 123;
        private const int revision = 1;

        public MID_0123() : base(length, MID, revision) { }

        internal MID_0123(IMID nextTemplate) : base(length, MID, revision)
        {
            this.NextTemplate = nextTemplate;
        }

        public override MID ProcessPackage(string package)
        {
            if (base.IsCorrectType(package))
                return (MID_0123)base.ProcessPackage(package);

            return this.NextTemplate.ProcessPackage(package);
        }

        protected override void RegisterDatafields() { }
    }
}