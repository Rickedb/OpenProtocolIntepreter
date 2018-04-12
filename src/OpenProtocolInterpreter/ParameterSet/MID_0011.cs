﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenProtocolInterpreter.ParameterSet
{
    /// <summary>
    /// MID: Parameter set ID upload reply
    /// Description: 
    ///     The transmission of all the valid parameter set IDs of the controller. In the revision 000-001 the data
    ///     field contains the number of valid parameter sets currently present in the controller, and the ID of each
    ///     parameter set present.In revision 2 is the number of stages on each Pset/Mset added.
    /// Message sent by: Controller
    /// Answer: None
    /// </summary>
    public class MID_0011 : MID, IParameterSet
    {
        private readonly Dictionary<int, Action<string>> revisionsActions;
        private const int length = 23;
        public const int MID = 11;
        private const int revision = 1;

        public int TotalParameterSets { get; private set; }

        public List<int> ParameterSets { get; set; }

        public MID_0011() : base(length, MID, revision)
        {
            this.ParameterSets = new List<int>();
        }

        public MID_0011(IEnumerable<int> parameterSets) : base(length, MID, revision)
        {
            this.ParameterSets = parameterSets.ToList();
        }

        internal MID_0011(IMID nextTemplate) : base(length, MID, revision)
        {
            this.ParameterSets = new List<int>();
            this.NextTemplate = nextTemplate;
            this.revisionsActions = new Dictionary<int, Action<string>>()
            {
                { 1, this.processRevision1 },
                { 2, this.processRevision2 },
                { 3, this.processRevision3 }
            };

        }

        public override string BuildPackage()
        {
            if (this.ParameterSets.Count == 0)
                throw new ArgumentException("Parameter Set list cannot be empty!!");

            string package = base.BuildHeader();
            package += this.ParameterSets.Count.ToString().PadLeft(this.RegisteredDataFields[(int)DataFields.TOTAL_PARAMETER_SETS].Size, '0');

            var datafield = this.RegisteredDataFields[(int)DataFields.EACH_PARAMETER_SET];
            foreach(int param in this.ParameterSets)
                package += param.ToString().PadLeft(datafield.Size, '0');

            return package;
        }

        public override MID ProcessPackage(string package)
        {
            if (base.IsCorrectType(package))
            {
                HeaderData = base.ProcessHeader(package);
                UpdateRevisionFromPackage

                var datafield = this.RegisteredDataFields[(int)DataFields.TOTAL_PARAMETER_SETS];
                this.TotalParameterSets = Convert.ToInt32(package.Substring(datafield.Index, datafield.Size));

                datafield = this.RegisteredDataFields[(int)DataFields.EACH_PARAMETER_SET];
                int packageIndex = datafield.Index;
                for (int i = 0; i < this.TotalParameterSets; i++)
                {
                    this.ParameterSets.Add(Convert.ToInt32(package.Substring(packageIndex, datafield.Size)));
                    packageIndex += datafield.Size;
                }

                return this;
            }

            return this.NextTemplate.ProcessPackage(package);
        }

        protected override void RegisterDatafields()
        {
            this.RegisteredDataFields.Add(new DataField((int)DataFields.TOTAL_PARAMETER_SETS, 20, 3));
            this.RegisteredDataFields.Add(new DataField((int)DataFields.EACH_PARAMETER_SET, 23, 3));
        }

        private void processRevision1(string package)
        {
            var datafield = this.RegisteredDataFields[(int)DataFields.TOTAL_PARAMETER_SETS];
            this.TotalParameterSets = Convert.ToInt32(package.Substring(datafield.Index, datafield.Size));

            datafield = this.RegisteredDataFields[(int)DataFields.EACH_PARAMETER_SET];
            int packageIndex = datafield.Index;
            for (int i = 0; i < this.TotalParameterSets; i++)
            {
                this.ParameterSets.Add(Convert.ToInt32(package.Substring(packageIndex, datafield.Size)));
                packageIndex += datafield.Size;
            }
        }

        private void processRevision2(string package)
        {
            
        }
        private void processRevision3(string package)
        {
            
        }

        public enum DataFields
        {
            TOTAL_PARAMETER_SETS,
            EACH_PARAMETER_SET
        }
    }
}