﻿using System;

namespace OpenProtocolInterpreter.IOInterface
{
    /// <summary>
    /// MID: IO device status request
    /// Description: 
    ///     Request for the status of the relays and digital inputs at a device, e.g. an I/O expander. 
    ///     The device is specified by a device number.
    /// Message sent by: Integrator
    /// Answer: MID 0215 IO device status or
    ///         MID 0004 Command error,
    ///         Faulty IO device ID, or IO device not connected
    /// </summary>
    public class MID_0214 : Mid, IIOInterface
    {
        public const int MID = 214;
        private const int length = 22;
        private const int revision = 1;

        public int DeviceNumber { get; set; }

        public MID_0214() : base(length, MID, revision) { }

        public MID_0214(int deviceNumber) : base(length, MID, revision)
        {
            DeviceNumber = deviceNumber;
        }

        internal MID_0214(IMid nextTemplate) : base(length, MID, revision)
        {
            NextTemplate = nextTemplate;
        }

        public override string BuildPackage()
        {
            if (DeviceNumber > 15 || DeviceNumber < 0)
                throw new ArgumentException("Invalid Device Number, Device number range is 00-15 => 00=internal device, 01 - 15 = I/O expanders");

            return base.BuildHeader() + DeviceNumber.ToString().PadLeft(2, '0');
        }

        public override Mid ProcessPackage(string package)
        {
            if (base.IsCorrectType(package))
            {
                HeaderData = ProcessHeader(package);
                var dataField = base.RegisteredDataFields[(int)DataFields.DEVICE_NUMBER];
                DeviceNumber = Convert.ToInt32(package.Substring(dataField.Index, dataField.Size));
                return this;
            }

            return NextTemplate.ProcessPackage(package);
        }

        protected override void RegisterDatafields()
        {
            this.RegisteredDataFields.Add(new DataField((int)DataFields.DEVICE_NUMBER, 20, 2));
        }

        public enum DataFields
        {
            DEVICE_NUMBER
        }
    }
}