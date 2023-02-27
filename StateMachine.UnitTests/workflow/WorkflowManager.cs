using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StateMachine;
using StateMachine.Machine;
namespace StateMachine.UnitTests.workflow
{
    public class WorkflowManager
    {

        private ILogger Logger { get; }
        private IStateMachine<WorkflowStates, WorkflowEvents> StateMachine { get; }

        public WorkflowManager(ILogger<WorkflowManager> logger) :
            this((ILogger)logger)
        {
        }

        public WorkflowManager(ILogger logger)
        {
            Logger = logger;
            var smdb = new StateMachineDefinitionBuilder<WorkflowStates, WorkflowEvents>();
            smdb.In(WorkflowStates.IdleVideoSignalExists).On(WorkflowEvents.RegisterPatient)
                .Goto(WorkflowStates.PatientRegistered).Execute(MoveToPatientRegisteredState);

            smdb.In(WorkflowStates.PatientRegistered).On(WorkflowEvents.ManualStartRecording)
                .Goto(WorkflowStates.ManualRecordingInProgress).Execute(MoveToManualRecordingInProgressState);

            smdb.In(WorkflowStates.RecordingManuallyStopped).On(WorkflowEvents.ManualStartRecording)
                .Goto(WorkflowStates.ManualRecordingInProgress).Execute(MoveToManualRecordingInProgressState);

            smdb.In(WorkflowStates.ManualRecordingInProgress).On(WorkflowEvents.ManualStopRecording)
                .Goto(WorkflowStates.RecordingManuallyStopped).Execute(MoveToManualRecordingStoppedState);

            smdb.In(WorkflowStates.AutomaticRecordingInProgress).On(WorkflowEvents.ManualStopRecording)
                .Goto(WorkflowStates.RecordingManuallyStopped).Execute(MoveToManualRecordingStoppedState);

            smdb.In(WorkflowStates.PatientRegistered).On(WorkflowEvents.AutomaticStartRecording)
                .Goto(WorkflowStates.AutomaticRecordingInProgress).Execute(MoveToAutomaticRecordingInProgressState);


            smdb.In(WorkflowStates.RecordingAutomaticallyStopped).On(WorkflowEvents.AutomaticStartRecording)
                .Goto(WorkflowStates.AutomaticRecordingInProgress).Execute(MoveToAutomaticRecordingInProgressState);


            smdb.In(WorkflowStates.AutomaticRecordingInProgress).On(WorkflowEvents.AutomaticStopRecording)
                .Goto(WorkflowStates.RecordingAutomaticallyStopped).Execute(MoveToAutomaticRecordingStoppedState);

            smdb.In(WorkflowStates.RecordingManuallyStopped).On(WorkflowEvents.EndProcedure)
                .Goto(WorkflowStates.IdleVideoSignalExists).Execute(MoveToIdleVideoSignalExistsState);

            smdb.In(WorkflowStates.RecordingAutomaticallyStopped).On(WorkflowEvents.EndProcedure)
                .Goto(WorkflowStates.IdleVideoSignalExists).Execute(MoveToIdleVideoSignalExistsState);


            var stateMachineDefinition = smdb
                .WithInitialState(WorkflowStates.IdleNoVideoSignal)
                .Build();

            StateMachine = stateMachineDefinition.CreateActiveStateMachine("Workflow Automation");
            StateMachine.TransitionExceptionThrown += (sender, e) => Logger.LogCritical(e.Exception, "Workflow State Machine: Transition Exception Thrown. State:{state}. Event: {event}. Exception: {error}", e.StateId, e.EventId, e.Exception);
            StateMachine.TransitionCompleted += (sender, e) => Logger.LogInformation("Workflow State Machine: Transition Completed. from State:{state}. Event: {event}. To state: {newState}", e.StateId, e.EventId, e.NewStateId);
            StateMachine.TransitionDeclined += (sender, e) => Logger.LogWarning("Workflow State Machine: TransitionDeclined. State:{state}. Event: {event}", e.StateId, e.EventId);

        }

        public void TriggerEvent(WorkflowEvents eventId)
        {
            Logger.LogInformation("Event requested: {event}", eventId);
            StateMachine.Fire(eventId);
        }
        private void MoveToPatientRegisteredState()
        {

        }
        private void MoveToManualRecordingInProgressState()
        {

        }
        private void MoveToManualRecordingStoppedState()
        {

        }
        private void MoveToAutomaticRecordingInProgressState()
        {

        }
        private void MoveToAutomaticRecordingStoppedState()
        {

        }

        private void MoveToIdleVideoSignalExistsState()
        {

        }
    }
}
