using System;
using CLWizardLib.Params;

namespace CLWizardLib.Commands
{
    public class SampleCommand : ICommand
    {
        public void Execute(ParamPresenter getter, DataContext context)
        {
            var parameters = getter.Get<SampleParameters>(context);

            Console.WriteLine("Path: " + parameters.Path);
            Console.WriteLine("Your Name: " + parameters.YourName);
        }

        public void Rollback(DataContext context)
        {
            Console.WriteLine("Rollback sample.");
        }
    }
}
