using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine.UnitTests.workflow
{
    /// <summary>
    /// The states used in the workflow state machines
    /// </summary>
    public enum WorkflowStates
    {
        IdleNoVideoSignal,
        IdleVideoSignalExists,
        PatientRegistered,
        AutomaticRecordingInProgress,
        ManualRecordingInProgress,
        RecordingManuallyStopped,
        RecordingAutomaticallyStopped
    }

    public enum WorkflowEvents
    {
        RegisterPatient,
        ManualStartRecording,
        ManualStopRecording,
        AutomaticStartRecording,
        AutomaticStopRecording,
        EndProcedure
    }
}
