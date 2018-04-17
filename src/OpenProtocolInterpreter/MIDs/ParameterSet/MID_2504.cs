﻿using System;

namespace OpenProtocolInterpreter.MIDs.ParameterSet
{
    /// <summary>
    /// MID: Select Parameter set, Dynamic Job Included
    /// Description: 
    ///     Select a parameter set.
    /// Message sent by: Integrator
    /// Answer: MID 0005 Command accepted or MID 0004 Command error, Dynamic Job cannot be created, non-existing pset
    /// </summary>
    public class MID_2504 : MID, IParameterSet
    {
        private const int length = 23;
        public const int MID = 2504;
        private const int revision = 1;

        public int ParameterSetID { get; set; }

        public MID_2504() : base(length, MID, revision) { }

        public MID_2504(int parameterSetId) : base(length, MID, revision)
        {
            this.ParameterSetID = parameterSetId;
        }

        internal MID_2504(IMID nextTemplate) : base(length, MID, revision)
        {
            this.nextTemplate = nextTemplate;
        }

        public override string buildPackage()
        {
            string package = base.buildPackage();
            package += this.ParameterSetID.ToString().PadLeft(this.RegisteredDataFields[(int)DataFields.PARAMETER_SET_ID].Size, '0');
            return package;
        }

        public override MID processPackage(string package)
        {
            if (base.isCorrectType(package))
            {
                this.HeaderData = base.processHeader(package);

                this.ParameterSetID = Convert.ToInt32(package.Substring(this.RegisteredDataFields[(int)DataFields.PARAMETER_SET_ID].Index,
                                                                        this.RegisteredDataFields[(int)DataFields.PARAMETER_SET_ID].Size));

                return this;
            }

            return this.nextTemplate.processPackage(package);
        }

        protected override void registerDatafields() 
        {
            this.RegisteredDataFields.Add(new DataField((int)DataFields.PARAMETER_SET_ID, 20, 3));
        }

        public enum DataFields
        {
            PARAMETER_SET_ID
        }
    }
}