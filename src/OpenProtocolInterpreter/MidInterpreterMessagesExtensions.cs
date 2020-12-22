using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenProtocolInterpreter
{
    /// <summary>
    /// Mid Interpreter initialization functions
    /// </summary>
    public static class MidInterpreterMessagesExtensions
    {
        private static readonly Func<Type, Type, bool> DoesImplementInteface = (mid, desiredInterface) => desiredInterface.IsAssignableFrom(mid);
        private static readonly Func<IEnumerable<Type>, Type, bool> IsValid = (mids, desiredInterface) => mids.All(x => DoesImplementInteface(x, desiredInterface) && x.IsSubclassOf(typeof(Mid)));

        /// <summary>
        /// Configure MidInterpreter to parse all available Mids of a mode
        /// <para>Select Integrator if you're integrator or Controller if you're a controller</para>
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns></returns>
        public static MidInterpreter UseAllMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            return midInterpreter
                .UseAlarmMessages(mode)
                .UseApplicationControllerMessage(mode)
                .UseApplicationSelectorMessages(mode)
                .UseApplicationToolLocationSystemMessages(mode)
                .UseAutomaticManualModeMessages(mode)
                .UseCommunicationMessages(mode)
                .UseIOInterfaceMessages(mode)
                .UseJobMessages(mode)
                .UseAdvancedJobMessages(mode)
                .UseMotorTuningMessages(mode)
                .UseMultipleIdentifiersMessages(mode)
                .UseMultiSpindleMessages(mode)
                .UseOpenProtocolCommandsDisabledMessages(mode)
                .UseParameterSetMessages(mode)
                .UsePLCUserDataMessages(mode)
                .UsePowerMACSMessages(mode)
                .UseResultMessages(mode)
                .UseStatisticMessages(mode)
                .UseTighteningMessages(mode)
                .UseTimeMessages(mode)
                .UseToolMessages(mode)
                .UseUserInterfaceMessages(mode)
                .UseVinMessages(mode)
                .UseModeMessages(mode);
        }

        /// <summary>
        /// Configure MidInterpreter to parse all available Mids of a mode
        /// <para>Select Integrator if you're integrator or Controller if you're a controller</para>
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns></returns>
        public static MidInterpreter UseAllMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (mids.Any(x => !x.IsSubclassOf(typeof(Mid))))
                throw new ArgumentException("All mids must inherit Mid class", nameof(mids));
            
            return midInterpreter
                .UseAlarmMessages(mids.Where(x=> DoesImplementInteface(x, typeof(Alarm.IAlarm))))
                .UseApplicationControllerMessage(mids.Where(x => DoesImplementInteface(x, typeof(ApplicationController.IApplicationController))))
                .UseApplicationSelectorMessages(mids.Where(x => DoesImplementInteface(x, typeof(ApplicationSelector.IApplicationSelector))))
                .UseApplicationToolLocationSystemMessages(mids.Where(x => DoesImplementInteface(x, typeof(ApplicationToolLocationSystem.IApplicationToolLocationSystem))))
                .UseAutomaticManualModeMessages(mids.Where(x => DoesImplementInteface(x, typeof(AutomaticManualMode.IAutomaticManualMode))))
                .UseCommunicationMessages(mids.Where(x => DoesImplementInteface(x, typeof(Communication.ICommunication))))
                .UseIOInterfaceMessages(mids.Where(x => DoesImplementInteface(x, typeof(IOInterface.IIOInterface))))
                .UseJobMessages(mids.Where(x => DoesImplementInteface(x, typeof(Job.IJob))))
                .UseAdvancedJobMessages(mids.Where(x => DoesImplementInteface(x, typeof(Job.Advanced.IAdvancedJob))))
                .UseMotorTuningMessages(mids.Where(x => DoesImplementInteface(x, typeof(MotorTuning.IMotorTuning))))
                .UseMultipleIdentifiersMessages(mids.Where(x => DoesImplementInteface(x, typeof(MultipleIdentifiers.IMultipleIdentifier))))
                .UseMultiSpindleMessages(mids.Where(x => DoesImplementInteface(x, typeof(MultiSpindle.IMultiSpindle))))
                .UseOpenProtocolCommandsDisabledMessages(mids.Where(x => DoesImplementInteface(x, typeof(OpenProtocolCommandsDisabled.IOpenProtocolCommandsDisabled))))
                .UseParameterSetMessages(mids.Where(x => DoesImplementInteface(x, typeof(ParameterSet.IParameterSet))))
                .UsePLCUserDataMessages(mids.Where(x => DoesImplementInteface(x, typeof(PLCUserData.IPLCUserData))))
                .UsePowerMACSMessages(mids.Where(x => DoesImplementInteface(x, typeof(PowerMACS.IPowerMACS))))
                .UseResultMessages(mids.Where(x => DoesImplementInteface(x, typeof(Result.IResult))))
                .UseStatisticMessages(mids.Where(x => DoesImplementInteface(x, typeof(Statistic.IStatistic))))
                .UseTighteningMessages(mids.Where(x => DoesImplementInteface(x, typeof(Tightening.ITightening))))
                .UseTimeMessages(mids.Where(x => DoesImplementInteface(x, typeof(Time.ITime))))
                .UseToolMessages(mids.Where(x => DoesImplementInteface(x, typeof(Tool.ITool))))
                .UseUserInterfaceMessages(mids.Where(x => DoesImplementInteface(x, typeof(UserInterface.IUserInterface))))
                .UseVinMessages(mids.Where(x => DoesImplementInteface(x, typeof(Vin.IVin))))
                .UseModeMessages(mids.Where(x => DoesImplementInteface(x, typeof(Mode.IMode))));
        }


        public static MidInterpreter UseAlarmMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Alarm.AlarmMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseAlarmMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (mids.Any())
            {
                if (!IsValid(mids, typeof(Alarm.IAlarm)))
                    throw new ArgumentException($"Types should inherit Mid class and must implement IAlarm interface");

                midInterpreter.UseTemplate<Alarm.AlarmMessages>(mids);
            }
            return midInterpreter;
        }

        public static MidInterpreter UseApplicationControllerMessage(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ApplicationController.ApplicationControllerMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseApplicationControllerMessage(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(ApplicationController.IApplicationController)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IApplicationController interface");

            midInterpreter.UseTemplate<ApplicationController.ApplicationControllerMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseApplicationSelectorMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ApplicationSelector.ApplicationSelectorMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseApplicationSelectorMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(ApplicationSelector.IApplicationSelector)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IApplicationSelector interface");

            midInterpreter.UseTemplate<ApplicationSelector.ApplicationSelectorMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseApplicationToolLocationSystemMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ApplicationToolLocationSystem.ApplicationToolLocationSystemMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseApplicationToolLocationSystemMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(ApplicationToolLocationSystem.IApplicationToolLocationSystem)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IApplicationToolLocationSystem interface");

            midInterpreter.UseTemplate<ApplicationToolLocationSystem.ApplicationToolLocationSystemMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseAutomaticManualModeMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<AutomaticManualMode.AutomaticManualModeMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseAutomaticManualModeMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(AutomaticManualMode.IAutomaticManualMode)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IAutomaticManualMode interface");

            midInterpreter.UseTemplate<AutomaticManualMode.AutomaticManualModeMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseCommunicationMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Communication.CommunicationMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseCommunicationMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Communication.ICommunication)))
                throw new ArgumentException($"Types should inherit Mid class and must implement ICommunication interface");

            midInterpreter.UseTemplate<Communication.CommunicationMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseIOInterfaceMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<IOInterface.IOInterfaceMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseIOInterfaceMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(IOInterface.IIOInterface)))
                throw new ArgumentException($"Types should inherit Mid class and implement IIOInterface interface");

            midInterpreter.UseTemplate<IOInterface.IOInterfaceMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseJobMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Job.JobMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseJobMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Job.IJob)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IJob interface");

            midInterpreter.UseTemplate<Job.JobMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseAdvancedJobMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Job.Advanced.AdvancedJobMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseAdvancedJobMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Job.Advanced.IAdvancedJob)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IAdvancedJob interface");

            midInterpreter.UseTemplate<Job.Advanced.AdvancedJobMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseMotorTuningMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<MotorTuning.MotorTuningMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseMotorTuningMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(MotorTuning.IMotorTuning)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IMotorTuning interface");

            midInterpreter.UseTemplate<MotorTuning.MotorTuningMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseMultipleIdentifiersMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<MultipleIdentifiers.MultipleIdentifierMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseMultipleIdentifiersMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(MultipleIdentifiers.IMultipleIdentifier)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IMultipleIdentifier interface");

            midInterpreter.UseTemplate<MultipleIdentifiers.MultipleIdentifierMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseMultiSpindleMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<MultiSpindle.MultiSpindleMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseMultiSpindleMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(MultiSpindle.IMultiSpindle)))
                throw new ArgumentException($"Types should inherit Mid class and implement IMultiSpindle interface");

            midInterpreter.UseTemplate<MultiSpindle.MultiSpindleMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseOpenProtocolCommandsDisabledMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<OpenProtocolCommandsDisabled.OpenProtocolCommandsDisabledMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseOpenProtocolCommandsDisabledMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(OpenProtocolCommandsDisabled.IOpenProtocolCommandsDisabled)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IOpenProtocolCommandsDisabled interface");

            midInterpreter.UseTemplate<OpenProtocolCommandsDisabled.OpenProtocolCommandsDisabledMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseParameterSetMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ParameterSet.ParameterSetMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseParameterSetMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(ParameterSet.IParameterSet)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IParameterSet interface");

            midInterpreter.UseTemplate<ParameterSet.ParameterSetMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UsePLCUserDataMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<PLCUserData.PLCUserDataMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UsePLCUserDataMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(PLCUserData.IPLCUserData)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IPLCUserData interface");

            midInterpreter.UseTemplate<PLCUserData.PLCUserDataMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UsePowerMACSMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<PowerMACS.PowerMACSMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UsePowerMACSMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(PowerMACS.IPowerMACS)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IPowerMACS interface");

            midInterpreter.UseTemplate<PowerMACS.PowerMACSMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseResultMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Result.ResultMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseResultMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Result.IResult)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IResult interface");

            midInterpreter.UseTemplate<Result.ResultMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseStatisticMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Statistic.StatisticMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseStatisticMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Statistic.IStatistic)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IStatistic interface");

            midInterpreter.UseTemplate<Statistic.StatisticMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseTighteningMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Tightening.TighteningMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseTighteningMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Tightening.ITightening)))
                throw new ArgumentException($"Types should inherit Mid class and must implement ITightening interface");

            midInterpreter.UseTemplate<Tightening.TighteningMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseTimeMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Time.TimeMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseTimeMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Time.ITime)))
                throw new ArgumentException($"Types should inherit Mid class and must implement ITime interface");

            midInterpreter.UseTemplate<Time.TimeMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseToolMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Tool.ToolMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseToolMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Tool.ITool)))
                throw new ArgumentException($"Types should inherit Mid class and must implement ITool interface");

            midInterpreter.UseTemplate<Tool.ToolMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseUserInterfaceMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<UserInterface.UserInterfaceMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseUserInterfaceMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(UserInterface.IUserInterface)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IUserInterface interface");

            midInterpreter.UseTemplate<UserInterface.UserInterfaceMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseVinMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Vin.VinMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseVinMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Vin.IVin)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IVin interface");

            midInterpreter.UseTemplate<Vin.VinMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseModeMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Mode.ModeMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseModeMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(Mode.IMode)))
                throw new ArgumentException($"Types should inherit Mid class and must implement IMode interface");

            midInterpreter.UseTemplate<Mode.ModeMessages>(mids);
            return midInterpreter;
        }
    }
}
