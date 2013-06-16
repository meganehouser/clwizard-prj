using CLWizardLib.Params;

namespace CLWizardLib.Commands
{
    public class SampleParameters : IParam
    {
        [CommandParameter(Default="Test", Description="Test Path")]
        public string Path { get; set; }

        [CommandParameter(Default = "XXXXX", Description = "What your name?")]
        public string YourName { get; set; }
    }
}
