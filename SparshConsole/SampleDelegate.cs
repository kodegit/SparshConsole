namespace SparshConsole
{
    using System;

    internal delegate void workPerforming();

    internal static class SampleDelegate
    {
        public static void InvokeDelegate()
        {
            var workerObj = new Worker();

            // Assign Method to Event using Delegate Inference
            workerObj.OnWorkPerforming += WorkerObj_OnWorkPerforming;

            // Lambda Operator Usage
            workerObj.OnWorkComplete += (s, e) =>
            {
                Console.WriteLine("EventArgs String Prop: " + e.MyStringProperty);
                Console.WriteLine("Completed Method IsPublic : " + s.GetType().IsPublic);
                Console.ReadLine();
            };

            workerObj.DoWork("dummyJob");
        }

        // Callback method on Event trigger
        private static void WorkerObj_OnWorkPerforming()
        {
            Console.WriteLine("progress call back");
        }
    }

    internal class Worker
    {
        internal event workPerforming OnWorkPerforming;
        internal event EventHandler<CustomEventArgs> OnWorkComplete;

        CustomEventArgs evntArgs = new CustomEventArgs() { MyIntProperty = 0, MyStringProperty = "String From EventArgs" };

        public void DoWork(string jobType)
        {
            for (int i = 0; i < 5; i++)
            {
                OnWorkPerforming();
            }
            OnWorkComplete(this, evntArgs);
        }
    }

    /// <summary>
    /// Custom Event Argument Creator
    /// </summary>
    internal class CustomEventArgs : EventArgs
    {
        public int MyIntProperty { get; set; }
        public string MyStringProperty { get; set; }
    }
}
