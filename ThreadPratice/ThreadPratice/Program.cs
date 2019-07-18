using System;
using System.Threading;
namespace ThreadPratice
{
    #region - ThreadCreate_Class -
    public class ThreadCreate
    {
        public static void StaticMethod()
        {
            Console.WriteLine(
                "StaticMethod is running on another thread.");

            // Pause for a moment to provide a delay to make
            // threads more apparent.
            Thread.Sleep(1000);
            Console.WriteLine(
                "The staticmethod called by the worker thread has ended.");
        }
        // The method that will be called when the thread is started.
        public void Thread2Method()
        {
            Console.WriteLine(
                "Thread1Method is running on another thread.");

            // Pause for a moment to provide a delay to make
            // threads more apparent.
            Thread.Sleep(1000);
            Console.WriteLine(
                "The Thread1Method called by the worker thread has ended.");
        }
    }
    #endregion

    #region - Info&MethodPass_Class -
    // Delegate that defines the signature for the callback method.
    public delegate void ExampleCallback(int lineCount);

    // The INFOnMETHODpassingPratice class contains the information needed for
    // a task, the method that executes the task, and a delegate
    // to call when the task is complete.
    public class InfoAndMethodPass
    {
        // State information used in the task.
        private string boilerplate;
        private int value;

        // Delegate used to execute the callback method when the
        // task is complete.
        private ExampleCallback callback;

        // The constructor obtains the state information and the
        // callback delegate.
        public InfoAndMethodPass(string text, int number,
            ExampleCallback callbackDelegate)
        {
            boilerplate = text;
            value = number;
            callback = callbackDelegate;
        }

        // The thread procedure performs the task, such as
        // formatting and printing a document, and then invokes
        // the callback delegate with the number of lines printed.
        public void Thread3Proc()
        {
            Console.WriteLine(boilerplate, value);
            if (callback != null)
                callback(1);
        }

        // The callback method must match the signature of the
        // callback delegate.
        public static void Callbackmethod(int lineCount)
        {
            Console.WriteLine(
                "Independent task printed {0} lines.", lineCount);
        }
    }
    #endregion

    #region - Pause&Interrupt_Class -
    public class PauseAndInterrupt
    {
        public static void SleepIndefinitely()
        {
            Console.WriteLine("Thread '{0}' about to sleep indefinitely.",
                              Thread.CurrentThread.Name);
            try
            {
                Thread.Sleep(Timeout.Infinite);//excute on this thread
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Thread '{0}' awoken.",
                                  Thread.CurrentThread.Name);
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Thread '{0}' aborted.",
                                  Thread.CurrentThread.Name);
            }
            finally
            {
                Console.WriteLine("Thread '{0}' executing finally block.",
                                  Thread.CurrentThread.Name);
            }
            Console.WriteLine("Thread '{0}' finishing normal execution.",
                              Thread.CurrentThread.Name);
            Console.WriteLine();
        }

    }    
    #endregion

    public class Sample
    {
        public static void Main()
        {
            #region - ThreadCreate -
            // Create the thread object, passing in the
            // ThreadCreatePractice.StaticMethod method using a
            // ThreadStart delegate.
            Thread Thread1_StaticCaller = new Thread(
                new ThreadStart(ThreadCreate.StaticMethod));

            // Create the thread object, passing in the
            // ServerObject.InstanceMethod method using a
            // ThreadStart delegate.
            ThreadCreate ServerObject = new ThreadCreate();
            Thread Thread2_ObjectCaller = new Thread(
                new ThreadStart(ServerObject.Thread2Method));

            // Supply the state information required by the task.
            InfoAndMethodPass State = new InfoAndMethodPass(
                "Thread {0} with State.",
                3,
                new ExampleCallback(InfoAndMethodPass.Callbackmethod)
            );
            // Create the thread object, passing the
            // Info and Method by
            // Delegate and CallBackMethod
            Thread Thread3_InfoAndMethodPass = new Thread(new ThreadStart(State.Thread3Proc));

            //Create the thread object
            var Thread4_Interrupt = new Thread(PauseAndInterrupt.SleepIndefinitely);
            var Thread5_Abort = new Thread(PauseAndInterrupt.SleepIndefinitely);
            #endregion

            #region - ThreadExecute - 
            // Start the Thread1.
            Thread1_StaticCaller.Start();
            Console.WriteLine("The Main() thread calls this after "
                + "starting the new Thread1_StaticCaller thread.");// Thread1 end

            // Start the Thread2.
            Thread2_ObjectCaller.Start();
            Console.WriteLine("The Main() thread calls this after "
                + "starting the new Thread2_ObjectCaller thread.");// Thread2 end

            // Start the Thread3.
            Thread3_InfoAndMethodPass.Start();
            Console.WriteLine("Main thread does some work, then waits.");
            // Blockade the Thread3.
            Thread3_InfoAndMethodPass.Join();
            Console.WriteLine(
                "Independent task has completed; main thread ends.");// Thread3 end

            // Interrupt a sleeping thread.
            Thread4_Interrupt.Name = "Thread4";
            Thread4_Interrupt.Start();
            Thread.Sleep(2000);//excute on main thread
            Thread4_Interrupt.Interrupt();// Thread4 end
            Thread.Sleep(1000);

            //Thread4_Interrupt = new Thread(PauseAndInterrupt.SleepIndefinitely);
            Thread5_Abort.Name = "Thread5";
            Thread5_Abort.Start();
            Thread.Sleep(2000);
            Thread5_Abort.Abort();// Thread5 end
            #endregion

            Console.ReadLine();
        }

    }
}
