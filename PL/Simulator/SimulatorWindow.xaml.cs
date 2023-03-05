using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Simulator;

namespace PL;
/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>

public partial class SimulatorWindow : Window
{
    int DelayMain = 0;
    int r = 0;
    private Stopwatch stopWatch;
    private volatile bool isTimerRun;
    BackgroundWorker timerworker;

    /// <summary>
    /// Ctor
    /// </summary>
    public SimulatorWindow()
    {
        InitializeComponent();
        stopWatch = new Stopwatch();
        timerworker = new BackgroundWorker();
        timerworker.DoWork += Worker_DoWork!;
        timerworker.ProgressChanged += Worker_ProgressChanged!;
        timerworker.WorkerReportsProgress = true;
        timerworker.WorkerSupportsCancellation = true;
        timerworker.RunWorkerAsync();
        isTimerRun = true;
        Simulator.Simulator.isAlreadyOpen = true;
    }


    /// <summary>
    /// Worker Func Of update for watch and show simulator
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        ///call from BackgroundWorker for the watch
        if (e.ProgressPercentage == 0)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.TimerBlock.Text = timerText;
            if (DelayMain != 0)
            {
                progresPer = (DelayMain - r + 1) * (100 / DelayMain);
                r--;
            }
        }

        /// call from thread in simulator.cs For update the details
        else
        {
            var args = (Tuple<BO.Order?, DateTime, int>)e.UserState!;
            ///if organ exites
            if (args.Item1 != null)
            {
                ID = args.Item1!.Id;
                OldStatus = args.Item1?.Status;
                if (args.Item1?.Status == BO.OrderStatus.Approved)
                {
                    StartTime = DateTime.Now;
                    NewStatus = BO.OrderStatus.Expedited;
                }
                else
                {
                    NewStatus = BO.OrderStatus.Received;
                    StartTime = DateTime.Now;
                }
                ExpectedDate = args.Item2;
                DelayMain = args.Item3;
                r = DelayMain;
                progresPer = 0;
            }

            ///if organ UNexites
            else
            {
                ID = null;
                progresPer = 0;
                OldStatus = null;
                NewStatus = null;
                StartTime = null;
                ExpectedDate = null;
                DelayMain = 0;
                r = DelayMain;
            }
        }
    }

    /// <summary>
    /// Do work of Backgroundworker
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        ///register func to events
        Simulator.Simulator.RegisterForSimulationCompleteEvent(HandleSimulationComplete);
        Simulator.Simulator.RegisterForUpdateEvent(HandleSimulationUpdate);
        Simulator.Simulator.StartSimulation();
        stopWatch.Start();
        while (isTimerRun)
        {
            int index = DelayMain;
            timerworker.ReportProgress(0);
            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// Stop simulation, say to simulator class to stop and after stop
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void stop_simulation(object sender, RoutedEventArgs e)
    {
        ///stop simultor and unregister
        Simulator.Simulator.UnregisterFromUpdateEvent(HandleSimulationUpdate);
        Simulator.Simulator.StopSimulation();
        ///stop the backgroundworker
        if (isTimerRun)
        {
            stopWatch.Stop();
            isTimerRun = false;
        }
        Simulator.Simulator.isAlreadyOpen = false;
        Close();
    }

    /// <summary>
    /// Finish
    /// </summary>
    private void HandleSimulationComplete()
    {
        timerworker.CancelAsync();
    }

    /// <summary>
    /// Update the show of PL with call to ReportProgress
    /// </summary>
    /// <param name="order"></param>
    /// <param name="newTime"></param>
    /// <param name="delay"></param>
    private void HandleSimulationUpdate(BO.Order? order, DateTime newTime, int delay)
    {
        var ta = new Tuple<BO.Order?, DateTime, int>(order, newTime, delay);
        timerworker.ReportProgress(1, ta);
    }

    #region DependencyProperty Variable
    public int? ID
    {
        get { return (int?)GetValue(idProperty); }
        set { SetValue(idProperty, value); }
    }

    // Using a DependencyProperty as the backing store for id.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty idProperty =
        DependencyProperty.Register("ID", typeof(int?), typeof(SimulatorWindow), new PropertyMetadata(null));

    public int progresPer
    {
        get { return (int)GetValue(progresPerProperty); }
        set { SetValue(progresPerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for id.  This enables animation, styling, binding, etc..
    public static readonly DependencyProperty progresPerProperty =
        DependencyProperty.Register("progresPer", typeof(int), typeof(SimulatorWindow), new PropertyMetadata(null));

    public BO.OrderStatus? OldStatus
    {
        get { return (BO.OrderStatus?)GetValue(OldStatusProperty); }
        set { SetValue(OldStatusProperty, value); }
    }


    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OldStatusProperty =
        DependencyProperty.Register("OldStatus", typeof(BO.OrderStatus?), typeof(SimulatorWindow), new PropertyMetadata(null));

    public BO.OrderStatus? NewStatus
    {
        get { return (BO.OrderStatus?)GetValue(NewStatusProperty); }
        set { SetValue(NewStatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NewStatusProperty =
        DependencyProperty.Register("NewStatus", typeof(BO.OrderStatus?), typeof(SimulatorWindow), new PropertyMetadata(null));


    public DateTime? ExpectedDate
    {
        get { return (DateTime?)GetValue(ExpectedDateProperty); }
        set { SetValue(ExpectedDateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ExpectedDateProperty =
        DependencyProperty.Register("ExpectedDate", typeof(DateTime?), typeof(SimulatorWindow), new PropertyMetadata(null));

    public DateTime? StartTime
    {
        get { return (DateTime?)GetValue(StartTimeProperty); }
        set { SetValue(StartTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StartTimeProperty =
        DependencyProperty.Register("StartTime", typeof(DateTime?), typeof(SimulatorWindow), new PropertyMetadata(null));
    #endregion
}