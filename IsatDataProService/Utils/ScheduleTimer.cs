namespace Gie.IsatDataPro.Utils
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Timer for scheduling polls. 
    /// </summary>
    public class ScheduleTimer
    {
        #region Events
        /// <summary>
        /// Event fired when poll period elapsed.
        /// </summary>
        public event EventHandler Elapsed;

        #endregion

        #region Members

        private int period;                 // milisegundos
        private DateTime nextExecution; 
        private CancellationTokenSource tokenSource;

        #endregion

        #region Properties

        /// <summary>
        /// Excecution period in seconds.
        /// </summary>
        public int Period
        {
            get { return this.period / 1000; }
        }

        /// <summary>
        /// Next excecution date and time
        /// </summary>
        public DateTime NextExcecution
        {
            get { return this.nextExecution; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ScheduleTimer() { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts timer.
        /// </summary>
        /// <param name="periodInSeconds">Excecution period in seconds.</param>
        public void Start(int periodInSeconds)
        {
            this.tokenSource = new CancellationTokenSource();
            _ = TimerFuctionAsync(periodInSeconds, tokenSource.Token);
        }

        /// <summary>
        /// Stops execution.
        /// </summary>
        public void Stop()
        {
            if(this.tokenSource != null) {
                tokenSource.Cancel();
            }
        }

        #endregion

        #region Private Methods

        private async Task TimerFuctionAsync(int periodInSeconds, CancellationToken token)
        {
            this.period = periodInSeconds * 1000;

            while (true)
            {

                // Calcula la demora hasta la proxima ejecucion
                DateTime timeNow = DateTime.Now;
                TimeSpan spanPeriod = TimeSpan.FromMilliseconds(this.period);
                long divFirstExec = (long)Math.Floor((decimal)(timeNow.Ticks / spanPeriod.Ticks)) + 1;
                this.nextExecution = new DateTime(divFirstExec * spanPeriod.Ticks);

                // Espera se cumpla el periodo o se cancele el timer
                try
                {
                    await Task.Delay(nextExecution.Subtract(timeNow), token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in ScheduleTimer Task (" + e.Message + ")");
                    break;
                }

                // Lanza el evento
                OnElapsed(new EventArgs());

                // Demora para evitar relanzamientos
                await Task.Delay(100, token);
            }
        }

        private void OnElapsed(EventArgs e)
        {
            Elapsed?.Invoke(this, e);
        }

        #endregion
    }
}