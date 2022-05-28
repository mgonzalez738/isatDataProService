namespace Gie.IsatDataPro.Utils
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ScheduleTimer
    {
        public event EventHandler Elapsed;

        private int period;                 // milisegundos
        private int offset;                 // milisegundos
        private DateTime nextExecution; 
        private CancellationTokenSource tokenSource; 

        public ScheduleTimer()
        {
            this.offset = 0;
        }
        public ScheduleTimer(int offset)
        {
            this.offset = offset;
        }

        private async Task timerFuction(int periodInSeconds, CancellationToken token)
        {
            this.period = periodInSeconds * 1000;

            while(true) {

                // Calcula la demora hasta la proxima ejecucion
                DateTime timeNow = DateTime.Now;
                TimeSpan spanPeriod = TimeSpan.FromMilliseconds(this.period);
                TimeSpan spanOffset = TimeSpan.FromMilliseconds(this.offset);
                long divFirstExec = (long)Math.Floor((decimal)(timeNow.Ticks / spanPeriod.Ticks)) + 1;
                this.nextExecution = new DateTime(divFirstExec * spanPeriod.Ticks + spanOffset.Ticks);

                // Espera se cumpla el periodo o se cancele el timer
                try {
                    await Task.Delay(nextExecution.Subtract(timeNow), token);
                } catch (TaskCanceledException) {
                    break;
                } catch (Exception e){
                    Console.WriteLine("Error in ScheduleTimer Task (" + e.Message + ")");
                    break;
                }

                // Lanza el evento
                OnElapsed(new EventArgs()); 

                // Demora para evitar relanzamientos
                await Task.Delay(100);
            }      
        }

        public void Start(int periodInSeconds)
        {
            this.tokenSource = new CancellationTokenSource();
            _ = timerFuction(periodInSeconds, tokenSource.Token);
        }

        public void Stop()
        {
            if(this.tokenSource != null) {
                tokenSource.Cancel();
            }
        }

        protected virtual void OnElapsed(EventArgs e)
        {
            Elapsed?.Invoke(this, e);
        }

        public int Period
        {
            get { return this.period / 1000; }
        }

        public DateTime NextExcecution
        {
            get { return this.nextExecution; }
        }
    }
}

