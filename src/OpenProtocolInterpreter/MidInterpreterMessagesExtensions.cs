using OpenProtocolInterpreter.Alarm;
using OpenProtocolInterpreter.ApplicationController;
using OpenProtocolInterpreter.ApplicationSelector;
using OpenProtocolInterpreter.ApplicationToolLocationSystem;
using OpenProtocolInterpreter.AutomaticManualMode;
using OpenProtocolInterpreter.Communication;
using OpenProtocolInterpreter.IOInterface;
using OpenProtocolInterpreter.Job;
using OpenProtocolInterpreter.Job.Advanced;
using OpenProtocolInterpreter.MotorTuning;
using OpenProtocolInterpreter.MultipleIdentifiers;
using OpenProtocolInterpreter.MultiSpindle;
using OpenProtocolInterpreter.OpenProtocolCommandsDisabled;
using OpenProtocolInterpreter.ParameterSet;
using OpenProtocolInterpreter.PLCUserData;
using OpenProtocolInterpreter.PowerMACS;
using OpenProtocolInterpreter.Result;
using OpenProtocolInterpreter.Statistic;
using OpenProtocolInterpreter.Tightening;
using OpenProtocolInterpreter.Time;
using OpenProtocolInterpreter.Tool;
using OpenProtocolInterpreter.UserInterface;
using OpenProtocolInterpreter.Vin;
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
        /// <summary>
        /// Configure MidInterpreter to parse all available Mids of a mode
        /// <para>Select Integrator if you're integrator or Controller if you're a controller</para>
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
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
                .UseTighteningResultsMessages(mode)
                .UseTimeMessages(mode)
                .UseToolMessages(mode)
                .UseUserInterfaceMessages(mode)
                .UseVinMessages(mode);
        }

        /// <summary>
        /// Configure MidInterpreter to parse all specified Mids in IEnumerable
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAllMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (mids.Any(x => !x.IsSubclassOf(typeof(Mid))))
                throw new ArgumentException("All mids must inherit Mid class", nameof(mids));

            return midInterpreter
                .UseAlarmMessages(mids.Where(x => DoesImplementInterface(x, typeof(IAlarm))))
                .UseApplicationControllerMessage(mids.Where(x => DoesImplementInterface(x, typeof(IApplicationController))))
                .UseApplicationSelectorMessages(mids.Where(x => DoesImplementInterface(x, typeof(IApplicationSelector))))
                .UseApplicationToolLocationSystemMessages(mids.Where(x => DoesImplementInterface(x, typeof(IApplicationToolLocationSystem))))
                .UseAutomaticManualModeMessages(mids.Where(x => DoesImplementInterface(x, typeof(IAutomaticManualMode))))
                .UseCommunicationMessages(mids.Where(x => DoesImplementInterface(x, typeof(ICommunication))))
                .UseIOInterfaceMessages(mids.Where(x => DoesImplementInterface(x, typeof(IIOInterface))))
                .UseJobMessages(mids.Where(x => DoesImplementInterface(x, typeof(IJob))))
                .UseAdvancedJobMessages(mids.Where(x => DoesImplementInterface(x, typeof(IAdvancedJob))))
                .UseMotorTuningMessages(mids.Where(x => DoesImplementInterface(x, typeof(IMotorTuning))))
                .UseMultipleIdentifiersMessages(mids.Where(x => DoesImplementInterface(x, typeof(IMultipleIdentifier))))
                .UseMultiSpindleMessages(mids.Where(x => DoesImplementInterface(x, typeof(IMultiSpindle))))
                .UseOpenProtocolCommandsDisabledMessages(mids.Where(x => DoesImplementInterface(x, typeof(IOpenProtocolCommandsDisabled))))
                .UseParameterSetMessages(mids.Where(x => DoesImplementInterface(x, typeof(IParameterSet))))
                .UsePLCUserDataMessages(mids.Where(x => DoesImplementInterface(x, typeof(IPLCUserData))))
                .UsePowerMACSMessages(mids.Where(x => DoesImplementInterface(x, typeof(IPowerMACS))))
                .UseResultMessages(mids.Where(x => DoesImplementInterface(x, typeof(IResult))))
                .UseStatisticMessages(mids.Where(x => DoesImplementInterface(x, typeof(IStatistic))))
                .UseTighteningMessages(mids.Where(x => DoesImplementInterface(x, typeof(ITightening))))
                .UseTimeMessages(mids.Where(x => DoesImplementInterface(x, typeof(ITime))))
                .UseToolMessages(mids.Where(x => DoesImplementInterface(x, typeof(ITool))))
                .UseUserInterfaceMessages(mids.Where(x => DoesImplementInterface(x, typeof(IUserInterface))))
                .UseVinMessages(mids.Where(x => DoesImplementInterface(x, typeof(Vin.IVin))));
        }

        /// <summary>
        /// Add custom MIDs that are not specified in Open Protocol documentation.
        /// <para>Might be used for weird controllers</para>
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseCustomMessage(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            if (mids.Values.Any(x => !x.IsSubclassOf(typeof(Mid))))
                throw new ArgumentException("All mids must inherit Mid class", nameof(mids));

            var customMessageTemplate = new CustomMessages(mids);
            midInterpreter.UseTemplate(customMessageTemplate);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IAlarm"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAlarmMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<AlarmMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IAlarm"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAlarmMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IAlarm>(mids);
            midInterpreter.UseTemplate<AlarmMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IAlarm"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAlarmMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IAlarm>(mids);
            midInterpreter.UseTemplate<AlarmMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IApplicationController"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseApplicationControllerMessage(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ApplicationControllerMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IApplicationController"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseApplicationControllerMessage(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IApplicationController>(mids);
            midInterpreter.UseTemplate<ApplicationControllerMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IApplicationController"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseApplicationControllerMessage(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IApplicationController>(mids);
            midInterpreter.UseTemplate<ApplicationControllerMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IApplicationSelector"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseApplicationSelectorMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ApplicationSelectorMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IApplicationSelector"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseApplicationSelectorMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IApplicationSelector>(mids);
            midInterpreter.UseTemplate<ApplicationSelectorMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IApplicationSelector"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseApplicationSelectorMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IApplicationSelector>(mids);
            midInterpreter.UseTemplate<ApplicationSelectorMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IApplicationToolLocationSystem"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseApplicationToolLocationSystemMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ApplicationToolLocationSystemMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IApplicationToolLocationSystem"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseApplicationToolLocationSystemMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IApplicationToolLocationSystem>(mids);
            midInterpreter.UseTemplate<ApplicationToolLocationSystemMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IApplicationToolLocationSystem"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseApplicationToolLocationSystemMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IApplicationToolLocationSystem>(mids);
            midInterpreter.UseTemplate<ApplicationToolLocationSystemMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IAutomaticManualMode"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAutomaticManualModeMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<AutomaticManualModeMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IAutomaticManualMode"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAutomaticManualModeMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IAutomaticManualMode>(mids);
            midInterpreter.UseTemplate<AutomaticManualModeMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IAutomaticManualMode"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAutomaticManualModeMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IAutomaticManualMode>(mids);
            midInterpreter.UseTemplate<AutomaticManualModeMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="ICommunication"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseCommunicationMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<CommunicationMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="ICommunication"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseCommunicationMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<ICommunication>(mids);
            midInterpreter.UseTemplate<CommunicationMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="ICommunication"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseCommunicationMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<ICommunication>(mids);
            midInterpreter.UseTemplate<CommunicationMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IIOInterface"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseIOInterfaceMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<IOInterfaceMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IIOInterface"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseIOInterfaceMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IIOInterface>(mids);
            midInterpreter.UseTemplate<IOInterfaceMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IIOInterface"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseIOInterfaceMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IIOInterface>(mids);
            midInterpreter.UseTemplate<IOInterfaceMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IJob"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseJobMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<JobMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IJob"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseJobMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IJob>(mids);
            midInterpreter.UseTemplate<JobMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IJob"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseJobMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IJob>(mids);
            midInterpreter.UseTemplate<JobMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IAdvancedJob"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAdvancedJobMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<AdvancedJobMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IAdvancedJob"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAdvancedJobMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IAdvancedJob>(mids);
            midInterpreter.UseTemplate<AdvancedJobMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IAdvancedJob"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseAdvancedJobMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IAdvancedJob>(mids);
            midInterpreter.UseTemplate<AdvancedJobMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IMotorTuning"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseMotorTuningMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<MotorTuningMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IMotorTuning"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseMotorTuningMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IMotorTuning>(mids);
            midInterpreter.UseTemplate<MotorTuningMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IMotorTuning"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseMotorTuningMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IMotorTuning>(mids);
            midInterpreter.UseTemplate<MotorTuningMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IMultipleIdentifier"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseMultipleIdentifiersMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<MultipleIdentifierMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IMultipleIdentifier"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseMultipleIdentifiersMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IMultipleIdentifier>(mids);
            midInterpreter.UseTemplate<MultipleIdentifierMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IMultipleIdentifier"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseMultipleIdentifiersMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IMultipleIdentifier>(mids);
            midInterpreter.UseTemplate<MultipleIdentifierMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IMultiSpindle"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseMultiSpindleMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<MultiSpindleMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IMultiSpindle"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseMultiSpindleMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IMultiSpindle>(mids);
            midInterpreter.UseTemplate<MultiSpindleMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IMultiSpindle"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseMultiSpindleMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IMultiSpindle>(mids);
            midInterpreter.UseTemplate<MultiSpindleMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IOpenProtocolCommandsDisabled"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseOpenProtocolCommandsDisabledMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<OpenProtocolCommandsDisabledMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IOpenProtocolCommandsDisabled"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseOpenProtocolCommandsDisabledMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IOpenProtocolCommandsDisabled>(mids);
            midInterpreter.UseTemplate<OpenProtocolCommandsDisabledMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IOpenProtocolCommandsDisabled"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseOpenProtocolCommandsDisabledMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IOpenProtocolCommandsDisabled>(mids);
            midInterpreter.UseTemplate<OpenProtocolCommandsDisabledMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IParameterSet"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseParameterSetMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ParameterSetMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IParameterSet"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseParameterSetMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IParameterSet>(mids);
            midInterpreter.UseTemplate<ParameterSetMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IParameterSet"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseParameterSetMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IParameterSet>(mids);
            midInterpreter.UseTemplate<ParameterSetMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IPLCUserData"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UsePLCUserDataMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<PLCUserDataMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IPLCUserData"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UsePLCUserDataMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IPLCUserData>(mids);
            midInterpreter.UseTemplate<PLCUserDataMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IPLCUserData"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UsePLCUserDataMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IPLCUserData>(mids);
            midInterpreter.UseTemplate<PLCUserDataMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IPowerMACS"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UsePowerMACSMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<PowerMACSMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IPowerMACS"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UsePowerMACSMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IPowerMACS>(mids);
            midInterpreter.UseTemplate<PowerMACSMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IPowerMACS"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UsePowerMACSMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IPowerMACS>(mids);
            midInterpreter.UseTemplate<PowerMACSMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IResult"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseResultMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ResultMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IResult"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseResultMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IResult>(mids);
            midInterpreter.UseTemplate<ResultMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IResult"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseResultMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IResult>(mids);
            midInterpreter.UseTemplate<ResultMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IStatistic"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseStatisticMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<StatisticMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IStatistic"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseStatisticMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IStatistic>(mids);
            midInterpreter.UseTemplate<StatisticMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IStatistic"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseStatisticMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IStatistic>(mids);
            midInterpreter.UseTemplate<StatisticMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="ITightening"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseTighteningMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<TighteningMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="ITightening"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseTighteningMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<ITightening>(mids);
            midInterpreter.UseTemplate<TighteningMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="ITightening"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseTighteningMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<ITightening>(mids);
            midInterpreter.UseTemplate<TighteningMessages>(mids);
            return midInterpreter;
        }

        public static MidInterpreter UseTighteningResultsMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<TighteningResults.TighteningResultMessages>(mode);
            return midInterpreter;
        }

        public static MidInterpreter UseTighteningResultsMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            if (!IsValid(mids, typeof(TighteningResults.ITighteningResults)))
                throw new ArgumentException($"Types should inherit Mid class and must implement ITighteningResults interface");

            midInterpreter.UseTemplate<TighteningResults.TighteningResultMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="ITime"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseTimeMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<TimeMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="ITime"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseTimeMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<ITime>(mids);
            midInterpreter.UseTemplate<TimeMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="ITime"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseTimeMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<ITime>(mids);
            midInterpreter.UseTemplate<TimeMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="ITool"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseToolMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<ToolMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="ITool"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseToolMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<ITool>(mids);
            midInterpreter.UseTemplate<ToolMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="ITool"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseToolMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<ITool>(mids);
            midInterpreter.UseTemplate<ToolMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IUserInterface"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseUserInterfaceMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<UserInterfaceMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IUserInterface"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseUserInterfaceMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<IUserInterface>(mids);
            midInterpreter.UseTemplate<UserInterfaceMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IUserInterface"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseUserInterfaceMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IUserInterface>(mids);
            midInterpreter.UseTemplate<UserInterfaceMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Include <see cref="IVin"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mode">Are you the integrator or controller?</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseVinMessages(this MidInterpreter midInterpreter, InterpreterMode mode = InterpreterMode.Both)
        {
            midInterpreter.UseTemplate<Vin.VinMessages>(mode);
            return midInterpreter;
        }

        /// <summary>
        /// Include only a specific collection of <see cref="IVin"/> MIDs into interpreter
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Mids that you want to be available for parsing</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseVinMessages(this MidInterpreter midInterpreter, IEnumerable<Type> mids)
        {
            ThrowIfInvalid<Vin.IVin>(mids);
            midInterpreter.UseTemplate<Vin.VinMessages>(mids);
            return midInterpreter;
        }

        /// <summary>
        /// Add all if not added yet and override specified <see cref="IVin"/> Mids
        /// </summary>
        /// <param name="midInterpreter">MidInterpreter instance</param>
        /// <param name="mids">Dictionary with Mid x your custom type to override</param>
        /// <returns>MidInterpreter instance</returns>
        public static MidInterpreter UseVinMessages(this MidInterpreter midInterpreter, IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<IVin>(mids);
            midInterpreter.UseTemplate<Vin.VinMessages>(mids);
            return midInterpreter;
        }

        private static bool IsValid(IEnumerable<Type> mids, Type desiredInterface)
        {
            return mids.All(x => DoesImplementInterface(x, desiredInterface) && x.IsSubclassOf(typeof(Mid)));
        }

        private static bool DoesImplementInterface(Type mid, Type desiredInterface)
        {
            return desiredInterface.IsAssignableFrom(mid);
        }

        private static void ThrowIfInvalid<T>(IEnumerable<Type> mids)
        {
            var type = typeof(T);
            if (!IsValid(mids, type))
                throw new ArgumentException($"Types should inherit Mid class and must implement {type.Name} interface", nameof(mids));
        }

        private static void ThrowIfInvalid<T>(IDictionary<int, Type> mids)
        {
            ThrowIfInvalid<T>(mids.Select(x => x.Value));
        }
    }
}
